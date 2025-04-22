using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.SetBitInstructions;

public class SetBitInLRegisterInstructions(IClockService clockService) : SetBitInstructionsBase(clockService), IInstructionSet
{
    public void SetBit0InLRegister(ICpuRegistersService registers)
    {
        registers.L = SetBit(0, registers.L, registers);
    }

    public void SetBit1InLRegister(ICpuRegistersService registers)
    {
        registers.L = SetBit(1, registers.L, registers);
    }

    public void SetBit2InLRegister(ICpuRegistersService registers)
    {
        registers.L = SetBit(2, registers.L, registers);
    }

    public void SetBit3InLRegister(ICpuRegistersService registers)
    {
        registers.L = SetBit(3, registers.L, registers);
    }

    public void SetBit4InLRegister(ICpuRegistersService registers)
    {
        registers.L = SetBit(4, registers.L, registers);
    }

    public void SetBit5InLRegister(ICpuRegistersService registers)
    {
        registers.L = SetBit(5, registers.L, registers);
    }

    public void SetBit6InLRegister(ICpuRegistersService registers)
    {
        registers.L = SetBit(6, registers.L, registers);
    }

    public void SetBit7InLRegister(ICpuRegistersService registers)
    {
        registers.L = SetBit(7, registers.L, registers);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0xC5:
                SetBit0InLRegister(registers);
                break;
            case 0xCD:
                SetBit1InLRegister(registers);
                break;
            case 0xD5:
                SetBit2InLRegister(registers);
                break;
            case 0xDD:
                SetBit3InLRegister(registers);
                break;
            case 0xE5:
                SetBit4InLRegister(registers);
                break;
            case 0xED:
                SetBit5InLRegister(registers);
                break;
            case 0xF5:
                SetBit6InLRegister(registers);
                break;
            case 0xFD:
                SetBit7InLRegister(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in SetBitInLRegisterInstructions.");
        }
    }

}