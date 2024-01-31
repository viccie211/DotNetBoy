#nullable disable
using DotNetBoy.Emulator.InstructionSet;

namespace DotNetBoy.Emulator.Tests.InstructionTests;

public class LoadInstructionTests
{
    private LoadInstructions _instructions;
    private ICpuRegistersService _registers;

    [SetUp]
    public void SetUp()
    {
        var mmuServiceMock = new Mock<IMmuService>();
        mmuServiceMock.Setup(m => m.ReadWordLittleEndian(0x0001)).Returns(0xFFAA);
        mmuServiceMock.Setup(m => m.ReadByte(0x1010)).Returns(0xAB);
        mmuServiceMock.Setup(m => m.ReadByte(0xFFAB)).Returns(0x12);

        var clockServiceMock = new Mock<IClockService>();

        _instructions = new LoadInstructions(mmuServiceMock.Object, clockServiceMock.Object);
        _registers = new TestCpuRegisterService()
        {
            ProgramCounter = 0x0000,
        };
    }

    [Test]
    public void LoadD16IntoBC()
    {
        const ushort expectedBC = 0xFFAA;
        const ushort expectedProgramCounter = 0x0003;
        _instructions.LoadD16IntoBC(_registers);
        Assert.That(_registers.BC, Is.EqualTo(expectedBC));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void LoadD16IntoStackPointer()
    {
        const ushort expectedStackPointer = 0xFFAA;
        const ushort expectedProgramCounter = 0x0003;
        _instructions.LoadD16IntoStackPointer(_registers);
        Assert.That(_registers.StackPointer, Is.EqualTo(expectedStackPointer));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void LoadD16IntoHL()
    {
        const ushort expectedHL = 0xFFAA;
        const ushort expectedProgramCounter = 0x0003;
        _instructions.LoadD16IntoHL(_registers);
        Assert.That(_registers.HL, Is.EqualTo(expectedHL));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void LoadD16IntoDE()
    {
        const ushort expectedDE = 0xFFAA;
        const ushort expectedProgramCounter = 0x0003;
        _instructions.LoadD16IntoDE(_registers);
        Assert.That(_registers.DE, Is.EqualTo(expectedDE));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void LoadAtAddressBCIntoA()
    {
        const byte expectedA = 0xAB;
        const ushort expectedProgramCounter = 0x0001;
        _registers.BC = 0x1010;
        _instructions.LoadAtAddressBCIntoA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void LoadAtAddressFF00PlusD8IntoA()
    {
        const byte expectedA = 0x12;
        const ushort expectedProgramCounter = 0x1011;
        _registers.ProgramCounter = 0x100F;
        _instructions.LoadAtAddressFF00PlusD8IntoA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void LoadEIntoA()
    {
        const byte expectedA = 0x45;
        const ushort expectedProgramCounter = 0x0001;
        _registers.E = 0x45;
        _instructions.LoadEIntoA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void LoadD8IntoB()
    {
        const byte expectedB = 0xAB;
        const ushort expectedProgramCounter = 0x1011;
        _registers.ProgramCounter = 0x100F;
        _instructions.LoadD8IntoB(_registers);
        Assert.That(_registers.B, Is.EqualTo(expectedB));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void LoadAIntoB()
    {
        const byte expectedB = 0xFF;
        const ushort expectedProgramCounter = 0x0001;
        _registers.A = 0xFF;
        _instructions.LoadAIntoB(_registers);
        Assert.That(_registers.B, Is.EqualTo(expectedB));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void LoadD8IntoC()
    {
        const byte expectedC = 0xAB;
        const ushort expectedProgramCounter = 0x1011;
        _registers.ProgramCounter = 0x100F;
        _instructions.LoadD8IntoC(_registers);
        Assert.That(_registers.C, Is.EqualTo(expectedC));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void LoadDIntoB()
    {
        const byte expectedB = 0xFF;
        const ushort expectedProgramCounter = 0x0001;
        _registers.D = 0xFF;
        _instructions.LoadDIntoB(_registers);
        Assert.That(_registers.B, Is.EqualTo(expectedB));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void LoadAtAddressHLIntoAIncrementHL()
    {
        const byte expectedA = 0xAB;
        const ushort expectedHL = 0x1011;
        const ushort expectedProgramCounter = 0x0001;
        _registers.HL = 0x1010;
        _instructions.LoadAtAddressHLIntoAIncrementHL(_registers);
        Assert.That(_registers.A,Is.EqualTo(expectedA));
        Assert.That(_registers.HL,Is.EqualTo(expectedHL));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

}