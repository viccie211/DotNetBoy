﻿using DotNetBoy.Emulator.InstructionSet;
using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.BitInstructions;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.RotateInstructions;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.SetBitInstructions;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ShiftInstructions;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.Services.Implementations;

public class InstructionSetService : IInstructionSetService
{
    public InstructionSetService(
        RotateInstructions rotateInstructions,
        JumpInstructions jumpInstructions,
        MiscellaneousInstructions miscellaneousInstructions,
        LoadInstructions loadInstructions,
        IncrementInstructions incrementInstructions,
        DecrementInstructions decrementInstructions,
        LogicInstructions logicInstructions,
        StoreInstructions storeInstructions,
        PushPopInstructions pushPopInstructions,
        LoadBetweenRegistersInstructions loadBetweenRegistersInstructions,
        ArithmeticInstructions arithmeticInstructions,
        ShiftRightLogicalInstructions shiftRightLogicalInstructions,
        ShiftRightArithmeticInstructions shiftRightArithmeticInstructions,
        ShiftLeftArithmeticInstructions shiftLeftArithmeticInstructions,
        BitInBRegisterInstructions bitInBRegisterInstructions,
        BitInCRegisterInstructions bitInCRegisterInstructions,
        BitInDRegisterInstructions bitInDRegisterInstructions,
        BitInERegisterInstructions bitInERegisterInstructions,
        BitInHRegisterInstructions bitInHRegisterInstructions,
        BitInLRegisterInstructions bitInLRegisterInstructions,
        BitAtAddressHLInstructions bitAtAddressHlInstructions,
        BitInARegisterInstructions bitInARegisterInstructions,
        ResetBitInBRegisterInstructions resetBitInBRegisterInstructions,
        ResetBitInCRegisterInstructions resetBitInCRegisterInstructions,
        ResetBitInDRegisterInstructions resetBitInDRegisterInstructions,
        ResetBitInERegisterInstructions resetBitInERegisterInstructions,
        ResetBitInHRegisterInstructions resetBitInHRegisterInstructions,
        ResetBitInLRegisterInstructions resetBitInLRegisterInstructions,
        ResetBitAtAddressHLInstructions resetBitAtAddressHlInstructions,
        ResetBitInARegisterInstructions resetBitInARegisterInstructions,
        SetBitInBRegisterInstructions setBitInBRegisterInstructions,
        SetBitInCRegisterInstructions setBitInCRegisterInstructions,
        SetBitInDRegisterInstructions setBitInDRegisterInstructions,
        SetBitInERegisterInstructions setBitInERegisterInstructions,
        SetBitInHRegisterInstructions setBitInHRegisterInstructions,
        SetBitInLRegisterInstructions setBitInLRegisterInstructions,
        SetBitAtAddressHLInstructions setBitAtAddressHlInstructions,
        SetBitInARegisterInstructions setBitInARegisterInstructions,
        RotateRightInstructions rotateRightInstructions,
        RotateLeftInstructions rotateLeftInstructions,
        RotateLeftThroughCarryInstructions rotateLeftThroughCarryInstructions,
        RotateRightThroughCarryInstructions rotateRightThroughCarryInstructions,
        SwapInstructions swapInstructions
    )
    {
        var nonPrefixedInstructions = new List<IInstructionSet>()
        {
            rotateInstructions,
            jumpInstructions,
            miscellaneousInstructions,
            loadInstructions,
            incrementInstructions,
            decrementInstructions,
            logicInstructions,
            storeInstructions,
            pushPopInstructions,
            loadBetweenRegistersInstructions,
            arithmeticInstructions
        };

        var prefixedInstructions = new List<IInstructionSet>()
        {
            shiftRightLogicalInstructions,
            shiftRightArithmeticInstructions,
            shiftLeftArithmeticInstructions,
            bitInBRegisterInstructions,
            bitInCRegisterInstructions,
            bitInDRegisterInstructions,
            bitInERegisterInstructions,
            bitInHRegisterInstructions,
            bitInLRegisterInstructions,
            bitAtAddressHlInstructions,
            bitInARegisterInstructions,
            resetBitInBRegisterInstructions,
            resetBitInCRegisterInstructions,
            resetBitInDRegisterInstructions,
            resetBitInERegisterInstructions,
            resetBitInHRegisterInstructions,
            resetBitInLRegisterInstructions,
            resetBitAtAddressHlInstructions,
            resetBitInARegisterInstructions,
            setBitInBRegisterInstructions,
            setBitInCRegisterInstructions,
            setBitInDRegisterInstructions,
            setBitInERegisterInstructions,
            setBitInHRegisterInstructions,
            setBitInLRegisterInstructions,
            setBitAtAddressHlInstructions,
            setBitInARegisterInstructions,
            rotateRightInstructions,
            rotateLeftInstructions,
            rotateRightThroughCarryInstructions,
            rotateLeftThroughCarryInstructions,
            swapInstructions
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