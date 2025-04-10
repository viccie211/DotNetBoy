#!/bin/sh
rgbasm -o CustomTest.o CustomTest.asm
rgblink -o CustomTest.gb CustomTest.o
rgbfix -v CustomTest.gb -p 0xfF
