using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Implementations;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class IncrementInstructions : IInstructionSet
{
    private readonly IClockService _clockService;

    public IncrementInstructions(IClockService clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x03, IncrementBC },
            { 0x04, IncrementB },
            { 0x33, IncrementStackPointer }
        };
        _clockService = clockService;
    }

    public void IncrementBC(ICpuRegistersService registers)
    {
        registers.BC++;
        _clockService.Clock(2);
        registers.ProgramCounter += 1;
    }

    public void IncrementB(ICpuRegistersService registers)
    {
        registers.F.Subtract = false;
        registers.F.HalfCarry = InstructionUtilFunctions.HalfCarryFor8BitAddition(registers.B, 1);
        registers.B++;
        registers.F.Zero = registers.B == 0;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }

    public void IncrementStackPointer(ICpuRegistersService registers)
    {
        registers.StackPointer++;
        _clockService.Clock(2);
        registers.ProgramCounter += 1;
    }

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }
}