namespace DotNetBoy.Emulator.Tests.InstructionTests.JumpInstructionTests;

public class MiscelaneousJumpInstructionTests : JumpInstructionTestsBase
{
    [Test]
    public void Jump()
    {
        const ushort expectedProgramCounter = 0xFFAA;
        _instructions.Jump(_registers);
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void CallA16()
    {
        _registers.ProgramCounter = 0x0020;
        var writtenStackUpper = false;
        var writtenStackLower = false;
        const ushort expectedProgramCounter = 0xCCDD;
        const ushort expectedStackPointer = 0xFFFC;
        _mmuServiceMock.Setup(m => m.WriteByte(0xFFFD, 0x00)).Callback(() => writtenStackUpper = true);
        _mmuServiceMock.Setup(m => m.WriteByte(0xFFFC, 0x23)).Callback(() => writtenStackLower = true);
        _instructions.CallA16(_registers);
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.StackPointer, Is.EqualTo(expectedStackPointer));
        Assert.That(writtenStackUpper, Is.True);
        Assert.That(writtenStackLower, Is.True);
    }

    [Test]
    public void ReturnFromSubroutine()
    {
        _registers.ProgramCounter = 0xCCDD;
        _registers.StackPointer = 0xFFFC;
        const ushort expectedProgramCounter = 0x0023;
        const ushort expectedStackPointer = 0xFFFE;
        _instructions.ReturnFromSubroutine(_registers);
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.StackPointer, Is.EqualTo(expectedStackPointer));
    }
}