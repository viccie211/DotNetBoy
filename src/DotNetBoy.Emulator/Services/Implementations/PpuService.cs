using System.Diagnostics;
using DotNetBoy.Emulator.Consts;
using DotNetBoy.Emulator.Enums;
using DotNetBoy.Emulator.Models;
using DotNetBoy.Emulator.Services.Interfaces;

public class PpuService : IPpuService
{
    private const int TILE_SIZE = 8;
    private const ushort LCD_CONTROL_REGISTER_ADDRESS = 0xFF40;
    private const ushort COLOR_PALETTE_REGISTER_ADDRESS = 0xFF47;
    private const ushort SCY_ADDRESS = 0xFF42;
    private const ushort SCX_ADDRESS = 0xFF43;

    private readonly IClockService _clockService;
    private readonly IMmuService _mmuService;
    private readonly ITileService _tileService;

    private LcdControlRegister LcdControlRegister => _mmuService.ReadByte(LCD_CONTROL_REGISTER_ADDRESS);
    private ColorPaletteRegister ColorPaletteRegister => _mmuService.ReadByte(COLOR_PALETTE_REGISTER_ADDRESS);
    private byte ScrollY => _mmuService.ReadByte(SCY_ADDRESS);
    private byte ScrollX => _mmuService.ReadByte(SCX_ADDRESS);

    public PpuModes Mode { get; internal set; }
    public int ScanLine { get; set; } = 0x90;
    public int Dot { get; set; } = 0;
    public int[,] FrameBuffer { get; private set; }


    public PpuService(IClockService clockService, IMmuService mmuService, ITileService tileService)
    {
        _clockService = clockService;
        _mmuService = mmuService;
        _tileService = tileService;
        _clockService.MClock += OnMClock;
        FrameBuffer = new int[ScreenDimensions.HEIGHT, ScreenDimensions.WIDTH];
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
            else if (Dot < 370) //TODO: Implement variability per sprite
            {
                var screenX = Dot - 79;
                var tileMap = LcdControlRegister.BackgroundTileMapDisplaySelect
                    ? ETileMap.TileMap1
                    : ETileMap.TileMap0;
                var tileSet = LcdControlRegister.BackgroundWindowTileDataSelect
                    ? ETileSet.TileSet1
                    : ETileSet.TileSet0;
                var tileX = (byte)(screenX + ScrollX) / TILE_SIZE;
                var tilePixelX = ((screenX + ScrollX) % TILE_SIZE);
                var tileY = (byte)((byte)(ScanLine + ScrollY) / TILE_SIZE);
                var tilePixelY = (ScanLine + ScrollY) % TILE_SIZE;

                if (ScanLine < ScreenDimensions.HEIGHT && screenX < ScreenDimensions.WIDTH)
                {
                    var colorIndex = _tileService.GetPixel(tileMap, tileSet, tileX, tileY, tilePixelX, tilePixelY);
                    FrameBuffer[ScanLine, screenX] = ColorPaletteRegister.Colors[colorIndex];
                }
                    
                

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

            if (ScanLine == 153)
            {
                ScanLine = 0;
                FrameBuffer = new int[ScreenDimensions.HEIGHT, ScreenDimensions.WIDTH];
            }
            else
            {
                ScanLine += 1;
            }

            _mmuService.WriteByte(0xff44, (byte)ScanLine);
        }
    }

    public event VBlankStart VBlankStart;

    protected virtual void OnVBlankStart(object? sender, EventArgs e)
    {
        VBlankStart?.Invoke(sender, e);
    }
}