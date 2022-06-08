# Hardware peripherals
This document shares details on how each of the relevant hardware peripherals function and also shares any information about alternative peripheral choices where possible. Each of the major sections below talks about a singular peripheral.

## MFRC522 (RFID Reader)
The MFRC522 is an RFID Reader that allows you to write and read information to and from 13.56MHz RFID tags. It uses the SPI interface in order to communicate with the microcontroller it is attached to<sup>[1](##Sources)</sup>. It also has an option to communicate over the I2C interface instead of the usual SPI interface by making some slight modifications to the board itself.

In our case the MFRC522 is connected to the microcontroller using 6 cables instead of the usual 7 as we do not have any need reset pin in our use-case. The SDA/Data pin is connected to pin 2, MISO to pin 12, MOSI to pin 13 and SCK to pin 15. The remaining 2 wires are for ground and power. I had to manually reconfigure the VSPI interface to use these pins instead of the default ones.

### Alternative
The alternative to the MFRC522 was the RDM6300, this reader module reads 125kHz RFID tags instead of the 13.56MHz RFID tags the MFRC522 reads. Instead of using an SPI interface to communicate over the RDM6300 makes use of the UART interface, which means that there is no clock signal needed to synchronize between transmitter and receiver devices as data is transmitted asynchronously<sup>[2](##Sources)</sup>. The MFRC522 was chosen over the RDM6300 as reading tags with the RDM was very inconsistent in my own testing compared to the MFRC522.

## WS2812B LED Strip
The LED strip used in each prototype consists of 6 WS2812B LEDs a.k.a. Neopixel LEDs. These LED's require one data connection 5v power and ground, which means we only need to use 1 IO pin (which is pin 27 in our case). The data connection between each LED can be put in series as each individual pixel interprets only 24 bits and sends any remaining data to the input of the next pixel<sup>[3](##Sources)</sup>. In the image below the schematic for how the LED strip is wired can be seen, further showing that the input of each LED in the chain is wired to the output of the previous LED.

<img src="https://cdn-apcpp.nitrocdn.com/bpkZtbhIUIRFwZCfdBtvcQKZzZYQkZZw/assets/static/optimized/rev-bdbb3ce/wp-content/uploads/2021/02/COLORED-WS2812B.jpg">

## Phone-style Keypad
When it comes to number inputs for the code game we have a few different choices for keypads. The regular flat keypad which is used most often of which an image can be seen below.

<img src="https://cdn.myonlinestore.eu/e40d5160-bac0-4897-baae-d6065a5d5915/image/cache/full/123e7714e988a4b465afb129f76527f8e2acb256.jpg">

A tactile non-flat keypad.

<img src="https://media.s-bol.com/Y5BE83B7ylk9/550x458.jpg">

And a higher quality tactile phone-style keypad.

<img src="https://www.sossolutions.nl/media/catalog/product/cache/5df5c040ed8cd3972c59a8e190e44350/1/8/1824-03.jpg">

We first of tried out the top one but found the tactile feedback from this keypad to be too little and the keypad is also very fidgety to use with gloves on. After that we tried the second option tactile keypad which was a major improvement in terms of tactility, but it had one major issue, that being that the contact pads used for connecting to the microcontroller were very thin and would break really fast if the keypad was moved around too much. Finally we found the third keypad which had higher quality buttons and already soldered on pins for jumper wires. The wiring for this last keypad also made a lot more sense with the column and row pins being grouped together instead of randomly interchanging like in the second keypad. 

The Matrix keypad works by having 4 row pins and 3 column pins being attached to the keypad. A microcontroller can scan these pins for a button-pressed state. In the keypad library, the Propeller sets all the column lines to input, and all the row lines to input. Then, it picks a row and sets it high. After that, it checks the column lines one at a time. If the column connection stays low, the button on the row has not been pressed. If it goes high, the microcontroller knows which row (the one it set high), and which column, (the one that was detected high when checked)<sup>[4](##Sources)</sup>.

## Sources
1. [MFRC522. (n.d.). NXP Semiconductors. Retrieved June 8, 2022](https://www.nxp.com/docs/en/data-sheet/MFRC522.pdf)
2. [Peňa, E. & Legaspi, M. G. (2020, December). UART: A hardware communication protocol. Analog Devices. Retrieved June 8, 2022](https://www.analog.com/en/analog-dialogue/articles/uart-a-hardware-communication-protocol.html)
3. [What is WS2812b LED and how to use WS2812b LED. (2021, June 23). SDIP Light. Retrieved June 8, 2022](https://www.sdiplight.com/what-is-ws2812b-led-and-how-to-use-ws2812b-led/)
4. [Read a 4x4 Matrix Keypad | LEARN.PARALLAX.COM. (n.d.). Parallax. Retrieved June 8, 2022](https://learn.parallax.com/tutorials/language/propeller-c/propeller-c-simple-devices/read-4x4-matrix-keypad)
