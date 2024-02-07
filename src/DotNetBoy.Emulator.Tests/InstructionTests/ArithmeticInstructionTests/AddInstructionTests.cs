namespace DotNetBoy.Emulator.Tests.InstructionTests.ArithmeticInstructionTests;

public class AddInstructionTests : ArithmeticInstructionTestsBase
{
    [Test]
    public void AddD8ToA_D80x00_A0x00()
    {
        const byte expectedA = 0x00;
        const ushort expectedProgramCounter = 0x0002;
        var expectedF = new FlagsRegister()
        {
            Zero = true,
            Subtract = false,
            Carry = false,
            HalfCarry = false,
        };
        _registers.A = 0x00;
        _registers.ProgramCounter = 0x0000;
        _instructions.AddD8ToA(_registers);
        PerformAsserts(expectedA, expectedProgramCounter, expectedF);
    }

    [Test]
    public void AddD8ToA_D80x88_A0x00()
    {
        const byte expectedA = 0x88;
        const ushort expectedProgramCounter = 0x0003;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            Carry = false,
            HalfCarry = false,
        };
        _registers.A = 0x00;
        _registers.ProgramCounter = 0x0001;
        _instructions.AddD8ToA(_registers);
        PerformAsserts(expectedA, expectedProgramCounter, expectedF);
    }

    
    [Test]
    public void AddD8ToA_D80x00_A0x88()
    {
        const byte expectedA = 0x88;
        const ushort expectedProgramCounter = 0x0002;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            Carry = false,
            HalfCarry = false,
        };
        _registers.A = 0x88;
        _registers.ProgramCounter = 0x0000;
        _instructions.AddD8ToA(_registers);
        PerformAsserts(expectedA, expectedProgramCounter, expectedF);
    }

        [Test]
    public void AddD8ToA_D80x88_A0x88()
    {
        const byte expectedA = 0x10;
        const ushort expectedProgramCounter = 0x0003;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            Carry = true,
            HalfCarry = true,
        };
        _registers.A = 0x88;
        _registers.ProgramCounter = 0x0001;
        _instructions.AddD8ToA(_registers);
        PerformAsserts(expectedA, expectedProgramCounter, expectedF);
    }


    private void PerformAsserts(byte expectedA, ushort expectedProgramCounter, FlagsRegister expectedF)
    {
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }
}