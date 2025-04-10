using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.SetBitInstructions;

public class SetBitAtAddressHLInstructions : SetBitInstructionsBase, IInstructionSet
{
    private readonly IMmuService _mmuService;
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public SetBitAtAddressHLInstructions(IClockService clockService, IMmuService mmuService) : base(clockService)
    {
        _mmuService = mmuService;
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0xC6, SetBit0AtAddressInHLRegister },
            { 0xCE, SetBit1AtAddressInHLRegister },
            { 0xD6, SetBit2AtAddressInHLRegister },
            { 0xDE, SetBit3AtAddressInHLRegister },
            { 0xE6, SetBit4AtAddressInHLRegister },
            { 0xEE, SetBit5AtAddressInHLRegister },
            { 0xF6, SetBit6AtAddressInHLRegister },
            { 0xFE, SetBit7AtAddressInHLRegister },
        };
    }

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
        var toSet = _mmuService.ReadByte(registers.HL);
        var reset = SetBit(bitNumber, toSet, registers);
        _mmuService.WriteByte(registers.HL, reset);
        ClockService.Clock(2);
    }
}