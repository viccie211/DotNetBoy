using DotNetBoy.Emulator.Enums;
using DotNetBoy.Emulator.Models;
using DotNetBoy.Emulator.Services.Interfaces;

public class PpuService : IPpuService
{
    private const int SCREEN_WIDTH = 160;
    private const int SCREEN_HEIGHT = 144;
    private const ushort LCD_CONTROL_REGISTER_ADDRESS = 0xFF40;
    
    private readonly IClockService _clockService;
    private readonly IMmuService _mmuService;
    
    public PpuModes Mode { get; internal set; }
    public int ScanLine { get; set; } = 0x90;
    public int Dot { get; set; } = 0;
    private LcdControlRegister LcdControlRegister => _mmuService.ReadByte(LCD_CONTROL_REGISTER_ADDRESS);

    public PpuService(IClockService clockService, IMmuService mmuService)
    {
        _clockService = clockService;
        _mmuService = mmuService;
        _clockService.MClock += OnMClock;
    }

    public void OnMClock(object? sender, ClockEventArgs e)
    {
        Dot = Dot == 455 ? 0 : Dot + 1;

        if (ScanLine > 143)
        {
            Mode = PpuModes.VerticalBlank;
        }
        else
        {
            if (Dot < 79)
                Mode = PpuModes.OAMSearch;
            else if (Dot < 257) //TODO: Implement variability per sprite
            {
                Mode = PpuModes.ActivePicture;
            }
            else
            {
                Mode = PpuModes.HorizontalBlank;
            }
        }


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