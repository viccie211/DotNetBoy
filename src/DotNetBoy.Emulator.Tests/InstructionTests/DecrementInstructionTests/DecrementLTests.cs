namespace DotNetBoy.Emulator.Tests.InstructionTests.DecrementInstructionTests;

public class DecrementLTests : DecrementTestsBase
{
    [Test]
    public void Decrement()
    {
        _registers.L = 0x02;
        const byte expectedL = 0x01;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = true,
            HalfCarry = false,
            Carry = false
        };
        const ushort expectedProgramCounter = 0x0001;

        PerformTest(expectedL, expectedProgramCounter, expectedFlagsRegister);
    }

    [Test]
    public void ToZero()
    {
        _registers.L = 1;
        const int expectedL = 0;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = true,
            Subtract = true,
            HalfCarry = false,
            Carry = false
        };
        const ushort expectedProgramCounter = 0x0001;

        PerformTest(expectedL, expectedProgramCounter, expectedFlagsRegister);
    }

    [Test]
    public void HalfCarry()
    {
        _registers.L = 0x10;
        const int expectedL = 0x0F;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = true,
            HalfCarry = true,
            Carry = false
        };
        const ushort expectedProgramCounter = 0x0001;

        PerformTest(expectedL, expectedProgramCounter, expectedFlagsRegister);
    }

    [Test]
    public void Underflow()
    {
        _registers.L = 0x00;
        const int expectedL = 0xFF;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = true,
            HalfCarry = true,
            Carry = false
        };
        const ushort expectedProgramCounter = 0x0001;

        PerformTest(expectedL, expectedProgramCounter, expectedFlagsRegister);
    }

    private void PerformTest(byte expectedL, ushort expectedProgramCounter, FlagsRegister expectedFlagsRegister)
    {
        _instructions.DecrementL(_registers!);
        Assert.That(_registers.L, Is.EqualTo(expectedL));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
}