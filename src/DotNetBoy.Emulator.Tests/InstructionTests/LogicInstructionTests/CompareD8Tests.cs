namespace DotNetBoy.Emulator.Tests.InstructionTests.LogicInstructionTests;

public class CompartD8Tests : LogicInstructionsTestsBase
{
    [Test]
    public void WithA_A0x00_D80x00()
    {
        const ushort expectedProgramCounter = 0x0002;
        var expectedF = new FlagsRegister()
        {
            Zero = true,
            Subtract = true,
            Carry = false,
            HalfCarry = false
        };
        _registers.A = 0x00;
        _registers.ProgramCounter = 0x0000;
        PerformTest(expectedProgramCounter, expectedF);
    }

    [Test]
    public void WithA_A0x00_D80x01()
    {
        const ushort expectedProgramCounter = 0x0011;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = true,
            Carry = true,
            HalfCarry = true
        };
        _registers.A = 0x00;
        _registers.ProgramCounter = 0x000F;
        PerformTest(expectedProgramCounter, expectedF);
    }

    [Test]
    public void WithA_Carry()
    {
        const ushort expectedProgramCounter = 0x0021;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = true,
            Carry = true,
            HalfCarry = false
        };
        _registers.A = 0x0F;
        _registers.ProgramCounter = 0x001F;
        PerformTest(expectedProgramCounter, expectedF);
    }

    [Test]
    public void WithA_HalfCarry()
    {
        const ushort expectedProgramCounter = 0x0031;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = true,
            Carry = false,
            HalfCarry = true
        };
        _registers.A = 0x10;
        _registers.ProgramCounter = 0x002F;
        PerformTest(expectedProgramCounter, expectedF);
    }

    private void PerformTest(ushort expectedProgramCounter, FlagsRegister expectedF)
    {
        _instructions.CompareD8WithA(_registers);
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }
}