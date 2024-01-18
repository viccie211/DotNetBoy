namespace DotNetBoy.Emulator.InstructionSet.Interfaces;

public interface IInstructionSet
{
    Dictionary<byte,Action<CpuRegisters>> Instructions { get; }
}