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
        _mmuServiceMock.Setup(m => m.ReadByte(0xFFFC)).Returns(0xAA);
        _mmuServiceMock.Setup(m => m.ReadByte(0xFFFD)).Returns(0xFF);

        var byteUshortServiceMock = new Mock<IByteUshortService>();
        byteUshortServiceMock.Setup(b => b.UpperByteOfSixteenBits(0xAAF0)).Returns(0xAA);
        byteUshortServiceMock.Setup(b => b.LowerByteOfSixteenBits(0xAAF0)).Returns(0xF0);
        byteUshortServiceMock.Setup(b => b.CombineBytes(0xFF, 0xAA)).Returns(0xFFAA);
        var clockServiceMock = new Mock<IClockService>();
        _registers = new TestCpuRegisterService()
        {
            StackPointer = 0xFFFE
        };
        _instructions = new PushPopInstructions(clockServiceMock.Object, _mmuServiceMock.Object, byteUshortServiceMock.Object);
    }

    [Test]
    public void PopHL()
    {
        const ushort expectedStackPointer = 0xFFFE;
        const ushort expectedHL = 0xFFAA;
        const ushort expectedProgramCounter = 0x0001;
        _registers.StackPointer = 0xFFFC;
        _registers.HL = 0x0000;
        _instructions.PopHL(_registers);
        Assert.That(_registers.StackPointer, Is.EqualTo(expectedStackPointer));
        Assert.That(_registers.HL, Is.EqualTo(expectedHL));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void PopAF()
    {
        const ushort expectedStackPointer = 0xFFFE;
        const ushort expectedAF = 0xFFAA;
        const ushort expectedProgramCounter = 0x0001;
        _registers.StackPointer = 0xFFFC;
        _registers.AF = 0x0000;
        _instructions.PopAF(_registers);
        Assert.That(_registers.StackPointer, Is.EqualTo(expectedStackPointer));
        Assert.That(_registers.AF, Is.EqualTo(expectedAF));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
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

        _registers.AF = 0xAAF0;
        _instructions.PushAF(_registers);
        Assert.That(_registers.StackPointer, Is.EqualTo(expectedStackPointer));
        Assert.That(writtenUpper, Is.EqualTo(expectedWrittenUpper));
        Assert.That(writtenLower, Is.EqualTo(expectedWrittenLower));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void PushHL()
    {
        const ushort expectedStackPointer = 0xFFFC;
        const byte expectedWrittenUpper = 0xAA;
        const byte expectedWrittenLower = 0xF0;
        const ushort expectedProgramCounter = 0x0001;
        byte writtenUpper = 0x00;
        byte writtenLower = 0x00;
        _mmuServiceMock.Setup(m => m.WriteByte(0xFFFD, It.IsAny<byte>())).Callback((ushort address, byte value) => writtenUpper = value);
        _mmuServiceMock.Setup(m => m.WriteByte(0xFFFC, It.IsAny<byte>())).Callback((ushort address, byte value) => writtenLower = value);

        _registers.HL = 0xAAF0;
        _instructions.PushHL(_registers);
        Assert.That(_registers.StackPointer, Is.EqualTo(expectedStackPointer));
        Assert.That(writtenUpper, Is.EqualTo(expectedWrittenUpper));
        Assert.That(writtenLower, Is.EqualTo(expectedWrittenLower));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
}