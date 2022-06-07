# Software features
This document summarizes the most important features that I had to use to get the hardware prototype to the current functional level. Each of the sections below summarizes one technology, explaining why I had to use this technology in order to achieve my goals for the hardware.

## LED Strip
After having developed the scaled down version of the capture point we still wanted to use some kind of accent light in order to indicate to the players to which team the capture point in question belongs. Victor brought along a WS2812B led strip in order to achieve this. From doing a quick search online there were 2 main libraries in use by people online and those were the FastLED library and the Adafruit NeoPixel library. 

When looking at simple examples of the 2 libraries side by side it was a lot easier to achieve the initial results I wanted within the FastLED library than the Neopixel library. To simply turn either the LED strip to a certain color I'll show the difference between the 2 libraries below.

### Neopixel
```cpp
#include <Adafruit_NeoPixel.h>

#define PIXELPIN 6
#define NUMPIXELS 16
Adafruit_NeoPixel pixels(NUMPIXELS, PIN, NEO_GRB + NEO_KHZ800);

void setup() {
  pixels.begin();
  pixels.clear();

  for(int i=0; i<NUMPIXELS; i++) {
    pixels.setPixelColor(i, pixels.Color(0, 150, 0));
    pixels.show();
  }
}
```

### FastLED
```cpp
#include <FastLED.h>

#define LED_PIN 27
#define NUM_LEDS 6
CRGB leds[NUM_LEDS];

void setup() {
    FastLED.addLeds<WS2812B, LED_PIN, GRB>(leds, NUM_LEDS);
    FastLED.showColor(CRGB::White);
}
```

As can be seen above, the FastLED library has a simple function turn the entire LED strip to a single solid color, which is ideal for what I need to use the LED strip for.

## Display Driver (tft_eSPI library)
In order to get the integrated display working on the TTGO T-Display in order to show the users instructions I had to make use of the tft_eSPI library. This library has a lot of drivers for all kinds of different displays and is the easiest way to get the display working. As it is a well-documented and well-adapted library within the PlatformIO development environment. 

When setting up the display in conjunction with PlatformIO (Plugin that allows me to use VSCode in order to write code for the microcontroller) I had a few troubles that I ran into. I was trying to modify the library itself to get the code working but found out by going through the code that platformio users can define a custom user setup which I used to load the correct settings and also free up the main SPI interface so that it could be used for the RFID reader.

## MQTT Communication
In order to get MQTT communication working I had to implement an MQTT client into the sketch that allowed you to connect over Wi-Fi in order to send and receive messages. The EspMQTTClient library did just that and also had integrated Wi-Fi connection support which made it super easy to connect the Microcontroller to a Wi-Fi network and then establish a connection with the MQTT broker. I found this library by going to the platformio libraries explorer and typing MQTT client. I then proceeded to look at the examples of each of the available libraries to see which would be easy to implement.

## Multi-threading
I didn't think I would have to use multi-threading but in order to get the desired blink effect I thought that making use of the second core on the microcontroller to run the blink code would be the easiest. Multi-threading also made the blink time very consistent and most important of all multi-threading made the blink code itself non-blocking for the rest of the code in the sketch.

I found [this](https://www.reddit.com/r/FastLED/comments/glc1hq/fastled_flashing_effect_as_a_separate_task/) discussion on reddit regarding a blink effect running as a task on a seperate core which gave me most of the inspiration to write the needed task that could then be pinned to the other core so it runs independently from the main arduino loop that attaches itself to core 1.

## Battery Indicator
In order to run the playing field tests more easily I remembered I still had Li-Po batteries laying around with the correct connector for attaching to the microcontroller. I thought that it would be a good idea to make the battery percentage monitorable to know when exactly to put the capture point back on the charger. I did a quick google search for a battery indicator sepcifically for the TTGO T-display and then found [this](https://www.youtube.com/watch?v=osAOdmZuvDc&vl=en) video with a link to the [GitHub code](https://github.com/0015/ThatProject/blob/master/ESP32_TTGO/TTGO_Battery_Indicator/TTGO_Battery_Indicator.ino) which I used and also simplified to work with the rest of the code. 

I found out that when I tried to use the code I was getting a lot of errors during the code compile. After looking into the stack-trace I found out the newly added TJpg_Decoder was clashing with the TFT_eFEX library built-in Jpg decoder of the same type. I then remembered that the TFT_eFEX also had some functions for loading images so I decided to remove the seperate decoder library and used the TFT_eFEX functions instead which almost worked flawlessly. 

The only remaining issue that I was running into was that the TFT_eFEX library was not able to decode the jpg images correctly so the colors were incorrect compared to the actual images. I showed this to Ramon and he told me to see if I could use bitmap instead as those format files do not have any encoding. I converted the images to .bmp and changed the function to use the drawBmp() function and that fixed this issue.