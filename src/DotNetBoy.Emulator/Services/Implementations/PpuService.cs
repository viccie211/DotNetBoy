using System.Diagnostics;
using DotNetBoy.Emulator.Enums;
using DotNetBoy.Emulator.Models;
using DotNetBoy.Emulator.Services.Interfaces;

public class PpuService : IPpuService
{
    private const int SCREEN_WIDTH = 160;
    private const int SCREEN_HEIGHT = 144;
    private const int TILE_SIZE = 8;
    private const ushort LCD_CONTROL_REGISTER_ADDRESS = 0xFF40;
    private const ushort SCY_ADDRESS = 0xFF42;
    private const ushort SCX_ADDRESS = 0xFF43;

    private readonly IClockService _clockService;
    private readonly IMmuService _mmuService;

    public PpuModes Mode { get; internal set; }
    public int ScanLine { get; set; } = 0x90;
    public int Dot { get; set; } = 0;
    private LcdControlRegister LcdControlRegister => _mmuService.ReadByte(LCD_CONTROL_REGISTER_ADDRESS);
    private byte ScrollY => _mmuService.ReadByte(SCY_ADDRESS);
    private byte ScrollX => _mmuService.ReadByte(SCX_ADDRESS);

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
            else if (Dot < 370) //TODO: Implement variability per sprite
            {
                try
                {

                    var screenX = Dot - 80;
                    var tileY = (byte)((ScanLine + ScrollY) / TILE_SIZE);
                    var yInTile = (ScanLine + ScrollY) % TILE_SIZE;
                    var tileX = (byte)(screenX + ScrollX) / TILE_SIZE;
                    var xInTile = (screenX + ScrollX) % TILE_SIZE;
                    TileSet tileSet = _mmuService.GetTileSet(LcdControlRegister.BackgroundWindowTileDataSelect ? ETileSet.TileSet1 : ETileSet.TileSet0);
                    // TileMap tileMap = _mmuService.GetTileMap(LcdControlRegister.BackgroundTileMapDisplaySelect ? ETileMap.TileMap1 : ETileMap.TileMap0);
                    // var tileNumber = tileMap.Map[tileY, tileX];
                    // var tile = tileSet.Set[tileNumber];
                    // FrameBuffer[ScanLine, screenX] = tile.Pixels[yInTile, xInTile];
                }
                catch (Exception ex)
                {
                    Debug.Write(ex);
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
                FrameBuffer = new int[SCREEN_HEIGHT, SCREEN_WIDTH];
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

    public int[,] FrameBuffer { get; internal set; }
}