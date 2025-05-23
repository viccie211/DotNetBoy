﻿using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;

public class ResetBitInDRegisterInstructions(IClockService clockService) : ResetBitInstructionsBase(clockService), IInstructionSet
{
    public void ResetBit0InDRegister(ICpuRegistersService registers)
    {
        registers.D = ResetBit(0, registers.D, registers);
    }

    public void ResetBit1InDRegister(ICpuRegistersService registers)
    {
        registers.D = ResetBit(1, registers.D, registers);
    }

    public void ResetBit2InDRegister(ICpuRegistersService registers)
    {
        registers.D = ResetBit(2, registers.D, registers);
    }

    public void ResetBit3InDRegister(ICpuRegistersService registers)
    {
        registers.D = ResetBit(3, registers.D, registers);
    }

    public void ResetBit4InDRegister(ICpuRegistersService registers)
    {
        registers.D = ResetBit(4, registers.D, registers);
    }

    public void ResetBit5InDRegister(ICpuRegistersService registers)
    {
        registers.D = ResetBit(5, registers.D, registers);
    }

    public void ResetBit6InDRegister(ICpuRegistersService registers)
    {
        registers.D = ResetBit(6, registers.D, registers);
    }

    public void ResetBit7InDRegister(ICpuRegistersService registers)
    {
        registers.D = ResetBit(7, registers.D, registers);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x82:
                ResetBit0InDRegister(registers);
                break;
            case 0x8A:
                ResetBit1InDRegister(registers);
                break;
            case 0x92:
                ResetBit2InDRegister(registers);
                break;
            case 0x9A:
                ResetBit3InDRegister(registers);
                break;
            case 0xA2:
                ResetBit4InDRegister(registers);
                break;
            case 0xAA:
                ResetBit5InDRegister(registers);
                break;
            case 0xB2:
                ResetBit6InDRegister(registers);
                break;
            case 0xBA:
                ResetBit7InDRegister(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in ResetBitInDRegisterInstructions.");
        }
    }
}