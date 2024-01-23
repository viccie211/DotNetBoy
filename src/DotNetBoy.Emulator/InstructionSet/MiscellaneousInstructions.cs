using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class MiscellaneousInstructions : IInstructionSet
{
    private readonly IClockService _clockService;

    public MiscellaneousInstructions(IClockService clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>
        {
            { 0x00, NOP },
            { 0xF3, DisableInterrupts }
        };
        _clockService = clockService;
    }

    public void NOP(ICpuRegistersService registers)
    {
        registers.ProgramCounter++;
        _clockService.Clock();
    }

    public void DisableInterrupts(ICpuRegistersService registers)
    {
        registers.InterruptMasterEnable = false;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }
}