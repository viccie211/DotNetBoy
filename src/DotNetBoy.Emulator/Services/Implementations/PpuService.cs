using DotNetBoy.Emulator.Services.Interfaces;

public class PpuService : IPpuService
{
    private readonly IClockService _clockService;
    private readonly IMmuService _mmuService;
    public int ScanLine { get; set; } = 90;
    public int Dot { get; set; } = 0;

    // //M-Clock should be devided into two for the Ppu Clock So we just flip this bool every clock and only act when true
    // public bool shouldActOnMClock {get;set;} = false;

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
            ScanLine = ScanLine == 153 ? 0 : ScanLine + 1;
            _mmuService.WriteByte(0xff44, (byte)ScanLine);
        }
    }
}