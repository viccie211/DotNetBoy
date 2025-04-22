using DotNetBoy.Emulator.InstructionSet;
using DotNetBoy.Emulator.Services.Implementations;

namespace DotNetBoy.Emulator.Tests.InstructionTests.ArithmeticInstructionTests;

public abstract class ArithmeticInstructionTestsBase
{
    protected ArithmeticInstructions _instructions;
    protected ICpuRegistersService _registers;

    [SetUp]
    public void SetUp()
    {
        var mmuServiceMock = new Mock<IMmuService>();
        mmuServiceMock.Setup(m => m.ReadByte(0x0001,false)).Returns(0x00);
        mmuServiceMock.Setup(m => m.ReadByte(0x0002,false)).Returns(0x88);
        mmuServiceMock.Setup(m => m.ReadByte(0x0003,false)).Returns(0x0F);
        var clockServiceMock = new Mock<IClockService>();
        _instructions = new ArithmeticInstructions(clockServiceMock.Object, mmuServiceMock.Object);
        _registers = new TestCpuRegisterService();
    }
}