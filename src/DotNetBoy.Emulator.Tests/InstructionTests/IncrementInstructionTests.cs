using DotNetBoy.Emulator.InstructionSet;
using DotNetBoy.Emulator.Models;
using DotNetBoy.Emulator.Services.Interfaces;
using Moq;

namespace DotNetBoy.Emulator.Tests.InstructionTests;

public class IncrementInstructionTests
{
    private IncrementInstructions? _instructions;
    private FlagsRegister? flagsRegister;
    private ICpuRegistersService? registers;
    private byte _b;
    private ushort _bc;
    private ushort _programCounter;
    private ushort _stackPointer;

    [SetUp]
    public void SetUp()
    {
        var clockServiceMock = new Mock<ClockService>();
        _instructions = new IncrementInstructions(clockServiceMock.Object);
        flagsRegister = new FlagsRegister();
        var registerServiceMock = new Mock<ICpuRegistersService>();
        registerServiceMock.Setup(c => c.F).Returns(flagsRegister);

        _b = 0;
        registerServiceMock.SetupGet(c => c.B).Returns(() => _b);
        registerServiceMock.SetupSet(c => c.B = It.IsAny<byte>()).Callback<byte>(value => _b = value);

        _bc = 0;
        registerServiceMock.SetupGet(c => c.BC).Returns(() => _bc);
        registerServiceMock.SetupSet(c => c.BC = It.IsAny<ushort>()).Callback<ushort>(value => _bc = value);

        _programCounter = 0;
        registerServiceMock.SetupGet(c => c.ProgramCounter).Returns(() => _programCounter);
        registerServiceMock.SetupSet(c => c.ProgramCounter = It.IsAny<ushort>()).Callback<ushort>(value => _programCounter = value);

        _programCounter = 0;
        registerServiceMock.SetupGet(c => c.StackPointer).Returns(() => _stackPointer);
        registerServiceMock.SetupSet(c => c.StackPointer = It.IsAny<ushort>()).Callback<ushort>(value => _stackPointer = value);
        registers = registerServiceMock.Object;
    }

    [Test]
    public void IncrementBC()
    {
        _bc = 0x0000;
        const ushort expectedBC = 0x0001;
        const ushort expectedProgramCounter = 0x0001;
        _instructions!.IncrementBC(registers!);
        Assert.That(_bc, Is.EqualTo(expectedBC));
        Assert.That(_programCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void IncrementBCOverflow()
    {
        _bc = 0xFFFF;
        const ushort expectedBC = 0x0000;
        const ushort expectedProgramCounter = 0x0001;
        _instructions!.IncrementBC(registers!);
        Assert.That(_bc, Is.EqualTo(expectedBC));
        Assert.That(_programCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void IncrementB()
    {
        _b = 0x00;
        const byte expectedB = 0x01;
        const ushort expectedProgramCounter = 0x0001;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            HalfCarry = false,
            Carry = false,
        };
        _instructions!.IncrementB(registers!);
        Assert.That(_b, Is.EqualTo(expectedB));
        Assert.That(_programCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(flagsRegister, Is.EqualTo(expectedFlagsRegister));
    }

    [Test]
    public void IncrementBHalfCarry()
    {
        _b = 0x0F;
        const byte expectedB = 0x10;
        const ushort expectedProgramCounter = 0x0001;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = false,
            HalfCarry = true,
            Carry = false,
        };
        _instructions!.IncrementB(registers!);
        Assert.That(_b, Is.EqualTo(expectedB));
        Assert.That(_programCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(flagsRegister, Is.EqualTo(expectedFlagsRegister));
    }
    
    [Test]
    public void IncrementBOverFlow()
    {
        _b = 0xFF;
        const byte expectedB = 0x00;
        const ushort expectedProgramCounter = 0x0001;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = true,
            Subtract = false,
            HalfCarry = true,
            Carry = false,
        };
        _instructions!.IncrementB(registers!);
        Assert.That(_b, Is.EqualTo(expectedB));
        Assert.That(_programCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(flagsRegister, Is.EqualTo(expectedFlagsRegister));
    }

    [Test]
    public void IncrementStackPointer()
    {
        _stackPointer = 0x0000;
        const ushort expectedStackPointer = 0x0001;
        const ushort expectedProgramCounter = 0x0001;
        
        _instructions!.IncrementStackPointer(registers!);
        Assert.That(_stackPointer, Is.EqualTo(expectedStackPointer));
        Assert.That(_programCounter, Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void IncrementStackPointerOverflow()
    {
        _stackPointer = 0xFFFF;
        const ushort expectedStackPointer = 0x0000;
        const ushort expectedProgramCounter = 0x0001;
        
        _instructions!.IncrementStackPointer(registers!);
        Assert.That(_stackPointer, Is.EqualTo(expectedStackPointer));
        Assert.That(_programCounter, Is.EqualTo(expectedProgramCounter));
    }
}