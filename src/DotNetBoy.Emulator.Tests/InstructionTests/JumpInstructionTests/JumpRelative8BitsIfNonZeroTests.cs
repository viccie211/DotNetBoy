namespace DotNetBoy.Emulator.Tests.InstructionTests.JumpInstructionTests;

public class JumpRelative8BitsIfNonZeroTests : JumpInstructionTestsBase
{
    [Test]
    public void Zero()
    {
        const ushort expectedProgramCounter = 0x0002;
        _registers.F.Zero = true;
        _instructions.JumpRelative8BitsIfNotZero(_registers);
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void Forward()
    {
        const ushort expectedProgramCounter = 0x000A;
        _registers.F.Zero = false;
        _instructions.JumpRelative8BitsIfNotZero(_registers);
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void Backward()
    {
        const ushort expectedProgramCounter = 0x0010;
        _registers.ProgramCounter = 0x000F;
        _registers.F.Zero = false;
        _instructions.JumpRelative8BitsIfNotZero(_registers);
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
}