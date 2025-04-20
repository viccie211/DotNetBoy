using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.BitInstructions;

public class BitAtAddressHLInstructions : BitInstructionsBase, IInstructionSet
{
    private readonly IMmuService _mmuService;

    public BitAtAddressHLInstructions(IClockService clockService, IMmuService mmuService) : base(clockService)
    {
        _mmuService = mmuService;
    }

    public void Bit0AtAddressHL(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(0, GetByteAtAddressHL(registers), registers);
    }

    public void Bit1AtAddressHL(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(1, GetByteAtAddressHL(registers), registers);
    }

    public void Bit2AtAddressHL(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(2, GetByteAtAddressHL(registers), registers);
    }

    public void Bit3AtAddressHL(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(3, GetByteAtAddressHL(registers), registers);
    }

    public void Bit4AtAddressHL(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(4, GetByteAtAddressHL(registers), registers);
    }

    public void Bit5AtAddressHL(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(5, GetByteAtAddressHL(registers), registers);
    }

    public void Bit6AtAddressHL(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(6, GetByteAtAddressHL(registers), registers);
    }

    public void Bit7AtAddressHL(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(7, GetByteAtAddressHL(registers), registers);
    }

    private byte GetByteAtAddressHL(ICpuRegistersService registers)
    {
        var result = _mmuService.ReadByte(registers.HL);
        ClockService.Clock();
        return result;
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x46:
                Bit0AtAddressHL(registers);
                break;
            case 0x4E:
                Bit1AtAddressHL(registers);
                break;
            case 0x56:
                Bit2AtAddressHL(registers);
                break;
            case 0x5E:
                Bit3AtAddressHL(registers);
                break;
            case 0x66:
                Bit4AtAddressHL(registers);
                break;
            case 0x6E:
                Bit5AtAddressHL(registers);
                break;
            case 0x76:
                Bit6AtAddressHL(registers);
                break;
            case 0x7E:
                Bit7AtAddressHL(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in BitAtAddressHLInstructions.");
        }
    }

}