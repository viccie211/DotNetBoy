using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.BitInstructions;

public class BitInCRegisterInstructions(IClockService clockService) : BitInstructionsBase(clockService), IInstructionSet
{
    public void Bit0InCRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(0, registers.C, registers);
    }

    public void Bit1InCRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(1, registers.C, registers);
    }

    public void Bit2InCRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(2, registers.C, registers);
    }

    public void Bit3InCRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(3, registers.C, registers);
    }

    public void Bit4InCRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(4, registers.C, registers);
    }

    public void Bit5InCRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(5, registers.C, registers);
    }

    public void Bit6InCRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(6, registers.C, registers);
    }

    public void Bit7InCRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(7, registers.C, registers);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x41:
                Bit0InCRegister(registers);
                break;
            case 0x49:
                Bit1InCRegister(registers);
                break;
            case 0x51:
                Bit2InCRegister(registers);
                break;
            case 0x59:
                Bit3InCRegister(registers);
                break;
            case 0x61:
                Bit4InCRegister(registers);
                break;
            case 0x69:
                Bit5InCRegister(registers);
                break;
            case 0x71:
                Bit6InCRegister(registers);
                break;
            case 0x79:
                Bit7InCRegister(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in BitInCRegisterInstructions.");
        }
    }
}

