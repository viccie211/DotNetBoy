using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;

public class ResetBitInLRegisterInstructions(IClockService clockService) : ResetBitInstructionsBase(clockService), IInstructionSet
{
    public void ResetBit0InLRegister(ICpuRegistersService registers)
    {
        registers.L = ResetBit(0, registers.L, registers);
    }

    public void ResetBit1InLRegister(ICpuRegistersService registers)
    {
        registers.L = ResetBit(1, registers.L, registers);
    }

    public void ResetBit2InLRegister(ICpuRegistersService registers)
    {
        registers.L = ResetBit(2, registers.L, registers);
    }

    public void ResetBit3InLRegister(ICpuRegistersService registers)
    {
        registers.L = ResetBit(3, registers.L, registers);
    }

    public void ResetBit4InLRegister(ICpuRegistersService registers)
    {
        registers.L = ResetBit(4, registers.L, registers);
    }

    public void ResetBit5InLRegister(ICpuRegistersService registers)
    {
        registers.L = ResetBit(5, registers.L, registers);
    }

    public void ResetBit6InLRegister(ICpuRegistersService registers)
    {
        registers.L = ResetBit(6, registers.L, registers);
    }

    public void ResetBit7InLRegister(ICpuRegistersService registers)
    {
        registers.L = ResetBit(7, registers.L, registers);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x85:
                ResetBit0InLRegister(registers);
                break;
            case 0x8D:
                ResetBit1InLRegister(registers);
                break;
            case 0x95:
                ResetBit2InLRegister(registers);
                break;
            case 0x9D:
                ResetBit3InLRegister(registers);
                break;
            case 0xA5:
                ResetBit4InLRegister(registers);
                break;
            case 0xAD:
                ResetBit5InLRegister(registers);
                break;
            case 0xB5:
                ResetBit6InLRegister(registers);
                break;
            case 0xBD:
                ResetBit7InLRegister(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in ResetBitInLRegisterInstructions.");
        }
    }

}