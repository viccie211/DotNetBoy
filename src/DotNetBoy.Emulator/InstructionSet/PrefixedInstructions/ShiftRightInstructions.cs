using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions;

public class ShiftRightInstructions : IInstructionSet
{
    public ShiftRightInstructions(IClockService clockService)
    {
        _clockService = clockService;
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x38, SRLB }
        };
    }

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    private readonly IClockService _clockService;

    public void SRLB(ICpuRegistersService registers)
    {
        registers.B = SRLByte(registers.B, registers);
    }

    private byte SRLByte(byte toSRL, ICpuRegistersService registers)
    {
        registers.F.Carry = (toSRL & 0x01) == 0x01;
        toSRL = (byte)(toSRL >> 1);
        registers.F.Zero = toSRL == 0;
        registers.F.Subtract = false;
        registers.F.HalfCarry = false;
        _clockService.Clock(2);
        registers.ProgramCounter += 2;
        return toSRL;
    }
}