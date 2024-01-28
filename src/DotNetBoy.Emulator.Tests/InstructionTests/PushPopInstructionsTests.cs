#nullable disable

using DotNetBoy.Emulator.InstructionSet;

namespace DotNetBoy.Emulator.Tests.InstructionTests;

public class PushPopInstructionsTests
{
    private Mock<IMmuService> _mmuServiceMock;
    private ICpuRegistersService _registers;
    private PushPopInstructions _instructions;

    [SetUp]
    public void SetUp()
    {
        _mmuServiceMock = new Mock<IMmuService>();
        var clockServiceMock = new Mock<IClockService>();
        _registers = new TestCpuRegisterService()
        {
            StackPointer = 0xFFFE
        };
        _instructions = new PushPopInstructions(clockServiceMock.Object, _mmuServiceMock.Object);
    }

    [Test]
    public void PushAF()
    {
        const ushort expectedStackPointer = 0xFFFC;
        const byte expectedWrittenUpper = 0xAA;
        const byte expectedWrittenLower = 0xF0;
        const ushort expectedProgramCounter = 0x0001;
        byte writtenUpper = 0x00;
        byte writtenLower = 0x00;
        _mmuServiceMock.Setup(m => m.WriteByte(0xFFFD, It.IsAny<byte>())).Callback((ushort address, byte value) => writtenUpper = value);
        _mmuServiceMock.Setup(m => m.WriteByte(0xFFFC, It.IsAny<byte>())).Callback((ushort address, byte value) => writtenLower = value);

        //Implementation uses the underlying registers and not the register view, the TestCpuRegisterService however doesn't keep those in sync.
        //So we need to set the invidual registers and not AF 
        _registers.A = 0xAA;
        _registers.F = 0xF0;
        _instructions.PushAF(_registers);
        Assert.That(_registers.StackPointer, Is.EqualTo(expectedStackPointer));
        Assert.That(writtenUpper, Is.EqualTo(expectedWrittenUpper));
        Assert.That(writtenLower, Is.EqualTo(expectedWrittenLower));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
}