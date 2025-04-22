using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;

public class ResetBitInCRegisterInstructions(IClockService clockService) : ResetBitInstructionsBase(clockService), IInstructionSet
{
    public void ResetBit0InCRegister(ICpuRegistersService registers)
    {
        registers.C = ResetBit(0, registers.C, registers);
    }

    public void ResetBit1InCRegister(ICpuRegistersService registers)
    {
        registers.C = ResetBit(1, registers.C, registers);
    }

    public void ResetBit2InCRegister(ICpuRegistersService registers)
    {
        registers.C = ResetBit(2, registers.C, registers);
    }

    public void ResetBit3InCRegister(ICpuRegistersService registers)
    {
        registers.C = ResetBit(3, registers.C, registers);
    }

    public void ResetBit4InCRegister(ICpuRegistersService registers)
    {
        registers.C = ResetBit(4, registers.C, registers);
    }

    public void ResetBit5InCRegister(ICpuRegistersService registers)
    {
        registers.C = ResetBit(5, registers.C, registers);
    }

    public void ResetBit6InCRegister(ICpuRegistersService registers)
    {
        registers.C = ResetBit(6, registers.C, registers);
    }

    public void ResetBit7InCRegister(ICpuRegistersService registers)
    {
        registers.C = ResetBit(7, registers.C, registers);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x81:
                ResetBit0InCRegister(registers);
                break;
            case 0x89:
                ResetBit1InCRegister(registers);
                break;
            case 0x91:
                ResetBit2InCRegister(registers);
                break;
            case 0x99:
                ResetBit3InCRegister(registers);
                break;
            case 0xA1:
                ResetBit4InCRegister(registers);
                break;
            case 0xA9:
                ResetBit5InCRegister(registers);
                break;
            case 0xB1:
                ResetBit6InCRegister(registers);
                break;
            case 0xB9:
                ResetBit7InCRegister(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in ResetBitInCRegisterInstructions.");
        }
    }

}