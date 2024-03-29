﻿using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class RotateAndShiftInstructions : IInstructionSet
{
    private readonly IClockService _clockService;

    public RotateAndShiftInstructions(IClockService clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x07, RotateLeftWithCarryA },
            { 0x1F, RotateRightA }
        };
        _clockService = clockService;
    }

    public void RotateLeftWithCarryA(ICpuRegistersService registers)
    {
        registers.F.Carry = (registers.A & 0x80) == 0x80;
        var tempByte = (byte)(registers.A << 1);
        registers.A = registers.F.Carry ? (byte)(tempByte | 0x01) : (byte)(tempByte & 0xFE);
        registers.F.Zero = false;
        registers.F.HalfCarry = false;
        registers.F.Subtract = false;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }

    public void RotateRightA(ICpuRegistersService registers)
    {
        var newCarry = (registers.A & 0x01) == 0x01;
        var shifted = (byte)(registers.A >> 1);

        if (registers.F.Carry)
        {
            shifted += 0x80;
        }

        registers.A = shifted;
        registers.F.Carry = newCarry;
        registers.F.Zero = false;
        registers.F.HalfCarry = false;
        registers.F.Subtract = false;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }
}