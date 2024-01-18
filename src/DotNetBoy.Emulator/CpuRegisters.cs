using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator;

public class CpuRegisters(IByteUshortService byteUshortService)
{
    internal byte A;
    internal byte B;
    internal byte C;
    internal byte D;
    internal byte E;
    internal byte H;
    internal byte L;
    internal byte M;
    internal byte T;
    internal FlagsRegister F = 0x00;
    internal bool InteruptMasterEnable = false;

    #region 16 Bit Register Views

    internal ushort AF
    {
        get => byteUshortService.CombineBytes(A, F);
        set
        {
            A = byteUshortService.UpperByteOfSixteenBits(value);
            F = byteUshortService.LowerByteOfSixteenBits(value);
        }
    }

    internal ushort BC
    {
        get => byteUshortService.CombineBytes(B, C);
        set
        {
            B = byteUshortService.UpperByteOfSixteenBits(value);
            C = byteUshortService.LowerByteOfSixteenBits(value);
        }
    }

    internal ushort DE
    {
        get => byteUshortService.CombineBytes(D, E);
        set
        {
            D = byteUshortService.UpperByteOfSixteenBits(value);
            E = byteUshortService.LowerByteOfSixteenBits(value);
        }
    }

    internal ushort HL
    {
        get => byteUshortService.CombineBytes(H, L);
        set
        {
            H = byteUshortService.UpperByteOfSixteenBits(value);
            L = byteUshortService.LowerByteOfSixteenBits(value);
        }
    }

    #endregion

    internal ushort ProgramCounter { get; set; }
    internal ushort StackPointer { get; set; }
    internal bool Halted { get; set; }

    public void Reset()
    {
        A = 0x11;
        B = 0;
        C = 0;
        D = 0xFF;
        E = 0X56;
        F = 0x80;
        H = 0;
        L = 0x0D;
        ProgramCounter = 0x100;
        StackPointer = 0;
        T = 0;
        M = 0;
    }
    
    public void Clock(int clockIncrement = 1)
    {
        T = (byte)(T + clockIncrement);
        M = (byte)(M + clockIncrement * 4);
    }
}