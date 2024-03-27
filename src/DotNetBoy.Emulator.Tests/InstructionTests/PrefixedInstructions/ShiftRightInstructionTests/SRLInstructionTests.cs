using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions;

namespace DotNetBoy.Emulator.Tests.InstructionTests.PrefixedInstructions.ShiftRightInstructionTests;

public class SRLInstructionTests
{
    private ICpuRegistersService _registers;
    private ShiftRightInstructions _instructions;

    [SetUp]
    public void SetUp()
    {
        _registers = new TestCpuRegisterService();
        var clockServiceMock = new Mock<IClockService>();
        var mmuServiceMock = new Mock<IMmuService>();
        _instructions = new ShiftRightInstructions(clockServiceMock.Object, mmuServiceMock.Object);
    }

    [Test]
    public void SRLB_0x00()
    {
        const byte expectedB = 0x00;
        const ushort expectedProgramCounter = 0x0002;
        var expectedF = new FlagsRegister()
        {
            Zero = true,
            Carry = false,
            HalfCarry = false,
            Subtract = false,
        };
        _registers.B = 0x00;
        _instructions.SRLB(_registers);
        Assert.That(_registers.B, Is.EqualTo(expectedB));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }

    [Test]
    public void SRLB_0x01()
    {
        const byte expectedB = 0x00;
        const ushort expectedProgramCounter = 0x0002;
        var expectedF = new FlagsRegister()
        {
            Zero = true,
            Carry = true,
            HalfCarry = false,
            Subtract = false,
        };
        _registers.B = 0x01;
        _instructions.SRLB(_registers);
        Assert.That(_registers.B, Is.EqualTo(expectedB));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }

    [Test]
    public void SRLB_0xFF()
    {
        const byte expectedB = 0x7F;
        const ushort expectedProgramCounter = 0x0002;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Carry = true,
            HalfCarry = false,
            Subtract = false,
        };
        _registers.B = 0xFF;
        _instructions.SRLB(_registers);
        Assert.That(_registers.B, Is.EqualTo(expectedB));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }

    [Test]
    public void SRLB_0xAA()
    {
        const byte expectedB = 0x55;
        const ushort expectedProgramCounter = 0x0002;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Carry = false,
            HalfCarry = false,
            Subtract = false,
        };
        _registers.B = 0xAA;
        _instructions.SRLB(_registers);
        Assert.That(_registers.B, Is.EqualTo(expectedB));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }
}