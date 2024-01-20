using DotNetBoy.Emulator.InstructionSet.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class DecrementInstructions : IInstructionSet
{
    public Dictionary<byte, Action<CpuRegisters>> Instructions { get; }
    private readonly IClockService _clockService;
    public DecrementInstructions(IClockService clockService)
    {
        Instructions = new Dictionary<byte, Action<CpuRegisters>>()
        {
            { 0x05, DecrementB }
        };
        _clockService = clockService;
    }


    private void DecrementB(CpuRegisters cpu)
    {
        cpu.F.Subtract = true;
        cpu.F.HalfCarry = InstructionUtilFunctions.HalfCarryFor8BitAddition(cpu.B, 0xFE);
        cpu.B--;
        cpu.F.Zero = cpu.B == 0;
        _clockService.Clock();
        cpu.ProgramCounter += 1;
    }


}