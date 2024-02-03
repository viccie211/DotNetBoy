namespace DotNetBoy.Emulator.Tests.InstructionTests.JumpInstructionTests;

public class JumpRelative8Bits : JumpInstructionTestsBase
{
    [Test]
    public void JumpRelative8BitsForward()
    {
        const ushort expectedProgramCounter = 0x000A;
        _instructions.JumpRelative8Bits(_registers);
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void JumpRelative8BitsBackward()
    {
        const ushort expectedProgramCounter = 0x0010;
        _registers.ProgramCounter = 0x000F;
        _instructions.JumpRelative8Bits(_registers);
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
}