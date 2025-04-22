using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.SetBitInstructions;

public class SetBitInARegisterInstructions(IClockService clockService) : SetBitInstructionsBase(clockService), IInstructionSet
{
    public void SetBit0InARegister(ICpuRegistersService registers)
    {
        registers.A = SetBit(0, registers.A, registers);
    }

    public void SetBit1InARegister(ICpuRegistersService registers)
    {
        registers.A = SetBit(1, registers.A, registers);
    }

    public void SetBit2InARegister(ICpuRegistersService registers)
    {
        registers.A = SetBit(2, registers.A, registers);
    }

    public void SetBit3InARegister(ICpuRegistersService registers)
    {
        registers.A = SetBit(3, registers.A, registers);
    }

    public void SetBit4InARegister(ICpuRegistersService registers)
    {
        registers.A = SetBit(4, registers.A, registers);
    }

    public void SetBit5InARegister(ICpuRegistersService registers)
    {
        registers.A = SetBit(5, registers.A, registers);
    }

    public void SetBit6InARegister(ICpuRegistersService registers)
    {
        registers.A = SetBit(6, registers.A, registers);
    }

    public void SetBit7InARegister(ICpuRegistersService registers)
    {
        registers.A = SetBit(7, registers.A, registers);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0xC7:
                SetBit0InARegister(registers);
                break;
            case 0xCF:
                SetBit1InARegister(registers);
                break;
            case 0xD7:
                SetBit2InARegister(registers);
                break;
            case 0xDF:
                SetBit3InARegister(registers);
                break;
            case 0xE7:
                SetBit4InARegister(registers);
                break;
            case 0xEF:
                SetBit5InARegister(registers);
                break;
            case 0xF7:
                SetBit6InARegister(registers);
                break;
            case 0xFF:
                SetBit7InARegister(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in SetBitInARegisterInstructions.");
        }
    }

}