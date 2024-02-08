#nullable disable
using DotNetBoy.Emulator.InstructionSet;

namespace DotNetBoy.Emulator.Tests.InstructionTests;

public class StoreInstructionsTests
{
    private StoreInstructions _instructions;
    private ICpuRegistersService _registers;
    private Mock<IMmuService> _mmuServiceMock;

    [SetUp]
    public void SetUp()
    {
        var clockServiceMock = new Mock<IClockService>();
        var byteUshortServiceMock = new Mock<IByteUshortService>();
        byteUshortServiceMock.Setup(b => b.LowerByteOfSixteenBits(0xFFFE)).Returns(0xFE);
        byteUshortServiceMock.Setup(b => b.UpperByteOfSixteenBits(0xFFFE)).Returns(0xFF);
        _mmuServiceMock = new Mock<IMmuService>();

        _registers = new TestCpuRegisterService();
        _instructions = new StoreInstructions(byteUshortServiceMock.Object, _mmuServiceMock.Object, clockServiceMock.Object);
    }

    [Test]
    public void StoreStackPointerAtAddressD16()
    {
        const byte expectedWrittenLower = 0xFE;
        const byte expectedWrittenUpper = 0xFF;
        const ushort expectedProgramCounter = 0x0003;

        byte writtenLower = 0x00;
        byte writtenUpper = 0x00;

        _mmuServiceMock.Setup(m => m.ReadWordLittleEndian(0x0001)).Returns(0x0020);
        _mmuServiceMock.Setup(m => m.WriteByte(0x0020, It.IsAny<byte>())).Callback((ushort addres, byte value) => writtenLower = value);
        _mmuServiceMock.Setup(m => m.WriteByte(0x0021, It.IsAny<byte>())).Callback((ushort addres, byte value) => writtenUpper = value);

        _registers.StackPointer = 0xFFFE;

        _instructions.StoreStackPointerAtAddressD16(_registers);

        Assert.That(writtenLower, Is.EqualTo(expectedWrittenLower));
        Assert.That(writtenUpper, Is.EqualTo(expectedWrittenUpper));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void StoreAtAddressFF00PlusD8FromA()
    {
        const byte expectedWrittenByte = 0xFF;
        const ushort expectedProgramCounter = 0x0002;
        byte writtenByte = 0x00;

        _mmuServiceMock.Setup(m => m.ReadByte(0x0001)).Returns(0xAA);
        _mmuServiceMock.Setup(m => m.WriteByte(0xFFAA, It.IsAny<byte>())).Callback((ushort address, byte value) => writtenByte = value);
        _registers.A = 0xFF;
        _instructions.StoreAtAddressFF00PlusD8FromA(_registers);

        Assert.That(writtenByte, Is.EqualTo(expectedWrittenByte));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void StoreAtAddressDEFromA()
    {
        const byte expectedWrittenByte = 0xFF;
        const ushort expectedProgramCounter = 0x0001;
        byte writtenByte = 0x00;
        _registers.DE = 0xFFAA;
        _registers.A = 0xFF;
        _mmuServiceMock.Setup(m => m.WriteByte(0xFFAA, It.IsAny<byte>())).Callback((ushort address, byte value) => writtenByte = value);
        _instructions.StoreAtAddressDEFromA(_registers);
        Assert.That(writtenByte, Is.EqualTo(expectedWrittenByte));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void StoreAtA16FromA()
    {
        const byte expectedWrittenByte = 0xFF;
        const ushort expectedWrittenAddress = 0x1000;
        const ushort expectedProgramCounter = 0x0003;
        byte writtenByte = 0x00;
        ushort writtenAddress = 0x0000;
        _registers.A = 0xFF;
        _mmuServiceMock.Setup(m => m.ReadWordLittleEndian(0x0001)).Returns(0x1000);
        _mmuServiceMock.Setup(m => m.WriteByte(It.IsAny<ushort>(), It.IsAny<byte>())).Callback((ushort address, byte value) =>
        {
            writtenAddress = address;
            writtenByte = value;
        });
        _instructions.StoreAtA16FromA(_registers);
        Assert.That(writtenByte, Is.EqualTo(expectedWrittenByte));
        Assert.That(writtenAddress, Is.EqualTo(expectedWrittenAddress));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void StoreAtAddressHLFromA()
    {
        const byte expectedWrittenByte = 0xFF;
        const ushort expectedWrittenAddress = 0x1000;
        const ushort expectedProgramCounter = 0x0001;
        byte writtenByte = 0x00;
        ushort writtenAddress = 0x0000;
        _registers.A = 0xFF;
        _registers.HL = 0x1000;

        _mmuServiceMock.Setup(m => m.WriteByte(It.IsAny<ushort>(), It.IsAny<byte>())).Callback((ushort address, byte value) =>
        {
            writtenAddress = address;
            writtenByte = value;
        });
        _instructions.StoreAtAddressHLFromA(_registers);
        Assert.That(writtenByte, Is.EqualTo(expectedWrittenByte));
        Assert.That(writtenAddress, Is.EqualTo(expectedWrittenAddress));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void StoreAtAddressHLFromAIncrementHL()
    {
        const byte expectedWrittenByte = 0xFF;
        const ushort expectedWrittenAddress = 0x1000;
        const ushort expectedProgramCounter = 0x0001;
        const ushort expectedHL = 0x1001;
        byte writtenByte = 0x00;
        ushort writtenAddress = 0x0000;
        _registers.A = 0xFF;
        _registers.HL = 0x1000;

        _mmuServiceMock.Setup(m => m.WriteByte(It.IsAny<ushort>(), It.IsAny<byte>())).Callback((ushort address, byte value) =>
        {
            writtenAddress = address;
            writtenByte = value;
        });
        _instructions.StoreAtAddressHLFromAIncrementHL(_registers);
        Assert.That(writtenByte, Is.EqualTo(expectedWrittenByte));
        Assert.That(writtenAddress, Is.EqualTo(expectedWrittenAddress));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.HL, Is.EqualTo(expectedHL));
    }

    [Test]
    public void StoreAtAddressHLFromAIncrementHLOverflow()
    {
        const byte expectedWrittenByte = 0xFF;
        const ushort expectedWrittenAddress = 0xFFFF;
        const ushort expectedProgramCounter = 0x0001;
        const ushort expectedHL = 0x0000;
        byte writtenByte = 0x00;
        ushort writtenAddress = 0x0000;
        _registers.A = 0xFF;
        _registers.HL = 0xFFFF;

        _mmuServiceMock.Setup(m => m.WriteByte(It.IsAny<ushort>(), It.IsAny<byte>())).Callback((ushort address, byte value) =>
        {
            writtenAddress = address;
            writtenByte = value;
        });
        _instructions.StoreAtAddressHLFromAIncrementHL(_registers);
        Assert.That(writtenByte, Is.EqualTo(expectedWrittenByte));
        Assert.That(writtenAddress, Is.EqualTo(expectedWrittenAddress));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.HL, Is.EqualTo(expectedHL));
    }
    
    [Test]
    public void StoreAtAddressHLFromADecrementHL()
    {
        const byte expectedWrittenByte = 0xFF;
        const ushort expectedWrittenAddress = 0x1000;
        const ushort expectedProgramCounter = 0x0001;
        const ushort expectedHL = 0x0FFF;
        byte writtenByte = 0x00;
        ushort writtenAddress = 0x0000;
        _registers.A = 0xFF;
        _registers.HL = 0x1000;

        _mmuServiceMock.Setup(m => m.WriteByte(It.IsAny<ushort>(), It.IsAny<byte>())).Callback((ushort address, byte value) =>
        {
            writtenAddress = address;
            writtenByte = value;
        });
        _instructions.StoreAtAddressHLFromADecrementHL(_registers);
        Assert.That(writtenByte, Is.EqualTo(expectedWrittenByte));
        Assert.That(writtenAddress, Is.EqualTo(expectedWrittenAddress));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.HL, Is.EqualTo(expectedHL));
    }

    [Test]
    public void StoreAtAddressHLFromADecrementHLUnderflow()
    {
        const byte expectedWrittenByte = 0xFF;
        const ushort expectedWrittenAddress = 0x0000;
        const ushort expectedProgramCounter = 0x0001;
        const ushort expectedHL = 0xFFFF;
        byte writtenByte = 0x00;
        ushort writtenAddress = 0x0000;
        _registers.A = 0xFF;
        _registers.HL = 0x0000;

        _mmuServiceMock.Setup(m => m.WriteByte(It.IsAny<ushort>(), It.IsAny<byte>())).Callback((ushort address, byte value) =>
        {
            writtenAddress = address;
            writtenByte = value;
        });
        _instructions.StoreAtAddressHLFromADecrementHL(_registers);
        Assert.That(writtenByte, Is.EqualTo(expectedWrittenByte));
        Assert.That(writtenAddress, Is.EqualTo(expectedWrittenAddress));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.HL, Is.EqualTo(expectedHL));
    }
}