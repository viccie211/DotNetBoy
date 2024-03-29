using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class RotateInstructions : IInstructionSet
{
    private readonly IClockService _clockService;
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public RotateInstructions(IClockService clockService)
    {
        _clockService = clockService;
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x07, RotateALeft },
            { 0x0F, RotateARight },
            { 0x17, RotateALeftThroughCarry },
            { 0x1F, RotateARightThroughCarry }
        };
    }

    public void RotateALeft(ICpuRegistersService registers)
    {
        registers.F.Carry = (registers.A & 0x80) == 0x80;
        var result = (byte)(registers.A << 1);
        if (registers.F.Carry)
        {
            result = (byte)(result | 0x01);
        }

        registers.A = result;
        registers.F.Zero = registers.A == 0;
        registers.F.Subtract = false;
        registers.F.HalfCarry = false;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }

    public void RotateALeftThroughCarry(ICpuRegistersService registers)
    {
        var previousCarry = registers.F.Carry;
        registers.F.Carry = (registers.A & 0x80) == 0x80;
        var result = (byte)(registers.A << 1);
        if (previousCarry)
        {
            result = (byte)(result | 0x01);
        }

        registers.A = result;
        registers.F.Zero = registers.A == 0;
        registers.F.Subtract = false;
        registers.F.HalfCarry = false;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }


    public void RotateARight(ICpuRegistersService registers)
    {
        registers.F.Carry = (registers.A & 0x01) == 0x01;
        var result = (byte)(registers.A >> 1);
        if (registers.F.Carry)
        {
            result = (byte)(result | 0x80);
        }

        registers.A = result;
        registers.F.Zero = registers.A == 0;
        registers.F.Subtract = false;
        registers.F.HalfCarry = false;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }

    public void RotateARightThroughCarry(ICpuRegistersService registers)
    {
        var previousCarry = registers.F.Carry;
        registers.F.Carry = (registers.A & 0x01) == 0x01;
        var result = (byte)(registers.A >> 1);
        if (previousCarry)
        {
            result = (byte)(result | 0x80);
        }

        registers.A = result;
        registers.F.Zero = registers.A == 0;
        registers.F.Subtract = false;
        registers.F.HalfCarry = false;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }
}