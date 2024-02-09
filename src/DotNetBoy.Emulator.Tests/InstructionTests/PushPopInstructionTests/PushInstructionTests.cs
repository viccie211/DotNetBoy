namespace DotNetBoy.Emulator.Tests.InstructionTests.PushPopInstructionTests;

public class PushInstructionsTests : PushPopInstructionsTestsBase
{
    [Test]
    public void PushBC()
    {
        const ushort expectedStackPointer = 0xFFFC;
        const byte expectedWrittenUpper = 0xAA;
        const byte expectedWrittenLower = 0xF0;
        const ushort expectedProgramCounter = 0x0001;
        byte writtenUpper = 0x00;
        byte writtenLower = 0x00;
        _mmuServiceMock.Setup(m => m.WriteByte(0xFFFD, It.IsAny<byte>())).Callback((ushort address, byte value) => writtenUpper = value);
        _mmuServiceMock.Setup(m => m.WriteByte(0xFFFC, It.IsAny<byte>())).Callback((ushort address, byte value) => writtenLower = value);

        _registers.BC = 0xAAF0;
        _instructions.PushBC(_registers);
        Assert.That(_registers.StackPointer, Is.EqualTo(expectedStackPointer));
        Assert.That(writtenUpper, Is.EqualTo(expectedWrittenUpper));
        Assert.That(writtenLower, Is.EqualTo(expectedWrittenLower));
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
    
    [Test]
    public void PushDE()
    {
        const ushort expectedStackPointer = 0xFFFC;
        const byte expectedWrittenUpper = 0xAA;
        const byte expectedWrittenLower = 0xF0;
        const ushort expectedProgramCounter = 0x0001;
        byte writtenUpper = 0x00;
        byte writtenLower = 0x00;
        _mmuServiceMock.Setup(m => m.WriteByte(0xFFFD, It.IsAny<byte>())).Callback((ushort address, byte value) => writtenUpper = value);
        _mmuServiceMock.Setup(m => m.WriteByte(0xFFFC, It.IsAny<byte>())).Callback((ushort address, byte value) => writtenLower = value);

        _registers.DE = 0xAAF0;
        _instructions.PushDE(_registers);
        Assert.That(_registers.StackPointer, Is.EqualTo(expectedStackPointer));
        Assert.That(writtenUpper, Is.EqualTo(expectedWrittenUpper));
        Assert.That(writtenLower, Is.EqualTo(expectedWrittenLower));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
}