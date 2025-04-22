using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.BitInstructions;

public class BitInHRegisterInstructions(IClockService clockService) : BitInstructionsBase(clockService), IInstructionSet
{
    public void Bit0InHRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(0, registers.H, registers);
    }

    public void Bit1InHRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(1, registers.H, registers);
    }

    public void Bit2InHRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(2, registers.H, registers);
    }

    public void Bit3InHRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(3, registers.H, registers);
    }

    public void Bit4InHRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(4, registers.H, registers);
    }

    public void Bit5InHRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(5, registers.H, registers);
    }

    public void Bit6InHRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(6, registers.H, registers);
    }

    public void Bit7InHRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(7, registers.H, registers);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x44:
                Bit0InHRegister(registers);
                break;
            case 0x4C:
                Bit1InHRegister(registers);
                break;
            case 0x54:
                Bit2InHRegister(registers);
                break;
            case 0x5C:
                Bit3InHRegister(registers);
                break;
            case 0x64:
                Bit4InHRegister(registers);
                break;
            case 0x6C:
                Bit5InHRegister(registers);
                break;
            case 0x74:
                Bit6InHRegister(registers);
                break;
            case 0x7C:
                Bit7InHRegister(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in BitInHRegisterInstructions.");
        }
    }
}