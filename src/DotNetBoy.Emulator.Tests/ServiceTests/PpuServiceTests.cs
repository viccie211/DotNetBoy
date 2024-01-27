namespace DotNetBoy.Emulator.Tests.ServiceTests;
public class PpuServiceTests
{
    private PpuService service;
    private Mock<IMmuService> mmuServiceMock;

    [SetUp]
    public void SetUp()
    {
        var clockServiceMock = new Mock<IClockService>();
        mmuServiceMock = new Mock<IMmuService>();
        service = new PpuService(clockServiceMock.Object, mmuServiceMock.Object);
    }

    [Test]
    public void DividesMClockInHalf()
    {
        var expectedShouldActOnMClock0 = false;
        var shouldActOnMClock0 = service.shouldActOnMClock;
        Assert.That(shouldActOnMClock0, Is.EqualTo(expectedShouldActOnMClock0));

        service.OnMClock(null, new ClockEventArgs());
        var expectedShouldActOnMClock1 = true;
        var shouldActOnMClock1 = service.shouldActOnMClock;
        Assert.That(shouldActOnMClock1, Is.EqualTo(expectedShouldActOnMClock1));

        service.OnMClock(null, new ClockEventArgs());
        var expectedShouldActOnMClock2 = false;
        var shouldActOnMClock2 = service.shouldActOnMClock;
        Assert.That(shouldActOnMClock2, Is.EqualTo(expectedShouldActOnMClock2));
    }

    [Test]
    public void IncrementsScanLineOnClockWhenDot455()
    {
        var calledWriteByte = false;
        mmuServiceMock.Setup(m => m.WriteByte(0xff44, 0x01)).Callback(() => calledWriteByte = true);
        var expectedScanLine = 1;

        service.Dot = 455;
        service.ScanLine = 0;
        service.shouldActOnMClock = true;
        service.OnMClock(null, new ClockEventArgs());

        Assert.That(service.ScanLine, Is.EqualTo(expectedScanLine));
        Assert.IsTrue(calledWriteByte);
    }

    [Test]
    public void RollsOverScanLine()
    {
        
        bool calledWriteByte = false;
        mmuServiceMock.Setup(m => m.WriteByte(0xff44, 0x00)).Callback(() => calledWriteByte = true);
        var expectedScanLine = 0;

        service.ScanLine = 153;
        service.Dot = 455;
        service.shouldActOnMClock = true;
        service.OnMClock(null, new ClockEventArgs());

        Assert.IsTrue(calledWriteByte);
        Assert.That(service.ScanLine, Is.EqualTo(expectedScanLine));
    }
}