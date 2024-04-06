using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions;

public abstract class ShiftInstructionsBase(IClockService clockService)
{
    protected readonly IClockService ClockService = clockService;


    protected byte ShiftRightLogicalByte(byte toSRL, ICpuRegistersService registers)
    {
        registers.F.Carry = (toSRL & 0x01) == 0x01;
        toSRL = (byte)(toSRL >> 1);
        registers.F.Zero = toSRL == 0;
        registers.F.Subtract = false;
        registers.F.HalfCarry = false;
        ClockService.Clock(2);
        registers.ProgramCounter += 2;
        return toSRL;
    }

    protected byte ShiftRightArithmeticByte(byte toSRA, ICpuRegistersService registers)
    {
        var previousBit7 = (byte)(toSRA & 0x80);
        registers.F.Carry = (toSRA & 0x01) == 0x01;
        toSRA = (byte)(toSRA >> 1);
        toSRA = (byte)(toSRA | previousBit7);
        registers.F.Zero = toSRA == 0;
        registers.F.Subtract = false;
        registers.F.HalfCarry = false;
        ClockService.Clock(2);
        registers.ProgramCounter += 2;
        return toSRA;
    }

    protected byte ShiftLeftArithmeticByte(byte toSLA, ICpuRegistersService registers)
    {
        registers.F.Carry = (toSLA & 0x80) == 0x80;
        toSLA = (byte)(toSLA << 1);
        registers.F.Zero = toSLA == 0;
        registers.F.Subtract = false;
        registers.F.HalfCarry = false;
        ClockService.Clock(2);
        registers.ProgramCounter += 2;
        return toSLA;
    }
}