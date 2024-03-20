using DotNetBoy.Emulator.Services.Implementations;

namespace DotNetBoy.Emulator.Tests.InstructionTests.PrefixedInstructions.RotateRightInstructions;

public class RotateCInstructionTests
{
    private ICpuRegistersService _registers;
    private InstructionSet.PrefixedInstructions.RotateRightInstructions _instructions;

    [SetUp]
    public void SetUp()
    {
        _registers = new TestCpuRegisterService();
        var clockServiceMock = new Mock<IClockService>();
        var mmuServiceMock = new Mock<IMmuService>();
        _instructions = new InstructionSet.PrefixedInstructions.RotateRightInstructions(clockServiceMock.Object,mmuServiceMock.Object);
    }

    [Test]
    public void RotateC_0x00()
    {
        const byte expectedC = 0x00;
        const ushort expectedProgramCounter = 0x0002;
        var expectedF = new FlagsRegister()
        {
            Zero = true,
            Carry = false,
            HalfCarry = false,
            Subtract = false,
        };
        _registers.C = 0x00;
        _instructions.RotateC(_registers);
        Assert.That(_registers.C, Is.EqualTo(expectedC));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }

    [Test]
    public void RotateC_0x00_CarryTrue()
    {
        const byte expectedC = 0x80;
        const ushort expectedProgramCounter = 0x0002;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Carry = false,
            HalfCarry = false,
            Subtract = false,
        };
        _registers.C = 0x00;
        _registers.F.Carry = true;
        _instructions.RotateC(_registers);
        Assert.That(_registers.C, Is.EqualTo(expectedC));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }

    [Test]
    public void RotateC_0x01()
    {
        const byte expectedC = 0x00;
        const ushort expectedProgramCounter = 0x0002;
        var expectedF = new FlagsRegister()
        {
            Zero = true,
            Carry = true,
            HalfCarry = false,
            Subtract = false,
        };
        _registers.C = 0x01;
        _instructions.RotateC(_registers);
        Assert.That(_registers.C, Is.EqualTo(expectedC));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }

    [Test]
    public void RotateC_0xFF()
    {
        const byte expectedC = 0x7F;
        const ushort expectedProgramCounter = 0x0002;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Carry = true,
            HalfCarry = false,
            Subtract = false,
        };
        _registers.C = 0xFF;
        _instructions.RotateC(_registers);
        Assert.That(_registers.C, Is.EqualTo(expectedC));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }

    [Test]
    public void RotateC_0xAA()
    {
        const byte expectedC = 0x55;
        const ushort expectedProgramCounter = 0x0002;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Carry = false,
            HalfCarry = false,
            Subtract = false,
        };
        _registers.C = 0xAA;
        _instructions.RotateC(_registers);
        Assert.That(_registers.C, Is.EqualTo(expectedC));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }

    [Test]
    public void RotateC_EntirelyAroundFromCarry()
    {
        _registers.C = 0x00;
        _registers.F.Carry = true;
        ushort expectedProgramCounter = 0x0000;

        for (int i = 7; i >= -1; i--)
        {
            var expectedC = (byte)(int)Math.Pow(2, i);
            var expectedF = new FlagsRegister()
            {
                Zero = i==-1,
                HalfCarry = false,
                Subtract = false,
                Carry = i==-1,
            };
            _instructions.RotateC(_registers);
            expectedProgramCounter += 2;
            Assert.That(_registers.C, Is.EqualTo(expectedC));
            Assert.That(_registers.F, Is.EqualTo(expectedF));
            Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        }
    }
}