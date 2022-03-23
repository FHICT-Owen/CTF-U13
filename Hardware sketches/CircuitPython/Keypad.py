import keypad
import board

km = keypad.KeyMatrix(
    row_pins=(board.IO0, board.IO3, board.IO1, board.IO5),
    column_pins=(board.IO6, board.IO4, board.IO2),
)

while True:
    event = km.events.get()
    if event:
        print(event)