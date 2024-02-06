namespace DotNetBoy.Emulator.Tests.InstructionTests.IncrementInstructionTests;

public class IncrementLTests : IncrementInstructionTestsBase
{
    [Test]
    public void Increment()
    {
        _registers.L = 0x00;
        const byte expectedL = 0x01;
        const ushort expectedProgramCounter = 0x0001;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            HalfCarry = false,
            Carry = false,
        };
        
        PerformTest(expectedL,expectedProgramCounter,expectedFlagsRegister);
    }

    [Test]
    public void HalfCarry()
    {
        _registers.L = 0x0F;
        const byte expectedL = 0x10;
        const ushort expectedProgramCounter = 0x0001;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            HalfCarry = true,
            Carry = false,
        };
        
        PerformTest(expectedL,expectedProgramCounter,expectedFlagsRegister);
    }

    [Test]
    public void OverFlow()
    {
        _registers.L = 0xFF;
        const byte expectedL = 0x00;
        const ushort expectedProgramCounter = 0x0001;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = true,
            Subtract = false,
            HalfCarry = true,
            Carry = false,
        };
        
        PerformTest(expectedL,expectedProgramCounter,expectedFlagsRegister);
    }


    private void PerformTest(byte expectedL, ushort expectedProgramCounter, FlagsRegister expectedFlagsRegister)
    {
        _instructions.IncrementL(_registers);
        Assert.That(_registers.L, Is.EqualTo(expectedL));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
    }
}