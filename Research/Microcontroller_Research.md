# Microcontroller Languages

In order to get the capture points operational with the different peripherals like a keypad, RFID reader, lighting and buttons we'll need to use a microcontroller to control the peripherals and send data to our dashboard. The modern microcontroller we chose to work with within our group project is the ESP32-S2 that is integrated into the [LILYGO TTGO T8 ESP32-S2 with LCD](http://www.lilygo.cn/prod_view.aspx?TypeId=50062&Id=1321&FId=t3:50062:3). [UPDATE 25-03] I put in the wrong link and now instead of the S2 variant of the TTGO with display we got the [LILYGO TTGO T-Display ESP32](http://www.lilygo.cn/prod_view.aspx?Id=1126) for our project.

In order to program either of the 2 microcontrollers we have a few possible different languages and frameworks we can use to get the results that we want. These options are:
- ESP IoT development framework (C/C++)
- Arduino framework (C++)
- MicroPython (Python with hardware support)
- CircuitPython (Only available on S2 variant)

## Research question

Which of the above development frameworks would be best for our rapid development use-case?

## Research sub-questions
1. Should we use either C++ or Python to develop our code?
	- What is Python language?
	- What is C++ language?
2. Which one of the above frameworks has the best support?
	- Which framework can we use for our dev board?
3. Which one of the above frameworks is the simplest to write code in?
	- Which editors are available to us and the dev board that we use?

## Methods
Now that the questions are formulated, we can use the DOT framework using the following methods and strategies to answer these questions.The best suited methods and strategies to the questions are assigned to their respective questions below.
- Literature study (question 1, 2 & 3)
- Workshop (question 2)

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
      <td>Python uses interpreter.</td>
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

As our microcontroller does not have a lot of memory available and a relatively slow processor code it would be best to choose for C++ in terms of performance. Memory also fills up relatively fast in python as the interpreter has to be on the board.

### 2. Framework support
#### Espressif IoT development framework
ESP-IDF is Espressif's official IoT Development Framework for the ESP32, ESP32-S and ESP32-C series of SoCs. It provides a self-sufficient SDK for any generic application development on these platforms, using programming languages such as C and C++. It is a very feature-rich framework that allows you to have very fine-tuned control over the things that are connected to your microcontroller <sup>[2](##Sources)</sup>. In terms of framework support ESP IDF takes the number 1 spot.

#### Arduino
Arduino is an open-source electronics platform based on easy-to-use hardware and software. It's intended for anyone making interactive projects <sup>[3](##Sources)</sup>. Arduino allows you to write code in C++ without the added complexity of having to manually configure all of the hardware on your microcontroller development board. Arduino is easier to use and also has more extensive ecosystem/library support compared to ESP IDF as it is the go-to development environment for makers. This definitely makes it to where Arduino takes 2nd place in the ranking.

#### MicroPython
MicroPython is a software implementation of a programming language largely compatible with Python 3, written in C, that is optimized to run on a microcontroller. MicroPython consists of a Python compiler to bytecode and a runtime interpreter of that bytecode <sup>[4](##Sources)</sup>. As micropython is further away from the native microcontroller language C than C++ is the amount of libraries available put this in the 3rd spot in terms of framework support ranking.

#### CircuitPython
CircuitPython is an open-source derivative of the MicroPython programming language targeted toward students and beginners. Development of CircuitPython is supported by Adafruit Industries <sup>[5](##Sources)</sup>. Where Arduino has the best library support out of all the selections, CircuitPython has the least amount of out of the box libraries available to it as it is not as widely adopted by creators and is based of of MicroPython specifically developed for Adafruit boards.

### 3. Development velocity
In terms of development velocity python has a lot more to offer due to its REPL function which allows you to write and compile code on the fly. This makes it so you can even rapid prototype in Python. There are however very massive performance differences between C++ and python makes it so you would want your production code to run in C++ instead of Python. As an example, the MFRC522 sketch for CircuitPython takes about 5-7 seconds to detect an RFID card while the Arduino library equivalent running on the same board can detect that same card in a matter of Milliseconds.
## Sources
1. [GeeksforGeeks. (2022, March 3). Differences between Python and C++. Retrieved March 23, 2022.](https://www.geeksforgeeks.org/difference-between-python-and-c/)
2. [ESP IoT development framework. (n.d.). Espressif. Retrieved March 23, 2022.](https://www.espressif.com/en/products/sdks/esp-idf)
3. [Arduino home page. (n.d.). Arduino. Retrieved March 23, 2022.](https://www.arduino.cc/)
4. [Wikipedia contributors. (2022, February 27). MicroPython. Wikipedia. Retrieved March 23, 2022.](https://en.wikipedia.org/wiki/MicroPython)
5. [Wikipedia contributors. (2022b, February 27). MicroPython. Wikipedia. Retrieved March 23, 2022.](https://en.wikipedia.org/wiki/MicroPython)
