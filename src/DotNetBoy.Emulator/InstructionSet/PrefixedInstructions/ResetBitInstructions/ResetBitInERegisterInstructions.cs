using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;

public class ResetBitInERegisterInstructions(IClockService clockService) : ResetBitInstructionsBase(clockService), IInstructionSet
{
    public void ResetBit0InERegister(ICpuRegistersService registers)
    {
        registers.E = ResetBit(0, registers.E, registers);
    }

    public void ResetBit1InERegister(ICpuRegistersService registers)
    {
        registers.E = ResetBit(1, registers.E, registers);
    }

    public void ResetBit2InERegister(ICpuRegistersService registers)
    {
        registers.E = ResetBit(2, registers.E, registers);
    }

    public void ResetBit3InERegister(ICpuRegistersService registers)
    {
        registers.E = ResetBit(3, registers.E, registers);
    }

    public void ResetBit4InERegister(ICpuRegistersService registers)
    {
        registers.E = ResetBit(4, registers.E, registers);
    }

    public void ResetBit5InERegister(ICpuRegistersService registers)
    {
        registers.E = ResetBit(5, registers.E, registers);
    }

    public void ResetBit6InERegister(ICpuRegistersService registers)
    {
        registers.E = ResetBit(6, registers.E, registers);
    }

    public void ResetBit7InERegister(ICpuRegistersService registers)
    {
        registers.E = ResetBit(7, registers.E, registers);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x83:
                ResetBit0InERegister(registers);
                break;
            case 0x8B:
                ResetBit1InERegister(registers);
                break;
            case 0x93:
                ResetBit2InERegister(registers);
                break;
            case 0x9B:
                ResetBit3InERegister(registers);
                break;
            case 0xA3:
                ResetBit4InERegister(registers);
                break;
            case 0xAB:
                ResetBit5InERegister(registers);
                break;
            case 0xB3:
                ResetBit6InERegister(registers);
                break;
            case 0xBB:
                ResetBit7InERegister(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in ResetBitInERegisterInstructions.");
        }
    }

}