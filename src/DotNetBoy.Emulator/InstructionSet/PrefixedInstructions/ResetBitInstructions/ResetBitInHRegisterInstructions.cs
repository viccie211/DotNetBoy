using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;

public class ResetBitInHRegisterInstructions(IClockService clockService) : ResetBitInstructionsBase(clockService), IInstructionSet
{
    public void ResetBit0InHRegister(ICpuRegistersService registers)
    {
        registers.H = ResetBit(0, registers.H, registers);
    }

    public void ResetBit1InHRegister(ICpuRegistersService registers)
    {
        registers.H = ResetBit(1, registers.H, registers);
    }

    public void ResetBit2InHRegister(ICpuRegistersService registers)
    {
        registers.H = ResetBit(2, registers.H, registers);
    }

    public void ResetBit3InHRegister(ICpuRegistersService registers)
    {
        registers.H = ResetBit(3, registers.H, registers);
    }

    public void ResetBit4InHRegister(ICpuRegistersService registers)
    {
        registers.H = ResetBit(4, registers.H, registers);
    }

    public void ResetBit5InHRegister(ICpuRegistersService registers)
    {
        registers.H = ResetBit(5, registers.H, registers);
    }

    public void ResetBit6InHRegister(ICpuRegistersService registers)
    {
        registers.H = ResetBit(6, registers.H, registers);
    }

    public void ResetBit7InHRegister(ICpuRegistersService registers)
    {
        registers.H = ResetBit(7, registers.H, registers);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x84:
                ResetBit0InHRegister(registers);
                break;
            case 0x8C:
                ResetBit1InHRegister(registers);
                break;
            case 0x94:
                ResetBit2InHRegister(registers);
                break;
            case 0x9C:
                ResetBit3InHRegister(registers);
                break;
            case 0xA4:
                ResetBit4InHRegister(registers);
                break;
            case 0xAC:
                ResetBit5InHRegister(registers);
                break;
            case 0xB4:
                ResetBit6InHRegister(registers);
                break;
            case 0xBC:
                ResetBit7InHRegister(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in ResetBitInHRegisterInstructions.");
        }
    }

}