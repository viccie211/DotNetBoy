using System.Diagnostics;
using DotNetBoy.Emulator;
using DotNetBoy.Emulator.Consts;
using DotNetBoy.Emulator.Enums;
using DotNetBoy.Emulator.Models;
using DotNetBoy.Emulator.Services.Interfaces;

public class PpuService : IPpuService
{
    private const int TILE_SIZE = 8;

    private readonly IClockService _clockService;
    private readonly IMmuService _mmuService;
    private readonly ITileService _tileService;

    private LcdControlRegister LcdControlRegister => _mmuService.ReadByte(AddressConsts.LCD_CONTROL_REGISTER_ADDRESS);

    private ColorPaletteRegister ColorPaletteRegister =>
        _mmuService.ReadByte(AddressConsts.COLOR_PALETTE_REGISTER_ADDRESS);

    private byte ScrollY => _mmuService.ReadByte(AddressConsts.SCY_ADDRESS);
    private byte ScrollX => _mmuService.ReadByte(AddressConsts.SCX_ADDRESS);

    private byte WindowY => _mmuService.ReadByte(AddressConsts.WY_ADDRESS);
    private byte WindowX => _mmuService.ReadByte(AddressConsts.WX_ADDRESS);
    private OamObject[] OamObjects => _mmuService.GetOamObjects();
    private List<OamObject> _oamObjectsThisScanLine;
    private int oamPenalty;

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
            if (Dot < 80)
            {
                Mode = PpuModes.OAMSearch;
                if (_oamObjectsThisScanLine.Count < 10)
                {
                    var currentOamObject = OamObjects[Dot / 2];
                    var spriteHeight = LcdControlRegister.SpriteSize ? 16 : 8;
                    if (ScanLine > currentOamObject.YPosition &&
                        ScanLine < currentOamObject.YPosition + spriteHeight)
                    {
                        _oamObjectsThisScanLine.Add(currentOamObject);
                    }
                }
            }
            else if (Dot < 252 + oamPenalty)
            {
                var screenX = Dot - 80;

                var inWindow = LcdControlRegister.WindowDisplayEnable && WindowX < screenX && WindowY < ScanLine;

                var tileMap = (inWindow && LcdControlRegister.WindowTileMapDisplaySelect) ||
                              LcdControlRegister.BackgroundTileMapDisplaySelect
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
            _oamObjectsThisScanLine = new();
            oamPenalty = 0;
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
        InterruptRegister interruptRegister = _mmuService.ReadByte(AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS);
        interruptRegister.VBlank = true;
        _mmuService.WriteByte(AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS, interruptRegister);
        VBlankStart?.Invoke(sender, e);
    }
}