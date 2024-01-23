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
}