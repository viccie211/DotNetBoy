using DotNetBoy.Emulator.InstructionSet;

namespace DotNetBoy.Emulator.Tests.InstructionTests.PushPopInstructionTests;

public abstract class PushPopInstructionsTestsBase
{
    protected Mock<IMmuService> _mmuServiceMock;
    protected ICpuRegistersService _registers;
    protected PushPopInstructions _instructions;

    [SetUp]
    public void SetUp()
    {
        _mmuServiceMock = new Mock<IMmuService>();
        _mmuServiceMock.Setup(m => m.ReadByte(0xFFFC,false)).Returns(0xAA);
        _mmuServiceMock.Setup(m => m.ReadByte(0xFFFD,false)).Returns(0xFF);

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
}