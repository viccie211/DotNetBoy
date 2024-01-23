using DotNetBoy.Emulator.Services.Implementations;

namespace DotNetBoy.Emulator.Services.Interfaces;

public interface IInstructionSetService
{
    public Action<ICpuRegistersService>[] NonPrefixedInstructions { get; }

    public Action<ICpuRegistersService>[] PrefixedInstructions { get; }
}