using DotNetBoy.Emulator.Services.Implementations;

namespace DotNetBoy.Emulator.Tests.ServiceTests;

public class ClockServiceTests
{
    [Test]
    public void Reset()
    {
        var mmuServiceMock = new Mock<IMmuService>();
        var timerserviceMock = new Mock<ITimerService>();
        var eventServiceMock = new Mock<IEventService>();
        var service = new ClockService(mmuServiceMock.Object,timerserviceMock.Object,eventServiceMock.Object)
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
}