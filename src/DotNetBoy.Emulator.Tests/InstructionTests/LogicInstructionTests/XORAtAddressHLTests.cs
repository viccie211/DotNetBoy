namespace DotNetBoy.Emulator.Tests.InstructionTests.LogicInstructionTests;

public class XORAtAddressHLTests : LogicInstructionsTestsBase
{
    [Test]
    public void WithA_HL0x0001_A0x00()
    {
        const byte expectedA = 0x00;
        const ushort expectedProgramCounter = 0x0001;
        var expectedF = new FlagsRegister()
        {
            Zero = true,
            Subtract = false,
            Carry = false,
            HalfCarry = false
        };
        _registers.A = 0x00;
        _registers.HL = 0x0001;

        PerformTest(expectedA, expectedProgramCounter, expectedF);
    }
    
    [Test]
    public void WithA_HL0x0050_A0x00()
    {
        const byte expectedA = 0x88;
        const ushort expectedProgramCounter = 0x0001;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            Carry = false,
            HalfCarry = false
        };
        _registers.A = 0x00;
        _registers.HL = 0x0050;

        PerformTest(expectedA, expectedProgramCounter, expectedF);
    }
    [Test]
    public void WithA_HL0x0001_A0x88()
    {
        const byte expectedA = 0x88;
        const ushort expectedProgramCounter = 0x0001;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            Carry = false,
            HalfCarry = false
        };
        _registers.A = 0x88;
        _registers.HL = 0x0001;

        PerformTest(expectedA, expectedProgramCounter, expectedF);
    }
    [Test]
    public void WithA_C0x0050_A0x88()
    {
        const byte expectedA = 0x00;
        const ushort expectedProgramCounter = 0x0001;
        var expectedF = new FlagsRegister()
        {
            Zero = true,
            Subtract = false,
            Carry = false,
            HalfCarry = false
        };
        _registers.A = 0x88;
        _registers.HL = 0x0050;

        PerformTest(expectedA, expectedProgramCounter, expectedF);
    }
    
    [Test]
    public void WithA_HL0x0041_A0x00()
    {
        const byte expectedA = 0xFF;
        const ushort expectedProgramCounter = 0x0001;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            Carry = false,
            HalfCarry = false
        };
        _registers.A = 0x00;
        _registers.HL = 0x0041;

        PerformTest(expectedA, expectedProgramCounter, expectedF);
    }
    [Test]
    public void WithA_HL0x0001_A0xFF()
    {
        const byte expectedA = 0xFF;
        const ushort expectedProgramCounter = 0x0001;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            Carry = false,
            HalfCarry = false
        };
        _registers.A = 0xFF;
        _registers.HL = 0x0001;

        PerformTest(expectedA, expectedProgramCounter, expectedF);
    }
    [Test]
    public void WithA_HL0x0041_A0xFF()
    {
        const byte expectedA = 0x00;
        const ushort expectedProgramCounter = 0x0001;
        var expectedF = new FlagsRegister()
        {
            Zero = true,
            Subtract = false,
            Carry = false,
            HalfCarry = false
        };
        _registers.A = 0xFF;
        _registers.HL = 0x0041;

        PerformTest(expectedA, expectedProgramCounter, expectedF);
    }
    
    private void PerformTest(byte expectedA, ushort expectedProgramCounter, FlagsRegister expectedF)
    {
        _instructions.XORAtAddressHLWithA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }
}