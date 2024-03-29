using DotNetBoy.Emulator.InstructionSet;

namespace DotNetBoy.Emulator.Tests.InstructionTests.LoadInstructionTests;

public class LoadBetweenRegisterTests
{
    protected LoadBetweenRegistersInstructions _instructions;
    protected ICpuRegistersService _registers;

    [SetUp]
    public void SetUp()
    {
        var clockServiceMock = new Mock<IClockService>();

        _instructions = new LoadBetweenRegistersInstructions(clockServiceMock.Object);
        _registers = new TestCpuRegisterService()
        {
            ProgramCounter = 0x0000,
        };
    }

    [Test]
    public void LoadBIntoA()
    {
        const byte expectedA = 0x45;
        const ushort expectedProgramCounter = 0x0001;
        _registers.B = 0x45;
        _instructions.LoadBIntoA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void LoadCIntoA()
    {
        const byte expectedA = 0x45;
        const ushort expectedProgramCounter = 0x0001;
        _registers.C = 0x45;
        _instructions.LoadCIntoA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void LoadDIntoA()
    {
        const byte expectedA = 0x45;
        const ushort expectedProgramCounter = 0x0001;
        _registers.D = 0x45;
        _instructions.LoadDIntoA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void LoadEIntoA()
    {
        const byte expectedA = 0x45;
        const ushort expectedProgramCounter = 0x0001;
        _registers.E = 0x45;
        _instructions.LoadEIntoA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void LoadLIntoA()
    {
        const byte expectedA = 0x45;
        const ushort expectedProgramCounter = 0x0001;
        _registers.L = 0x45;
        _instructions.LoadLIntoA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void LoadHIntoA()
    {
        const byte expectedA = 0x45;
        const ushort expectedProgramCounter = 0x0001;
        _registers.H = 0x45;
        _instructions.LoadHIntoA(_registers);
        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void LoadAIntoB()
    {
        const byte expectedB = 0xFF;
        const ushort expectedProgramCounter = 0x0001;
        _registers.A = 0xFF;
        _instructions.LoadAIntoB(_registers);
        Assert.That(_registers.B, Is.EqualTo(expectedB));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void LoadAIntoC()
    {
        const byte expectedC = 0xFF;
        const ushort expectedProgramCounter = 0x0001;
        _registers.A = 0xFF;
        _instructions.LoadAIntoC(_registers);
        Assert.That(_registers.C, Is.EqualTo(expectedC));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void LoadAIntoD()
    {
        const byte expectedD = 0xFF;
        const ushort expectedProgramCounter = 0x0001;
        _registers.A = 0xFF;
        _instructions.LoadAIntoD(_registers);
        Assert.That(_registers.D, Is.EqualTo(expectedD));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void LoadAIntoE()
    {
        const byte expectedE = 0xFF;
        const ushort expectedProgramCounter = 0x0001;
        _registers.A = 0xFF;
        _instructions.LoadAIntoE(_registers);
        Assert.That(_registers.E, Is.EqualTo(expectedE));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void LoadDIntoB()
    {
        const byte expectedB = 0xFF;
        const ushort expectedProgramCounter = 0x0001;
        _registers.D = 0xFF;
        _instructions.LoadDIntoB(_registers);
        Assert.That(_registers.B, Is.EqualTo(expectedB));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }
}