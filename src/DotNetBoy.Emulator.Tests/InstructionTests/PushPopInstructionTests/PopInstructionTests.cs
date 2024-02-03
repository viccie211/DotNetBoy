using DotNetBoy.Emulator.InstructionSet;

namespace DotNetBoy.Emulator.Tests.InstructionTests.PushPopInstructionTests;

public class PopInstructionsTests : PushPopInstructionsTestsBase
{

    [Test]
    public void PopHL()
    {
        const ushort expectedStackPointer = 0xFFFE;
        const ushort expectedHL = 0xFFAA;
        const ushort expectedProgramCounter = 0x0001;
        _registers.StackPointer = 0xFFFC;
        _registers.HL = 0x0000;
        _instructions.PopHL(_registers);
        Assert.That(_registers.StackPointer, Is.EqualTo(expectedStackPointer));
        Assert.That(_registers.HL, Is.EqualTo(expectedHL));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void PopAF()
    {
        const ushort expectedStackPointer = 0xFFFE;
        const ushort expectedAF = 0xFFAA;
        const ushort expectedProgramCounter = 0x0001;
        _registers.StackPointer = 0xFFFC;
        _registers.AF = 0x0000;
        _instructions.PopAF(_registers);
        Assert.That(_registers.StackPointer, Is.EqualTo(expectedStackPointer));
        Assert.That(_registers.AF, Is.EqualTo(expectedAF));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }


    [Test]
    public void PopBC()
    {
        const ushort expectedStackPointer = 0xFFFE;
        const ushort expectedBC = 0xFFAA;
        const ushort expectedProgramCounter = 0x0001;
        _registers.StackPointer = 0xFFFC;
        _registers.BC = 0x0000;
        _instructions.PopBC(_registers);
        Assert.That(_registers.StackPointer, Is.EqualTo(expectedStackPointer));
        Assert.That(_registers.BC, Is.EqualTo(expectedBC));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
}