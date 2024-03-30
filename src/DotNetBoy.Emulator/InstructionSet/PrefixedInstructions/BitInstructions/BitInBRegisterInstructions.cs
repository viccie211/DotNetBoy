using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.BitInstructions;

public class BitInBRegisterInstructions : BitInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public BitInBRegisterInstructions(IClockService clockService) : base(clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x40, Bit0InBRegister },
            { 0x48, Bit1InBRegister },
            { 0x50, Bit2InBRegister },
            { 0x58, Bit3InBRegister },
            { 0x60, Bit4InBRegister },
            { 0x68, Bit5InBRegister },
            { 0x70, Bit6InBRegister },
            { 0x78, Bit7InBRegister },
        };
    }

    public void Bit0InBRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(0, registers.B, registers);
    }

    public void Bit1InBRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(1, registers.B, registers);
    }

    public void Bit2InBRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(2, registers.B, registers);
    }

    public void Bit3InBRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(3, registers.B, registers);
    }

    public void Bit4InBRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(4, registers.B, registers);
    }

    public void Bit5InBRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(5, registers.B, registers);
    }

    public void Bit6InBRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(6, registers.B, registers);
    }

    public void Bit7InBRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(7, registers.B, registers);
    }
}