namespace DotNetBoy.Emulator.Tests.InstructionTests.DecrementInstructionTests;

public class DecrementHTests : DecrementTestsBase
{
    [Test]
    public void Decrement()
    {
        _registers.H = 0x02;
        const byte expectedH = 0x01;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = true,
            HalfCarry = false,
            Carry = false
        };
        const ushort expectedProgramCounter = 0x0001;

        PerformTest(expectedH, expectedProgramCounter, expectedFlagsRegister);
    }

    [Test]
    public void ToZero()
    {
        _registers.H = 1;
        const int expectedH = 0;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = true,
            Subtract = true,
            HalfCarry = false,
            Carry = false
        };
        const ushort expectedProgramCounter = 0x0001;

        PerformTest(expectedH, expectedProgramCounter, expectedFlagsRegister);
    }

    [Test]
    public void HalfCarry()
    {
        _registers.H = 0x10;
        const int expectedH = 0x0F;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = true,
            HalfCarry = true,
            Carry = false
        };
        const ushort expectedProgramCounter = 0x0001;

        PerformTest(expectedH, expectedProgramCounter, expectedFlagsRegister);
    }

    [Test]
    public void Underflow()
    {
        _registers.H = 0x00;
        const int expectedH = 0xFF;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = true,
            HalfCarry = true,
            Carry = false
        };
        const ushort expectedProgramCounter = 0x0001;

        PerformTest(expectedH, expectedProgramCounter, expectedFlagsRegister);
    }

    private void PerformTest(byte expectedH, ushort expectedProgramCounter, FlagsRegister expectedFlagsRegister)
    {
        _instructions.DecrementH(_registers!);
        Assert.That(_registers.H, Is.EqualTo(expectedH));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
}