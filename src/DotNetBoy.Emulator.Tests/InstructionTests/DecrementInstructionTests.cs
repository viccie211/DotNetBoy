#nullable disable
using DotNetBoy.Emulator.InstructionSet;

namespace DotNetBoy.Emulator.Tests.InstructionTests;

public class DecrementInstructionTests
{
    private DecrementInstructions _instructions;
    private ICpuRegistersService _registers;

    [SetUp]
    public void SetUp()
    {
        var clockServiceMock = new Mock<ClockService>();
        _instructions = new DecrementInstructions(clockServiceMock.Object);

        _registers = new TestCpuRegisterService
        {
            B = 0x00,
            ProgramCounter = 0x0000,
            F = new FlagsRegister()
        };
    }

    #region Decrement 8 bit register

    [Test]
    public void DecrementB()
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

        _instructions.DecrementB(_registers!);
        Assert.That(_registers.B, Is.EqualTo(expectedB));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void DecrementBToZero()
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

        _instructions.DecrementB(_registers!);
        Assert.That(_registers.B, Is.EqualTo(expectedB));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void DecrementBHalfCarry()
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

        _instructions.DecrementB(_registers!);
        Assert.That(_registers.B, Is.EqualTo(expectedB));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void DecrementBUnderflow()
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

        _instructions.DecrementB(_registers!);
        Assert.That(_registers.B, Is.EqualTo(expectedB));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void DecrementC()
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

        _instructions.DecrementC(_registers!);
        Assert.That(_registers.C, Is.EqualTo(expectedC));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void DecrementCToZero()
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

        _instructions.DecrementC(_registers!);
        Assert.That(_registers.C, Is.EqualTo(expectedC));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void DecrementCHalfCarry()
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

        _instructions.DecrementC(_registers!);
        Assert.That(_registers.C, Is.EqualTo(expectedC));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void DecrementCUnderflow()
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

        _instructions.DecrementC(_registers!);
        Assert.That(_registers.C, Is.EqualTo(expectedC));
        Assert.That(_registers.F, Is.EqualTo(expectedFlagsRegister));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    #endregion
}