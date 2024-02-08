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
    public void IncrementsScanLineOnClockWhenDot455()
    {
        var calledWriteByte = false;
        mmuServiceMock.Setup(m => m.WriteByte(0xff44, 0x01)).Callback(() => calledWriteByte = true);
        var expectedScanLine = 1;

        service.Dot = 455;
        service.ScanLine = 0;
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
        service.OnMClock(null, new ClockEventArgs());

        Assert.IsTrue(calledWriteByte);
        Assert.That(service.ScanLine, Is.EqualTo(expectedScanLine));
    }
}