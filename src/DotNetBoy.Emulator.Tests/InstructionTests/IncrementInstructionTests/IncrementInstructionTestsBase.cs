using DotNetBoy.Emulator.InstructionSet;

namespace DotNetBoy.Emulator.Tests.InstructionTests.IncrementInstructionTests;

public abstract class IncrementInstructionTestsBase
{
    protected IncrementInstructions _instructions;
    protected ICpuRegistersService _registers;

    [SetUp]
    public void SetUp()
    {
        var clockServiceMock = new Mock<ClockService>();
        _instructions = new IncrementInstructions(clockServiceMock.Object);
        _registers = new TestCpuRegisterService(){
            F=new FlagsRegister(),
            B=0x00,
            BC=0x0000,
            ProgramCounter=0x0000,
            StackPointer=0x0000
        };
    }
}