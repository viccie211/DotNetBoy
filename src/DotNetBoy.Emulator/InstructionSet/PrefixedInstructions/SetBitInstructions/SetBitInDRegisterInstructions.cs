using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.SetBitInstructions;

public class SetBitInDRegisterInstructions(IClockService clockService) : SetBitInstructionsBase(clockService), IInstructionSet
{
    public void SetBit0InDRegister(ICpuRegistersService registers)
    {
        registers.D = SetBit(0, registers.D, registers);
    }

    public void SetBit1InDRegister(ICpuRegistersService registers)
    {
        registers.D = SetBit(1, registers.D, registers);
    }

    public void SetBit2InDRegister(ICpuRegistersService registers)
    {
        registers.D = SetBit(2, registers.D, registers);
    }

    public void SetBit3InDRegister(ICpuRegistersService registers)
    {
        registers.D = SetBit(3, registers.D, registers);
    }

    public void SetBit4InDRegister(ICpuRegistersService registers)
    {
        registers.D = SetBit(4, registers.D, registers);
    }

    public void SetBit5InDRegister(ICpuRegistersService registers)
    {
        registers.D = SetBit(5, registers.D, registers);
    }

    public void SetBit6InDRegister(ICpuRegistersService registers)
    {
        registers.D = SetBit(6, registers.D, registers);
    }

    public void SetBit7InDRegister(ICpuRegistersService registers)
    {
        registers.D = SetBit(7, registers.D, registers);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0xC2:
                SetBit0InDRegister(registers);
                break;
            case 0xCA:
                SetBit1InDRegister(registers);
                break;
            case 0xD2:
                SetBit2InDRegister(registers);
                break;
            case 0xDA:
                SetBit3InDRegister(registers);
                break;
            case 0xE2:
                SetBit4InDRegister(registers);
                break;
            case 0xEA:
                SetBit5InDRegister(registers);
                break;
            case 0xF2:
                SetBit6InDRegister(registers);
                break;
            case 0xFA:
                SetBit7InDRegister(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in SetBitInDRegisterInstructions.");
        }
    }
}