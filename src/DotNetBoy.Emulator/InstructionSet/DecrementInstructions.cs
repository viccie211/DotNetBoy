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
            { 0x05, DecrementB },
            { 0x0D, DecrementC },
            { 0x2D, DecrementL },
        };
        _clockService = clockService;
    }

    /// <summary>
    /// Decrement the contents of the B register Z 1 H - 
    /// </summary>
    /// Verified against BGB 
    public void DecrementB(ICpuRegistersService registers)
    {
        registers.B = Decrement8Bits(registers.B, registers);
    }

    /// <summary>
    /// Decrement the contents of the C register Z 1 H - 
    /// </summary>
    /// Verified against BGB 
    public void DecrementC(ICpuRegistersService registers)
    {
        registers.C = Decrement8Bits(registers.C, registers);
    }

    /// <summary>
    /// Decrement the contents of the L register Z 1 H - 
    /// </summary>
    /// 
    public void DecrementL(ICpuRegistersService registers)
    {
        registers.L = Decrement8Bits(registers.L, registers);
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