namespace DotNetBoy.Emulator.Tests.InstructionTests.DecrementInstructionTests;

public class DecrementBTests : DecrementTestsBase
{
    [Test]
    public void Decrement()
    {
        _registers.B = 0x02;
        const byte expectedB = 0x01;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = true,
            HalfCarry = false,
            Carry = false
        };
        const ushort expectedProgramCounter = 0x0001;

        PerformTest(expectedB, expectedProgramCounter, expectedFlagsRegister);
    }

    [Test]
    public void ToZero()
    {
        _registers.B = 1;
        const int expectedB = 0;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = true,
            Subtract = true,
            HalfCarry = false,
            Carry = false
        };
        const ushort expectedProgramCounter = 0x0001;

        PerformTest(expectedB, expectedProgramCounter, expectedFlagsRegister);
    }

    [Test]
    public void HalfCarry()
    {
        _registers.B = 0x10;
        const int expectedB = 0x0F;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = true,
            HalfCarry = true,
            Carry = false
        };
        const ushort expectedProgramCounter = 0x0001;

        PerformTest(expectedB, expectedProgramCounter, expectedFlagsRegister);
    }

    [Test]
    public void Underflow()
    {
        _registers.B = 0x00;
        const int expectedB = 0xFF;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = true,
            HalfCarry = true,
            Carry = false
        };
        const ushort expectedProgramCounter = 0x0001;

        PerformTest(expectedB, expectedProgramCounter, expectedFlagsRegister);
    }

    private void PerformTest(byte expectedB, ushort expectedProgramCounter, FlagsRegister expectedFlagsRegister)
    {
        _instructions.DecrementB(_registers!);
        Assert.That(_registers.B, Is.EqualTo(expectedB));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
}