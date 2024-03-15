using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;

public class RotateRightInstructions : IInstructionSet
{
    private readonly IClockService _clockService;
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public RotateRightInstructions(IClockService clockService)
    {
        _clockService = clockService;
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x19, RotateC },
            { 0x1A, RotateD }
        };
    }

    public void RotateC(ICpuRegistersService registers)
    {
        registers.C = RotateRight(registers.C, registers);
    }

    public void RotateD(ICpuRegistersService registers)
    {
        registers.D = RotateRight(registers.D, registers);
    }

    private byte RotateRight(byte toRotate, ICpuRegistersService registers)
    {
        var newCarry = (toRotate & 0x01) == 0x01;
        var shifted = (byte)(toRotate >> 1);

        if (registers.F.Carry)
        {
            shifted += 0x80;
        }

        registers.F.Carry = newCarry;
        registers.F.Zero = shifted == 0;
        registers.ProgramCounter += 2;
        _clockService.Clock(2);
        return shifted;
    }
}