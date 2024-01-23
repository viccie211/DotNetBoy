using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class PushPopInstructions : IInstructionSet
{
    private readonly IClockService _clockService;
    private readonly IMmuService _mmuService;

    public PushPopInstructions(IClockService clockService, IMmuService mmuService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>
        {
            { 0xF5, PushAF },
        };
        _clockService = clockService;
        _mmuService = mmuService;
    }

    public void PushAF(ICpuRegistersService registers)
    {
        registers.StackPointer--;
        _mmuService.WriteByte(registers.StackPointer, registers.A);
        registers.StackPointer--;
        _mmuService.WriteByte(registers.StackPointer, registers.F);
        registers.ProgramCounter += 1;
        _clockService.Clock(4);
    }

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }
}