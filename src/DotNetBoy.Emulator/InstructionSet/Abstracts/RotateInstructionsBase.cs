using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.Abstracts;

public class RotateInstructionsBase(IClockService clockService)
{
    protected readonly IClockService ClockService = clockService;
    protected byte RotateByteLeft(byte toRotate, ICpuRegistersService registers)
    {
        registers.F.Carry = (toRotate & 0x80) == 0x80;
        var result = (byte)(toRotate << 1);
        if (registers.F.Carry)
        {
            result = (byte)(result | 0x01);
        }

        registers.F.Zero = result == 0;
        registers.F.Subtract = false;
        registers.F.HalfCarry = false;
        ClockService.Clock();
        registers.ProgramCounter += 1;
        return result;
    }

    protected byte RotateByteLeftThroughCarry(byte toRotate, ICpuRegistersService registers)
    {
        var previousCarry = registers.F.Carry;
        registers.F.Carry = (toRotate & 0x80) == 0x80;
        var result = (byte)(toRotate << 1);
        if (previousCarry)
        {
            result = (byte)(result | 0x01);
        }

        registers.F.Zero = result == 0;
        registers.F.Subtract = false;
        registers.F.HalfCarry = false;
        ClockService.Clock();
        registers.ProgramCounter += 1;
        return result;
    }


    protected byte RotateByteRight(byte toRotate, ICpuRegistersService registers)
    {
        registers.F.Carry = (toRotate & 0x01) == 0x01;
        var result = (byte)(toRotate >> 1);
        if (registers.F.Carry)
        {
            result = (byte)(result | 0x80);
        }

        registers.F.Zero = result == 0;
        registers.F.Subtract = false;
        registers.F.HalfCarry = false;
        ClockService.Clock();
        registers.ProgramCounter += 1;
        return result;
    }

    protected byte RotateByteRightThroughCarry(byte toRotate, ICpuRegistersService registers)
    {
        var previousCarry = registers.F.Carry;
        registers.F.Carry = (toRotate & 0x01) == 0x01;
        var result = (byte)(toRotate >> 1);
        if (previousCarry)
        {
            result = (byte)(result | 0x80);
        }

        registers.F.Zero = result == 0;
        registers.F.Subtract = false;
        registers.F.HalfCarry = false;
        ClockService.Clock();
        registers.ProgramCounter += 1;
        return result;
    }
}