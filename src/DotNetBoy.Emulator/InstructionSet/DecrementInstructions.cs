using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Implementations;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class DecrementInstructions : IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }
    private readonly IClockService _clockService;

    public DecrementInstructions(IClockService clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x05, DecrementB }
        };
        _clockService = clockService;
    }

    public void DecrementB(ICpuRegistersService registers)
    {
        registers.B = Decrement8Bits(registers.B, registers);
    }

    private byte Decrement8Bits(byte initial, ICpuRegistersService registers)
    {
        registers.F.Subtract = true;
        registers.F.HalfCarry = InstructionUtilFunctions.HalfCarryFor8BitSubtraction(initial, 0x01);
        var result = (byte)(initial - 1);
        registers.F.Zero = result == 0;
        registers.ProgramCounter += 1;
        _clockService.Clock();
        return result;
    }
}