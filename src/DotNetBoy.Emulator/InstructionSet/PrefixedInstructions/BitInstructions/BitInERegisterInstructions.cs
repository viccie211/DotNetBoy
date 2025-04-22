using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.BitInstructions;

public class BitInERegisterInstructions(IClockService clockService) : BitInstructionsBase(clockService), IInstructionSet
{
    public void Bit0InERegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(0, registers.E, registers);
    }

    public void Bit1InERegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(1, registers.E, registers);
    }

    public void Bit2InERegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(2, registers.E, registers);
    }

    public void Bit3InERegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(3, registers.E, registers);
    }

    public void Bit4InERegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(4, registers.E, registers);
    }

    public void Bit5InERegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(5, registers.E, registers);
    }

    public void Bit6InERegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(6, registers.E, registers);
    }

    public void Bit7InERegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(7, registers.E, registers);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x43:
                Bit0InERegister(registers);
                break;
            case 0x4B:
                Bit1InERegister(registers);
                break;
            case 0x53:
                Bit2InERegister(registers);
                break;
            case 0x5B:
                Bit3InERegister(registers);
                break;
            case 0x63:
                Bit4InERegister(registers);
                break;
            case 0x6B:
                Bit5InERegister(registers);
                break;
            case 0x73:
                Bit6InERegister(registers);
                break;
            case 0x7B:
                Bit7InERegister(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in BitInERegisterInstructions.");
        }
    }
}