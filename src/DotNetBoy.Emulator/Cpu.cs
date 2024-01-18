using DotNetBoy.Emulator.InstructionSet;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator;

public class Cpu
{
    internal const byte INSTRUCTION_PREFIX = 0xCB;

    internal readonly IByteUshortService ByteUshortService;
    public IMmuService MmuService { get; init; }

    public Cpu(IByteUshortService byteUshortService, IMmuService mmuService)
    {
        ByteUshortService = byteUshortService;
        MmuService = mmuService;
    }

    internal byte regA;
    internal byte regB;
    internal byte regC;
    internal byte regD;
    internal byte regE;
    internal byte regH;
    internal byte regL;
    internal byte regM;
    internal byte regT;
    internal FlagsRegister regF;
    internal bool interuptMasterEnable = false;

    #region 16 Bit Register Views

    internal ushort regAF
    {
        get => ByteUshortService.CombineBytes(regA, regF);
        set
        {
            regA = ByteUshortService.UpperByteOfSixteenBits(value);
            regF = ByteUshortService.LowerByteOfSixteenBits(value);
        }
    }

    internal ushort regBC
    {
        get => ByteUshortService.CombineBytes(regB, regC);
        set
        {
            regB = ByteUshortService.UpperByteOfSixteenBits(value);
            regC = ByteUshortService.LowerByteOfSixteenBits(value);
        }
    }

    internal ushort regDE
    {
        get => ByteUshortService.CombineBytes(regD, regE);
        set
        {
            regD = ByteUshortService.UpperByteOfSixteenBits(value);
            regE = ByteUshortService.LowerByteOfSixteenBits(value);
        }
    }

    internal ushort regHL
    {
        get => ByteUshortService.CombineBytes(regH, regL);
        set
        {
            regH = ByteUshortService.UpperByteOfSixteenBits(value);
            regL = ByteUshortService.LowerByteOfSixteenBits(value);
        }
    }

    #endregion

    internal ushort regPC { get; set; }
    internal ushort regSP { get; set; }
    internal bool halted { get; set; }

    public void Reset()
    {
        regA = 0x11;
        regB = 0;
        regC = 0;
        regD = 0xFF;
        regE = 0X56;
        regF = 0x80;
        regH = 0;
        regL = 0x0D;
        regPC = 0x100;
        regSP = 0;
        regT = 0;
        regM = 0;
    }

    public void Loop()
    {
        while (!halted)
        {
            //Fetch
            var instruction = MmuService.ReadByte(regPC);

            if (instruction == INSTRUCTION_PREFIX)
            {
                var actualInstruction = MmuService.ReadByte((ushort)(regPC + 1));
                prefixedInstructions[actualInstruction](this);
            }
            else
            {
                nonPrefixedInstructions[instruction](this);
            }
        }
    }

    public void Clock(int clockIncrement = 1)
    {
        regT = (byte)(regT + clockIncrement);
        regM = (byte)(regM + clockIncrement * 4);
    }

    internal Action<Cpu>[] nonPrefixedInstructions = InstructionSetMethods.CreateNonPrefixedInstructionSet();
    internal Action<Cpu>[] prefixedInstructions = InstructionSetMethods.CreatePrefixedInstructionSet();
}