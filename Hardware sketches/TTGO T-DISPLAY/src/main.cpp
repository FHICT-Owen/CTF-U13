#include <Arduino.h>
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

TaskHandle_t blinkTask;
CRGB leds[NUM_LEDS];
MFRC522 mfrc522 = MFRC522(RFID_SS, 39);
TFT_eSPI tft = TFT_eSPI(135, 240);
TFT_eFEX fex = TFT_eFEX(&tft);
ezButton button(36);
ezButton changeGame(0);

const byte ROWS = 4;
const byte COLS = 3;
char keys[ROWS][COLS] = {
    {'1', '2', '3'},
    {'4', '5', '6'},
    {'7', '8', '9'},
    {'*', '0', '#'}};
byte rowPins[ROWS] = {22, 26, 25, 32};
byte colPins[COLS] = {17, 21, 33};
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
int progressCounter = 0;
int tftCenterHeight = tft.width() / 2;
int tftCenterWidth = tft.height() / 2;

// External parameters > Can be modified using MQTT input
int teamROGColor = TFT_GREEN;
int teamSFAColor = TFT_RED;
bool captured = false;
String teamROGCode;
String teamSFACode;
bool isCodeGame = true;
bool isEnglish = false;

EspMQTTClient client(
    "WEEB WIFI",
    "OwendB01",
    "192.168.240.148",
    uniqueName.c_str());

void reset(bool resetFully)
{
    vTaskSuspend(blinkTask);
    tft.fillScreen(TFT_BLACK);
    if (isCodeGame)
    {
        if (isEnglish)
        {
            tft.drawString("Enter code", tftCenterWidth, tftCenterHeight);
        }
        else
        {
            tft.drawString("Voer code in", tftCenterWidth, tftCenterHeight);
        }
    }
    else
    {
        if (isEnglish)
        {
            tft.drawString("Approximate card", tftCenterWidth, tftCenterHeight);
        }
        else
        {
            tft.drawString("Scan kaart", tftCenterWidth, tftCenterHeight);
        }
    }
    FastLED.showColor(CRGB::White);
    if (resetFully)
    {
        lastUID = "";
        captured = false;
    }
}

void setup()
{
    Serial.begin(115200); // Initiate a serial communication
    SPI.begin(RFID_SCK, RFID_MISO, RFID_MOSI, RFID_SS);
    mfrc522.PCD_Init(RFID_SS, -1);
    button.setDebounceTime(10);
    changeGame.setDebounceTime(10);

    xTaskCreatePinnedToCore(
        blinkTaskCode,
        "BlinkTask",
        50000,
        NULL,
        1,
        &blinkTask,
        0);
    delay(100);
    vTaskSuspend(blinkTask);

    FastLED.addLeds<WS2812B, LED_PIN, GRB>(leds, NUM_LEDS);
    FastLED.setBrightness(25.5);
    FastLED.showColor(CRGB::White);

    tft.init();
    tft.setRotation(1);
    tft.fillScreen(TFT_BLACK);
    tft.setTextSize(2);
    tft.setTextDatum(MC_DATUM);
    tft.setCursor(0, 0);
    tft.setTextColor(TFT_WHITE);
    if (isCodeGame)
    {
        if (isEnglish)
        {
            Serial.println("Enter code on the keypad...");
            tft.drawString("Enter code", tftCenterWidth, tftCenterHeight);
        }
        else
        {
            Serial.println("Voer code in op het keypad...");
            tft.drawString("Voer code in", tftCenterWidth, tftCenterHeight);
        }
    }
    else
    {
        if (isEnglish)
        {
            Serial.println("Approximate your card to the reader...");
            tft.drawString("Approximate card", tftCenterWidth, tftCenterHeight);
        }
        else
        {
            Serial.println("Scan de kaart door hem bij de reader te houden...");
            tft.drawString("Scan kaart", tftCenterWidth, tftCenterHeight);
        }
    }
    Serial.println("MAC: " + WiFi.macAddress());
    Serial.println();
}

void onConnectionEstablished()
{
    client.subscribe("gadgets/" + macAddress + "/settings", [](const String &payload)
    {
        Serial.println("Received Settings");
        DynamicJsonDocument doc(1024);
        deserializeJson(doc, payload);
        teamROGColor=doc["teamROGColor"];
        teamSFAColor=doc["teamSFAColor"];
        captured=doc["captured"];
        teamROGCode=doc["teamROGCode"].as<String>();
        teamSFACode=doc["teamSFACode"].as<String>();
        isCodeGame=doc["isCodeGame"];
        isEnglish=doc["isEnglish"];
        reset(true);
    });
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

void loop()
{
    client.loop();
    long currentMillis = millis();
    // if (isCodeGame)
    // {
    //     teamROGCode = "1465";
    //     teamSFACode = "7777";
    //     passwordLength = 4;
    // }
    // else
    // {
    //     teamROGCode = "91 E5 07 1B";
    //     teamSFACode = "91 73 4D 1B";
    // }

    button.loop();
    changeGame.loop();
    if (changeGame.isPressed())
    {
        isCodeGame = !isCodeGame;
        reset(true);
    }

    if (!mfrc522.PICC_IsNewCardPresent())
    {
    }

    if (button.getState() == HIGH)
    {
        if (captured == false && currentMillis - lastMillis > 25)
        {
            if (lastUID == teamROGCode)
            {
                if (progressCounter % 5 == 0)
                {
                    sendCaptureState(progressCounter, 1);
                    Serial.println(progressCounter);
                }
                fex.drawProgressBar(10, 105, 220, 30, progressCounter, TFT_WHITE, teamROGColor);
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
                        tft.drawString("Gecaptured", tftCenterWidth, tftCenterHeight + 5);
                    }
                    capturedUID = lastUID;
                    captured = true;
                    vTaskSuspend(blinkTask);
                    FastLED.showColor(teamROGColor);
                    delay(500);
                }
            }
            if (lastUID == teamSFACode)
            {
                if (progressCounter % 5 == 0)
                {
                    sendCaptureState(progressCounter, 2);
                    Serial.println(progressCounter);
                }
                fex.drawProgressBar(10, 105, 220, 30, progressCounter, TFT_WHITE, teamSFAColor);
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
                        tft.drawString("Gecaptured", tftCenterWidth, tftCenterHeight + 5);
                    }
                    capturedUID = lastUID;
                    captured = true;
                    vTaskSuspend(blinkTask);
                    FastLED.showColor(teamSFAColor);
                    delay(500);
                }
            }
        }
    }
    else
    {
        progressCounter = 0;
        if (progressCounter != 0 && progressCounter != 100)
        {
            sendCaptureState(0, 0);
        }
        if (captured == true)
        {
            if (lastUID == teamROGCode)
                fex.drawProgressBar(10, 105, 220, 30, 100, TFT_WHITE, teamROGColor);
            if (lastUID == teamSFACode)
                fex.drawProgressBar(10, 105, 220, 30, 100, TFT_WHITE, teamSFAColor);
        }

        if (isCodeGame)
        {
            char key = keypad.getKey();
            if (key)
            {
                Serial.print(key);
                enteredCode.concat(key);
                tft.fillRect(tftCenterWidth - 50, tftCenterHeight + 20, 100, 20, TFT_BLACK);
                tft.drawString(enteredCode, tftCenterWidth, tftCenterHeight + 30);
                if (enteredCode.length() >= teamROGCode.length())
                {
                    delay(1000);
                    if (enteredCode == teamROGCode)
                    {
                        captured = false;
                        sendCaptureState(0, 0);
                        Serial.println("\nEntered ROG code!");
                        tft.fillScreen(TFT_BLACK);
                        if (isEnglish)
                        {
                            tft.drawString("Entered ROG code!", tftCenterWidth, tftCenterHeight - 10);
                            tft.drawString("Start capturing!", tftCenterWidth, tftCenterHeight + 5);
                        }
                        else
                        {
                            tft.drawString("ROG code ingevoerd!", tftCenterWidth, tftCenterHeight - 10);
                            tft.drawString("Begin met capturen!", tftCenterWidth, tftCenterHeight + 5);
                        }
                        blinkColor = teamROGColor;
                        vTaskResume(blinkTask);
                        lastUID = teamROGCode;
                    }
                    else if (enteredCode == teamSFACode)
                    {
                        captured = false;
                        sendCaptureState(0, 0);
                        Serial.println("\nEntered SFA code!");
                        tft.fillScreen(TFT_BLACK);
                        if (isEnglish)
                        {
                            tft.drawString("Entered SFA code!", tftCenterWidth, tftCenterHeight - 10);
                            tft.drawString("Start capturing!", tftCenterWidth, tftCenterHeight + 5);
                        }
                        else
                        {
                            tft.drawString("SFA code ingevoerd!", tftCenterWidth, tftCenterHeight - 10);
                            tft.drawString("Begin met capturen!", tftCenterWidth, tftCenterHeight + 5);
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
            }
        }
        else
        {
            if (mfrc522.PICC_ReadCardSerial())
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
                        sendCaptureState(0, 0);
                        Serial.println("Found ROG tag, you can now start capturing.\n");
                        tft.fillScreen(TFT_BLACK);
                        if (isEnglish)
                        {
                            tft.drawString("Found ROG card!", tftCenterWidth, tftCenterHeight - 10);
                            tft.drawString("Start capturing!", tftCenterWidth, tftCenterHeight + 5);
                        }
                        else
                        {
                            tft.drawString("ROG kaart gevonden!", tftCenterWidth, tftCenterHeight - 10);
                            tft.drawString("Begin met capturen!", tftCenterWidth, tftCenterHeight + 5);
                        }
                        blinkColor = CRGB::Green;
                        vTaskResume(blinkTask);
                    }
                    else if (lastUID == teamSFACode)
                    {
                        captured = false;
                        sendCaptureState(0, 0);
                        Serial.println("Found SFA tag, you can now start capturing.\n");
                        tft.fillScreen(TFT_BLACK);
                        if (isEnglish)
                        {
                            tft.drawString("Found SFA card!", tftCenterWidth, tftCenterHeight - 10);
                            tft.drawString("Start capturing!", tftCenterWidth, tftCenterHeight + 5);
                        }
                        else
                        {
                            tft.drawString("SFA kaart gevonden!", tftCenterWidth, tftCenterHeight - 10);
                            tft.drawString("Begin met capturen!", tftCenterWidth, tftCenterHeight + 5);
                        }
                        blinkColor = CRGB::Red;
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
                        reset(true);
                    }
                }
            }
        }
    }
}