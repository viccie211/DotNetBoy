# DotNetBoy
A .NET 8 Gameboy Emulator, written as a programming exercise

## DotNetBoy.AvaloniaFrontend
A (rudimentary) frontend for DotNetBoy written in Avalonia UI

# DON'T EXPECT ANYTHING OF THIS! THIS IS NOT MADE TO BE AN ACCURATE OR EVEN FUNCTIONAL EMULATOR IN THE FIRST PLACE!
It's an exercise for me to understand how computers work at the low level.
Tetris is playable though, albeit slowly for now!

## Current progress:
Implemented __All__ instructions (HALT and STOP need work)

Not yet implemented:
* Most Memory mappers
* Window Layer graphics (? Maybe I'm not sure) 
* Sprite delay in the PPU
* Keyboard Input
* Sound

# Special Thanks 
* The developers of BGB (https://bgb.bircd.org/)
* The developer SpecBoy: spec-chum (https://github.com/spec-chum/SpecBoy)
For their emulators which I used as my "hardware" references on many occasions
Also SpecBoy for some small code snippets that I "borrowed" from it. SpecBoy is licensed under the MIT License which you can find in it's own repo here: https://github.com/spec-chum/SpecBoy/blob/master/LICENSE
I also included SpecBoys license in the class where I actually copied code from it.