namespace DotNetBoy.Emulator.Services.Interfaces;

public interface IInstructionSetService
{
    public Action<CpuRegisters>[] NonPrefixedInstructions { get; }
    public Action<CpuRegisters>[] PrefixedInstructions { get; }
}