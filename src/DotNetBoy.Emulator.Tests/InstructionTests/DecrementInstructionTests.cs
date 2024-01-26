using DotNetBoy.Emulator.InstructionSet;
namespace DotNetBoy.Emulator.Tests.InstructionTests;

public class DecrementInstructionTests
{
    private DecrementInstructions? _instructions;
    private FlagsRegister? _flagsRegister;
    private ICpuRegistersService? _registers;
    private byte _b = 0;
    private ushort _programCounter = 0;

    [SetUp]
    public void SetUp()
    {
        var clockServiceMock = new Mock<ClockService>();
        _instructions = new DecrementInstructions(clockServiceMock.Object);
        _flagsRegister = new FlagsRegister();
        var registerServiceMock = new Mock<ICpuRegistersService>();
        registerServiceMock.Setup(c => c.F).Returns(_flagsRegister);

        _b = 0;
        registerServiceMock.SetupGet(c => c.B).Returns(() => _b);
        registerServiceMock.SetupSet(c => c.B = It.IsAny<byte>()).Callback<byte>(value => _b = value);

        _programCounter = 0;
        registerServiceMock.SetupGet(c => c.ProgramCounter).Returns(() => _programCounter);
        registerServiceMock.SetupSet(c => c.ProgramCounter = It.IsAny<ushort>()).Callback<ushort>(value => _programCounter = value);

        _registers = registerServiceMock.Object;
    }

    [Test]
    public void DecrementB()
    {
        _b = 0x02;
        const byte expectedB = 0x01;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = true,
            HalfCarry = false,
            Carry = false
        };
        const ushort expectedProgramCounter = 0x0001;

        _instructions!.DecrementB(_registers!);
        Assert.That(_b, Is.EqualTo(expectedB));
        Assert.That(_flagsRegister, Is.EqualTo(expectedFlagsRegister));
        Assert.That(_programCounter,Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void DecrementBToZero()
    {
        _b = 1;
        const int expectedB = 0;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = true,
            Subtract = true,
            HalfCarry = false,
            Carry = false
        };
        const ushort expectedProgramCounter = 0x0001;

        _instructions!.DecrementB(_registers!);
        Assert.That(_b, Is.EqualTo(expectedB));
        Assert.That(_flagsRegister, Is.EqualTo(expectedFlagsRegister));
        Assert.That(_programCounter,Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void DecrementBHalfCarry()
    {
        _b = 0x10;
        const int expectedB = 0x0F;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = true,
            HalfCarry = true,
            Carry = false
        };
        const ushort expectedProgramCounter = 0x0001;

        _instructions!.DecrementB(_registers!);
        Assert.That(_b, Is.EqualTo(expectedB));
        Assert.That(_flagsRegister, Is.EqualTo(expectedFlagsRegister));
        Assert.That(_programCounter,Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void DecrementBUnderflow()
    {
        _b = 0x00;
        const int expectedB = 0xFF;
        var expectedFlagsRegister = new FlagsRegister()
        {
            Zero = false,
            Subtract = true,
            HalfCarry = true,
            Carry = false
        };
        const ushort expectedProgramCounter = 0x0001;

        _instructions!.DecrementB(_registers!);
        Assert.That(_b, Is.EqualTo(expectedB));
        Assert.That(_flagsRegister, Is.EqualTo(expectedFlagsRegister));
        Assert.That(_programCounter,Is.EqualTo(expectedProgramCounter));
    }
}