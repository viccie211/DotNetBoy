using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;

public class ResetBitAtAddressHLInstructions(IClockService clockService, IMmuService mmuService) : ResetBitInstructionsBase(clockService), IInstructionSet
{
    public void ResetBit0AtAddressInHLRegister(ICpuRegistersService registers)
    {
        ResetBitAtAddressHL(0, registers);
    }

    public void ResetBit1AtAddressInHLRegister(ICpuRegistersService registers)
    {
        ResetBitAtAddressHL(1, registers);
    }

    public void ResetBit2AtAddressInHLRegister(ICpuRegistersService registers)
    {
        ResetBitAtAddressHL(2, registers);
    }

    public void ResetBit3AtAddressInHLRegister(ICpuRegistersService registers)
    {
        ResetBitAtAddressHL(3, registers);
    }

    public void ResetBit4AtAddressInHLRegister(ICpuRegistersService registers)
    {
        ResetBitAtAddressHL(4, registers);
    }

    public void ResetBit5AtAddressInHLRegister(ICpuRegistersService registers)
    {
        ResetBitAtAddressHL(5, registers);
    }

    public void ResetBit6AtAddressInHLRegister(ICpuRegistersService registers)
    {
        ResetBitAtAddressHL(6, registers);
    }

    public void ResetBit7AtAddressInHLRegister(ICpuRegistersService registers)
    {
        ResetBitAtAddressHL(7, registers);
    }

    private void ResetBitAtAddressHL(int bitNumber, ICpuRegistersService registers)
    {
        var toReset = mmuService.ReadByte(registers.HL);
        var reset = ResetBit(bitNumber, toReset, registers);
        mmuService.WriteByte(registers.HL, reset);
        ClockService.Clock(2);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x86:
                ResetBit0AtAddressInHLRegister(registers);
                break;
            case 0x8E:
                ResetBit1AtAddressInHLRegister(registers);
                break;
            case 0x96:
                ResetBit2AtAddressInHLRegister(registers);
                break;
            case 0x9E:
                ResetBit3AtAddressInHLRegister(registers);
                break;
            case 0xA6:
                ResetBit4AtAddressInHLRegister(registers);
                break;
            case 0xAE:
                ResetBit5AtAddressInHLRegister(registers);
                break;
            case 0xB6:
                ResetBit6AtAddressInHLRegister(registers);
                break;
            case 0xBE:
                ResetBit7AtAddressInHLRegister(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in ResetBitAtAddressHLInstructions.");
        }
    }

}