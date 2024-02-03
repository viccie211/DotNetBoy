using DotNetBoy.Emulator.InstructionSet;

namespace DotNetBoy.Emulator.Tests.InstructionTests.DecrementInstructionTests;

public abstract class DecrementTestsBase
{
    protected DecrementInstructions _instructions;
    protected ICpuRegistersService _registers;

    [SetUp]
    public void SetUp()
    {
        var clockServiceMock = new Mock<ClockService>();
        _instructions = new DecrementInstructions(clockServiceMock.Object);

        _registers = new TestCpuRegisterService();
    }
}