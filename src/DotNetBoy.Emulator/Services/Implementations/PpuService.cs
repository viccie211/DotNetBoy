using DotNetBoy.Emulator.Services.Interfaces;

public class PpuService : IPpuService
{
    private readonly IClockService _clockService;
    private readonly IMmuService _mmuService;
    private int scanLine = 0;
    private int dot = 0;

    //T-Clock should be devided into two for the Ppu Clock So we just flip this bool every clock and only act when true
    private bool shouldActOnTClock = false;

    public PpuService(IClockService clockService, IMmuService mmuService)
    {
        _clockService = clockService;
        _mmuService = mmuService;
        _clockService.TClock += OnTClock;
    }

    public void OnTClock(object? sender, ClockEventArgs e)
    {
        if (shouldActOnTClock)
        {
            dot = dot == 456 ? 0 : dot + 1;
            
            if (dot == 0)
            {
                scanLine = scanLine == 154 ? 0 : scanLine + 1;
                _mmuService.WriteByte(0xff44, (byte)scanLine);
            }
        }
        shouldActOnTClock = !shouldActOnTClock;
    }
}