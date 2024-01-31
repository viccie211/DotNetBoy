using DotNetBoy.Emulator.Services.Implementations;
namespace DotNetBoy.Emulator.Tests.ServiceTests;
public class CpuRegistersServiceTests
{
    private CpuRegistersService service;
    private Mock<IByteUshortService> byteUshortServiceMock;

    [SetUp]
    public void SetUp()
    {
        byteUshortServiceMock = new Mock<IByteUshortService>();
        byteUshortServiceMock.Setup(b => b.CombineBytes(0xAA, 0xF0)).Returns(0xAAF0);
        byteUshortServiceMock.Setup(b => b.LowerByteOfSixteenBits(0xAAF0)).Returns(0xF0);
        byteUshortServiceMock.Setup(b => b.UpperByteOfSixteenBits(0xAAF0)).Returns(0xAA);
        service = new CpuRegistersService(byteUshortServiceMock.Object);
    }

    [Test]
    public void Reset()
    {
        byte expectedA = 0x11;
        byte expectedB = 0;
        byte expectedC = 0;
        byte expectedD = 0xFF;
        byte expectedE = 0X56;
        byte expectedFByte = 0x80;
        FlagsRegister expectedF = new FlagsRegister
        {
            Zero = true,
            HalfCarry = false,
            Carry = false,
            Subtract = false,
        };
        byte expectedH = 0;
        byte expectedL = 0x0D;
        ushort expectedProgramCounter = 0x100;
        ushort expectedStackPointer = 0xFFFE;
        service.Reset();
        Assert.That(service.A, Is.EqualTo(expectedA));
        Assert.That(service.B, Is.EqualTo(expectedB));
        Assert.That(service.C, Is.EqualTo(expectedC));
        Assert.That(service.D, Is.EqualTo(expectedD));
        Assert.That(service.E, Is.EqualTo(expectedE));
        Assert.That((byte)service.F, Is.EqualTo(expectedFByte));
        Assert.That(service.F.Zero, Is.EqualTo(expectedF.Zero));
        Assert.That(service.F.HalfCarry, Is.EqualTo(expectedF.HalfCarry));
        Assert.That(service.F.Carry, Is.EqualTo(expectedF.Carry));
        Assert.That(service.F.Subtract, Is.EqualTo(expectedF.Subtract));
        Assert.That(service.H, Is.EqualTo(expectedH));
        Assert.That(service.L, Is.EqualTo(expectedL));
        Assert.That(service.ProgramCounter, Is.EqualTo(expectedProgramCounter));
        Assert.That(service.StackPointer, Is.EqualTo(expectedStackPointer));
    }

    [Test]
    public void SixteenBitRegisterViewsSet()
    {
        var setValue = (ushort)0xAAF0;
        var expectedSixteenBits = 0xAAF0;
        var expectedUpper = 0xAA;
        var expectedLower = 0xF0;

        service.AF = setValue;
        Assert.That(service.AF, Is.EqualTo(expectedSixteenBits));
        Assert.That(service.A, Is.EqualTo(expectedUpper));
        Assert.That((byte)service.F, Is.EqualTo(expectedLower));

        service.BC = setValue;
        Assert.That(service.BC, Is.EqualTo(expectedSixteenBits));
        Assert.That(service.B, Is.EqualTo(expectedUpper));
        Assert.That(service.C, Is.EqualTo(expectedLower));

        service.DE = setValue;
        Assert.That(service.DE, Is.EqualTo(expectedSixteenBits));
        Assert.That(service.D, Is.EqualTo(expectedUpper));
        Assert.That(service.E, Is.EqualTo(expectedLower));

        service.HL = setValue;
        Assert.That(service.HL, Is.EqualTo(expectedSixteenBits));
        Assert.That(service.H, Is.EqualTo(expectedUpper));
        Assert.That(service.L, Is.EqualTo(expectedLower));
    }

    [Test]
    public void SixteenBitRegisterViewsGet()
    {
        var upper = (byte)0xAA;
        var lower = (byte)0xF0;
        var expected = 0xAAF0;

        service.A = upper;
        service.F = lower;
        var actualAF = service.AF;
        Assert.That(actualAF, Is.EqualTo(expected));

        service.B = upper;
        service.C = lower;
        var actualBC = service.BC;
        Assert.That(actualBC, Is.EqualTo(expected));

        service.D = upper;
        service.E = lower;
        var actualDE = service.DE;
        Assert.That(actualDE, Is.EqualTo(expected));

        service.H = upper;
        service.L = lower;
        var actualHL = service.HL;
        Assert.That(actualHL, Is.EqualTo(expected));
    }
}