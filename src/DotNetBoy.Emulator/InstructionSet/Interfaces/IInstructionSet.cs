using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.Interfaces;

public interface IInstructionSet
{
    Dictionary<byte,Action<ICpuRegistersService>> Instructions { get; }
}