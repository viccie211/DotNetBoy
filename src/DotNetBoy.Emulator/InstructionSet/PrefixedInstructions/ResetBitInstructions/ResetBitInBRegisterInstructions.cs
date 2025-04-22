using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;

public class ResetBitInBRegisterInstructions(IClockService clockService) : ResetBitInstructionsBase(clockService), IInstructionSet
{
    public void ResetBit0InBRegister(ICpuRegistersService registers)
    {
        registers.B = ResetBit(0, registers.B, registers);
    }

    public void ResetBit1InBRegister(ICpuRegistersService registers)
    {
        registers.B = ResetBit(1, registers.B, registers);
    }

    public void ResetBit2InBRegister(ICpuRegistersService registers)
    {
        registers.B = ResetBit(2, registers.B, registers);
    }

    public void ResetBit3InBRegister(ICpuRegistersService registers)
    {
        registers.B = ResetBit(3, registers.B, registers);
    }

    public void ResetBit4InBRegister(ICpuRegistersService registers)
    {
        registers.B = ResetBit(4, registers.B, registers);
    }

    public void ResetBit5InBRegister(ICpuRegistersService registers)
    {
        registers.B = ResetBit(5, registers.B, registers);
    }

    public void ResetBit6InBRegister(ICpuRegistersService registers)
    {
        registers.B = ResetBit(6, registers.B, registers);
    }

    public void ResetBit7InBRegister(ICpuRegistersService registers)
    {
        registers.B = ResetBit(7, registers.B, registers);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x80:
                ResetBit0InBRegister(registers);
                break;
            case 0x88:
                ResetBit1InBRegister(registers);
                break;
            case 0x90:
                ResetBit2InBRegister(registers);
                break;
            case 0x98:
                ResetBit3InBRegister(registers);
                break;
            case 0xA0:
                ResetBit4InBRegister(registers);
                break;
            case 0xA8:
                ResetBit5InBRegister(registers);
                break;
            case 0xB0:
                ResetBit6InBRegister(registers);
                break;
            case 0xB8:
                ResetBit7InBRegister(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in ResetBitInBRegisterInstructions.");
        }
    }

}