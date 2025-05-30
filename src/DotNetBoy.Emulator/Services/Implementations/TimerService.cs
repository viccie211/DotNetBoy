﻿using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.Services.Implementations;

/*
 * This class currently makes heavy use of code written by spec-chum in their SpecBoy project.
 * I'm due to refactor this back to my own implementation but for now it is what it is.
 * For fullness this is the LICENSE found in SpecBoy's Repo accessed on 2025-05-22
MIT License

Copyright (c) 2020 spec-chum

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */
public class TimerService : ITimerService
{
    private static readonly int[] triggerBits = [9, 3, 5, 7];

    private int divBit;
    public ushort divCounter;
    public ushort DivCounter => divCounter;
    private bool lastResult;
    private bool reloadTima;
    private bool reloadingTima;

    // Registers
    private byte tima;
    private byte tma;
    private byte tac;

    public byte Div
    {
        get => (byte)(divCounter >> 8);

        set
        {
            // Writing to Div always sets it to 0
            divCounter = 0;

            // Can't have falling edge if last result was 0
            if (!lastResult)
            {
                return;
            }

            DetectFallingEdge();
        }
    }

    public byte Tima
    {
        get => tima;
        set
        {
            // Block write if TIMA is being reloaded
            if (reloadingTima)
            {
                return;
            }

            // Writing to TIMA can cancel IRQ request, do that here
            tima = value;
            reloadTima = false;
        }
    }

    public byte Tma
    {
        get => tma;
        set
        {
            tma = value;

            // TIMA also updated if it's being reloaded when TMA written to
            if (reloadingTima)
            {
                tima = tma;
            }
        }
    }

    public byte Tac
    {
        get => tac;
        set
        {
            tac = value;

            divBit = triggerBits[tac & 3];

            // Can't have falling edge if last result was 0
            if (!lastResult)
            {
                return;
            }

            DetectFallingEdge();
        }
    }

    public bool Tick()
    {
        var raiseInterrupt = false;
        reloadingTima = false;

        if (reloadTima)
        {
            reloadTima = false;
            reloadingTima = true;
            tima = tma;

            raiseInterrupt = true;
        }

        divCounter += 4;
        DetectFallingEdge();
        return raiseInterrupt;
    }

    private void DetectFallingEdge()
    {
        bool result = IsBitSet(2, tac) && IsBitSet(divBit, divCounter);

        // Detect falling edge
        if (lastResult && !result)
        {
            tima++;

            if (tima == 0)
            {
                reloadTima = true;
            }
        }

        lastResult = result;
    }

    private bool IsBitSet(int bitNumber, uint toTest)
    {
        var mask = 1 << bitNumber;
        return (mask & toTest) == mask;
    }
}