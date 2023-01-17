# SNES/SFC LoHiROM Converter
This is a VERY simple command-line utility that converts SNES/SFC LoROM-mapped games into HiROM compatible format.

THIS WILL NOT WORK RIGHT IF THE GAME UTILIZES CARTRIDGE SRAM.
(^^^ You would need to hack the game to read/write to the correct bank for HiROM-mapped SRAM if you wish to use this utility.)

I created this utility because I wanted to test my SuperFamicade (Arcade-Compatible, Hacked) ROMs of ZAMN and TestMenu on a HiROM cart.

Added the ability to convert from HiROM back toLoRom at the request of someone else. Doing a file > hi > file recreates the original file but I don't know what it does to a HiROM that shouldn't be converted. Use at own risk.
