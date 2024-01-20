using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class JumpInstructions : IInstructionSet
{
    private readonly IMmuService _mmuService;
    private readonly IClockService _clockService;

    public JumpInstructions(IMmuService mmuService, IClockService clockService)
    {
        Instructions = new Dictionary<byte, Action<CpuRegisters>>()
        {
            { 0xC3, Jump },
            { 0x20, JumpRelative8BitsIfNotZero }
        };
        _mmuService = mmuService;
        _clockService = clockService;
    }

    private void Jump(CpuRegisters cpu)
    {
        cpu.ProgramCounter = _mmuService.ReadWordLittleEndian((ushort)(cpu.ProgramCounter + 1));
        _clockService.Clock(4);
    }

    private void JumpRelative8BitsIfNotZero(CpuRegisters cpu)
    {
        if (!cpu.F.Zero)
        {
            cpu.ProgramCounter =InstructionUtilFunctions.SignedAdd(cpu.ProgramCounter,_mmuService.ReadByte((ushort)(cpu.ProgramCounter + 1)));
            _clockService.Clock(3);
            return;
        }

        cpu.ProgramCounter += 2;
        _clockService.Clock(2);
    }

    public Dictionary<byte, Action<CpuRegisters>> Instructions { get; init; }
}