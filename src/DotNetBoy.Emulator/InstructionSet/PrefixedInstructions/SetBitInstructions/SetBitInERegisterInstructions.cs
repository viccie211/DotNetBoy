using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.SetBitInstructions;

public class SetBitInERegisterInstructions(IClockService clockService) : SetBitInstructionsBase(clockService), IInstructionSet
{
    public void SetBit0InERegister(ICpuRegistersService registers)
    {
        registers.E = SetBit(0, registers.E, registers);
    }

    public void SetBit1InERegister(ICpuRegistersService registers)
    {
        registers.E = SetBit(1, registers.E, registers);
    }

    public void SetBit2InERegister(ICpuRegistersService registers)
    {
        registers.E = SetBit(2, registers.E, registers);
    }

    public void SetBit3InERegister(ICpuRegistersService registers)
    {
        registers.E = SetBit(3, registers.E, registers);
    }

    public void SetBit4InERegister(ICpuRegistersService registers)
    {
        registers.E = SetBit(4, registers.E, registers);
    }

    public void SetBit5InERegister(ICpuRegistersService registers)
    {
        registers.E = SetBit(5, registers.E, registers);
    }

    public void SetBit6InERegister(ICpuRegistersService registers)
    {
        registers.E = SetBit(6, registers.E, registers);
    }

    public void SetBit7InERegister(ICpuRegistersService registers)
    {
        registers.E = SetBit(7, registers.E, registers);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0xC3:
                SetBit0InERegister(registers);
                break;
            case 0xCB:
                SetBit1InERegister(registers);
                break;
            case 0xD3:
                SetBit2InERegister(registers);
                break;
            case 0xDB:
                SetBit3InERegister(registers);
                break;
            case 0xE3:
                SetBit4InERegister(registers);
                break;
            case 0xEB:
                SetBit5InERegister(registers);
                break;
            case 0xF3:
                SetBit6InERegister(registers);
                break;
            case 0xFB:
                SetBit7InERegister(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in SetBitInERegisterInstructions.");
        }
    }

}