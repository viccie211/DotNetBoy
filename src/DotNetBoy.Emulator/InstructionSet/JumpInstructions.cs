using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class JumpInstructions : IInstructionSet
{
    private readonly IMmuService _mmuService;
    public JumpInstructions(IMmuService mmuService)
    {
        Instructions = new Dictionary<byte, Action<CpuRegisters>>()
        {
            { 0xC3, Jump }
        };
        _mmuService = mmuService;
    }

    private void Jump(CpuRegisters cpu)
    {
        cpu.ProgramCounter = _mmuService.ReadWordLittleEndian((ushort)(cpu.ProgramCounter + 1));
        cpu.Clock(4);
    }

    public Dictionary<byte,Action<CpuRegisters>> Instructions { get; init; }
}