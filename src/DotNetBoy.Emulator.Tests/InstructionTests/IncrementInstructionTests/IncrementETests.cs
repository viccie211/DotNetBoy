namespace DotNetBoy.Emulator.Tests.InstructionTests.IncrementInstructionTests;

public class IncrementETests : IncrementInstructionTestsBase
{
    [Test]
    public void Increment()
    {
        _registers.E = 0x00;
        const byte expectedE = 0x01;
        const ushort expectedProgramCounter = 0x0001;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            HalfCarry = false,
            Carry = false,
        };
        
        PerformTest(expectedE,expectedProgramCounter,expectedFlagsRegister);
    }

    [Test]
    public void HalfCarry()
    {
        _registers.E = 0x0F;
        const byte expectedE = 0x10;
        const ushort expectedProgramCounter = 0x0001;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            HalfCarry = true,
            Carry = false,
        };
        
        PerformTest(expectedE,expectedProgramCounter,expectedFlagsRegister);
    }

    [Test]
    public void OverFlow()
    {
        _registers.E = 0xFF;
        const byte expectedE = 0x00;
        const ushort expectedProgramCounter = 0x0001;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = true,
            Subtract = false,
            HalfCarry = true,
            Carry = false,
        };
        
        PerformTest(expectedE,expectedProgramCounter,expectedFlagsRegister);
    }


    private void PerformTest(byte expectedE, ushort expectedProgramCounter, FlagsRegister expectedFlagsRegister)
    {
        _instructions.IncrementE(_registers);
        Assert.That(_registers.E, Is.EqualTo(expectedE));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
    }
}