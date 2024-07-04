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

    private LcdStatusRegister LcdStatusRegister
    {
        get => _mmuService.ReadByte(AddressConsts.LCD_STATUS_REGISTER_ADDRESS);
        set => _mmuService.WriteByte(AddressConsts.LCD_STATUS_REGISTER_ADDRESS, value);
    }

    private byte LyRegister
    {
        get => _mmuService.ReadByte(AddressConsts.LY_REGISTER_ADDRESS);
        set => _mmuService.WriteByte(AddressConsts.LY_REGISTER_ADDRESS, value);
    }

    private byte LycRegister => _mmuService.ReadByte(AddressConsts.LYC_REGISTER_ADDRESS);


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
        var stat = LcdStatusRegister.Clone();
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
                if (Dot == 0) ScanOAM();
            }
            else if (Dot < 252)
            {
                var screenX = Dot - 80;
                RenderBackgroundAndWindow(screenX);
                RenderSprites(screenX);

                Mode = PpuModes.ActivePicture;
            }
            else
            {
                Mode = PpuModes.HorizontalBlank;
            }
        }

        stat.PpuMode = Mode;


        if (Dot == 0)
        {
            _oamObjectsThisScanLine = new();
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


            LyRegister = (byte)ScanLine;
            stat.LycEqualsLy = LycRegister == LyRegister;
        }

        LcdStatusRegister = stat;
        InterruptRegister interruptRegister = _mmuService.ReadByte(AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS);
        interruptRegister.LCD =
            stat.LycIntSelect && stat.LycEqualsLy
            || stat.Mode0IntSelect && Mode == PpuModes.HorizontalBlank
            || stat.Mode1IntSelect && Mode == PpuModes.VerticalBlank
            || stat.Mode2IntSelect && Mode == PpuModes.OAMSearch;
        _mmuService.WriteByte(AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS, interruptRegister);
    }

    public event VBlankStart VBlankStart;

    protected virtual void OnVBlankStart(object? sender, EventArgs e)
    {
        InterruptRegister interruptRegister = _mmuService.ReadByte(AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS);
        interruptRegister.VBlank = true;
        _mmuService.WriteByte(AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS, interruptRegister);
        VBlankStart?.Invoke(sender, e);
    }


    private void ScanOAM()
    {
        _oamObjectsThisScanLine = [];
        int spriteHeight = LcdControlRegister.SpriteSize ? 16 : 8;


        for (int i = 0; i < 40; i++)
        {
            var currentOamObject = OamObjects[i];
            int spriteY = currentOamObject.YPosition;

            if (ScanLine >= spriteY && ScanLine < spriteY + spriteHeight)
            {
                _oamObjectsThisScanLine.Add(currentOamObject);

                if (_oamObjectsThisScanLine.Count == 10) break; // Max 10 sprites per scanline
            }
        }
    }

    private void RenderBackgroundAndWindow(int screenX)
    {
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
            var colorIndex = _tileService.GetTilePixel(tileMap, tileSet, tileX, tileY, tilePixelX, tilePixelY);
            FrameBuffer[ScanLine, screenX] = ColorPaletteRegister.Colors[colorIndex];
        }
    }

    private void RenderSprites(int screenX)
    {
        if (!LcdControlRegister.SpriteDisplayEnable) return;

        foreach (var sprite in _oamObjectsThisScanLine.OrderByDescending(s => s.XPosition))
        {
            if (screenX >= sprite.XPosition && screenX < sprite.XPosition + 8)
            {
                int tilePixelX = screenX - sprite.XPosition;
                int tilePixelY = ScanLine - sprite.YPosition;

                if ((sprite.Flags & 0x40) != 0) tilePixelX = 7 - tilePixelX; // X flip
                if ((sprite.Flags & 0x80) != 0) tilePixelY = 7 - tilePixelY; // Y flip

                int colorIndex = _tileService.GetSpritePixel(sprite.TileIndex, tilePixelX, tilePixelY);

                if (colorIndex != 0) // 0 is transparent for sprites
                {
                    bool bgPriority = (sprite.Flags & 0x80) != 0;
                    if (!bgPriority || FrameBuffer[ScanLine, screenX] == 0)
                    {
                        byte paletteRegister = (sprite.Flags & 0x10) != 0 ? _mmuService.ReadByte(0xFF49) : _mmuService.ReadByte(0xFF48);
                        int color = (paletteRegister >> (colorIndex * 2)) & 0x03;
                        FrameBuffer[ScanLine, screenX] = color;
                    }
                }
            }
        }
    }
}