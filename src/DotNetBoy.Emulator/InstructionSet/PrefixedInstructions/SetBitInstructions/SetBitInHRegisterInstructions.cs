using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.SetBitInstructions;

public class SetBitInHRegisterInstructions(IClockService clockService) : SetBitInstructionsBase(clockService), IInstructionSet
{
    public void SetBit0InHRegister(ICpuRegistersService registers)
    {
        registers.H = SetBit(0, registers.H, registers);
    }

    public void SetBit1InHRegister(ICpuRegistersService registers)
    {
        registers.H = SetBit(1, registers.H, registers);
    }

    public void SetBit2InHRegister(ICpuRegistersService registers)
    {
        registers.H = SetBit(2, registers.H, registers);
    }

    public void SetBit3InHRegister(ICpuRegistersService registers)
    {
        registers.H = SetBit(3, registers.H, registers);
    }

    public void SetBit4InHRegister(ICpuRegistersService registers)
    {
        registers.H = SetBit(4, registers.H, registers);
    }

    public void SetBit5InHRegister(ICpuRegistersService registers)
    {
        registers.H = SetBit(5, registers.H, registers);
    }

    public void SetBit6InHRegister(ICpuRegistersService registers)
    {
        registers.H = SetBit(6, registers.H, registers);
    }

    public void SetBit7InHRegister(ICpuRegistersService registers)
    {
        registers.H = SetBit(7, registers.H, registers);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0xC4:
                SetBit0InHRegister(registers);
                break;
            case 0xCC:
                SetBit1InHRegister(registers);
                break;
            case 0xD4:
                SetBit2InHRegister(registers);
                break;
            case 0xDC:
                SetBit3InHRegister(registers);
                break;
            case 0xE4:
                SetBit4InHRegister(registers);
                break;
            case 0xEC:
                SetBit5InHRegister(registers);
                break;
            case 0xF4:
                SetBit6InHRegister(registers);
                break;
            case 0xFC:
                SetBit7InHRegister(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in SetBitInHRegisterInstructions.");
        }
    }

}