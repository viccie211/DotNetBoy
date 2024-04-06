using DotNetBoy.Emulator.Services.Implementations;

namespace DotNetBoy.Emulator.Tests.ServiceTests;

public class ClockServiceTests
{
    [Test]
    public void Reset()
    {
        var mmuServiceMock = new Mock<IMmuService>();
        var service = new ClockService(mmuServiceMock.Object)
        {
            T = 1,
            M = 1
        };

        byte expectedT = 0;
        byte expectedM = 0;

        service.Reset();

        Assert.That(service.T, Is.EqualTo(expectedT));
        Assert.That(service.M, Is.EqualTo(expectedM));
    }

    [Test]
    public void Clock()
    {
        var mmuServiceMock = new Mock<IMmuService>();
        var service = new ClockService(mmuServiceMock.Object);

        var tClockEventCalled = 0;
        service.TClock += (o, e) => tClockEventCalled++;

        var mClockEventCalled = 0;
        service.MClock += (o, e) => mClockEventCalled++;

        var expectedT = 1;
        var expectedM = 4;

        service.Clock();

        Assert.That(tClockEventCalled, Is.EqualTo(expectedT));
        Assert.That(service.T, Is.EqualTo(expectedT));

        Assert.That(mClockEventCalled, Is.EqualTo(expectedM));
        Assert.That(service.M, Is.EqualTo(expectedM));
    }

    [Test]
    public void Clock4Increments()
    {
        var clockIncrement = 4;
        var mmuServiceMock = new Mock<IMmuService>();
        var service = new ClockService(mmuServiceMock.Object);

        var tClockEventCalled = 0;
        service.TClock += (o, e) => tClockEventCalled++;

        var mClockEventCalled = 0;
        service.MClock += (o, e) => mClockEventCalled++;

        var expectedT = 1 * clockIncrement;
        var expectedM = 4 * clockIncrement;

        service.Clock(clockIncrement);

        Assert.That(tClockEventCalled, Is.EqualTo(expectedT));
        Assert.That(service.T, Is.EqualTo(expectedT));

        Assert.That(mClockEventCalled, Is.EqualTo(expectedM));
        Assert.That(service.M, Is.EqualTo(expectedM));
    }
}