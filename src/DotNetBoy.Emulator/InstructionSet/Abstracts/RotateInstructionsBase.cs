using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.Abstracts;

public class RotateInstructionsBase(IClockService clockService)
{
    protected readonly IClockService ClockService = clockService;

    protected byte RotateByteLeft(byte toRotate, ICpuRegistersService registers, bool includeZeroFlag = true)
    {
        registers.F.Carry = (toRotate & 0x80) == 0x80;
        var result = (byte)(toRotate << 1);
        if (registers.F.Carry)
        {
            result = (byte)(result | 0x01);
        }

        registers.F.Zero = includeZeroFlag && result == 0;
        registers.F.Subtract = false;
        registers.F.HalfCarry = false;
        registers.ProgramCounter += 1;
        return result;
    }

    protected byte RotateByteLeftThroughCarry(byte toRotate, ICpuRegistersService registers, bool includeZeroFlag = true)
    {
        var previousCarry = registers.F.Carry;
        registers.F.Carry = (toRotate & 0x80) == 0x80;
        var result = (byte)(toRotate << 1);
        if (previousCarry)
        {
            result = (byte)(result | 0x01);
        }

        registers.F.Zero = includeZeroFlag && result == 0;
        registers.F.Subtract = false;
        registers.F.HalfCarry = false;
        registers.ProgramCounter += 1;
        return result;
    }


    protected byte RotateByteRight(byte toRotate, ICpuRegistersService registers, bool includeZeroFlag = true)
    {
        registers.F.Carry = (toRotate & 0x01) == 0x01;
        var result = (byte)(toRotate >> 1);
        if (registers.F.Carry)
        {
            result = (byte)(result | 0x80);
        }

        registers.F.Zero = includeZeroFlag && result == 0;
        registers.F.Subtract = false;
        registers.F.HalfCarry = false;
        registers.ProgramCounter += 1;
        return result;
    }

    protected byte RotateByteRightThroughCarry(byte toRotate, ICpuRegistersService registers, bool includeZeroFlag = true)
    {
        var previousCarry = registers.F.Carry;
        registers.F.Carry = (toRotate & 0x01) == 0x01;
        var result = (byte)(toRotate >> 1);
        if (previousCarry)
        {
            result = (byte)(result | 0x80);
        }

        registers.F.Zero = includeZeroFlag && result == 0;
        registers.F.Subtract = false;
        registers.F.HalfCarry = false;
        registers.ProgramCounter += 1;
        return result;
    }
}