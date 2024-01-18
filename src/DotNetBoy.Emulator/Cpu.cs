using DotNetBoy.Emulator.InstructionSet;
using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator;

public class Cpu
{
    internal const byte INSTRUCTION_PREFIX = 0xCB;

    internal readonly IByteUshortService ByteUshortService;
    public IMmuService MmuService { get; init; }

    public void Loop()
    {
        while (!_cpuRegisters.Halted)
        {
            //Fetch
            var instruction = MmuService.ReadByte(_cpuRegisters.ProgramCounter);

            if (instruction == INSTRUCTION_PREFIX)
            {
                var actualInstruction = MmuService.ReadByte((ushort)(_cpuRegisters.ProgramCounter + 1));
                var decodedInstruction = _prefixedInstructions[actualInstruction] ?? throw new NotImplementedException($"Prefixed instruction {actualInstruction:X2} not impemented"); 
                decodedInstruction(_cpuRegisters);
            }
            else
            {
                var decodedInstruction = _prefixedInstructions[instruction] ?? throw new NotImplementedException($"NonPrefixed instruction {instruction:X2} not impemented"); 
                decodedInstruction(_cpuRegisters);
            }
        }
    }

    private readonly Action<CpuRegisters>?[] _nonPrefixedInstructions;
    private readonly Action<CpuRegisters>?[] _prefixedInstructions;
    private readonly CpuRegisters _cpuRegisters;

    public Cpu(IByteUshortService byteUshortService, IMmuService mmuService, CpuRegisters cpuRegisters, IList<IInstructionSet> nonPrefixedInstructions, IList<IInstructionSet> prefixedInstructions)
    {
        _cpuRegisters = cpuRegisters;
        ByteUshortService = byteUshortService;
        MmuService = mmuService;
        _nonPrefixedInstructions = new Action<CpuRegisters>[0xFF];
        _prefixedInstructions = new Action<CpuRegisters>[0xFF];

        foreach (var instructionSet in nonPrefixedInstructions)
        {
            foreach (var instruction in instructionSet.Instructions)
            {
                if (_nonPrefixedInstructions[instruction.Key] != null)
                {
                    throw new Exception($"Can't register non prefixed instruction {instruction.Key:X2} twice");
                }

                _nonPrefixedInstructions[instruction.Key] = instruction.Value;
            }
        }
        
        foreach (var instructionSet in prefixedInstructions)
        {
            foreach (var instruction in instructionSet.Instructions)
            {
                if (prefixedInstructions[instruction.Key] != null)
                {
                    throw new Exception($"Can't register prefixed instruction {instruction.Key:X2} twice");
                }
                _prefixedInstructions[instruction.Key] = instruction.Value;
            }
        }
    }
}