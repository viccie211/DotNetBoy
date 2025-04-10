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
            { 0x86, ResetBit0AtAddressInHLRegister },
            { 0x8E, ResetBit1AtAddressInHLRegister },
            { 0x96, ResetBit2AtAddressInHLRegister },
            { 0x9E, ResetBit3AtAddressInHLRegister },
            { 0xA6, ResetBit4AtAddressInHLRegister },
            { 0xAE, ResetBit5AtAddressInHLRegister },
            { 0xB6, ResetBit6AtAddressInHLRegister },
            { 0xBE, ResetBit7AtAddressInHLRegister },
        };
    }

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
        var toReset = _mmuService.ReadByte(registers.HL);
        var reset = ResetBit(bitNumber, toReset, registers);
        _mmuService.WriteByte(registers.HL, reset);
        ClockService.Clock(2);
    }
}