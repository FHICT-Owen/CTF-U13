# 3rd party
import board
import busio
import digitalio
from adafruit_bus_device.spi_device import SPIDevice
from microcontroller import Pin

class MFRC522:
    OK = 0
    NOTAGERR = 1
    ERR = 2

    REQIDL = 0x26
    REQALL = 0x52
    AUTHENT1A = 0x60
    AUTHENT1B = 0x61

    def __init__(self, sck: Pin, mosi: Pin, miso: Pin, rst: Pin, cs: Pin):

        self.cs = digitalio.DigitalInOut(cs)

        self.rst = digitalio.DigitalInOut(rst)
        self.rst.switch_to_output()

        self.rst.value = 0
        self.rst.value = 1

        self.spi = busio.SPI(sck, MOSI=mosi, MISO=miso)
        self.spi_device = SPIDevice(self.spi, self.cs)

        self.init()

    def _wreg(self, reg: int, val):

        with self.spi_device as bus_device:
            bus_device.write(b"%c" % int(0xFF & ((reg << 1) & 0x7E)))
            bus_device.write(b"%c" % int(0xFF & val))

    def _rreg(self, reg: int):

        with self.spi_device as bus_device:
            bus_device.write(b"%c" % int(0xFF & (((reg << 1) & 0x7E) | 0x80)))
            val = bytearray(1)
            bus_device.readinto(val)

        return val[0]

    def _sflags(self, reg: int, mask: int):
        self._wreg(reg, self._rreg(reg) | mask)

    def _cflags(self, reg: int, mask: int):
        self._wreg(reg, self._rreg(reg) & (~mask))

    def _tocard(self, cmd: int, send):

        recv = []
        bits = irq_en = wait_irq = 0
        stat = self.ERR

        if cmd == 0x0E:
            irq_en = 0x12
            wait_irq = 0x10
        elif cmd == 0x0C:
            irq_en = 0x77
            wait_irq = 0x30

        self._wreg(0x02, irq_en | 0x80)
        self._cflags(0x04, 0x80)
        self._sflags(0x0A, 0x80)
        self._wreg(0x01, 0x00)

        for c in send:
            self._wreg(0x09, c)
        self._wreg(0x01, cmd)

        if cmd == 0x0C:
            self._sflags(0x0D, 0x80)

        i = 2000
        while True:
            n = self._rreg(0x04)
            i -= 1
            if ~((i != 0) and ~(n & 0x01) and ~(n & wait_irq)):
                break

        self._cflags(0x0D, 0x80)

        if i:
            if (self._rreg(0x06) & 0x1B) == 0x00:
                stat = self.OK

                if n & irq_en & 0x01:
                    stat = self.NOTAGERR
                elif cmd == 0x0C:
                    n = self._rreg(0x0A)
                    lbits = self._rreg(0x0C) & 0x07
                    if lbits != 0:
                        bits = (n - 1) * 8 + lbits
                    else:
                        bits = n * 8

                    if n == 0:
                        n = 1
                    elif n > 16:
                        n = 16

                    for _ in range(n):
                        recv.append(self._rreg(0x09))
            else:
                stat = self.ERR

        return stat, recv, bits

    def _crc(self, data):

        self._cflags(0x05, 0x04)
        self._sflags(0x0A, 0x80)

        for c in data:
            self._wreg(0x09, c)

        self._wreg(0x01, 0x03)

        i = 0xFF
        while True:
            n = self._rreg(0x05)
            i -= 1
            if not ((i != 0) and not (n & 0x04)):
                break

        return [self._rreg(0x22), self._rreg(0x21)]

    def init(self):

        self.reset()
        self._wreg(0x2A, 0x8D)
        self._wreg(0x2B, 0x3E)
        self._wreg(0x2D, 30)
        self._wreg(0x2C, 0)
        self._wreg(0x15, 0x40)
        self._wreg(0x11, 0x3D)
        self.antenna_on()

    def reset(self):
        self._wreg(0x01, 0x0F)

    def antenna_on(self, on=True):

        if on and ~(self._rreg(0x14) & 0x03):
            self._sflags(0x14, 0x03)
        else:
            self._cflags(0x14, 0x03)

    def request(self, mode):

        self._wreg(0x0D, 0x07)
        (stat, recv, bits) = self._tocard(0x0C, [mode])

        if (stat != self.OK) | (bits != 0x10):
            stat = self.ERR

        return stat, bits

    def anticoll(self):

        ser_chk = 0
        ser = [0x93, 0x20]

        self._wreg(0x0D, 0x00)
        (stat, recv, bits) = self._tocard(0x0C, ser)

        if stat == self.OK:
            if len(recv) == 5:
                for i in range(4):
                    ser_chk = ser_chk ^ recv[i]
                if ser_chk != recv[4]:
                    stat = self.ERR
            else:
                stat = self.ERR

        return stat, recv

    def set_antenna_gain(self, gain: int):
        """
        Set the MFRC522 Receiver Gain

        :param gain:

        Possible values are:

        * ``0x00 << 4`` -- 000b - 18 dB, minimum
        * ``0x01 << 4`` -- 001b - 23 dB
        * ``0x02 << 4`` -- 010b - 18 dB, it seems 010b is a duplicate for 000b
        * ``0x03 << 4`` -- 011b - 23 dB, it seems 011b is a duplicate for 001b
        * ``0x04 << 4`` -- 100b - 33 dB, average, and typical default
        * ``0x05 << 4`` -- 101b - 38 dB
        * ``0x06 << 4`` -- 110b - 43 dB
        * ``0x07 << 4`` -- 111b - 48 dB, maximum
        * ``0x00 << 4`` -- 000b - 18 dB, minimum, convenience for RxGain_18dB
        * ``0x04 << 4`` -- 100b - 33 dB, average, convenience for RxGain_33dB
        * ``0x07 << 4`` -- 111b - 48 dB, maximum, convenience for RxGain_48dB

        :return:
        """

        # Above table from https://github.com/miguelbalboa/rfid/blob/master/src/MFRC522.h
        # See also 9.3.3.6 / table 98 of the datasheet (http://www.nxp.com/documents/data_sheet/MFRC522.pdf)

        self._cflags(0x26, 0x07 << 4)
        self._sflags(0x26, gain & (0x07 << 4))


rdr = MFRC522(board.IO40, board.SD_MOSI, board.SD_MISO, board.IO39, board.IO41)
rdr.set_antenna_gain(0x04 << 4)

print("")
print("Place card before reader to read UID")
print("")

try:
    while True:

        (stat, tag_type) = rdr.request(rdr.REQIDL)

        if stat == rdr.OK:

            (stat, raw_uid) = rdr.anticoll()

            if stat == rdr.OK:
                print("New card detected")
                print(
                    "  - uid\t : %02X%02X%02X%02X"
                    % (raw_uid[0], raw_uid[1], raw_uid[2], raw_uid[3])
                )
                print("")

except KeyboardInterrupt:
    print("Bye")