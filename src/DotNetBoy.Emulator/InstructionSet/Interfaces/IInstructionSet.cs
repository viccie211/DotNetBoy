using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.Interfaces;

public interface IInstructionSet
{
    void ExecuteInstruction(byte opCode, ICpuRegistersService registers);
}