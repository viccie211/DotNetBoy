#nullable disable
using DotNetBoy.Emulator.InstructionSet;

namespace DotNetBoy.Emulator.Tests.InstructionTests;

public class LogicInstructionsTests
{
    private LogicInstructions _instructions;
    private ICpuRegistersService _registers;

    [SetUp]
    public void SetUp()
    {
        var mmuServiceMock = new Mock<IMmuService>();
        var clockServiceMock = new Mock<IClockService>();

        _registers = new TestCpuRegisterService();

        _instructions = new LogicInstructions(mmuServiceMock.Object, clockServiceMock.Object);
    }

    [Test]
    public void ORAWithAZero()
    {
        const byte expectedA = 0x00;
        const ushort expectedProgramCounter = 0x0001;
        var expectedF = new FlagsRegister()
        {
            Zero = true,
            Subtract = false,
            Carry = false,
            HalfCarry = false
        };
        _registers.A = 0x00;

        _instructions.ORAWithA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F,Is.EqualTo(expectedF));
    }
    [Test]
    public void ORAWithA88()
    {
        const byte expectedA = 0x88;
        const ushort expectedProgramCounter = 0x0001;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            Carry = false,
            HalfCarry = false
        };
        _registers.A = 0x88;
        _instructions.ORAWithA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F,Is.EqualTo(expectedF));
    }

    [Test]
    public void ORAWithAFF()
    {
        const byte expectedA = 0xFF;
        const ushort expectedProgramCounter = 0x0001;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            Carry = false,
            HalfCarry = false
        };
        _registers.A = 0xFF;
        _instructions.ORAWithA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F,Is.EqualTo(expectedF));
    }

    [Test]
    public void XORAWithAZero()
    {
        const byte expectedA = 0x00;
        const ushort expectedProgramCounter = 0x0001;
        var expectedF = new FlagsRegister()
        {
            Zero = true,
            Subtract = false,
            Carry = false,
            HalfCarry = false
        };
        _registers.A = 0x00;

        _instructions.XORAWithA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F,Is.EqualTo(expectedF));
    }
    [Test]
    public void XORAWithA88()
    {
        const byte expectedA = 0x00;
        const ushort expectedProgramCounter = 0x0001;
        var expectedF = new FlagsRegister()
        {
            Zero = true,
            Subtract = false,
            Carry = false,
            HalfCarry = false
        };
        _registers.A = 0x88;
        _instructions.XORAWithA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F,Is.EqualTo(expectedF));
    }

    [Test]
    public void XORAWithAFF()
    {
        const byte expectedA = 0x00;
        const ushort expectedProgramCounter = 0x0001;
        var expectedF = new FlagsRegister()
        {
            Zero = true,
            Subtract = false,
            Carry = false,
            HalfCarry = false
        };
        _registers.A = 0xFF;
        _instructions.XORAWithA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F,Is.EqualTo(expectedF));
    }
}