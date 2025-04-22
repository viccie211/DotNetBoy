using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.BitInstructions;

public class BitInDRegisterInstructions(IClockService clockService) : BitInstructionsBase(clockService), IInstructionSet
{
    public void Bit0InDRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(0, registers.D, registers);
    }

    public void Bit1InDRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(1, registers.D, registers);
    }

    public void Bit2InDRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(2, registers.D, registers);
    }

    public void Bit3InDRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(3, registers.D, registers);
    }

    public void Bit4InDRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(4, registers.D, registers);
    }

    public void Bit5InDRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(5, registers.D, registers);
    }

    public void Bit6InDRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(6, registers.D, registers);
    }

    public void Bit7InDRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(7, registers.D, registers);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x42:
                Bit0InDRegister(registers);
                break;
            case 0x4A:
                Bit1InDRegister(registers);
                break;
            case 0x52:
                Bit2InDRegister(registers);
                break;
            case 0x5A:
                Bit3InDRegister(registers);
                break;
            case 0x62:
                Bit4InDRegister(registers);
                break;
            case 0x6A:
                Bit5InDRegister(registers);
                break;
            case 0x72:
                Bit6InDRegister(registers);
                break;
            case 0x7A:
                Bit7InDRegister(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in BitInDRegisterInstructions.");
        }
    }
}