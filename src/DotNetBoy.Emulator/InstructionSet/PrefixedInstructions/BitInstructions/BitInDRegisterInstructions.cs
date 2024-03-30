using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.BitInstructions;

public class BitInDRegisterInstructions : BitInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public BitInDRegisterInstructions(IClockService clockService) : base(clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x42, Bit0InDRegister },
            { 0x4A, Bit1InDRegister },
            { 0x52, Bit2InDRegister },
            { 0x5A, Bit3InDRegister },
            { 0x62, Bit4InDRegister },
            { 0x6A, Bit5InDRegister },
            { 0x72, Bit6InDRegister },
            { 0x7A, Bit7InDRegister },
        };
    }

    public void Bit0InDRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(0, registers.D, registers);
    }

    public void Bit1InDRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(1, registers.D, registers);
    }

    public void Bit2InDRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(2, registers.D, registers);
    }

    public void Bit3InDRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(3, registers.D, registers);
    }

    public void Bit4InDRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(4, registers.D, registers);
    }

    public void Bit5InDRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(5, registers.D, registers);
    }

    public void Bit6InDRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(6, registers.D, registers);
    }

    public void Bit7InDRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(7, registers.D, registers);
    }
}