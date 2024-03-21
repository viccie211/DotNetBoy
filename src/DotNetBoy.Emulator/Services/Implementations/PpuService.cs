using DotNetBoy.Emulator.Services.Interfaces;

public class PpuService : IPpuService
{
    private readonly IClockService _clockService;
    private readonly IMmuService _mmuService;
    public int ScanLine { get; set; } = 0x90;
    public int Dot { get; set; } = 0;

    public PpuService(IClockService clockService, IMmuService mmuService)
    {
        _clockService = clockService;
        _mmuService = mmuService;
        _clockService.MClock += OnMClock;
    }

    public void OnMClock(object? sender, ClockEventArgs e)
    {
        Dot = Dot == 455 ? 0 : Dot + 1;

        if (Dot == 0)
        {
            if (ScanLine == 144)
            {
                OnVBlankStart(this, EventArgs.Empty);
            }

            ScanLine = ScanLine == 153 ? 0 : ScanLine + 1;
            _mmuService.WriteByte(0xff44, (byte)ScanLine);
        }
    }

    public event VBlankStart VBlankStart;

    protected virtual void OnVBlankStart(object? sender, EventArgs e)
    {
        VBlankStart?.Invoke(sender, e);
    }
}