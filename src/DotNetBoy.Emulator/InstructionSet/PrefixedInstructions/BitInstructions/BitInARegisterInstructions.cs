using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.BitInstructions;

public class BitInARegisterInstructions(IClockService clockService) : BitInstructionsBase(clockService), IInstructionSet
{
    public void Bit0InARegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(0, registers.A, registers);
    }

    public void Bit1InARegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(1, registers.A, registers);
    }

    public void Bit2InARegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(2, registers.A, registers);
    }

    public void Bit3InARegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(3, registers.A, registers);
    }

    public void Bit4InARegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(4, registers.A, registers);
    }

    public void Bit5InARegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(5, registers.A, registers);
    }

    public void Bit6InARegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(6, registers.A, registers);
    }

    public void Bit7InARegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(7, registers.A, registers);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x47:
                Bit0InARegister(registers);
                break;
            case 0x4F:
                Bit1InARegister(registers);
                break;
            case 0x57:
                Bit2InARegister(registers);
                break;
            case 0x5F:
                Bit3InARegister(registers);
                break;
            case 0x67:
                Bit4InARegister(registers);
                break;
            case 0x6F:
                Bit5InARegister(registers);
                break;
            case 0x77:
                Bit6InARegister(registers);
                break;
            case 0x7F:
                Bit7InARegister(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in BitInARegisterInstructions.");
        }
    }

}