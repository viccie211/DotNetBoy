#nullable disable
using DotNetBoy.Emulator.InstructionSet;

namespace DotNetBoy.Emulator.Tests.InstructionTests;

public class IncrementInstructionTests
{
    private IncrementInstructions _instructions;
    private ICpuRegistersService _registers;

    [SetUp]
    public void SetUp()
    {
        var clockServiceMock = new Mock<ClockService>();
        _instructions = new IncrementInstructions(clockServiceMock.Object);
        _registers = new TestCpuRegisterService(){
            F=new FlagsRegister(),
            B=0x00,
            BC=0x0000,
            ProgramCounter=0x0000,
            StackPointer=0x0000
        };
    }

    [Test]
    public void IncrementBC()
    {
        _registers.BC = 0x0000;
        const ushort expectedBC = 0x0001;
        const ushort expectedProgramCounter = 0x0001;
        _instructions.IncrementBC(_registers);
        Assert.That(_registers.BC, Is.EqualTo(expectedBC));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void IncrementHL()
    {
        _registers.BC = 0x0000;
        const ushort expectedHL = 0x0001;
        const ushort expectedProgramCounter = 0x0001;
        _instructions.IncrementHL(_registers);
        Assert.That(_registers.HL, Is.EqualTo(expectedHL));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void IncrementBCOverflow()
    {
        _registers.BC = 0xFFFF;
        const ushort expectedBC = 0x0000;
        const ushort expectedProgramCounter = 0x0001;
        _instructions.IncrementBC(_registers);
        Assert.That(_registers.BC, Is.EqualTo(expectedBC));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }


    #region Increment 8 bit registers

    

    
    [Test]
    public void IncrementB()
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
        _instructions.IncrementB(_registers);
        Assert.That(_registers.B, Is.EqualTo(expectedB));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
    }

    [Test]
    public void IncrementBHalfCarry()
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
        _instructions.IncrementB(_registers);
        Assert.That(_registers.B, Is.EqualTo(expectedB));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
    }
    
    [Test]
    public void IncrementBOverFlow()
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
        _instructions.IncrementB(_registers);
        Assert.That(_registers.B, Is.EqualTo(expectedB));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
    }
    
    [Test]
    public void IncrementE()
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
        _instructions.IncrementE(_registers);
        Assert.That(_registers.E, Is.EqualTo(expectedE));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
    }

    [Test]
    public void IncrementEHalfCarry()
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
        _instructions.IncrementE(_registers);
        Assert.That(_registers.E, Is.EqualTo(expectedE));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
    }
    
    [Test]
    public void IncrementEOverFlow()
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
        _instructions.IncrementE(_registers);
        Assert.That(_registers.E, Is.EqualTo(expectedE));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
    }
    
    [Test]
    public void IncrementD()
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
        _instructions.IncrementD(_registers);
        Assert.That(_registers.D, Is.EqualTo(expectedD));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
    }

    [Test]
    public void IncrementDHalfCarry()
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
        _instructions.IncrementD(_registers);
        Assert.That(_registers.D, Is.EqualTo(expectedD));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
    }
    
    [Test]
    public void IncrementDOverFlow()
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
        _instructions.IncrementD(_registers);
        Assert.That(_registers.D, Is.EqualTo(expectedD));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
    }
    #endregion

    [Test]
    public void IncrementStackPointer()
    {
        _registers.StackPointer = 0x0000;
        const ushort expectedStackPointer = 0x0001;
        const ushort expectedProgramCounter = 0x0001;
        
        _instructions.IncrementStackPointer(_registers);
        Assert.That(_registers.StackPointer, Is.EqualTo(expectedStackPointer));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void IncrementStackPointerOverflow()
    {
        _registers.StackPointer = 0xFFFF;
        const ushort expectedStackPointer = 0x0000;
        const ushort expectedProgramCounter = 0x0001;
        
        _instructions.IncrementStackPointer(_registers);
        Assert.That(_registers.StackPointer, Is.EqualTo(expectedStackPointer));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
    
}