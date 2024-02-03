using DotNetBoy.Emulator.InstructionSet;

namespace DotNetBoy.Emulator.Tests.InstructionTests.LoadInstructionTests;

public class LoadD8Tests : LoadInstructionTestsBase
{
[Test]
    public void IntoB()
    {
        const byte expectedB = 0xAB;
        const ushort expectedProgramCounter = 0x1011;
        _registers.ProgramCounter = 0x100F;
        _instructions.LoadD8IntoB(_registers);
        Assert.That(_registers.B, Is.EqualTo(expectedB));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    

    [Test]
    public void IntoC()
    {
        const byte expectedC = 0xAB;
        const ushort expectedProgramCounter = 0x1011;
        _registers.ProgramCounter = 0x100F;
        _instructions.LoadD8IntoC(_registers);
        Assert.That(_registers.C, Is.EqualTo(expectedC));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void IntoA()
    {
        const byte expectedA = 0xAB;
        const ushort expectedProgramCounter = 0x1011;
        _registers.ProgramCounter = 0x100F;
        _instructions.LoadD8IntoA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
}