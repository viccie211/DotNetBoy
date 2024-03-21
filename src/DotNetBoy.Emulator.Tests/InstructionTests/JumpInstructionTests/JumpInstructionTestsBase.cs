using DotNetBoy.Emulator.InstructionSet;
using DotNetBoy.Emulator.Services.Implementations;

namespace DotNetBoy.Emulator.Tests.InstructionTests.JumpInstructionTests;

public abstract class JumpInstructionTestsBase
{
    protected JumpInstructions _instructions;

    protected Mock<IMmuService> _mmuServiceMock;

    protected ICpuRegistersService _registers;

    [SetUp]
    public void SetUp()
    {
        var clockServiceMock = new Mock<ClockService>();
        _mmuServiceMock = new Mock<IMmuService>();
        var byteUshortServiceMock = new Mock<IByteUshortService>();
        byteUshortServiceMock.Setup(b => b.LowerByteOfSixteenBits(0x0023)).Returns(0x23);
        byteUshortServiceMock.Setup(b => b.UpperByteOfSixteenBits(0x0023)).Returns(0x00);
        byteUshortServiceMock.Setup(b => b.CombineBytes(0x00, 0x23)).Returns(0x0023);

        _mmuServiceMock.Setup(m => m.ReadWordLittleEndian(0x0001)).Returns(0xFFAA);
        _mmuServiceMock.Setup(m => m.ReadByte(0x0001)).Returns(0x08);
        _mmuServiceMock.Setup(m => m.ReadByte(0x0010)).Returns(0xFF);
        _mmuServiceMock.Setup(m => m.ReadByte(0xFFFD)).Returns(0x00);
        _mmuServiceMock.Setup(m => m.ReadByte(0xFFFC)).Returns(0x23);
        _mmuServiceMock.Setup(m => m.ReadWordLittleEndian(0x0021)).Returns(0xCCDD);
        _mmuServiceMock.Setup(m => m.ReadWordLittleEndian(0x0023)).Returns(0xABCD);


        _instructions = new JumpInstructions(_mmuServiceMock.Object, clockServiceMock.Object, byteUshortServiceMock.Object);

        _registers = new TestCpuRegisterService
        {
            ProgramCounter = 0x0000,
            StackPointer = 0xFFFE
        };
    }
}