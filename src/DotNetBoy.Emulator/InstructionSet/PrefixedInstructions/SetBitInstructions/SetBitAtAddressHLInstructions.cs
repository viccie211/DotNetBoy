using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.SetBitInstructions;

public class SetBitAtAddressHLInstructions(IClockService clockService, IMmuService mmuService) : SetBitInstructionsBase(clockService), IInstructionSet
{
    public void SetBit0AtAddressInHLRegister(ICpuRegistersService registers)
    {
        SetBitAtAddressHL(0, registers);
    }

    public void SetBit1AtAddressInHLRegister(ICpuRegistersService registers)
    {
        SetBitAtAddressHL(1, registers);
    }

    public void SetBit2AtAddressInHLRegister(ICpuRegistersService registers)
    {
        SetBitAtAddressHL(2, registers);
    }

    public void SetBit3AtAddressInHLRegister(ICpuRegistersService registers)
    {
        SetBitAtAddressHL(3, registers);
    }

    public void SetBit4AtAddressInHLRegister(ICpuRegistersService registers)
    {
        SetBitAtAddressHL(4, registers);
    }

    public void SetBit5AtAddressInHLRegister(ICpuRegistersService registers)
    {
        SetBitAtAddressHL(5, registers);
    }

    public void SetBit6AtAddressInHLRegister(ICpuRegistersService registers)
    {
        SetBitAtAddressHL(6, registers);
    }

    public void SetBit7AtAddressInHLRegister(ICpuRegistersService registers)
    {
        SetBitAtAddressHL(7, registers);
    }

    private void SetBitAtAddressHL(int bitNumber, ICpuRegistersService registers)
    {
        var toSet = mmuService.ReadByte(registers.HL);
        var reset = SetBit(bitNumber, toSet, registers);
        mmuService.WriteByte(registers.HL, reset);
        ClockService.Clock(2);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0xC6:
                SetBit0AtAddressInHLRegister(registers);
                break;
            case 0xCE:
                SetBit1AtAddressInHLRegister(registers);
                break;
            case 0xD6:
                SetBit2AtAddressInHLRegister(registers);
                break;
            case 0xDE:
                SetBit3AtAddressInHLRegister(registers);
                break;
            case 0xE6:
                SetBit4AtAddressInHLRegister(registers);
                break;
            case 0xEE:
                SetBit5AtAddressInHLRegister(registers);
                break;
            case 0xF6:
                SetBit6AtAddressInHLRegister(registers);
                break;
            case 0xFE:
                SetBit7AtAddressInHLRegister(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in SetBitAtAddressHLInstructions.");
        }
    }

}