#include <Arduino.h>
#include <SPI.h>
#include <Keypad.h>
#include <MFRC522.h>
#include <FastLED.h>
#include <TFT_eSPI.h>
#include <TFT_eFEX.h>
#include <ezButton.h>

#define LED_PIN 27
#define NUM_LEDS 6
#define RFID_SS 2
#define RFID_MISO 12
#define RFID_MOSI 13
#define RFID_SCK 15

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

String text;
String lastUID;
String teamROG;
String teamSFA;

int teamROGColor = TFT_GREEN;
int teamSFAColor = TFT_RED;

int progressCounter = 0;
bool captured = false;
String capturedUID;
int passwordLength;
String enteredCode;
String keyCode = "156840";
char c;
bool isCodeGame = true;

void reset(bool resetFully)
{
    tft.fillScreen(TFT_BLACK);
    if (isCodeGame)
    {
        tft.drawString("Enter code", tft.width() / 2 - 65, tft.height() / 2 - 5);
    }
    else
    {
        tft.drawString("Approximate card", tft.width() / 2 - 95, tft.height() / 2 - 5);
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
    FastLED.addLeds<WS2812B, LED_PIN, GRB>(leds, NUM_LEDS);

    FastLED.setBrightness(25.5);
    FastLED.showColor(CRGB::White);
    mfrc522.PCD_Init(RFID_SS, -1);
    button.setDebounceTime(50);
    changeGame.setDebounceTime(50);

    tft.init();
    tft.setRotation(1);
    tft.fillScreen(TFT_BLACK);
    tft.setTextSize(2);
    tft.setCursor(0, 0);
    tft.setTextColor(TFT_WHITE);

    if (isCodeGame)
    {
        teamROG = "1";
        teamSFA = "2";
        passwordLength = 1;
        Serial.println("Enter code on the keypad...");
        tft.drawString("Enter code", tft.width() / 2 - 65, tft.height() / 2 - 5);
    }
    else
    {
        teamROG = "3A FD 90 15";
        teamSFA = "2A 01 FF B2";
        Serial.println("Approximate your card to the reader...");
        tft.drawString("Approximate card", tft.width() / 2 - 95, tft.height() / 2 - 5);
    }
    Serial.println();
}

void loop()
{
    button.loop();
    changeGame.loop();
    if (changeGame.isPressed()) {
        isCodeGame = !isCodeGame;
        reset(true);
    }

    if (!mfrc522.PICC_IsNewCardPresent())
    {
    }

    if (button.getState() == HIGH)
    {
        if (captured == false)
        {
            if (lastUID == teamROG)
            {
                fex.drawProgressBar(10, 105, 220, 30, progressCounter, TFT_WHITE, teamROGColor);
                progressCounter++;
            }
            if (lastUID == teamSFA)
            {
                fex.drawProgressBar(10, 105, 220, 30, progressCounter, TFT_WHITE, teamSFAColor);
                progressCounter++;
            }
            delay(50);
        }
        if (progressCounter == 100)
        {
            tft.fillScreen(TFT_BLACK);
            tft.drawString("Successfully", tft.width() / 2 - 75, tft.height() / 2 - 10);
            tft.drawString("Captured", tft.width() / 2 - 50, tft.height() / 2 + 5);
            capturedUID = lastUID;
            captured = true;
            FastLED.showColor(CRGB::White);
            delay(500);
        }
    }
    else
    {
        progressCounter = 0;
        if (captured == true)
        {
            if (lastUID == teamROG)
                fex.drawProgressBar(10, 105, 220, 30, 100, TFT_WHITE, teamROGColor);
            if (lastUID == teamSFA)
                fex.drawProgressBar(10, 105, 220, 30, 100, TFT_WHITE, teamSFAColor);
        }
    }

    if (isCodeGame)
    {
        char key = keypad.getKey();
        if (key)
        {
            Serial.print(key);
            enteredCode.concat(key);
            if (enteredCode.length() == passwordLength)
            {
                if (enteredCode == teamROG)
                {
                    captured = false;
                    Serial.println("\nEntered ROG code!");
                    tft.fillScreen(TFT_BLACK);
                    tft.drawString("Entered ROG code!", tft.width() / 2 - 95, tft.height() / 2 - 10);
                    tft.drawString("Start capturing!", tft.width() / 2 - 93, tft.height() / 2 + 5);
                    FastLED.showColor(CRGB::Green);
                    lastUID = "1";
                }
                else if (enteredCode == teamSFA)
                {
                    captured = false;
                    Serial.println("\nEntered SFA code!");
                    tft.fillScreen(TFT_BLACK);
                    tft.drawString("Entered SFA code!", tft.width() / 2 - 95, tft.height() / 2 - 10);
                    tft.drawString("Start capturing!", tft.width() / 2 - 93, tft.height() / 2 + 5);
                    FastLED.showColor(CRGB::Red);
                    lastUID = "2";
                }
                else
                {
                    Serial.println("\nAccess denied!");
                    tft.fillScreen(TFT_BLACK);
                    tft.drawString("Wrong code!", tft.width() / 2 - 65, tft.height() / 2 - 5);
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
                if (lastUID == teamROG)
                {
                    captured = false;
                    Serial.println("Found ROG tag, you can now start capturing.\n");
                    tft.fillScreen(TFT_BLACK);
                    tft.drawString("Found ROG card!", tft.width() / 2 - 95, tft.height() / 2 - 10);
                    tft.drawString("Start capturing!", tft.width() / 2 - 98, tft.height() / 2 + 5);
                    FastLED.showColor(CRGB::Green);
                }
                else if (lastUID == teamSFA)
                {
                    captured = false;
                    Serial.println("Found SFA tag, you can now start capturing.\n");
                    tft.fillScreen(TFT_BLACK);
                    tft.drawString("Found SFA card!", tft.width() / 2 - 95, tft.height() / 2 - 10);
                    tft.drawString("Start capturing!", tft.width() / 2 - 98, tft.height() / 2 + 5);
                    FastLED.showColor(CRGB::Red);
                }
                else
                {
                    Serial.println("Access denied, wrong tag was used!");
                    tft.fillScreen(TFT_BLACK);
                    tft.drawString("Wrong card!", tft.width() / 2 - 70, tft.height() / 2 - 5);
                    FastLED.showColor(CRGB::Yellow);
                    delay(2000);
                    reset(true);
                }
            }
        }
    }
}