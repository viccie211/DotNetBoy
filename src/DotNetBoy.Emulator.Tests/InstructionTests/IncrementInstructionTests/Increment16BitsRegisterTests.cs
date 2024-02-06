namespace DotNetBoy.Emulator.Tests.InstructionTests.IncrementInstructionTests;

public class Increment16BitsRegisterTests : IncrementInstructionTestsBase
{
    [Test]
    public void IncrementBC()
    {
        _registers.BC = 0x0000;
        const ushort expectedBC = 0x0001;
        const ushort expectedProgramCounter = 0x0001;
        _instructions.IncrementBC(_registers);
        Assert.That(_registers.BC, Is.EqualTo(expectedBC));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void IncrementBCOverflow()
    {
        _registers.BC = 0xFFFF;
        const ushort expectedBC = 0x0000;
        const ushort expectedProgramCounter = 0x0001;
        _instructions.IncrementBC(_registers);
        Assert.That(_registers.BC, Is.EqualTo(expectedBC));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void IncrementDE()
    {
        _registers.BC = 0x0000;
        const ushort expectedDE = 0x0001;
        const ushort expectedProgramCounter = 0x0001;
        _instructions.IncrementDE(_registers);
        Assert.That(_registers.DE, Is.EqualTo(expectedDE));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void IncrementDEOverflow()
    {
        _registers.DE = 0xFFFF;
        const ushort expectedDE = 0x0000;
        const ushort expectedProgramCounter = 0x0001;
        _instructions.IncrementDE(_registers);
        Assert.That(_registers.DE, Is.EqualTo(expectedDE));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void IncrementHL()
    {
        _registers.BC = 0x0000;
        const ushort expectedHL = 0x0001;
        const ushort expectedProgramCounter = 0x0001;
        _instructions.IncrementHL(_registers);
        Assert.That(_registers.HL, Is.EqualTo(expectedHL));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void IncrementHLOverflow()
    {
        _registers.HL = 0xFFFF;
        const ushort expectedHL = 0x0000;
        const ushort expectedProgramCounter = 0x0001;
        _instructions.IncrementHL(_registers);
        Assert.That(_registers.HL, Is.EqualTo(expectedHL));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void IncrementStackPointer()
    {
        _registers.StackPointer = 0x0000;
        const ushort expectedStackPointer = 0x0001;
        const ushort expectedProgramCounter = 0x0001;
        
        _instructions.IncrementStackPointer(_registers);
        Assert.That(_registers.StackPointer, Is.EqualTo(expectedStackPointer));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void IncrementStackPointerOverflow()
    {
        _registers.StackPointer = 0xFFFF;
        const ushort expectedStackPointer = 0x0000;
        const ushort expectedProgramCounter = 0x0001;
        
        _instructions.IncrementStackPointer(_registers);
        Assert.That(_registers.StackPointer, Is.EqualTo(expectedStackPointer));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
}