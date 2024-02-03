namespace DotNetBoy.Emulator.Tests.InstructionTests.LogicInstructionTests;

public class ORCTests : LogicInstructionsTestsBase
{
    [Test]
    public void WithA_0x00()
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
        _registers.C = 0x00;

        PerformTest(expectedA, expectedProgramCounter, expectedF);
    }

    [Test]
    public void WithA_0x88()
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
        _registers.ProgramCounter = 0x0000;
        _registers.A = 0x88;

        PerformTest(expectedA, expectedProgramCounter, expectedF);

        expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            Carry = false,
            HalfCarry = false
        };
        _registers.ProgramCounter = 0x0000;
        _registers.A = 0x00;
        _registers.C = 0x88;

        PerformTest(expectedA, expectedProgramCounter, expectedF);

        expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            Carry = false,
            HalfCarry = false
        };
        _registers.A = 0x88;
        _registers.C = 0x00;
        _registers.ProgramCounter = 0x0000;

        PerformTest(expectedA, expectedProgramCounter, expectedF);
    }

    [Test]
    public void WithA_0xFF()
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
        _registers.ProgramCounter = 0x0000;
        _registers.A = 0xFF;
       
       PerformTest(expectedA, expectedProgramCounter, expectedF);

        expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            Carry = false,
            HalfCarry = false
        };
        _registers.ProgramCounter = 0x0000;
        _registers.A = 0x00;
        _registers.C = 0xFF;
        
        PerformTest(expectedA, expectedProgramCounter, expectedF);

        expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            Carry = false,
            HalfCarry = false
        };
        _registers.A = 0xFF;
        _registers.C = 0x00;
        _registers.ProgramCounter = 0x0000;
        
        PerformTest(expectedA, expectedProgramCounter, expectedF);
    }

    private void PerformTest(byte expectedA, ushort expectedProgramCounter, FlagsRegister expectedF)
    {
        _instructions.ORCWithA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }
}