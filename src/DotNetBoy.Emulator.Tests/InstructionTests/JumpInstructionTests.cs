using DotNetBoy.Emulator.InstructionSet;

namespace DotNetBoy.Emulator.Tests.InstructionTests;

public class JumpInstructionTests
{
    private JumpInstructions? _instructions;
    private FlagsRegister _flagsRegister = 0x00;
    private ushort _programCounter = 0x0000;
    private ICpuRegistersService?_registers;

    [SetUp]
    public void SetUp()
    {
        var clockServiceMock = new Mock<ClockService>();
        var mmuServiceMock = new Mock<IMmuService>();
        var byteUshortServiceMock = new Mock<IByteUshortService>();

        mmuServiceMock.Setup(m => m.ReadWordLittleEndian(0x0001)).Returns(0xFFAA);
        mmuServiceMock.Setup(m => m.ReadByte(0x0001)).Returns(0x08);
        mmuServiceMock.Setup(m => m.ReadByte(0x0010)).Returns(0xFF);

        _instructions = new JumpInstructions(mmuServiceMock.Object, clockServiceMock.Object, byteUshortServiceMock.Object);

        _flagsRegister = new FlagsRegister();
        var registerServiceMock = new Mock<ICpuRegistersService>();
        registerServiceMock.Setup(c => c.F).Returns(_flagsRegister);
        
        _programCounter = 0;
        registerServiceMock.SetupGet(c => c.ProgramCounter).Returns(() => _programCounter);
        registerServiceMock.SetupSet(c => c.ProgramCounter = It.IsAny<ushort>()).Callback<ushort>(value => _programCounter = value);
        _registers = registerServiceMock.Object;
    }

    [Test]
    public void Jump()
    {
        const ushort expectedProgramCounter = 0xFFAA;
        _instructions!.Jump(_registers!);
        Assert.That(_programCounter,Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void JumpRelative8BitsIfNonZeroZero()
    {
        const ushort expectedProgramCounter = 0x0002;
        _flagsRegister.Zero = true;
        _instructions!.JumpRelative8BitsIfNotZero(_registers!);
        Assert.That(_programCounter,Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void JumpRelative8BitsIfNonZeroForwardNonZero()
    {
        const ushort expectedProgramCounter = 0x0008;
        _flagsRegister.Zero = false;
        _instructions!.JumpRelative8BitsIfNotZero(_registers!);
        Assert.That(_programCounter,Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void JumpRelative8BitsIfNonZeroBackwardNonZero()
    {
        const ushort expectedProgramCounter = 0x000E;
        _programCounter = 0x000F;
        _flagsRegister.Zero = false;
        _instructions!.JumpRelative8BitsIfNotZero(_registers!);
        Assert.That(_programCounter,Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void JumpRelative8BitsIfZeroNonZero()
    {
        const ushort expectedProgramCounter = 0x0002;
        _flagsRegister.Zero = false;
        _instructions!.JumpRelative8BitsIfZero(_registers!);
        Assert.That(_programCounter,Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void JumpRelative8BitsIfZeroForwardZero()
    {
        const ushort expectedProgramCounter = 0x0008;
        _flagsRegister.Zero = true;
        _instructions!.JumpRelative8BitsIfZero(_registers!);
        Assert.That(_programCounter,Is.EqualTo(expectedProgramCounter));
    }
    
    [Test]
    public void JumpRelative8BitsIfZeroBackwardZero()
    {
        const ushort expectedProgramCounter = 0x000E;
        _programCounter = 0x000F;
        _flagsRegister.Zero = true;
        _instructions!.JumpRelative8BitsIfZero(_registers!);
        Assert.That(_programCounter,Is.EqualTo(expectedProgramCounter));
    }

}