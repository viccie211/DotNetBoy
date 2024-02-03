using DotNetBoy.Emulator.InstructionSet;

namespace DotNetBoy.Emulator.Tests.InstructionTests.LoadInstructionTests;

public class LoadD16Tests : LoadInstructionTestsBase
{
    [Test]
    public void IntoBC()
    {
        const ushort expectedBC = 0xFFAA;
        const ushort expectedProgramCounter = 0x0003;
        _instructions.LoadD16IntoBC(_registers);
        Assert.That(_registers.BC, Is.EqualTo(expectedBC));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void IntoStackPointer()
    {
        const ushort expectedStackPointer = 0xFFAA;
        const ushort expectedProgramCounter = 0x0003;
        _instructions.LoadD16IntoStackPointer(_registers);
        Assert.That(_registers.StackPointer, Is.EqualTo(expectedStackPointer));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void IntoHL()
    {
        const ushort expectedHL = 0xFFAA;
        const ushort expectedProgramCounter = 0x0003;
        _instructions.LoadD16IntoHL(_registers);
        Assert.That(_registers.HL, Is.EqualTo(expectedHL));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void IntoDE()
    {
        const ushort expectedDE = 0xFFAA;
        const ushort expectedProgramCounter = 0x0003;
        _instructions.LoadD16IntoDE(_registers);
        Assert.That(_registers.DE, Is.EqualTo(expectedDE));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

}