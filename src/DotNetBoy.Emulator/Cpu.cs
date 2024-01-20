using DotNetBoy.Emulator.Services.Implementations;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator;

public class Cpu(IMmuService mmuService, CpuRegisters cpuRegisters, IInstructionSetService instructionSetService)
{
    private const byte INSTRUCTION_PREFIX = 0xCB;

    public void Loop()
    {
        while (!cpuRegisters.Halted)
        {
            //Fetch
            var instruction = mmuService.ReadByte(cpuRegisters.ProgramCounter);

            if (instruction == INSTRUCTION_PREFIX)
            {
                var actualInstruction = mmuService.ReadByte((ushort)(cpuRegisters.ProgramCounter + 1));
                Console.Write($"Prefixed instruction: {actualInstruction:X2}");
                var decodedInstruction = instructionSetService.PrefixedInstructions[actualInstruction] ?? throw new NotImplementedException($"\nPrefixed instruction {actualInstruction:X2} not impemented");
                Console.Write($" decoded as {decodedInstruction.Target!.GetType().Name}.{decodedInstruction.Method.Name}\n");
                decodedInstruction(cpuRegisters);
            }
            else
            {
                Console.Write($"NonPrefixed instruction: {instruction:X2}");
                var decodedInstruction = instructionSetService.NonPrefixedInstructions[instruction] ?? throw new NotImplementedException($"\nNonPrefixed instruction {instruction:X2} not impemented");
                Console.Write($" decoded as {decodedInstruction.Target!.GetType().Name}.{decodedInstruction.Method.Name}\n");
                decodedInstruction(cpuRegisters);
            }
        }
    }
}