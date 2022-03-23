import board
import mfrc522

rdr = mfrc522.MFRC522(board.IO40, board.SD_MOSI, board.SD_MISO, board.IO39, board.IO41)
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
