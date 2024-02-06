namespace DotNetBoy.Emulator.Tests.InstructionTests.IncrementInstructionTests;

public class IncrementHTests : IncrementInstructionTestsBase
{
    [Test]
    public void Increment()
    {
        _registers.H = 0x00;
        const byte expectedH = 0x01;
        const ushort expectedProgramCounter = 0x0001;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            HalfCarry = false,
            Carry = false,
        };
        
        PerformTest(expectedH,expectedProgramCounter,expectedFlagsRegister);
    }

    [Test]
    public void HalfCarry()
    {
        _registers.H = 0x0F;
        const byte expectedH = 0x10;
        const ushort expectedProgramCounter = 0x0001;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            HalfCarry = true,
            Carry = false,
        };
        
        PerformTest(expectedH,expectedProgramCounter,expectedFlagsRegister);
    }

    [Test]
    public void OverFlow()
    {
        _registers.H = 0xFF;
        const byte expectedH = 0x00;
        const ushort expectedProgramCounter = 0x0001;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = true,
            Subtract = false,
            HalfCarry = true,
            Carry = false,
        };
        
        PerformTest(expectedH,expectedProgramCounter,expectedFlagsRegister);
    }


    private void PerformTest(byte expectedH, ushort expectedProgramCounter, FlagsRegister expectedFlagsRegister)
    {
        _instructions.IncrementH(_registers);
        Assert.That(_registers.H, Is.EqualTo(expectedH));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
    }
}