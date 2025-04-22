using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;

public class ResetBitInARegisterInstructions(IClockService clockService) : ResetBitInstructionsBase(clockService), IInstructionSet
{
    public void ResetBit0InARegister(ICpuRegistersService registers)
    {
        registers.A = ResetBit(0, registers.A, registers);
    }

    public void ResetBit1InARegister(ICpuRegistersService registers)
    {
        registers.A = ResetBit(1, registers.A, registers);
    }

    public void ResetBit2InARegister(ICpuRegistersService registers)
    {
        registers.A = ResetBit(2, registers.A, registers);
    }

    public void ResetBit3InARegister(ICpuRegistersService registers)
    {
        registers.A = ResetBit(3, registers.A, registers);
    }

    public void ResetBit4InARegister(ICpuRegistersService registers)
    {
        registers.A = ResetBit(4, registers.A, registers);
    }

    public void ResetBit5InARegister(ICpuRegistersService registers)
    {
        registers.A = ResetBit(5, registers.A, registers);
    }

    public void ResetBit6InARegister(ICpuRegistersService registers)
    {
        registers.A = ResetBit(6, registers.A, registers);
    }

    public void ResetBit7InARegister(ICpuRegistersService registers)
    {
        registers.A = ResetBit(7, registers.A, registers);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x87:
                ResetBit0InARegister(registers);
                break;
            case 0x8F:
                ResetBit1InARegister(registers);
                break;
            case 0x97:
                ResetBit2InARegister(registers);
                break;
            case 0x9F:
                ResetBit3InARegister(registers);
                break;
            case 0xA7:
                ResetBit4InARegister(registers);
                break;
            case 0xAF:
                ResetBit5InARegister(registers);
                break;
            case 0xB7:
                ResetBit6InARegister(registers);
                break;
            case 0xBF:
                ResetBit7InARegister(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in ResetBitInARegisterInstructions.");
        }
    }

}