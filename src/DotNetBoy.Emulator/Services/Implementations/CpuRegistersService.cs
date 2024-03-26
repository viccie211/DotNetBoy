using System.Text;
using DotNetBoy.Emulator.Models;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.Services.Implementations;

public class CpuRegistersService(IByteUshortService byteUshortService) : ICpuRegistersService
{
    public byte A { get; set; }
    public byte B { get; set; }
    public byte C { get; set; }
    public byte D { get; set; }
    public byte E { get; set; }
    public byte H { get; set; }
    public byte L { get; set; }
    public FlagsRegister F { get; set; } = 0x00;
    public bool InterruptMasterEnable { get; set; } = false;
    public bool InterruptsJustEnabled { get; set; } = false;

    #region 16 Bit Register Views

    public ushort AF
    {
        get => byteUshortService.CombineBytes(A, F);
        set
        {
            A = byteUshortService.UpperByteOfSixteenBits(value);
            F = byteUshortService.LowerByteOfSixteenBits(value);
        }
    }

    public ushort BC
    {
        get => byteUshortService.CombineBytes(B, C);
        set
        {
            B = byteUshortService.UpperByteOfSixteenBits(value);
            C = byteUshortService.LowerByteOfSixteenBits(value);
        }
    }

    public ushort DE
    {
        get => byteUshortService.CombineBytes(D, E);
        set
        {
            D = byteUshortService.UpperByteOfSixteenBits(value);
            E = byteUshortService.LowerByteOfSixteenBits(value);
        }
    }

    public ushort HL
    {
        get => byteUshortService.CombineBytes(H, L);
        set
        {
            H = byteUshortService.UpperByteOfSixteenBits(value);
            L = byteUshortService.LowerByteOfSixteenBits(value);
        }
    }

    #endregion

    public ushort ProgramCounter { get; set; }
    public ushort StackPointer { get; set; }
    public bool Halted { get; set; }

    public void Reset()
    {
        A = 0x01;
        B = 0x00;
        C = 0x13;
        D = 0x00;
        E = 0XD8;
        F = new FlagsRegister() { Zero = true, Subtract = false, HalfCarry = false, Carry = false };
        H = 0x01;
        L = 0x4D;
        ProgramCounter = 0x100;
        StackPointer = 0xFFFE;
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"AF= {AF:X4} | ");
        stringBuilder.Append($"BC= {BC:X4} | ");
        stringBuilder.Append($"DE= {DE:X4} | ");
        stringBuilder.Append($"HL= {HL:X4} | ");
        stringBuilder.Append($"SP= {StackPointer:X4} | ");
        stringBuilder.Append($"PC= {ProgramCounter:X4}| ");
        stringBuilder.Append($"F= {F}");
        return stringBuilder.ToString();
    }
}