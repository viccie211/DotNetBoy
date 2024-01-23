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
        registers.F.Subtract = true;
        registers.F.HalfCarry = InstructionUtilFunctions.HalfCarryFor8BitAddition(registers.B, 0xFE);
        registers.B--;
        registers.F.Zero = registers.B == 0;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }


}