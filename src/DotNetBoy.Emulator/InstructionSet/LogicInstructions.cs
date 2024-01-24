﻿using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Implementations;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class LogicInstructions : IInstructionSet
{
    private readonly IMmuService _mmuService;
    private readonly IClockService _clockService;

    public LogicInstructions(IMmuService mmuService, IClockService clockService)
    {
        _mmuService = mmuService;
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0xB7, ORAWithA },
            { 0xAF, XORAWithA },
            { 0xFE, CompareAToD8 },
        };
        _clockService = clockService;
    }

    public void CompareAToD8(ICpuRegistersService registers)
    {
        var d8 = _mmuService.ReadByte((ushort)(registers.ProgramCounter + 1));
        var result = (byte)(registers.A - d8);
        registers.F.Subtract = true;
        registers.F.Zero = result == 0;
        registers.F.HalfCarry = InstructionUtilFunctions.HalfCarryFor8BitSubtraction(registers.A, d8);
        registers.F.Carry = InstructionUtilFunctions.CarryFor8BitSubtract(registers.A, d8);
        _clockService.Clock(2);
        registers.ProgramCounter += 2;
    }

    public void XORAWithA(ICpuRegistersService registers)
    {
        registers.A = (byte)(registers.A ^ registers.A);
        registers.F.Zero = registers.A == 0;
        registers.F.Subtract = false;
        registers.F.Carry = false;
        registers.F.HalfCarry = false;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }

    public void ORAWithA(ICpuRegistersService registers)
    {
        registers.A = (byte)(registers.A | registers.A);
        registers.F.Zero = registers.A == 0;
        registers.F.Subtract = false;
        registers.F.Carry = false;
        registers.F.HalfCarry = false;
        registers.ProgramCounter += 1;
        _clockService.Clock();
    }

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }
}