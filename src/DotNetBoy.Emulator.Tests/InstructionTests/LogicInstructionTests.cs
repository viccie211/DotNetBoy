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
        mmuServiceMock.Setup(m => m.ReadByte(0x0001)).Returns(0x00);
        mmuServiceMock.Setup(m => m.ReadByte(0x0010)).Returns(0x01);
        mmuServiceMock.Setup(m => m.ReadByte(0x0020)).Returns(0x10);
        mmuServiceMock.Setup(m => m.ReadByte(0x0030)).Returns(0x0F);
        mmuServiceMock.Setup(m => m.ReadByte(0x0041)).Returns(0xFF);
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
        Assert.That(_registers.F, Is.EqualTo(expectedF));
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
        Assert.That(_registers.F, Is.EqualTo(expectedF));
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
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }

    [Test]
    public void ORCWithAZero()
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
        _registers.C = 0x00;

        _instructions.ORCWithA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }
    [Test]
    public void ORCWithA88()
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
        _registers.ProgramCounter = 0x0000;
        _registers.A = 0x88;
        _instructions.ORCWithA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));

        expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            Carry = false,
            HalfCarry = false
        };
        _registers.ProgramCounter = 0x0000;
        _registers.A = 0x00;
        _registers.C = 0x88;
        _instructions.ORCWithA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));

        expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            Carry = false,
            HalfCarry = false
        };
        _registers.A = 0x88;
        _registers.C = 0x00;
        _registers.ProgramCounter = 0x0000;
        _instructions.ORCWithA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }

    [Test]
    public void ORCWithAFF()
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
        _registers.ProgramCounter = 0x0000;
        _registers.A = 0xFF;
        _instructions.ORCWithA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));

        expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            Carry = false,
            HalfCarry = false
        };
        _registers.ProgramCounter = 0x0000;
        _registers.A = 0x00;
        _registers.C = 0xFF;
        _instructions.ORCWithA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));

        expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            Carry = false,
            HalfCarry = false
        };
        _registers.A = 0xFF;
        _registers.C = 0x00;
        _registers.ProgramCounter = 0x0000;
        _instructions.ORCWithA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }

    [Test]
    public void ANDD8WithAZeroZero()
    {
        const byte expectedA = 0x00;
        const ushort expectedProgramCounter = 0x0002;
        var expectedF = new FlagsRegister()
        {
            Zero = true,
            Subtract = false,
            HalfCarry = true,
            Carry = false,
        };
        _registers.A = 0;
        _instructions.ANDD8WithA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }

    [Test]
    public void ANDD8WithAZeroFF()
    {
        const byte expectedA = 0x00;
        const ushort expectedProgramCounter = 0x0002;
        var expectedF = new FlagsRegister()
        {
            Zero = true,
            Subtract = false,
            HalfCarry = true,
            Carry = false,
        };
        _registers.A = 0xFF;
        _instructions.ANDD8WithA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }

    [Test]
    public void ANDD8WithAFFZero()
    {
        const byte expectedA = 0x00;
        const ushort expectedProgramCounter = 0x0042;
        var expectedF = new FlagsRegister()
        {
            Zero = true,
            Subtract = false,
            HalfCarry = true,
            Carry = false,
        };
        _registers.ProgramCounter = 0x0040;
        _registers.A = 0x00;
        _instructions.ANDD8WithA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }

    [Test]
    public void ANDD8WithAFFFF()
    {
        const byte expectedA = 0xFF;
        const ushort expectedProgramCounter = 0x0042;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            HalfCarry = true,
            Carry = false,
        };
        _registers.ProgramCounter = 0x0040;
        _registers.A = 0xFF;
        _instructions.ANDD8WithA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
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
        Assert.That(_registers.F, Is.EqualTo(expectedF));
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
        Assert.That(_registers.F, Is.EqualTo(expectedF));
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
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }

    [Test]
    public void CompareAToD8_A_0_D8_0()
    {
        const ushort expectedProgramCounter = 0x0002;
        var expectedF = new FlagsRegister()
        {
            Zero = true,
            Subtract = true,
            Carry = false,
            HalfCarry = false
        };
        _registers.A = 0x00;
        _registers.ProgramCounter = 0x0000;
        _instructions.CompareAToD8(_registers);
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }

    [Test]
    public void CompareAToD8_A_0_D8_1()
    {
        const ushort expectedProgramCounter = 0x0011;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = true,
            Carry = true,
            HalfCarry = true
        };
        _registers.A = 0x00;
        _registers.ProgramCounter = 0x000F;
        _instructions.CompareAToD8(_registers);
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }

    [Test]
    public void CompareAToD8_Carry()
    {
        const ushort expectedProgramCounter = 0x0021;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = true,
            Carry = true,
            HalfCarry = false
        };
        _registers.A = 0x0F;
        _registers.ProgramCounter = 0x001F;
        _instructions.CompareAToD8(_registers);
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }

    [Test]
    public void CompareAToD8HalfCarry()
    {
        const ushort expectedProgramCounter = 0x0031;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Subtract = true,
            Carry = false,
            HalfCarry = true
        };
        _registers.A = 0x10;
        _registers.ProgramCounter = 0x002F;
        _instructions.CompareAToD8(_registers);
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }
}