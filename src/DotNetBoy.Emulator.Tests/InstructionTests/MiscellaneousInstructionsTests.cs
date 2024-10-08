#nullable disable
using DotNetBoy.Emulator.InstructionSet;

namespace DotNetBoy.Emulator.Tests.InstructionTests;

public class MiscellaneousInstructionsTests
{
    private MiscellaneousInstructions _instructions;
    private ICpuRegistersService _registers;

    [SetUp]
    public void SetUp()
    {
        var clockServiceMock = new Mock<IClockService>();
        var mmuServiceMock = new Mock<IMmuService>();
        _registers = new TestCpuRegisterService();
        _instructions = new MiscellaneousInstructions(clockServiceMock.Object, mmuServiceMock.Object);
    }

    [Test]
    public void NOP()
    {
        const ushort expectedProgramCounter = 0x0001;
        _instructions.NOP(_registers);
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void DisableInterrupts()
    {
        const bool expectedInterruptMasterEnable = false;
        const ushort expectedProgramCounter = 0x0001;
        _instructions.DisableInterrupts(_registers);
        Assert.That(_registers.InterruptMasterEnable, Is.EqualTo(expectedInterruptMasterEnable));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void Halt()
    {
        const bool expectedHalted = true;
        const ushort expectedProgramCounter = 0x0001;
        _instructions.Halt(_registers);
        Assert.That(_registers.Halted, Is.EqualTo(expectedHalted));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
}