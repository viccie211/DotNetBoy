namespace DotNetBoy.Emulator.Tests.InstructionTests.PrefixedInstructions.RotateRightInstructions;

public class RotateDInstructionTests
{
    private ICpuRegistersService _registers;
    private InstructionSet.PrefixedInstructions.ResetBitInstructions.RotateRightInstructions _instructions;

    [SetUp]
    public void SetUp()
    {
        _registers = new TestCpuRegisterService();
        var clockServiceMock = new Mock<IClockService>();
        _instructions = new InstructionSet.PrefixedInstructions.ResetBitInstructions.RotateRightInstructions(clockServiceMock.Object);
    }

    [Test]
    public void RotateD_0x00()
    {
        const byte expectedD = 0x00;
        const ushort expectedProgramCounter = 0x0002;
        var expectedF = new FlagsRegister()
        {
            Zero = true,
            Carry = false,
            HalfCarry = false,
            Subtract = false,
        };
        _registers.D = 0x00;
        _instructions.RotateD(_registers);
        Assert.That(_registers.D, Is.EqualTo(expectedD));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }
    
    [Test]
    public void RotateD_0x00_CarryTrue()
    {
        const byte expectedD = 0x80;
        const ushort expectedProgramCounter = 0x0002;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Carry = false,
            HalfCarry = false,
            Subtract = false,
        };
        _registers.D = 0x00;
        _registers.F.Carry = true;
        _instructions.RotateD(_registers);
        Assert.That(_registers.D, Is.EqualTo(expectedD));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }

    [Test]
    public void RotateD_0x01()
    {
        const byte expectedD = 0x00;
        const ushort expectedProgramCounter = 0x0002;
        var expectedF = new FlagsRegister()
        {
            Zero = true,
            Carry = true,
            HalfCarry = false,
            Subtract = false,
        };
        _registers.D = 0x01;
        _instructions.RotateD(_registers);
        Assert.That(_registers.D, Is.EqualTo(expectedD));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }

    [Test]
    public void RotateD_0xFF()
    {
        const byte expectedD = 0x7F;
        const ushort expectedProgramCounter = 0x0002;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Carry = true,
            HalfCarry = false,
            Subtract = false,
        };
        _registers.D = 0xFF;
        _instructions.RotateD(_registers);
        Assert.That(_registers.D, Is.EqualTo(expectedD));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }

    [Test]
    public void RotateD_0xAA()
    {
        const byte expectedD = 0x55;
        const ushort expectedProgramCounter = 0x0002;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            Carry = false,
            HalfCarry = false,
            Subtract = false,
        };
        _registers.D = 0xAA;
        _instructions.RotateD(_registers);
        Assert.That(_registers.D, Is.EqualTo(expectedD));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
    }

    [Test]
    public void RotateD_EntirelyAroundFromCarry()
    {
        _registers.D = 0x00;
        _registers.F.Carry = true;
        ushort expectedProgramCounter = 0x0000;

        for (int i = 7; i >= -1; i--)
        {
            var expectedD = (byte)(int)Math.Pow(2, i);
            var expectedF = new FlagsRegister()
            {
                Zero = i==-1,
                HalfCarry = false,
                Subtract = false,
                Carry = i==-1,
            };
            _instructions.RotateD(_registers);
            expectedProgramCounter += 2;
            Assert.That(_registers.D, Is.EqualTo(expectedD));
            Assert.That(_registers.F, Is.EqualTo(expectedF));
            Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        }
    }
}