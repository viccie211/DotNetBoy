namespace DotNetBoy.Emulator.Tests.InstructionTests.DecrementInstructionTests;

public class DecrementCTests : DecrementTestsBase
{
    [Test]
    public void Decrement()
    {
        _registers.C = 0x02;
        const byte expectedC = 0x01;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = true,
            HalfCarry = false,
            Carry = false
        };
        const ushort expectedProgramCounter = 0x0001;

        PerformTest(expectedC, expectedProgramCounter, expectedFlagsRegister);
    }

    [Test]
    public void ToZero()
    {
        _registers.C = 1;
        const int expectedC = 0;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = true,
            Subtract = true,
            HalfCarry = false,
            Carry = false
        };
        const ushort expectedProgramCounter = 0x0001;

        PerformTest(expectedC, expectedProgramCounter, expectedFlagsRegister);
    }

    [Test]
    public void HalfCarry()
    {
        _registers.C = 0x10;
        const int expectedC = 0x0F;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = true,
            HalfCarry = true,
            Carry = false
        };
        const ushort expectedProgramCounter = 0x0001;

        PerformTest(expectedC, expectedProgramCounter, expectedFlagsRegister);
    }

    [Test]
    public void Underflow()
    {
        _registers.C = 0x00;
        const int expectedC = 0xFF;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = true,
            HalfCarry = true,
            Carry = false
        };
        const ushort expectedProgramCounter = 0x0001;

        PerformTest(expectedC, expectedProgramCounter, expectedFlagsRegister);
    }

    private void PerformTest(byte expectedC, ushort expectedProgramCounter, FlagsRegister expectedFlagsRegister)
    {
        _instructions.DecrementC(_registers!);
        Assert.That(_registers.C, Is.EqualTo(expectedC));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
}