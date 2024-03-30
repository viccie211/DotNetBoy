using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.BitInstructions;

public class BitAtAddressHLInstructions : BitInstructionsBase, IInstructionSet
{
    private readonly IMmuService _mmuService;
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public BitAtAddressHLInstructions(IClockService clockService, IMmuService mmuService) : base(clockService)
    {
        _mmuService = mmuService;
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x46, Bit0AtAddressHL },
            { 0x4E, Bit1AtAddressHL },
            { 0x56, Bit2AtAddressHL },
            { 0x5E, Bit3AtAddressHL },
            { 0x66, Bit4AtAddressHL },
            { 0x6E, Bit5AtAddressHL },
            { 0x76, Bit6AtAddressHL },
            { 0x7E, Bit7AtAddressHL },
        };
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
}