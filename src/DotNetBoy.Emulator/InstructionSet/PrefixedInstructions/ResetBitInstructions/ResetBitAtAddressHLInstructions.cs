using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;

public class ResetBitAtAddressHLInstructions : ResetBitInstructionsBase, IInstructionSet
{
    private readonly IMmuService _mmuService;
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public ResetBitAtAddressHLInstructions(IClockService clockService, IMmuService mmuService) : base(clockService)
    {
        _mmuService = mmuService;
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x86, ResetBit0InLRegister },
            { 0x8E, ResetBit1InLRegister },
            { 0x96, ResetBit2InLRegister },
            { 0x9E, ResetBit3InLRegister },
            { 0xA6, ResetBit4InLRegister },
            { 0xAE, ResetBit5InLRegister },
            { 0xB6, ResetBit6InLRegister },
            { 0xBE, ResetBit7InLRegister },
        };
    }

    public void ResetBit0InLRegister(ICpuRegistersService registers)
    {
        ResetBitAtAddressHL(0, registers);
    }

    public void ResetBit1InLRegister(ICpuRegistersService registers)
    {
        ResetBitAtAddressHL(1, registers);
    }

    public void ResetBit2InLRegister(ICpuRegistersService registers)
    {
        ResetBitAtAddressHL(2, registers);
    }

    public void ResetBit3InLRegister(ICpuRegistersService registers)
    {
        ResetBitAtAddressHL(3, registers);
    }

    public void ResetBit4InLRegister(ICpuRegistersService registers)
    {
        ResetBitAtAddressHL(4, registers);
    }

    public void ResetBit5InLRegister(ICpuRegistersService registers)
    {
        ResetBitAtAddressHL(5, registers);
    }

    public void ResetBit6InLRegister(ICpuRegistersService registers)
    {
        ResetBitAtAddressHL(6, registers);
    }

    public void ResetBit7InLRegister(ICpuRegistersService registers)
    {
        ResetBitAtAddressHL(7, registers);
    }

    private void ResetBitAtAddressHL(int bitNumber, ICpuRegistersService registers)
    {
        var toReset = _mmuService.ReadByte(registers.HL);
        var reset = ResetBit(bitNumber, toReset, registers);
        _mmuService.WriteByte(registers.HL, reset);
        ClockService.Clock();
    }
}