using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;

namespace DotNetBoy.Emulator.Tests.InstructionTests.PrefixedInstructions.ResetBitInstructionTests;

public class ResetBitInCRegisterInstructionTests
{
    private ICpuRegistersService _registers;
    private ResetBitInCRegisterInstructions _instructions;

    [SetUp]
    public void SetUp()
    {
        var clockServiceMock = new Mock<IClockService>();
        _registers = new TestCpuRegisterService();
        _instructions = new ResetBitInCRegisterInstructions(clockServiceMock.Object);
    }

    [Test]
    public void ResetBit2_C0x00()
    {
        const ushort expectedProgramCounter = 0x0002;
        const byte expectedC = 0x00;
        _registers.C = 0x00;
        _instructions.ResetBit2InCRegister(_registers);
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.C, Is.EqualTo(expectedC));
    }
    
    [Test]
    public void ResetBit2_C0xFB()
    {
        const ushort expectedProgramCounter = 0x0002;
        const byte expectedC = 0xFB;
        _registers.C = 0xFB;
        _instructions.ResetBit2InCRegister(_registers);
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.C, Is.EqualTo(expectedC));
    }
    
    [Test]
    public void ResetBit2_C0xFF()
    {
        const ushort expectedProgramCounter = 0x0002;
        const byte expectedC = 0xFB;
        _registers.C = 0xFF;
        _instructions.ResetBit2InCRegister(_registers);
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.C, Is.EqualTo(expectedC));
    }
    
    
}