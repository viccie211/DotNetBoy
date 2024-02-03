using DotNetBoy.Emulator.InstructionSet;

namespace DotNetBoy.Emulator.Tests.InstructionTests.LoadInstructionTests;

public class LoadAtAddressTests : LoadInstructionTestsBase
{
    [Test]
    public void AtAddressBCIntoA()
    {
        const byte expectedA = 0xAB;
        const ushort expectedProgramCounter = 0x0001;
        _registers.BC = 0x1010;
        _instructions.LoadAtAddressBCIntoA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void AtAddressFF00PlusD8IntoA()
    {
        const byte expectedA = 0x12;
        const ushort expectedProgramCounter = 0x1011;
        _registers.ProgramCounter = 0x100F;
        _instructions.LoadAtAddressFF00PlusD8IntoA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }



    [Test]
    public void AtAddressHLIntoAIncrementHL()
    {
        const byte expectedA = 0xAB;
        const ushort expectedHL = 0x1011;
        const ushort expectedProgramCounter = 0x0001;
        _registers.HL = 0x1010;
        _instructions.LoadAtAddressHLIntoAIncrementHL(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.HL, Is.EqualTo(expectedHL));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void AtAddressA16IntoA()
    {
        const byte expectedA = 0x88;
        const ushort expectedProgramCounter = 0x0003;
        _instructions.LoadAtAddressA16IntoA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
}