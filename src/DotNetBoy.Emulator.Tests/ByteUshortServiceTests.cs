using DotNetBoy.Emulator.Services.Implementations;

namespace DotNetBoy.Emulator.Tests;

public class ByteUshortServiceTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CombineBytesBoth0()
    {
        var service = new ByteUshortService();
        byte lower = 0;
        byte upper = 0;
        ushort expected = 0;
        var result = service.CombineBytes(upper, lower);
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public void CombineBytesLower1Upper0()
    {
        var service = new ByteUshortService();
        byte lower = 1;
        byte upper = 0;
        ushort expected = 1;
        var result = service.CombineBytes(upper, lower);
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public void CombineBytesLower0UpperFF()
    {
        var service = new ByteUshortService();
        byte lower = 0;
        byte upper = 0xFF;
        ushort expected = 0xFF00;
        var result = service.CombineBytes(upper, lower);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void LowerByteOfSixteenBits0()
    {
        var service = new ByteUshortService();
        ushort source = 0;
        var expected = 0;
        var result = service.LowerByteOfSixteenBits(source);
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public void LowerByteOfSixteenBitsFF()
    {
        var service = new ByteUshortService();
        ushort source = 0x00FF;
        var expected = 0xFF;
        var result = service.LowerByteOfSixteenBits(source);
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public void LowerByteOfSixteenBitsOnlyLowerByte()
    {
        var service = new ByteUshortService();
        ushort source = 0xFF01;
        var expected = 1;
        var result = service.LowerByteOfSixteenBits(source);
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public void UpperByteOfSixteenBits0()
    {
        var service = new ByteUshortService();
        ushort source = 0;
        var expected = 0;
        var result = service.UpperByteOfSixteenBits(source);
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public void UpperByteOfSixteenBitsFF()
    {
        var service = new ByteUshortService();
        ushort source = 0xFF00;
        var expected = 0xFF;
        var result = service.UpperByteOfSixteenBits(source);
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public void UpperByteOfSixteenBitsOnlyUpperByte()
    {
        var service = new ByteUshortService();
        ushort source = 0x01FF;
        var expected = 0x01;
        var result = service.UpperByteOfSixteenBits(source);
        Assert.That(result, Is.EqualTo(expected));
    }
    
}