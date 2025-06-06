﻿using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Implementations;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class IncrementInstructions(IClockService clockService, IMmuService mmuService) : IInstructionSet
{
    /// <summary>
    /// Increment the BC register by one. 
    /// </summary>
    /// Verified with BGB
    public void IncrementBC(ICpuRegistersService registers)
    {
        registers.BC = Increment16Bits(registers.BC, registers);
    }

    /// <summary>
    /// Increment the DE register by one. 
    /// </summary>
    /// Verified with BGB
    public void IncrementDE(ICpuRegistersService registers)
    {
        registers.DE = Increment16Bits(registers.DE, registers);
    }

    /// <summary>
    /// Increment the HL register by one. 
    /// </summary>
    /// Verified with BGB
    public void IncrementHL(ICpuRegistersService registers)
    {
        registers.HL = Increment16Bits(registers.HL, registers);
    }

    /// <summary>
    /// Increment the B register by one. Sets Z 0 H - 
    /// </summary>
    /// Verified with BGB
    public void IncrementB(ICpuRegistersService registers)
    {
        registers.B = Increment8Bits(registers.B, registers);
    }

    /// <summary>
    /// Increment the C register by one. Sets Z 0 H - 
    /// </summary>
    /// Verified with BGB
    public void IncrementC(ICpuRegistersService registers)
    {
        registers.C = Increment8Bits(registers.C, registers);
    }

    /// <summary>
    /// Increment the D register by one. Sets Z 0 H - 
    /// </summary>
    /// Verified against BGB
    public void IncrementD(ICpuRegistersService registers)
    {
        registers.D = Increment8Bits(registers.D, registers);
    }

    /// <summary>
    /// Increment the E register by one. Sets Z 0 H - 
    /// </summary>
    /// Verified against BGB
    public void IncrementE(ICpuRegistersService registers)
    {
        registers.E = Increment8Bits(registers.E, registers);
    }

    /// <summary>
    /// Increment the H register by one. Sets Z 0 H - 
    /// </summary>
    /// Verified against BGB
    public void IncrementH(ICpuRegistersService registers)
    {
        registers.H = Increment8Bits(registers.H, registers);
    }

    /// <summary>
    /// Increment the L register by one. Sets Z 0 H - 
    /// </summary>
    /// Verified against BGB
    public void IncrementL(ICpuRegistersService registers)
    {
        registers.L = Increment8Bits(registers.L, registers);
    }

    /// <summary>
    /// Increment the A register by one. Sets Z 0 H - 
    /// </summary>
    /// Verified against BGB
    public void IncrementA(ICpuRegistersService registers)
    {
        registers.A = Increment8Bits(registers.A, registers);
    }

    /// <summary>
    /// Increment the StackPointer register by one. 
    /// </summary>
    /// 
    public void IncrementStackPointer(ICpuRegistersService registers)
    {
        registers.StackPointer = Increment16Bits(registers.StackPointer, registers);
    }

    public void IncrementAtAddressHL(ICpuRegistersService registers)
    {
        var toIncrement = mmuService.ReadByte(registers.HL);
        clockService.Clock();
        var incremented = Increment8Bits(toIncrement, registers);
        mmuService.WriteByte(registers.HL, incremented);
        clockService.Clock();
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x03:
                IncrementBC(registers);
                break;
            case 0x04:
                IncrementB(registers);
                break;
            case 0x0C:
                IncrementC(registers);
                break;
            case 0x13:
                IncrementDE(registers);
                break;
            case 0x14:
                IncrementD(registers);
                break;
            case 0x1C:
                IncrementE(registers);
                break;
            case 0x23:
                IncrementHL(registers);
                break;
            case 0x24:
                IncrementH(registers);
                break;
            case 0x2C:
                IncrementL(registers);
                break;
            case 0x33:
                IncrementStackPointer(registers);
                break;
            case 0x34:
                IncrementAtAddressHL(registers);
                break;
            case 0x3C:
                IncrementA(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in IncrementInstructions.");
        }
    }


    #region private methods

    private byte Increment8Bits(byte initial, ICpuRegistersService registers)
    {
        registers.F.Subtract = false;
        registers.F.HalfCarry = InstructionUtilFunctions.HalfCarryFor8BitAddition(initial, 0x01);
        var result = (byte)(initial + 1);
        registers.F.Zero = result == 0;
        registers.ProgramCounter += 1;
        return result;
    }

    private ushort Increment16Bits(ushort initial, ICpuRegistersService registers)
    {
        var result = (ushort)(initial + 1);
        clockService.Clock();
        registers.ProgramCounter += 1;
        return result;
    }

    #endregion
}