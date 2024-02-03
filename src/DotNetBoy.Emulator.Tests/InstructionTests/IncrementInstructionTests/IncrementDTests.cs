namespace DotNetBoy.Emulator.Tests.InstructionTests.IncrementInstructionTests;

public class IncrementDTests : IncrementInstructionTestsBase
{
    [Test]
    public void Increment()
    {
        _registers.D = 0x00;
        const byte expectedD = 0x01;
        const ushort expectedProgramCounter = 0x0001;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            HalfCarry = false,
            Carry = false,
        };

        PerformTest(expectedD, expectedProgramCounter, expectedFlagsRegister);
    }

    [Test]
    public void HalfCarry()
    {
        _registers.D = 0x0F;
        const byte expectedD = 0x10;
        const ushort expectedProgramCounter = 0x0001;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            HalfCarry = true,
            Carry = false,
        };

        PerformTest(expectedD, expectedProgramCounter, expectedFlagsRegister);
    }

    [Test]
    public void OverFlow()
    {
        _registers.D = 0xFF;
        const byte expectedD = 0x00;
        const ushort expectedProgramCounter = 0x0001;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = true,
            Subtract = false,
            HalfCarry = true,
            Carry = false,
        };

        PerformTest(expectedD, expectedProgramCounter, expectedFlagsRegister);
    }

    private void PerformTest(byte expectedD, ushort expectedProgramCounter, FlagsRegister expectedFlagsRegister)
    {
        _instructions.IncrementD(_registers);
        Assert.That(_registers.D, Is.EqualTo(expectedD));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
    }
}