using DotNetBoy.Emulator.InstructionSet;
using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.Services.Implementations;

public class InstructionSetService : IInstructionSetService
{
    public InstructionSetService(JumpInstructions jumpInstructions,
        MiscellaneousInstructions miscellaneousInstructions,
        LoadInstructions loadInstructions,
        IncrementInstructions incrementInstructions,
        DecrementInstructions decrementInstructions,
        LogicInstructions logicInstructions,
        RotateAndShiftInstructions rotateAndShiftInstructions,
        StoreInstructions storeInstructions,
        PushPopInstructions pushPopInstructions,
        LoadBetweenRegistersInstructions loadBetweenRegistersInstructions,
        ArithmeticInstructions arithmeticInstructions,
        ShiftRightInstructions shiftRightInstructions,
        ResetBitInCRegisterInstructions resetBitInCRegisterInstructions,
        RotateRightInstructions rotateRightInstructions
    )
    {
        var nonPrefixedInstructions = new List<IInstructionSet>()
        {
            jumpInstructions,
            miscellaneousInstructions,
            loadInstructions,
            incrementInstructions,
            decrementInstructions,
            logicInstructions,
            rotateAndShiftInstructions,
            storeInstructions,
            pushPopInstructions,
            loadBetweenRegistersInstructions,
            arithmeticInstructions
        };

        var prefixedInstructions = new List<IInstructionSet>()
        {
            shiftRightInstructions,
            resetBitInCRegisterInstructions,
            rotateRightInstructions
        };

        NonPrefixedInstructions = new Action<ICpuRegistersService>[0x100];
        PrefixedInstructions = new Action<ICpuRegistersService>[0x100];

        foreach (var instructionSet in nonPrefixedInstructions)
        {
            foreach (var instruction in instructionSet.Instructions)
            {
                if (NonPrefixedInstructions[instruction.Key] != null)
                {
                    throw new Exception($"Can't register non prefixed instruction {instruction.Key:X2} twice");
                }

                NonPrefixedInstructions[instruction.Key] = instruction.Value;
            }
        }

        foreach (var instructionSet in prefixedInstructions)
        {
            foreach (var instruction in instructionSet.Instructions)
            {
                if (PrefixedInstructions[instruction.Key] != null)
                {
                    throw new Exception($"Can't register prefixed instruction {instruction.Key:X2} twice");
                }

                PrefixedInstructions[instruction.Key] = instruction.Value;
            }
        }
    }

    public Action<ICpuRegistersService>[] NonPrefixedInstructions { get; }
    public Action<ICpuRegistersService>[] PrefixedInstructions { get; }
}