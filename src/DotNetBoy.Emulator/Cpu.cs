using DotNetBoy.Emulator.Services.Implementations;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator;

public class Cpu(
    IMmuService mmuService,
    ICpuRegistersService cpuRegistersService,
    IInstructionSetService instructionSetService)
{
    private const byte INSTRUCTION_PREFIX = 0xCB;

    public void Loop()
    {
        while (!cpuRegistersService.Halted)
        {
            //Fetch
            var instruction = mmuService.ReadByte(cpuRegistersService.ProgramCounter);

            if (instruction == INSTRUCTION_PREFIX)
            {
                var actualInstruction = mmuService.ReadByte((ushort)(cpuRegistersService.ProgramCounter + 1));
                // Console.Write($"{cpuRegistersService.ProgramCounter:X4}: Pi: {actualInstruction:X2}");
                var decodedInstruction = instructionSetService.PrefixedInstructions[actualInstruction] ??
                                         throw new NotImplementedException(
                                             $"\nPrefixed instruction {actualInstruction:X2} not implemented");
                // Console.Write($" decoded as {decodedInstruction.Target!.GetType().Name}.{decodedInstruction.Method.Name}\n");
                decodedInstruction(cpuRegistersService);
            }
            else
            {
                // Console.Write($"{cpuRegistersService.ProgramCounter:X4}: NPi: {instruction:X2}");
                var decodedInstruction = instructionSetService.NonPrefixedInstructions[instruction] ??
                                         throw new NotImplementedException(
                                             $"\nNonPrefixed instruction {instruction:X2} not implemented");
                // Console.Write($" decoded as {decodedInstruction.Target!.GetType().Name}.{decodedInstruction.Method.Name}\n");
                decodedInstruction(cpuRegistersService);
            }

        //     if (cpuRegistersService.ProgramCounter == 0xC834)
        //     {
        //         Console.WriteLine(cpuRegistersService.HL.ToString("x4"));
        //     }
        //     // Console.WriteLine(cpuRegistersService.ToString());
        }

        Console.WriteLine("Halted");
    }
}