#include <Arduino.h>
#include <Battery18650Stats.h>
#include <SPI.h>
#include <Keypad.h>
#include <MFRC522.h>
#include <FastLED.h>
#include <TFT_eSPI.h>
#include <TFT_eFEX.h>
#include <ezButton.h>
#include <WiFi.h>
#include <ArduinoJson.h>
#include "EspMQTTClient.h"

#define LED_PIN 27
#define NUM_LEDS 6
#define RFID_SS 2
#define RFID_MISO 12
#define RFID_MOSI 13
#define RFID_SCK 15

#define ICON_WIDTH 70
#define ICON_HEIGHT 36
#define STATUS_HEIGHT_BAR ICON_HEIGHT
#define ARRAY_SIZE(x) (sizeof(x) / sizeof(x[0]))
#define ICON_POS_X (tft.width() - ICON_WIDTH)

#define MIN_USB_VOL 4.6
#define ADC_PIN 34
#define CONV_FACTOR 1.8
#define READS 20

TaskHandle_t blinkTask;
CRGB leds[NUM_LEDS];
MFRC522 mfrc522 = MFRC522(RFID_SS, -1);
TFT_eSPI tft = TFT_eSPI(135, 240);
TFT_eFEX fex = TFT_eFEX(&tft);
ezButton button(36);
ezButton changeGame(0);
ezButton disableButton(35);

Battery18650Stats BL(ADC_PIN, CONV_FACTOR, READS);
char *batteryImages[] = {(char *)"/battery_01.jpg", (char *)"/battery_02.jpg", (char *)"/battery_03.jpg", (char *)"/battery_04.jpg", (char *)"/battery_05.jpg"};

const byte ROWS = 4;
const byte COLS = 3;
char keys[ROWS][COLS] = {
    {'1', '2', '3'},
    {'4', '5', '6'},
    {'7', '8', '9'},
    {'*', '0', '#'}};
byte rowPins[ROWS] = {32, 33, 25, 26};
byte colPins[COLS] = {21, 22, 17};
Keypad keypad = Keypad(makeKeymap(keys), rowPins, colPins, ROWS, COLS);

// Internal parameters > No MQTT manipulation
String macAddress = WiFi.macAddress();
String uniqueName = "CTF-" + macAddress;
String text;
String lastUID;
String capturedUID;
String enteredCode;
CRGB blinkColor;
void blinkTaskCode(void *pvParameters);
char c;
int counter = 0;
long lastMillis = 0;
long lastMillisBattery = 0;
int progressCounter = 0;
int tftCenterHeight = tft.width() / 2 + 5;
int tftCenterWidth = tft.height() / 2;

// External parameters > Can be modified using MQTT input
uint32_t teamROGColor = 0x00FF00;
uint32_t teamSFAColor = 0xFF0000;
bool captured = false;
bool isCodeGame = false;
bool isEnglish = false;
bool disabled = false;
bool once = false;
String teamROGCode = isCodeGame ? "1465" : "3A FD 90 15";
String teamSFACode = isCodeGame ? "8149" : "2A 01 FF B2";

EspMQTTClient client(
    "LTP",
    "1234567890",
    "192.168.2.123",
    uniqueName.c_str());

void pinoutInit()
{
    pinMode(14, OUTPUT);
    digitalWrite(14, HIGH);
}

void SPIFFSInit()
{
    if (!SPIFFS.begin())
    {
        Serial.println("SPIFFS initialisation failed!");
        while (1)
            yield();
    }
    Serial.println("\r\nInitialisation done.");
}

void drawingBatteryIcon(String filePath)
{
    fex.drawJpeg(filePath, ICON_POS_X, 0);
}

void drawingText(String text)
{
    tft.fillRect(0, 0, ICON_POS_X, ICON_HEIGHT, TFT_BLACK);
    tft.setTextDatum(5);
    tft.drawString(text, ICON_POS_X - 2, STATUS_HEIGHT_BAR / 2, 4);
}

void battery_info()
{
    long currentMillisBattery = millis();
    if (currentMillisBattery - lastMillisBattery > 3000)
    {
        tft.setCursor(0, STATUS_HEIGHT_BAR);
        tft.setTextSize(1);
        if (BL.getBatteryVolts() >= MIN_USB_VOL)
        {
            for (int i = 0; i < ARRAY_SIZE(batteryImages); i++)
            {
                drawingBatteryIcon(batteryImages[i]);
                drawingText("Chrg");
            }
        }
        else
        {
            int imgNum = 0;
            int batteryLevel = BL.getBatteryChargeLevel();
            if (batteryLevel >= 80)
            {
                imgNum = 3;
            }
            else if (batteryLevel < 80 && batteryLevel >= 50)
            {
                imgNum = 2;
            }
            else if (batteryLevel < 50 && batteryLevel >= 20)
            {
                imgNum = 1;
            }
            else if (batteryLevel < 20)
            {
                imgNum = 0;
            }

            drawingBatteryIcon(batteryImages[imgNum]);
            drawingText(String(batteryLevel) + "%");
        }
        tft.setCursor(0, 0);
        tft.setTextDatum(MC_DATUM);
        tft.setTextSize(2);
        lastMillisBattery = currentMillisBattery;
    };
}

void onConnectionEstablished()
{
    client.subscribe("gadgets/" + macAddress + "/settings", [](const String &payload)
                     {
        Serial.println("Received Settings");
        DynamicJsonDocument doc(2056);
        deserializeJson(doc, payload);
        teamROGColor=doc["teamROGColor"];
        teamSFAColor=doc["teamSFAColor"];
        captured=doc["captured"];
        teamROGCode=doc["teamROGCode"].as<String>();
        teamSFACode=doc["teamSFACode"].as<String>();
        isCodeGame=doc["isCodeGame"];
        isEnglish=doc["isEnglish"]; 
        disabled=doc["disabled"]; });
}

void sendCaptureState(int capturePercentage, int capturer)
{
    DynamicJsonDocument doc(128);
    doc["capturePercentage"] = capturePercentage;
    doc["capturer"] = capturer;
    char JSONMessageBuffer[128];
    serializeJson(doc, JSONMessageBuffer);
    client.publish("gadgets/" + macAddress + "/state", JSONMessageBuffer);
}

void blinkTaskCode(void *pvParameters)
{
    Serial.print("Blink running on core ");
    Serial.println(xPortGetCoreID());
    for (;;)
    {
        FastLED.showColor(blinkColor);
        vTaskDelay(500);
        FastLED.showColor(CRGB::Black);
        vTaskDelay(500);
    }
}

void reset(bool resetFully)
{
    vTaskSuspend(blinkTask);
    tft.fillScreen(TFT_BLACK);
    if (isEnglish && isCodeGame)
    {
        tft.drawString("Enter code", tftCenterWidth, tftCenterHeight);
        tft.setTextDatum(ML_DATUM);
        tft.drawString("*=Back    #=Confirm", 5, 125);
        tft.setTextDatum(MC_DATUM);
    }
    if (!isEnglish && isCodeGame)
    {
        tft.drawString("Voer code in", tftCenterWidth, tftCenterHeight);
        tft.setTextDatum(ML_DATUM);
        tft.drawString("*=Back    #=Confirm", 5, 125);
        tft.setTextDatum(MC_DATUM);
    }
    if (isEnglish && !isCodeGame)
    {
        tft.drawString("Scan card", tftCenterWidth, tftCenterHeight);
    }
    if (!isEnglish && !isCodeGame)
    {
        tft.drawString("Scan kaart", tftCenterWidth, tftCenterHeight);
    }
    if (resetFully)
    {
        lastUID = "";
        captured = false;
        progressCounter = 0;
    }
    if (captured == true && lastUID == teamROGCode)
    {
        FastLED.showColor(teamROGColor);
    }
    else if (captured == true && lastUID == teamSFACode)
    {
        FastLED.showColor(teamSFAColor);
    }
    else
    {
        FastLED.showColor(CRGB::White);
    }
}

void setup()
{
    Serial.begin(115200);
    pinoutInit();
    SPIFFSInit();

    tft.begin();
    tft.setRotation(1);
    tft.setTextColor(TFT_WHITE, TFT_BLACK);
    tft.fillScreen(TFT_BLACK);
    tft.setTextDatum(MC_DATUM);
    tft.setSwapBytes(false);
    tft.setTextSize(2);
    tft.setCursor(0, 0);

    SPI.begin(RFID_SCK, RFID_MISO, RFID_MOSI, RFID_SS);
    mfrc522.PCD_Init();
    button.setDebounceTime(50);
    changeGame.setDebounceTime(10);
    disableButton.setDebounceTime(10);
    FastLED.addLeds<WS2812B, LED_PIN, GRB>(leds, NUM_LEDS);
    FastLED.setBrightness(25.5);
    xTaskCreatePinnedToCore(
        blinkTaskCode,
        "BlinkTask",
        50000,
        NULL,
        1,
        &blinkTask,
        0);
    vTaskSuspend(blinkTask);

    if (isEnglish && isCodeGame)
    {
        Serial.println("Enter code on the keypad...");
        tft.drawString("Enter code", tftCenterWidth, tftCenterHeight);
        tft.setTextDatum(ML_DATUM);
        tft.drawString("*=Back    #=Confirm", 5, 125);
        tft.setTextDatum(MC_DATUM);
    }
    if (!isEnglish && isCodeGame)
    {
        Serial.println("Voer code in op het keypad...");
        tft.drawString("Voer code in", tftCenterWidth, tftCenterHeight);
        tft.setTextDatum(ML_DATUM);
        tft.drawString("*=Back    #=Confirm", 5, 125);
        tft.setTextDatum(MC_DATUM);
    }
    if (isEnglish && !isCodeGame)
    {
        Serial.println("Approximate your card to the reader...");
        tft.drawString("Scan card", tftCenterWidth, tftCenterHeight);
    }
    if (!isEnglish && !isCodeGame)
    {
        Serial.println("Scan de kaart door hem bij de reader te houden...");
        tft.drawString("Scan kaart", tftCenterWidth, tftCenterHeight);
    }
    Serial.println("MAC: " + WiFi.macAddress());
    FastLED.showColor(CRGB::White);
    Serial.println();
}

void loop()
{
    client.loop();
    button.loop();
    changeGame.loop();
    disableButton.loop();
    battery_info();
    if (disableButton.isPressed())
    {
        disabled = !disabled;
    }
    if (disabled)
    {
        if (!once)
        {
            tft.fillScreen(TFT_BLACK);
            tft.drawString("Disabled", tftCenterWidth, tftCenterHeight);
            vTaskSuspend(blinkTask);
            FastLED.showColor(CRGB::Black);
            once = true;
        }
        return;
    }
    if (!disabled && once)
    {
        reset(true);
        once = false;
    }
    if (changeGame.isPressed())
    {
        isCodeGame = !isCodeGame;
        if (isCodeGame)
        {
            teamROGCode = "1465";
            teamSFACode = "8149";
        }
        else
        {
            teamROGCode = "3A FD 90 15";
            teamSFACode = "2A 01 FF B2";
        }
        reset(true);
    }

    long currentMillis = millis();
    if (button.getState() == HIGH)
    {
        if (captured == false && currentMillis - lastMillis > 25)
        {
            if (teamROGCode == "" || teamSFACode == "")
            {
                return;
            }
            if (lastUID == teamROGCode)
            {
                if (progressCounter % 5 == 0)
                {
                    sendCaptureState(progressCounter, 1);
                    Serial.println(progressCounter);
                }
                fex.drawProgressBar(10, 105, 220, 30, progressCounter, TFT_WHITE, fex.color24to16(teamROGColor));
                progressCounter++;
                if (progressCounter == 100)
                {
                    sendCaptureState(progressCounter, 1);
                    Serial.println(progressCounter);
                    tft.fillScreen(TFT_BLACK);
                    if (isEnglish)
                    {
                        tft.drawString("Successfully", tftCenterWidth, tftCenterHeight - 10);
                        tft.drawString("Captured", tftCenterWidth, tftCenterHeight + 5);
                    }
                    else
                    {
                        tft.drawString("Succesvol", tftCenterWidth, tftCenterHeight - 10);
                        tft.drawString("Veroverd", tftCenterWidth, tftCenterHeight + 5);
                    }
                    capturedUID = lastUID;
                    captured = true;
                    vTaskSuspend(blinkTask);
                    FastLED.showColor(teamROGColor);
                    delay(2200);
                    reset(false);
                }
            }
            if (lastUID == teamSFACode)
            {
                if (progressCounter % 5 == 0)
                {
                    sendCaptureState(progressCounter, 2);
                    Serial.println(progressCounter);
                }
                fex.drawProgressBar(10, 105, 220, 30, progressCounter, TFT_WHITE, fex.color24to16(teamSFAColor));
                progressCounter++;
                if (progressCounter == 100)
                {
                    sendCaptureState(progressCounter, 2);
                    Serial.println(progressCounter);
                    tft.fillScreen(TFT_BLACK);
                    if (isEnglish)
                    {
                        tft.drawString("Successfully", tftCenterWidth, tftCenterHeight - 10);
                        tft.drawString("Captured", tftCenterWidth, tftCenterHeight + 5);
                    }
                    else
                    {
                        tft.drawString("Succesvol", tftCenterWidth, tftCenterHeight - 10);
                        tft.drawString("Veroverd", tftCenterWidth, tftCenterHeight + 5);
                    }
                    capturedUID = lastUID;
                    captured = true;
                    vTaskSuspend(blinkTask);
                    FastLED.showColor(teamSFAColor);
                    delay(2200);
                    reset(false);
                }
            }
            lastMillis = currentMillis;
        }
    }
    else
    {
        if (progressCounter != 0 && progressCounter != 100)
        {
            progressCounter = 0;
            sendCaptureState(0, 0);
            Serial.println(progressCounter);
            fex.drawProgressBar(10, 105, 220, 30, 100, TFT_WHITE, TFT_BLACK);
        }

        if (isCodeGame)
        {
            char key = keypad.getKey();
            if (key == '#')
            {
                if (enteredCode == teamROGCode)
                {
                    captured = false;
                    sendCaptureState(0, 0);
                    progressCounter = 0;
                    Serial.println("\nEntered ROG code!");
                    tft.fillScreen(TFT_BLACK);
                    if (isEnglish)
                    {
                        tft.drawString("Code accepted!", tftCenterWidth, tftCenterHeight - 10);
                        tft.drawString("Hold button!", tftCenterWidth, tftCenterHeight + 5);
                    }
                    else
                    {
                        tft.drawString("Code geaccepteerd!", tftCenterWidth, tftCenterHeight - 10);
                        tft.drawString("Houd knop in!", tftCenterWidth, tftCenterHeight + 5);
                    }
                    blinkColor = teamROGColor;
                    vTaskResume(blinkTask);
                    lastUID = teamROGCode;
                }
                else if (enteredCode == teamSFACode)
                {
                    captured = false;
                    sendCaptureState(0, 0);
                    progressCounter = 0;
                    Serial.println("\nEntered SFA code!");
                    tft.fillScreen(TFT_BLACK);
                    if (isEnglish)
                    {
                        tft.drawString("Code accepted!", tftCenterWidth, tftCenterHeight - 10);
                        tft.drawString("Hold button!", tftCenterWidth, tftCenterHeight + 5);
                    }
                    else
                    {
                        tft.drawString("Code geaccepteerd!", tftCenterWidth, tftCenterHeight - 10);
                        tft.drawString("Houd knop in!", tftCenterWidth, tftCenterHeight + 5);
                    }
                    blinkColor = teamSFAColor;
                    vTaskResume(blinkTask);
                    lastUID = teamSFACode;
                }
                else
                {
                    Serial.println("\nAccess denied!");
                    tft.fillScreen(TFT_BLACK);
                    if (isEnglish)
                    {
                        tft.drawString("Wrong code!", tftCenterWidth, tftCenterHeight);
                    }
                    else
                    {
                        tft.drawString("Verkeerde code!", tftCenterWidth, tftCenterHeight);
                    }
                    vTaskSuspend(blinkTask);
                    FastLED.showColor(CRGB::Yellow);
                    delay(2000);
                    reset(false);
                }
                enteredCode = "";
            }
            else if (key == '*')
            {
                enteredCode = enteredCode.substring(0, enteredCode.length() - 1);
                tft.fillRect(tftCenterWidth - 50, tftCenterHeight + 15, 100, 20, TFT_BLACK);
                tft.drawString(enteredCode, tftCenterWidth, tftCenterHeight + 25);
            }
            else if (key)
            {
                Serial.print(key);
                enteredCode.concat(key);
                tft.fillRect(tftCenterWidth - 50, tftCenterHeight + 15, 100, 20, TFT_BLACK);
                tft.drawString(enteredCode, tftCenterWidth, tftCenterHeight + 25);
            }
        }
        else
        {
            if (mfrc522.PICC_IsNewCardPresent() && mfrc522.PICC_ReadCardSerial())
            {
                String content = "";
                for (byte i = 0; i < mfrc522.uid.size; i++)
                {
                    content.concat(String(mfrc522.uid.uidByte[i] < 0x10 ? " 0" : " "));
                    content.concat(String(mfrc522.uid.uidByte[i], HEX));
                }
                content.toUpperCase();
                String finalUID = content.substring(1);
                if (finalUID != lastUID)
                {
                    Serial.println("finalUID: " + finalUID);
                    lastUID = finalUID;
                    if (lastUID == teamROGCode)
                    {
                        captured = false;
                        progressCounter = 0;
                        Serial.println("Found ROG tag, you can now start capturing.\n");
                        tft.fillScreen(TFT_BLACK);
                        if (isEnglish)
                        {
                            tft.drawString("Card accepted!", tftCenterWidth, tftCenterHeight - 10);
                            tft.drawString("Hold button!", tftCenterWidth, tftCenterHeight + 5);
                        }
                        else
                        {
                            tft.drawString("Kaart geaccepteerd!", tftCenterWidth, tftCenterHeight - 10);
                            tft.drawString("Houd knop in!", tftCenterWidth, tftCenterHeight + 5);
                        }
                        blinkColor = teamROGColor;
                        vTaskResume(blinkTask);
                    }
                    else if (lastUID == teamSFACode)
                    {
                        captured = false;
                        progressCounter = 0;
                        Serial.println("Found SFA tag, you can now start capturing.\n");
                        tft.fillScreen(TFT_BLACK);
                        if (isEnglish)
                        {
                            tft.drawString("Card accepted!", tftCenterWidth, tftCenterHeight - 10);
                            tft.drawString("Hold button!", tftCenterWidth, tftCenterHeight + 5);
                        }
                        else
                        {
                            tft.drawString("Kaart geaccepteerd!", tftCenterWidth, tftCenterHeight - 10);
                            tft.drawString("Houd knop in!", tftCenterWidth, tftCenterHeight + 5);
                        }
                        blinkColor = teamSFAColor;
                        vTaskResume(blinkTask);
                    }
                    else
                    {
                        Serial.println("Access denied, wrong tag was used!");
                        tft.fillScreen(TFT_BLACK);
                        if (isEnglish)
                        {
                            tft.drawString("Wrong card!", tftCenterWidth, tftCenterHeight);
                        }
                        else
                        {
                            tft.drawString("Verkeerde kaart!", tftCenterWidth, tftCenterHeight);
                        }
                        vTaskSuspend(blinkTask);
                        FastLED.showColor(CRGB::Yellow);
                        delay(2000);
                        reset(false);
                    }
                }
            }
        }
    }
}