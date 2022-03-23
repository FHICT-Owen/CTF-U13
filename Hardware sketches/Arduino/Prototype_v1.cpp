#include <Arduino.h>
#include <SPI.h>
#include <Keypad.h>
#include <MFRC522.h>
#include <FastLED.h>

#define RX_PIN 40
#define TX_PIN 39
#define LED_PIN 21
#define NUM_LEDS 6
#define SS_PIN 15
#define RST_PIN 17

CRGB leds[NUM_LEDS];
MFRC522 mfrc522 = MFRC522(SS_PIN, RST_PIN);

const byte ROWS = 4; // four rows
const byte COLS = 3; // three columns
char keys[ROWS][COLS] = {
    {'1', '2', '3'},
    {'4', '5', '6'},
    {'7', '8', '9'},
    {'*', '0', '#'}};
byte rowPins[ROWS] = {0, 3, 1, 5}; // connect to the row pinouts of the kpd
byte colPins[COLS] = {4, 2, 6};    // connect to the column pinouts of the kpd

// initialize an instance of class
Keypad keypad = Keypad(makeKeymap(keys), rowPins, colPins, ROWS, COLS);

String text;
String lastUID;
String encryption;

int passwordLength = 6;
String enteredCode;
String keyCode = "156840";
char c;

void setup()
{
    Serial.begin(115200); // Initiate a serial communication
    SPI.begin(12, 13, 11, SS_PIN);
    FastLED.addLeds<WS2812B, LED_PIN, RGB>(leds, NUM_LEDS);
    Serial.println("Approximate your card to the reader...");
    Serial.println();
    encryption = "2A 01 FF B2";
    FastLED.showColor(CRGB::White);
    mfrc522.PCD_Init();
}

void loop()
{
    if (!mfrc522.PICC_IsNewCardPresent())
    {
    }

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
            if (lastUID == encryption)
            {
                Serial.println("Found correct tag, you can now enter the " + String(passwordLength) + " digit code.\n");
                Serial.print("Entered code: ");
                FastLED.showColor(CRGB::Green);
            }
            else
            {
                Serial.println("Access denied, wrong tag was used!");
                FastLED.showColor(CRGB::Red);
            }
        }
    }

    if (lastUID == encryption)
    {
        char key = keypad.getKey();
        if (key)
        {
            Serial.print(key);
            enteredCode.concat(key);
            if (enteredCode.length() == passwordLength)
            {
                if (enteredCode == keyCode)
                {
                    Serial.println("\nAccess granted!");
                }
                else
                {
                    Serial.println("\nAccess Denied!");
                }
                enteredCode = "";
                keyCode = "";
            }
        }
    }
}