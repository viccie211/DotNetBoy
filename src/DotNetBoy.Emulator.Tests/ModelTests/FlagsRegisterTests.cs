namespace DotNetBoy.Emulator.Tests.ModelTests;

public class FlagsRegisterTests
{
    [Test]
    public void ImplicitOperatorFromByte()
    {
        byte carry = 0x10;
        byte halfCarry = 0x20;
        byte subtract = 0x40;
        byte zero = 0x80;
        byte all = 0xF0;

        var expectedCarry = new FlagsRegister
        {
            Carry = true,
            HalfCarry = false,
            Subtract = false,
            Zero = false,
        };
        FlagsRegister actualCarry = carry;
        Assert.That(actualCarry.Carry, Is.EqualTo(expectedCarry.Carry));
        Assert.That(actualCarry.HalfCarry, Is.EqualTo(expectedCarry.HalfCarry));
        Assert.That(actualCarry.Subtract, Is.EqualTo(expectedCarry.Subtract));
        Assert.That(actualCarry.Zero, Is.EqualTo(expectedCarry.Zero));
        Assert.That(actualCarry,Is.EqualTo(expectedCarry));

        var expectedHalfCarry = new FlagsRegister
        {
            Carry = false,
            HalfCarry = true,
            Subtract = false,
            Zero = false,
        };
        FlagsRegister actualHalfCarry = halfCarry;
        Assert.That(actualHalfCarry.Carry, Is.EqualTo(expectedHalfCarry.Carry));
        Assert.That(actualHalfCarry.HalfCarry, Is.EqualTo(expectedHalfCarry.HalfCarry));
        Assert.That(actualHalfCarry.Subtract, Is.EqualTo(expectedHalfCarry.Subtract));
        Assert.That(actualHalfCarry.Zero, Is.EqualTo(expectedHalfCarry.Zero));
        Assert.That(actualHalfCarry,Is.EqualTo(expectedHalfCarry));

        var expectedSubtract = new FlagsRegister
        {
            Carry = false,
            HalfCarry = false,
            Subtract = true,
            Zero = false,
        };
        FlagsRegister actualSubtract = subtract;
        Assert.That(actualSubtract.Carry, Is.EqualTo(expectedSubtract.Carry));
        Assert.That(actualSubtract.HalfCarry, Is.EqualTo(expectedSubtract.HalfCarry));
        Assert.That(actualSubtract.Subtract, Is.EqualTo(expectedSubtract.Subtract));
        Assert.That(actualSubtract.Zero, Is.EqualTo(expectedSubtract.Zero));
        Assert.That(actualSubtract,Is.EqualTo(expectedSubtract));

        var expectedZero = new FlagsRegister
        {
            Carry = false,
            HalfCarry = false,
            Subtract = false,
            Zero = true,
        };
        FlagsRegister actualZero = zero;
        Assert.That(actualZero.Carry, Is.EqualTo(expectedZero.Carry));
        Assert.That(actualZero.HalfCarry, Is.EqualTo(expectedZero.HalfCarry));
        Assert.That(actualZero.Subtract, Is.EqualTo(expectedZero.Subtract));
        Assert.That(actualZero.Zero, Is.EqualTo(expectedZero.Zero));
        Assert.That(actualZero,Is.EqualTo(expectedZero));

        var expectedAll = new FlagsRegister
        {
            Carry = true,
            HalfCarry = true,
            Subtract = true,
            Zero = true,
        };
        FlagsRegister actualAll = all;
        Assert.That(actualAll.Carry, Is.EqualTo(expectedAll.Carry));
        Assert.That(actualAll.HalfCarry, Is.EqualTo(expectedAll.HalfCarry));
        Assert.That(actualAll.Subtract, Is.EqualTo(expectedAll.Subtract));
        Assert.That(actualAll.Zero, Is.EqualTo(expectedAll.Zero));
    }

    [Test]
    public void ImplicitOperatorToByte()
    {
        var carry = new FlagsRegister
        {
            Carry = true,
            HalfCarry = false,
            Subtract = false,
            Zero = false,
        };
        byte expectedCarry = 0x10;
        Assert.That((byte)carry, Is.EqualTo(expectedCarry));

        var halfCarry = new FlagsRegister
        {
            Carry = false,
            HalfCarry = true,
            Subtract = false,
            Zero = false,
        };
        byte expectedHalfCarry = 0x20;
        Assert.That((byte)halfCarry, Is.EqualTo(expectedHalfCarry));

        var subtract = new FlagsRegister
        {
            Carry = false,
            HalfCarry = false,
            Subtract = true,
            Zero = false,
        };
        byte expectedSubtract = 0x40;
        Assert.That((byte)subtract, Is.EqualTo(expectedSubtract));

        var zero = new FlagsRegister
        {
            Carry = false,
            HalfCarry = false,
            Subtract = false,
            Zero = true,
        };
        byte expectedZero = 0x80;
        Assert.That((byte)zero, Is.EqualTo(expectedZero));

        var all = new FlagsRegister
        {
            Carry = true,
            HalfCarry = true,
            Subtract = true,
            Zero = true,
        };
        byte expectedAll = 0xF0;
        Assert.That((byte)all, Is.EqualTo(expectedAll));
    }
}