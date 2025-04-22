using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.SetBitInstructions;

public class SetBitInCRegisterInstructions(IClockService clockService) : SetBitInstructionsBase(clockService), IInstructionSet
{
    public void SetBit0InCRegister(ICpuRegistersService registers)
    {
        registers.C = SetBit(0, registers.C, registers);
    }

    public void SetBit1InCRegister(ICpuRegistersService registers)
    {
        registers.C = SetBit(1, registers.C, registers);
    }

    public void SetBit2InCRegister(ICpuRegistersService registers)
    {
        registers.C = SetBit(2, registers.C, registers);
    }

    public void SetBit3InCRegister(ICpuRegistersService registers)
    {
        registers.C = SetBit(3, registers.C, registers);
    }

    public void SetBit4InCRegister(ICpuRegistersService registers)
    {
        registers.C = SetBit(4, registers.C, registers);
    }

    public void SetBit5InCRegister(ICpuRegistersService registers)
    {
        registers.C = SetBit(5, registers.C, registers);
    }

    public void SetBit6InCRegister(ICpuRegistersService registers)
    {
        registers.C = SetBit(6, registers.C, registers);
    }

    public void SetBit7InCRegister(ICpuRegistersService registers)
    {
        registers.C = SetBit(7, registers.C, registers);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0xC1:
                SetBit0InCRegister(registers);
                break;
            case 0xC9:
                SetBit1InCRegister(registers);
                break;
            case 0xD1:
                SetBit2InCRegister(registers);
                break;
            case 0xD9:
                SetBit3InCRegister(registers);
                break;
            case 0xE1:
                SetBit4InCRegister(registers);
                break;
            case 0xE9:
                SetBit5InCRegister(registers);
                break;
            case 0xF1:
                SetBit6InCRegister(registers);
                break;
            case 0xF9:
                SetBit7InCRegister(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in SetBitInCRegisterInstructions.");
        }
    }

}