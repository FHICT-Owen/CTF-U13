# Capture point realization

This document serves to give more insight into the process of going from a first concept of the flags to the final product. 

During this preparation period the group shortly discussed how the challenge should be translated into the first concept. We wrote a challenge debrief from which this important relevant analysis information came that can be seen below. 

```
Focus of new hardware > points to improve upon:

- Visibility
- Scoring mechanics
- Interaction difficulty
```

With the initial budget of about 600 euros we set out to design 3 capture points for 200 each. With that budget in mind I started with researching which possible microcontrollers we could use and what kind of peripherals we should attach to the system. The old flagpoles were about 5 meters high and the ceiling in the new arena would only be 5-6 meters so if we want to design new flag poles they would have to be about 4-4.5 meters high. To come up with a more concrete design faster we brainstormed with the entire team on what hardware we could use.

```
Main board: ESP32 
Height: 4/4.5 meters 
Main lighting: Diffused WS2812B NEOPIXEL 
Top light: Some kind of siren light or LED matrix 
Encryption: RFID (Tags, cards, wristbands), Keypad encryption
Diagnostics: Small OLED screen 
```
With these technical requirements and hardware specs we designed a basic visualization of what the flagpoles could possibly look like. We made 2 visualizations based upon what would could possibly attract the most attention in our mind.

<img src="https://i.ibb.co/5k6H5mM/Teams-Bv-BWpa-FEWd.png">

The first budget calculation with designing the flags like this was established relatively fast and can be seen below.

<table>
 <colgroup><col>
 <col >
 <col >
 <col >
 <col >
 </colgroup><tbody>
 <tr class="xl658825" height="20" style="height:15.0pt">
  <td >Component</td>
  <td >Link</td>
  <td >Quantity</td>
  <td >Unit price</td>
  <td>Total price</td>
 </tr>
 <tr height="20" style="height:15.0pt">
  <td >WS2812B LED STRIP</td>
  <td ><a href="https://nl.aliexpress.com/item/2036819167.html?spm=a2g0o.productlist.0.0.3b0514d4i5MUgF&amp;algo_pvid=584178a8-0ca3-4991-81de-9179eecb98bf&amp;aem_p4p_detail=2022030704501445642828267840000792194&amp;algo_exp_id=584178a8-0ca3-4991-81de-9179eecb98bf-1&amp;pdp_ext_f=%7B%22sku_id%22%3A%2267389781291%22%7D&amp;pdp_pi=-1%3B11.48%3B-1%3B-1%40salePrice%3BEUR%3Bsearch-mainSearch">https://nl.aliexpress.com/item/2036819167.html?spm=a2g0o.productlist</a></td>
  <td >3</td>
  <td >€20,30 </td>
  <td >€60,90 </td>
 </tr>
 <tr height="20" style="height:15.0pt">
  <td >WS2812B LED Matrix</td>
  <td ><a href="https://nl.aliexpress.com/item/33025679652.html?_randl_currency=EUR&amp;_randl_shipto=NL&amp;src=google&amp;src=google&amp;albch=shopping&amp;acnt=494-037-6276&amp;slnk=&amp;plac=&amp;mtctp=&amp;albbt=Google_7_shopping&amp;albagn=888888&amp;isSmbAutoCall=false&amp;needSmbHouyi=false&amp;albcp=9317229739&amp;albag=97939647727&amp;trgt=536572975094&amp;crea=nl33025679652&amp;netw=u&amp;device=c&amp;albpg=536572975094&amp;albpd=nl33025679652&amp;gclid=Cj0KCQiA95aRBhCsARIsAC2xvfw_OJSxyJTA-SVojZZUtsOPG63JbEgltVEouWAGG42J3Pf6yibcKo4aAhREEALw_wcB&amp;gclsrc=aw.ds&amp;aff_fcid=5009e9b824bc48868db17be6f1a4bb00-1646656828963-01391-UneMJZVf&amp;aff_fsk=UneMJZVf&amp;aff_platform=aaf&amp;sk=UneMJZVf&amp;aff_trace_key=5009e9b824bc48868db17be6f1a4bb00-1646656828963-01391-UneMJZVf&amp;terminal_id=93dd53c70cdb442dba05d09f60d9da7d&amp;afSmartRedirect=y">https://nl.aliexpress.com/item/33025679652.html?_randl_currency=EUR</a></td>
  <td >6</td>
  <td>€16,75</td>
  <td >€100,50 </td>
 </tr>
 <tr height="20" style="height:15.0pt">
  <td >Acryl Tube</td>
  <td ><a href="https://kunststofshop.nl/acrylaat-plexiglas/acrylaat-buizen/melkwit-opaal/acrylaat-buis-opaal-2000x40x3mm-2000x40x3mm/a-6130-20000036">https://kunststofshop.nl/acrylaat-plexiglas/acrylaat-buizen/melkwit-op</a></td>
  <td >6</td>
  <td >€34,50 </td>
  <td >€207,00 </td>
 </tr>
 <tr height="20" style="height:15.0pt">
  <td >LILYGO TTGO T-DISPLAY
  v1.1 ESP32</td>
  <td ><a href="https://www.tinytronics.nl/shop/en/development-boards/microcontroller-boards/with-wi-fi/lilygo-ttgo-t-display-v1.1-esp32-with-1.14-inch-tft-display">https://www.tinytronics.nl/shop/en/development-boards/microcontroller-boards/with-wi-fi/lilygo-ttgo-t-display-v1.1-esp32-with-1.14-inch-tft-display</a></td>
  <td >3</td>
  <td >€13,50 </td>
  <td >€40,50 </td>
 </tr>
 <tr height="20" style="height:15.0pt">
  <td >MFRC522 RFID Kit</td>
  <td ><a href="https://www.tinytronics.nl/shop/en/communication-and-signals/wireless/rfid/rfid-kit-mfrc522-s50-mifare-with-card-and-key-tag">https://www.tinytronics.nl/shop/en/communication-and-signals/wireless/rfid/rfid-kit-mfrc522-s50-mifare-with-card-and-key-tag</a></td>
  <td >3</td>
  <td >€5,50 </td>
  <td >€16,50 </td>
 </tr>
 <tr height="20" style="height:15.0pt">
  <td >Keypad</td>
  <td><a href="https://www.amazon.nl/dp/B07ZT2RRT1/?coliid=I3LGUMVUDYYJ19&amp;colid=9EX3SF8SLQV8&amp;psc=1&amp;ref_=lv_ov_lig_dp_it_im">https://www.amazon.nl/dp/B07ZT2RRT1/?coliid=I3LGUMVUDYYJ19</a></td>
  <td >3</td>
  <td >€5,00 </td>
  <td >€15,00 </td>
 </tr>
 <tr height="20" style="height:15.0pt">
  <td >5v 40A power supply</td>
  <td ><a href="https://www.amazon.nl/Schakelende-Universele-Stroomvoorziening-Stroomadapter-Transformator/dp/B07Y38SMQ3/ref=sr_1_4?__mk_nl_NL=%C3%85M%C3%85%C5%BD%C3%95%C3%91&amp;crid=2MI848YVJX7MO&amp;keywords=5v+300+watt+voeding&amp;qid=1646658837&amp;sprefix=5v+300+watt+voeding%2Caps%2C55&amp;sr=8-4">https://www.amazon.nl/Schakelende-Universele-Stroomvoorziening-St</a></td>
  <td >3</td>
  <td >€27,00</td>
  <td >€81,00</td>
 </tr>
 <tr height="20" style="height:15.0pt">
  <td >Button</td>
  <td ><a href="https://www.tinytronics.nl/shop/en/switches/manual-switches/push-buttons-and-switches/large-white-push-button-24mm-of-40mm-reset">https://www.tinytronics.nl/shop/en/switches/manual-switches/push-buttons-and-switches/large-white-push-button</a></td>
  <td >3</td>
  <td >€3,50</td>
  <td >€10,50</td>
 </tr>
</tbody></table>

At the start of sprint 2 Ceremco (event manager at Unit 13) disclosed that some budget cuts were necessary to make as the budget was needed for the new location. Our initial budget was shrunk from 600 down to about 250, therefore we had to make some revisions in the hardware. As the lighting aspect was the most costly part of the flags we decided to take that part out and use the integrated lighting system that is already in the arena instead. We went from about 200 euro's per flag to about 150-200 euro for 2 fields in total which can be seen below.

<table>
 <colgroup><col>
 <col >
 <col >
 <col >
 <col >
 </colgroup><tbody>
 <tr class="xl658825" height="20" style="height:15.0pt">
  <td >Component</td>
  <td >Link</td>
  <td >Quantity</td>
  <td >Unit price</td>
  <td>Total price</td>
 </tr>
 <tr height="20" style="height:15.0pt">
  <td >LILYGO TTGO T-DISPLAY
  v1.1 ESP32</td>
  <td ><a href="https://www.tinytronics.nl/shop/en/development-boards/microcontroller-boards/with-wi-fi/lilygo-ttgo-t-display-v1.1-esp32-with-1.14-inch-tft-display">https://www.tinytronics.nl/shop/en/development-boards/microcontroller-boards/with-wi-fi/lilygo-ttgo-t-display-v1.1-esp32-with-1.14-inch-tft-display</a></td>
  <td >6</td>
  <td >€13,50 </td>
  <td >€40,50 </td>
 </tr>
 <tr height="20" style="height:15.0pt">
  <td >MFRC522 RFID Kit</td>
  <td ><a href="https://www.tinytronics.nl/shop/en/communication-and-signals/wireless/rfid/rfid-kit-mfrc522-s50-mifare-with-card-and-key-tag">https://www.tinytronics.nl/shop/en/communication-and-signals/wireless/rfid/rfid-kit-mfrc522-s50-mifare-with-card-and-key-tag</a></td>
  <td >6</td>
  <td >€5,50 </td>
  <td >€16,50 </td>
 </tr>
 <tr height="20" style="height:15.0pt">
  <td >Keypad</td>
  <td><a href="https://www.amazon.nl/dp/B07ZT2RRT1/?coliid=I3LGUMVUDYYJ19&amp;colid=9EX3SF8SLQV8&amp;psc=1&amp;ref_=lv_ov_lig_dp_it_im">https://www.amazon.nl/dp/B07ZT2RRT1/?coliid=I3LGUMVUDYYJ19</a></td>
  <td >6</td>
  <td >€5,00 </td>
  <td >€15,00 </td>
 </tr>
 <tr height="20" style="height:15.0pt">
  <td >Button</td>
  <td ><a href="https://www.tinytronics.nl/shop/en/switches/manual-switches/push-buttons-and-switches/large-white-push-button-24mm-of-40mm-reset">https://www.tinytronics.nl/shop/en/switches/manual-switches/push-buttons-and-switches/large-white-push-button</a></td>
  <td >6</td>
  <td >€3,50</td>
  <td >€10,50</td>
 </tr>
</tbody></table>

After having the budget calculation be approved for what we had in mind I then set out to start developing the prototypes. To illustrate what the prototype might possibly look like I used https://www.circuito.io/ to do a basic schematic layout of the hardware. This schematic can be seen below.

<img src="https://i.ibb.co/4PfPQN2/chrome-Adb2-I6h-S4a.png">

In order to get the fully functioning prototype working I tested every peripheral seperately on the microcontroller after which I proceeded to integrate the peripherals in the overall software design. The example below shows the integration of the LED strip, Keypad and RFID reader simultaneously.

```cpp
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
    FastLED.addLeds<WS2812B, LED_PIN, GRB>(leds, NUM_LEDS);
    Serial.println("Approximate your card to the reader...");
    Serial.println();
    encryption = "3A FD 90 15";
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
```

After having developed the above protype I had to further implement the lighting effects, screen implementation and MQTT integration. I also had to apply multitasking during this project in order to let the lighting effects run uninterrupted. The end of sprint 4 schematic can be seen below.

```cpp
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

// External parameters > Can be modified using MQTT input
int teamROGColor = TFT_GREEN;
int teamSFAColor = TFT_RED;
bool captured = false;
String teamROGCode;
String teamSFACode;
int passwordLength;
bool isCodeGame = true;
bool isEnglish = false;

EspMQTTClient client(
    "WEEB WIFI",
    "*********",
    "192.168.6.26",
    uniqueName.c_str());

void reset(bool resetFully)
{
    vTaskSuspend(blinkTask);
    tft.fillScreen(TFT_BLACK);
    if (isCodeGame)
    {
        if (isEnglish)
        {
            tft.drawString("Enter code", tft.width() / 2 - 95, tft.height() / 2 - 5);
        }
        else
        {
            tft.drawString("Voer code in", 0, tft.height() / 2 - 5);
        }
    }
    else
    {
        if (isEnglish)
        {
            tft.drawString("Approximate card", tft.width() / 2 - 95, tft.height() / 2 - 5);
        }
        else
        {
            tft.drawString("Scan kaart", tft.width() / 2 - 45, tft.height() / 2 - 5);
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
    tft.setTextSize(3);
    tft.setCursor(0, 0);
    tft.setTextColor(TFT_WHITE);
    if (isCodeGame)
    {
        Serial.println("Enter code on the keypad...");
        tft.drawString("Enter code", tft.width() / 2 - 65, tft.height() / 2 - 5);
    }
    else
    {
        Serial.println("Approximate your card to the reader...");
        tft.drawString("Approximate card", tft.width() / 2 - 95, tft.height() / 2 - 5);
    }
    Serial.println("MAC: " + WiFi.macAddress());
    Serial.println();
}

void onConnectionEstablished()
{
    client.subscribe("gadgets/" + macAddress + "/settings", [](const String &payload)
    {
        DynamicJsonDocument doc(1024);
        deserializeJson(doc, payload);
        teamROGColor=doc["teamROGColor"];
        teamSFAColor=doc["teamSFAColor"];
        captured=doc["captured"];
        teamROGCode=doc["teamROGCode"].as<String>();
        teamSFACode=doc["teamSFACode"].as<String>();
        passwordLength=doc["passwordLength"];
        isCodeGame=doc["isCodeGame"];
        isEnglish=doc["isEnglish"]; 
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
    if (isCodeGame)
    {
        teamROGCode = "1465";
        teamSFACode = "5612";
    }
    else
    {
        teamROGCode = "3A FD 90 15";
        teamSFACode = "2A 01 FF B2";
    }

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
                        tft.drawString("Successfully", tft.width() / 2 - 75, tft.height() / 2 - 10);
                        tft.drawString("Captured", tft.width() / 2 - 50, tft.height() / 2 + 5);
                    }
                    else
                    {
                        tft.drawString("Succesvol", tft.width() / 2 - 55, tft.height() / 2 - 10);
                        tft.drawString("Gecaptured", tft.width() / 2 - 55, tft.height() / 2 + 5);
                    }
                    capturedUID = lastUID;
                    captured = true;
                    vTaskSuspend(blinkTask);
                    FastLED.showColor(CRGB::Green);
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
                        tft.drawString("Successfully", tft.width() / 2 - 75, tft.height() / 2 - 10);
                        tft.drawString("Captured", tft.width() / 2 - 50, tft.height() / 2 + 5);
                    }
                    else
                    {
                        tft.drawString("Succesvol", tft.width() / 2 - 55, tft.height() / 2 - 10);
                        tft.drawString("Gecaptured", tft.width() / 2 - 55, tft.height() / 2 + 5);
                    }
                    capturedUID = lastUID;
                    captured = true;
                    vTaskSuspend(blinkTask);
                    FastLED.showColor(CRGB::Red);
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
                tft.drawString(enteredCode, tft.width() / 2 - 30, tft.height() / 2 + 30);
                if (enteredCode.length() == passwordLength)
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
                            tft.drawString("Entered ROG code!", tft.width() / 2 - 95, tft.height() / 2 - 10);
                            tft.drawString("Start capturing!", tft.width() / 2 - 93, tft.height() / 2 + 5);
                        }
                        else
                        {
                            tft.drawString("ROG code ingevoerd!", tft.width() / 2 - 93, tft.height() / 2 - 10);
                            tft.drawString("Begin met capturen!", tft.width() / 2 - 93, tft.height() / 2 + 5);
                        }
                        blinkColor = CRGB::Green;
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
                            tft.drawString("Entered SFA code!", tft.width() / 2 - 95, tft.height() / 2 - 10);
                            tft.drawString("Start capturing!", tft.width() / 2 - 93, tft.height() / 2 + 5);
                        }
                        else
                        {
                            tft.drawString("SFA code ingevoerd!", tft.width() / 2 - 93, tft.height() / 2 - 10);
                            tft.drawString("Begin met capturen!", tft.width() / 2 - 93, tft.height() / 2 + 5);
                        }
                        blinkColor = CRGB::Red;
                        vTaskResume(blinkTask);
                        lastUID = teamSFACode;
                    }
                    else
                    {
                        Serial.println("\nAccess denied!");
                        tft.fillScreen(TFT_BLACK);
                        if (isEnglish)
                        {
                            tft.drawString("Wrong code!", tft.width() / 2 - 65, tft.height() / 2 - 5);
                        }
                        else
                        {
                            tft.drawString("Verkeerde code!", tft.width() / 2 - 50, tft.height() / 2 - 5);
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
                            tft.drawString("Found ROG card!", tft.width() / 2 - 95, tft.height() / 2 - 10);
                            tft.drawString("Start capturing!", tft.width() / 2 - 93, tft.height() / 2 + 5);
                        }
                        else
                        {
                            tft.drawString("ROG kaart gevonden!", tft.width() / 2 - 95, tft.height() / 2 - 10);
                            tft.drawString("Begin met capturen!", tft.width() / 2 - 93, tft.height() / 2 + 5);
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
                            tft.drawString("Found SFA card!", tft.width() / 2 - 95, tft.height() / 2 - 10);
                            tft.drawString("Start capturing!", tft.width() / 2 - 93, tft.height() / 2 + 5);
                        }
                        else
                        {
                            tft.drawString("SFA kaart gevonden!", tft.width() / 2 - 95, tft.height() / 2 - 10);
                            tft.drawString("Begin met capturen!", tft.width() / 2 - 93, tft.height() / 2 + 5);
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
                            tft.drawString("Wrong card!", tft.width() / 2 - 70, tft.height() / 2 - 5);
                        }
                        else
                        {
                            tft.drawString("Verkeerde card!", tft.width() / 2 - 55, tft.height() / 2 - 5);
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
```
The iterative development of the software was managed through GitHub. All major version changed with their attached user stories can be found there.

GitHub link: https://github.com/FHICT-Owen/CTF-U13/