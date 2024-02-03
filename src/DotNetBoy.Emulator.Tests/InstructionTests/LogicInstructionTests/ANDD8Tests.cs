namespace DotNetBoy.Emulator.Tests.InstructionTests.LogicInstructionTests;

public class ANDD8Tests : LogicInstructionsTestsBase
{
    [Test]
    public void WithA_D80x00_A0x00()
    {
        const byte expectedA = 0x00;
        const ushort expectedProgramCounter = 0x0002;
        var expectedF = new FlagsRegister()
        {
            Zero = true,
            Subtract = false,
            HalfCarry = true,
            Carry = false,
        };
        _registers.A = 0;
        
        PerformTest(expectedA, expectedProgramCounter, expectedF);
    }

    [Test]
    public void WithA_D80x00_A0xFF()
    {
        const byte expectedA = 0x00;
        const ushort expectedProgramCounter = 0x0002;
        var expectedF = new FlagsRegister()
        {
            Zero = true,
            Subtract = false,
            HalfCarry = true,
            Carry = false,
        };
        _registers.A = 0xFF;

        PerformTest(expectedA, expectedProgramCounter, expectedF);
    }

    [Test]
    public void WithA_D80xFF_A0x00()
    {
        const byte expectedA = 0x00;
        const ushort expectedProgramCounter = 0x0042;
        var expectedF = new FlagsRegister()
        {
            Zero = true,
            Subtract = false,
            HalfCarry = true,
            Carry = false,
        };
        _registers.ProgramCounter = 0x0040;
        _registers.A = 0x00;

        PerformTest(expectedA, expectedProgramCounter, expectedF);
    }

    [Test]
    public void WithA_D80xFF_AxFF()
    {
        const byte expectedA = 0xFF;
        const ushort expectedProgramCounter = 0x0042;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            HalfCarry = true,
            Carry = false,
        };
        _registers.ProgramCounter = 0x0040;
        _registers.A = 0xFF;

        PerformTest(expectedA, expectedProgramCounter, expectedF);
    }

    private void PerformTest(byte expectedA, ushort expectedProgramCounter, FlagsRegister expectedF)
    {
        _instructions.ANDD8WithA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }
}