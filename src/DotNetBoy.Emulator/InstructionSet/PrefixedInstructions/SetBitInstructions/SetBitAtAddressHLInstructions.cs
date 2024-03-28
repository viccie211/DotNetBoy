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
            { 0xC6, SetBit0InLRegister },
            { 0xCE, SetBit1InLRegister },
            { 0xD6, SetBit2InLRegister },
            { 0xDE, SetBit3InLRegister },
            { 0xE6, SetBit4InLRegister },
            { 0xEE, SetBit5InLRegister },
            { 0xF6, SetBit6InLRegister },
            { 0xFE, SetBit7InLRegister },
        };
    }

    public void SetBit0InLRegister(ICpuRegistersService registers)
    {
        SetBitAtAddressHL(0, registers);
    }

    public void SetBit1InLRegister(ICpuRegistersService registers)
    {
        SetBitAtAddressHL(1, registers);
    }

    public void SetBit2InLRegister(ICpuRegistersService registers)
    {
        SetBitAtAddressHL(2, registers);
    }

    public void SetBit3InLRegister(ICpuRegistersService registers)
    {
        SetBitAtAddressHL(3, registers);
    }

    public void SetBit4InLRegister(ICpuRegistersService registers)
    {
        SetBitAtAddressHL(4, registers);
    }

    public void SetBit5InLRegister(ICpuRegistersService registers)
    {
        SetBitAtAddressHL(5, registers);
    }

    public void SetBit6InLRegister(ICpuRegistersService registers)
    {
        SetBitAtAddressHL(6, registers);
    }

    public void SetBit7InLRegister(ICpuRegistersService registers)
    {
        SetBitAtAddressHL(7, registers);
    }

    private void SetBitAtAddressHL(int bitNumber, ICpuRegistersService registers)
    {
        var toSet = _mmuService.ReadByte(registers.HL);
        var reset = SetBit(bitNumber, toSet, registers);
        _mmuService.WriteByte(registers.HL, reset);
        ClockService.Clock();
    }
}