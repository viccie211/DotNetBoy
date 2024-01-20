using DotNetBoy.Emulator.InstructionSet.Interfaces;

namespace DotNetBoy.Emulator.Services.Implementations;

public interface IInstructionSetService
{
    public Action<CpuRegisters>[] NonPrefixedInstructions { get; }
    public Action<CpuRegisters>[] PrefixedInstructions { get; }
}