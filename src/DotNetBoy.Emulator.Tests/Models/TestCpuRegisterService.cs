public class TestCpuRegisterService : ICpuRegistersService
{
    public bool InterruptMasterEnable { get; set; }
    public bool InterruptsJustEnabled { get; set; }
    public byte A { get; set; }
    public byte B { get; set; }
    public byte C { get; set; }
    public byte D { get; set; }
    public byte E { get; set; }
    public FlagsRegister F { get; set; }
    public byte H { get; set; }
    public byte L { get; set; }
    public ushort AF { get; set; }
    public ushort BC { get; set; }
    public ushort DE { get; set; }
    public ushort HL { get; set; }
    public ushort ProgramCounter { get; set; }
    public ushort StackPointer { get; set; }
    public bool Halted { get; set; }
    public bool HaltBug { get; set; }

    public TestCpuRegisterService()
    {
        F = new FlagsRegister();
    }

    public void Reset()
    {
        A = 0x11;
        B = 0x00;
        C = 0x00;
        D = 0xFF;
        E = 0x56;
        F = 0xB0;
        H = 0x00;
        L = 0x0D;
        ProgramCounter = 0x0100;
        StackPointer = 0xFFFE;
        AF = 0x11F0;
        BC = 0x0000;
        DE = 0xFF56;
        HL = 0x000D;
    }
}