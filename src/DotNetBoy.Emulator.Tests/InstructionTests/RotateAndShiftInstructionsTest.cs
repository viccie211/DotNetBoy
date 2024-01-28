#nullable disable

using DotNetBoy.Emulator.InstructionSet;

namespace DotNetBoy.Emulator.Tests.InstructionTests;

public class RotateAndShiftInstructionsTests
{
    private ICpuRegistersService _registers;
    private RotateAndShiftInstructions _instructions;

    [SetUp]
    public void SetUp()
    {
        var clockServiceMock = new Mock<IClockService>();
        _registers = new TestCpuRegisterService();
        _instructions = new RotateAndShiftInstructions(clockServiceMock.Object);
    }

    [Test]
    public void RotateLeftWithCarryA_0x01()
    {
        const byte expectedA = 0x02;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            HalfCarry = false,
            Subtract = false,
            Carry = false,
        };
        const ushort expectedProgramCounter = 0x0001;

        _registers.A = 0x01;

        _instructions.RotateLeftWithCarryA(_registers);

        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void RotateLeftWithCarryA_0x80()
    {
        const byte expectedA = 0x01;
        var expectedF = new FlagsRegister()
        {
            Zero = false,
            HalfCarry = false,
            Subtract = false,
            Carry = true,
        };
        const ushort expectedProgramCounter = 0x0001;

        _registers.A = 0x80;

        _instructions.RotateLeftWithCarryA(_registers);

        Assert.That(_registers.A, Is.EqualTo(expectedA));
        Assert.That(_registers.F, Is.EqualTo(expectedF));
        Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
    }

    [Test]
    public void RotateLeftWithCarryA_FirstBitEntirelyRound()
    {
        _registers.A = 0x01;
        for (int i = 0; i < 8; i++)
        {
            //We expect the set bit to shift left every loop and on the last loop to immediately jump back to the first bit
            var expectedA = i != 7 ? (byte)(int)Math.Pow(2, i + 1) : 0x01;
            var expectedF = new FlagsRegister()
            {
                Zero = false,
                HalfCarry = false,
                Subtract = false,
                Carry = i == 7,
            };
            var expectedProgramCounter = (ushort)i + 1;
            _instructions.RotateLeftWithCarryA(_registers);
            Assert.That(_registers.A, Is.EqualTo(expectedA));
            Assert.That(_registers.F, Is.EqualTo(expectedF));
            Assert.That(_registers.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        }

    }
}