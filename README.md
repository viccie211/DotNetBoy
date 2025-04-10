# DotNetBoy
A .NET 8 Gameboy Emulator, written as a programming exercise

## DotNetBoy.FrontEnd
Is a FrontEnd for the DotNetBoy emulator written in DotNet MAUI

# DON'T EXPECT ANYTHING OF THIS! THIS IS NOT MADE TO BE AN ACCURATE OR EVEN FUNCTIONAL EMULATOR IN THE FIRST PLACE!
It's an exercise for me to understand how computers work at the low level.

## Current progress:
Implemented __All__ instructions (HALT and STOP need work)

Not yet implemented:
* Raising the following interrupts: 
    * LCD
    * Timer
    * Serial
    * Joypad
* Memory mappers
* Rom bank switching
* Window Layer graphics
* Sprites
* Input
* Sound

# Special Thanks 
* The developers of BGB (https://bgb.bircd.org/)
* The developer SpecBoy: spec-chum (https://github.com/spec-chum/SpecBoy)
For their emulators which I used as my hardware references on many occasions
Also SpecBoy for some small code snippets that I "borrowed" from it. SpecBoy is licensed under the MIT License which you can find in it's own repo here: https://github.com/spec-chum/SpecBoy/blob/master/LICENSE