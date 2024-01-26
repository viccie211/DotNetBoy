using DotNetBoy.Emulator.InstructionSet;

namespace DotNetBoy.Emulator.Tests.InstructionTests;

public class JumpInstructionTests
{
    private JumpInstructions? _instructions;
    private FlagsRegister _flagsRegister = 0x00;
    private ushort _programCounter = 0x0000;
    private ushort _stackPointer = 0xFFFE;
    private Mock<IMmuService> _mmuServiceMock;

    private ICpuRegistersService?_registers;

    [SetUp]
    public void SetUp()
    {
        var clockServiceMock = new Mock<ClockService>();
        _mmuServiceMock = new Mock<IMmuService>();
        var byteUshortServiceMock = new Mock<IByteUshortService>();
        byteUshortServiceMock.Setup(b => b.LowerByteOfSixteenBits(0x0023)).Returns(0x23);
        byteUshortServiceMock.Setup(b => b.UpperByteOfSixteenBits(0x0023)).Returns(0x00);
        byteUshortServiceMock.Setup(b => b.CombineBytes(0x00, 0x23)).Returns(0x0023);

        _mmuServiceMock.Setup(m => m.ReadWordLittleEndian(0x0001)).Returns(0xFFAA);
        _mmuServiceMock.Setup(m => m.ReadByte(0x0001)).Returns(0x08);
        _mmuServiceMock.Setup(m => m.ReadByte(0x0010)).Returns(0xFF);
        _mmuServiceMock.Setup(m => m.ReadByte(0xFFFD)).Returns(0x00);
        _mmuServiceMock.Setup(m => m.ReadByte(0xFFFC)).Returns(0x23);
        _mmuServiceMock.Setup(m => m.ReadWordLittleEndian(0x0021)).Returns(0xCCDD);
        _mmuServiceMock.Setup(m => m.ReadWordLittleEndian(0x0023)).Returns(0xABCD);
        

        _instructions = new JumpInstructions(_mmuServiceMock.Object, clockServiceMock.Object, byteUshortServiceMock.Object);

        _flagsRegister = new FlagsRegister();
        var registerServiceMock = new Mock<ICpuRegistersService>();
        registerServiceMock.Setup(c => c.F).Returns(_flagsRegister);
        
        _programCounter = 0;
        registerServiceMock.SetupGet(c => c.ProgramCounter).Returns(() => _programCounter);
        registerServiceMock.SetupSet(c => c.ProgramCounter = It.IsAny<ushort>()).Callback<ushort>(value => _programCounter = value);
        
        _stackPointer = 0xFFFE;
        registerServiceMock.SetupGet(c => c.StackPointer).Returns(() => _stackPointer);
        registerServiceMock.SetupSet(c => c.StackPointer = It.IsAny<ushort>()).Callback<ushort>(value => _stackPointer = value);
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

    [Test]
    public void CallA16()
    {
        _programCounter = 0x0020;
        var writtenStackUpper = false;
        var writtenStackLower = false;
        const ushort expectedProgramCounter = 0xCCDD;
        const ushort expectedStackPointer = 0xFFFC;
        _mmuServiceMock.Setup(m => m.WriteByte(0xFFFD, 0x00)).Callback(() => writtenStackUpper = true);
        _mmuServiceMock.Setup(m => m.WriteByte(0xFFFC, 0x23)).Callback(() => writtenStackLower = true);
        _instructions!.CallA16(_registers!);
        Assert.That(_programCounter,Is.EqualTo(expectedProgramCounter));
        Assert.That(_stackPointer,Is.EqualTo(expectedStackPointer));
        Assert.That(writtenStackUpper,Is.True);
        Assert.That(writtenStackLower,Is.True);
    }
    
    [Test]
    public void Return()
    {
        _programCounter = 0xCCDD;
        _stackPointer = 0xFFFC;
        const ushort expectedProgramCounter = 0x0023;
        const ushort expectedStackPointer = 0xFFFE;
        _instructions!.Return(_registers!);
        Assert.That(_programCounter,Is.EqualTo(expectedProgramCounter));
        Assert.That(_stackPointer,Is.EqualTo(expectedStackPointer));
    }
}