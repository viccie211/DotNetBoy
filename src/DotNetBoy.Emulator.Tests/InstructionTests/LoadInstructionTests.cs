#nullable disable
using DotNetBoy.Emulator.InstructionSet;
using DotNetBoy.Emulator.Services.Implementations;
using Microsoft.VisualBasic;

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
        var expectedBC = 0xFFAA;
        var expectedProgramCounter = 0x0003;
        _instructions.LoadD16IntoBC(_registers);
        Assert.That(_registers.BC, Is.EqualTo(expectedBC));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void LoadD16IntoStackPointer()
    {
        var expectedStackPointer = 0xFFAA;
        var expectedProgramCounter = 0x0003;
        _instructions.LoadD16IntoStackPointer(_registers);
        Assert.That(_registers.StackPointer, Is.EqualTo(expectedStackPointer));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void LoadD16IntoHL()
    {
        var expectedHL = 0xFFAA;
        var expectedProgramCounter = 0x0003;
        _instructions.LoadD16IntoHL(_registers);
        Assert.That(_registers.HL, Is.EqualTo(expectedHL));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void LoadD16IntoDE()
    {
        var expectedDE = 0xFFAA;
        var expectedProgramCounter = 0x0003;
        _instructions.LoadD16IntoDE(_registers);
        Assert.That(_registers.DE, Is.EqualTo(expectedDE));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void LoadAtAddressBCIntoA()
    {
        var expectedA = 0xAB;
        var expectedProgramCounter = 0x0001;
        _registers.BC = 0x1010;
        _instructions.LoadAtAddressBCIntoA(_registers);
        Assert.That(_registers.A,Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

}