# Microcontroller Languages

In order to get the capture points operational with the different peripherals like a keypad, RFID reader, lighting and buttons we'll need to use a microcontroller to control the peripherals and send data to our dashboard. The modern microcontroller we chose to work with within our group project is the ESP32 that is integrated into the [LILYGO TTGO T-Display ESP32](http://www.lilygo.cn/prod_view.aspx?Id=1126).

In order to program either of the 2 microcontrollers we have a few possible different languages and frameworks we can use to get the results that we want. These options are:
- ESP IoT development framework (C/C++)
- Arduino framework (C++)
- MicroPython (Python with dedicated microcontroller support)

## Research question

Which of the above development frameworks would be best for our rapid development use-case?

## Research sub-questions
1. Should we use either C++ or Python to develop our code?
	- What is Python language?
	- What is C++ language?
2. Which one of the above frameworks has the best support?
	- Which framework can we use for our dev board?
3. Which one of the above framework gives us the best code-writing to performance ratio?
    - Can we do a performance comparison between the different frameworks?

## Methods
Now that the questions are formulated, we can use the DOT framework using the following methods and strategies to answer these questions.The best suited methods and strategies to the questions are assigned to their respective questions below.
- Literature study (question 1, 2 & 3)
- Benchmark (question 3)

## Answers
### 1. C++ vs Python

#### What is Python?
Python is a high-level, interpreted programming language. It was invented back in 1991, by Guido Van Rossum. Python is an object-oriented programming language that has large enormous library support making the implementation of various programs and algorithms easy. Its language constructs and object-oriented approach aims to help programmers to write clear, logical code for various projects.<sup>[1](##Sources)</sup>

#### What is C++?
 C++ is a high-level, general-purpose programming language created by Bjarne Stroustrup as an extension of the C programming language, or “C with Classes”. The language has expanded significantly over time, and modern C++ has object-oriented, generic, and functional features in addition to facilities for low-level memory manipulation.<sup>[1](##Sources)</sup>

In the table below there is a small overview of Python compared to C++. A few small points are removed compared to the [original table](https://www.geeksforgeeks.org/difference-between-python-and-c/).

<table>
  <thead>
    <tr>
      <th></th>
      <th>Python</th>
      <th>C++</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>Code</th>
      <td>Python has fewer lines of code.	</td>
      <td>C++ tends to have large lines of code and in more as well.</td>
    </tr>
    <tr>
      <th>Garbage Collection</th>
      <td>Python supports garbage collection.</td>
      <td>C++ doesn’t support garbage collection.</td>
    </tr>
    <tr>
      <th>Syntax</th>
      <td>Python is easy to remember almost similar to human language.</td>
      <td>C++ has a stiff learning curve as it has lots of predefined syntaxes and structure.</td>
    </tr>
    <tr>
      <th>Compilation</th>
      <td>Python uses an interpreter.</td>
      <td>C++ is precompiled.</td>
    </tr>
    <tr>
      <th>Speed</th>
      <td>Python is slower since it uses interpreter and also determines the data type at run time.</td>
      <td>C++ is faster in speed as compared to python as it is closer to the machine due to it not needing an interpreter.</td>
    </tr>
    <tr>
      <th>Rapid Prototyping</th>
      <td>Rapid Prototyping is easier due to the small size of the code and REPL function.</td>
      <td>Rapid Prototyping is more difficult due to larger code size and no REPL function.</td>
    </tr>
  </tbody>
</table>

As our microcontroller does not have a lot of memory available and a relatively slow processor code it would be best to choose for C++ in terms of performance. Memory also fills up relatively fast in python as the interpreter has to run on the board.

### 2. Framework support
#### Espressif IoT development framework
ESP-IDF is Espressif's official IoT Development Framework for the ESP32, ESP32-S and ESP32-C series of SoCs. It provides a self-sufficient SDK for any generic application development on these platforms, using programming languages such as C and C++. It is a very feature-rich framework that allows you to have very fine-tuned control over the things that are connected to your microcontroller <sup>[2](##Sources)</sup>. In terms of framework support ESP IDF takes the number 1 spot, but it might pose quite the challenge to learn and understand in order to get writing.

#### Arduino
Arduino is an open-source electronics platform based on easy-to-use hardware and software. It's intended for anyone making interactive projects <sup>[3](##Sources)</sup>. Arduino allows you to write code in C++ without the added complexity of having to manually configure all of the hardware on your microcontroller development board. Arduino is easier to use and also has more extensive ecosystem/library support compared to ESP IDF as it is the go-to development environment for makers. This definitely makes it to where Arduino takes 2nd place in the ranking.

#### MicroPython
MicroPython is a software implementation of a programming language largely compatible with Python 3, written in C, that is optimized to run on a microcontroller. MicroPython consists of a Python compiler to bytecode and a runtime interpreter of that bytecode <sup>[4](##Sources)</sup>. As micropython is further away from the native microcontroller language C than C++ is the amount of libraries available put this in the 3rd spot in terms of framework support ranking.

### 3. Code writing & performance
In terms of development velocity Micro- and CircuitPython have a lot more to offer due to the REPL function which allows you to write and compile code on the fly. This makes it so you can even rapidly prototype in Python. 

In order to compare performance between MicroPython and the Arduino framework I wrote 2 simple code examples that both try to read the UID of an RFID card and print that to the serial monitor. The performance measurement that is used is the amount of loops that can be executed per second by the microcontroller. Both MicroPython and C++ make use of bitbanging to get SPI to work on the non-standard SPI pinout.

The code below shows the python example code for reading the RFID card, every 10 seconds it prints out the amount of loops it went through using the timer function and a counter that gets increased whenever it loops. The benchmark result was that this code consistently does 17 loops in 10 seconds.

```py
from mfrc522 import MFRC522
from machine import Pin
from machine import SoftSPI
from machine import Timer

spi = SoftSPI(baudrate=100000, polarity=0, phase=0, sck=Pin(15), mosi=Pin(13), miso=Pin(12))
spi.init()
rdr = MFRC522(spi=spi, gpioRst=0, gpioCs=2)
print("Place card")
counter = 0
timer = Timer(-1)
timer.init(period=10000, mode=Timer.PERIODIC, callback=lambda a:print(counter))

while True:
    counter += 1
    (stat, tag_type) = rdr.request(rdr.REQIDL)
    if stat == rdr.OK:
        (stat, raw_uid) = rdr.anticoll()
        if stat == rdr.OK:
            card_id = "uid: %02X %02X %02X %02X" % (raw_uid[0], raw_uid[1], raw_uid[2], raw_uid[3])
            print(card_id)
```


The code below shows the C++ example code for reading the RFID card, every 10 seconds it prints out the amount of loops it went through using a conditional millis function and a counter that gets increased whenever it loops. The benchmark result was that this code consistently does 198 loops in 10 seconds.
```cpp
#include <Arduino.h>
#include <SPI.h>
#include <MFRC522.h>

#define RFID_SS 2
#define RFID_MISO 12
#define RFID_MOSI 13
#define RFID_SCK 15
MFRC522 mfrc522(RFID_SS, 39);

long lastMillis = 0;
long loops = 0;

void setup()
{
    Serial.begin(115200);
    SPI.begin(RFID_SCK, RFID_MISO, RFID_MOSI, RFID_SS);
    mfrc522.PCD_Init();
    Serial.println("Approximate your card to the reader...");
    Serial.println();
}
void loop()
{
    long currentMillis = millis();
    loops++;

    if (!mfrc522.PICC_IsNewCardPresent())
    {
    }

    if (mfrc522.PICC_ReadCardSerial())
    {
        Serial.print("UID tag :");
        String content = "";
        for (byte i = 0; i < mfrc522.uid.size; i++)
        {
            Serial.print(mfrc522.uid.uidByte[i] < 0x10 ? " 0" : " ");
            Serial.print(mfrc522.uid.uidByte[i], HEX);
            content.concat(String(mfrc522.uid.uidByte[i] < 0x10 ? " 0" : " "));
            content.concat(String(mfrc522.uid.uidByte[i], HEX));
        }
        Serial.println();
    }

    if (currentMillis - lastMillis > 10000)
    {
        Serial.print("Loops in 10 seconds:");
        Serial.println(loops);

        lastMillis = currentMillis;
        loops = 0;
    }    
}
```

## Conclusion
While MicroPython is a good option in case you want to rapidly prototype and test out new code, but because the performance compared to C++ is so poor it is better to go with the latter option. C++ is quite a bit more verbose compared to MicroPython as you can see by the difference in the amount of lines of code needed. 

Between ESP-IDF and Arduino the results are also very clear, if you need the extensive configuration options and very fine control that C offers then that is the way to go, but for our current use-case it makes the most sense to use the creator-friendly Arduino platform with it's many libraries and lower skill floor. 

## Sources
1. [GeeksforGeeks. (2022, March 3). Differences between Python and C++. Retrieved March 23, 2022.](https://www.geeksforgeeks.org/difference-between-python-and-c/)
2. [ESP IoT development framework. (n.d.). Espressif. Retrieved March 23, 2022.](https://www.espressif.com/en/products/sdks/esp-idf)
3. [Arduino home page. (n.d.). Arduino. Retrieved March 23, 2022.](https://www.arduino.cc/)
4. [Wikipedia contributors. (2022, February 27). MicroPython. Wikipedia. Retrieved March 23, 2022.](https://en.wikipedia.org/wiki/MicroPython)
