using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.BitInstructions;

public class BitInLRegisterInstructions(IClockService clockService) : BitInstructionsBase(clockService), IInstructionSet
{
    public void Bit0InLRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(0, registers.L, registers);
    }

    public void Bit1InLRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(1, registers.L, registers);
    }

    public void Bit2InLRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(2, registers.L, registers);
    }

    public void Bit3InLRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(3, registers.L, registers);
    }

    public void Bit4InLRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(4, registers.L, registers);
    }

    public void Bit5InLRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(5, registers.L, registers);
    }

    public void Bit6InLRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(6, registers.L, registers);
    }

    public void Bit7InLRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(7, registers.L, registers);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x45:
                Bit0InLRegister(registers);
                break;
            case 0x4D:
                Bit1InLRegister(registers);
                break;
            case 0x55:
                Bit2InLRegister(registers);
                break;
            case 0x5D:
                Bit3InLRegister(registers);
                break;
            case 0x65:
                Bit4InLRegister(registers);
                break;
            case 0x6D:
                Bit5InLRegister(registers);
                break;
            case 0x75:
                Bit6InLRegister(registers);
                break;
            case 0x7D:
                Bit7InLRegister(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in BitInLRegisterInstructions.");
        }
    }
}