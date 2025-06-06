using DotNetBoy.Emulator.InstructionSet;

namespace DotNetBoy.Emulator.Tests.InstructionTests.LoadInstructionTests;

public abstract class LoadInstructionTestsBase
{
    protected LoadInstructions _instructions;
    protected ICpuRegistersService _registers;

    [SetUp]
    public void SetUp()
    {
        var mmuServiceMock = new Mock<IMmuService>();
        mmuServiceMock.Setup(m => m.ReadWordLittleEndian(0x0001)).Returns(0xFFAA);
        mmuServiceMock.Setup(m => m.ReadByte(0x0000,false)).Returns(0x67);
        mmuServiceMock.Setup(m => m.ReadByte(0xFFAA,false)).Returns(0x88);
        mmuServiceMock.Setup(m => m.ReadByte(0x1010,false)).Returns(0xAB);
        mmuServiceMock.Setup(m => m.ReadByte(0xFFAB,false)).Returns(0x12);
        mmuServiceMock.Setup(m => m.ReadByte(0xFFFF,false)).Returns(0x34);

        var clockServiceMock = new Mock<IClockService>();

        _instructions = new LoadInstructions(mmuServiceMock.Object, clockServiceMock.Object);
        _registers = new TestCpuRegisterService()
        {
            ProgramCounter = 0x0000,
        };
    }
}