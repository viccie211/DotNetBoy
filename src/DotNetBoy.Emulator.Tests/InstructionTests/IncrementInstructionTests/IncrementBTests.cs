namespace DotNetBoy.Emulator.Tests.InstructionTests.IncrementInstructionTests;

public class IncrementBTests : IncrementInstructionTestsBase
{
    [Test]
    public void Increment()
    {
        _registers.B = 0x00;
        const byte expectedB = 0x01;
        const ushort expectedProgramCounter = 0x0001;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            HalfCarry = false,
            Carry = false,
        };

        PerformTest(expectedB, expectedProgramCounter, expectedFlagsRegister);
    }

    [Test]
    public void HalfCarry()
    {
        _registers.B = 0x0F;
        const byte expectedB = 0x10;
        const ushort expectedProgramCounter = 0x0001;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            HalfCarry = true,
            Carry = false,
        };

        PerformTest(expectedB, expectedProgramCounter, expectedFlagsRegister);
    }

    [Test]
    public void OverFlow()
    {
        _registers.B = 0xFF;
        const byte expectedB = 0x00;
        const ushort expectedProgramCounter = 0x0001;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = true,
            Subtract = false,
            HalfCarry = true,
            Carry = false,
        };

        PerformTest(expectedB, expectedProgramCounter, expectedFlagsRegister);
    }

    private void PerformTest(byte expectedB, ushort expectedProgramCounter, FlagsRegister expectedFlagsRegister)
    {
        _instructions.IncrementB(_registers);
        Assert.That(_registers.B, Is.EqualTo(expectedB));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
    }
}