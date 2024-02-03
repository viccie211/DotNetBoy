using DotNetBoy.Emulator.InstructionSet;

namespace DotNetBoy.Emulator.Tests.InstructionTests.LogicInstructionTests;

public abstract class LogicInstructionsTestsBase
{
    protected LogicInstructions _instructions;
    protected ICpuRegistersService _registers;

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
}