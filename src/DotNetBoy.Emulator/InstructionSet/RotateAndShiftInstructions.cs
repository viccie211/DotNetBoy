using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class RotateAndShiftInstructions : IInstructionSet
{
    private readonly IClockService _clockService;

    public RotateAndShiftInstructions(IClockService clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x07, RotateLeftWithCarryA }
        };
        _clockService = clockService;
    }

    public void RotateLeftWithCarryA(ICpuRegistersService registers)
    {
        registers.F.Carry = (registers.A & 0x80) == 0x80;
        var tempByte = (byte)(registers.A << 1);
        registers.A = registers.F.Carry ? (byte)(tempByte | 0x01) : (byte)(tempByte ^ 0x01);
        registers.F.Zero = false;
        registers.F.HalfCarry = false;
        registers.F.Subtract = false;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }
}