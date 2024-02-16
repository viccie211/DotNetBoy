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
    public void AtAddressHLIntoAIncrementHLOverflow()
    {
        const byte expectedA = 0x34;
        const ushort expectedHL = 0x0000;
        const ushort expectedProgramCounter = 0x0001;
        _registers.HL = 0xFFFF;
        _instructions.LoadAtAddressHLIntoAIncrementHL(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.HL, Is.EqualTo(expectedHL));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

        [Test]
    public void AtAddressHLIntoADecrementHL()
    {
        const byte expectedA = 0xAB;
        const ushort expectedHL = 0x100F;
        const ushort expectedProgramCounter = 0x0001;
        _registers.HL = 0x1010;
        _instructions.LoadAtAddressHLIntoADecrementHL(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.HL, Is.EqualTo(expectedHL));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void AtAddressHLIntoADecrementHLUnderflow()
    {
        const byte expectedA = 0x67;
        const ushort expectedHL = 0xFFFF;
        const ushort expectedProgramCounter = 0x0001;
        _registers.HL = 0x0000;
        _instructions.LoadAtAddressHLIntoADecrementHL(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.HL, Is.EqualTo(expectedHL));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void AtAddressDEIntoA()
    {
        const byte expectedA = 0xAB;
        const ushort expectedProgramCounter = 0x0001;
        _registers.DE = 0x1010;
        _instructions.LoadAtAddressDEIntoA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
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
    
    [Test]
    public void AtAddressHLIntoB()
    {
        const byte expectedB = 0xAB;
        const ushort expectedProgramCounter = 0x0001;
        _registers.HL = 0x1010;
        _instructions.LoadAtAddressHLIntoB(_registers);
        Assert.That(_registers.B, Is.EqualTo(expectedB));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void AtAddressHLIntoC()
    {
        const byte expectedC = 0xAB;
        const ushort expectedProgramCounter = 0x0001;
        _registers.HL = 0x1010;
        _instructions.LoadAtAddressHLIntoC(_registers);
        Assert.That(_registers.C, Is.EqualTo(expectedC));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void AtAddressHLIntoD()
    {
        const byte expectedD = 0xAB;
        const ushort expectedProgramCounter = 0x0001;
        _registers.HL = 0x1010;
        _instructions.LoadAtAddressHLIntoD(_registers);
        Assert.That(_registers.D, Is.EqualTo(expectedD));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
}