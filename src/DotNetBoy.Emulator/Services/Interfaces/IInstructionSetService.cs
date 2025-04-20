namespace DotNetBoy.Emulator.Services.Interfaces;

public interface IInstructionSetService
{
    void NonPrefixedInstruction(byte opCode, ICpuRegistersService registers);
    void PrefixedInstruction(byte opCode, ICpuRegistersService registers);
}