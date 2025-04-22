using DotNetBoy.Emulator.Consts;
using DotNetBoy.Emulator.Enums;
using DotNetBoy.Emulator.Events;
using DotNetBoy.Emulator.Models;
using DotNetBoy.Emulator.Services.Interfaces;

public class PpuService : IPpuService
{
    private const int TILE_SIZE = 8;
    private readonly IMmuService _mmuService;
    private readonly ITileService _tileService;
    private readonly IEventService _eventService;

    private LcdControlRegister LcdControlRegister => _mmuService.ReadByte(AddressConsts.LCD_CONTROL_REGISTER_ADDRESS);

    private ColorPaletteRegister ColorPaletteRegister =>
        _mmuService.ReadByte(AddressConsts.COLOR_PALETTE_REGISTER_ADDRESS);

    private byte ScrollY => _mmuService.ReadByte(AddressConsts.SCY_ADDRESS);
    private byte ScrollX => _mmuService.ReadByte(AddressConsts.SCX_ADDRESS);

    private byte WindowY => _mmuService.ReadByte(AddressConsts.WY_ADDRESS);
    private byte WindowX => _mmuService.ReadByte(AddressConsts.WX_ADDRESS);
    private OamObject[] OamObjects => _mmuService.GetOamObjects();
    private List<OamObject> _oamObjectsThisScanLine = [];

    private LcdStatusRegister LcdStatusRegister
    {
        get => _mmuService.ReadByte(AddressConsts.LCD_STATUS_REGISTER_ADDRESS);
        set => _mmuService.WriteByte(AddressConsts.LCD_STATUS_REGISTER_ADDRESS, value);
    }

    public byte LyRegister
    {
        get => _mmuService.ReadByte(AddressConsts.LY_REGISTER_ADDRESS);
        set => _mmuService.WriteByte(AddressConsts.LY_REGISTER_ADDRESS, value);
    }

    private byte LycRegister => _mmuService.ReadByte(AddressConsts.LYC_REGISTER_ADDRESS);


    public PpuModes Mode { get; internal set; }
    public int ScanLine { get; set; } = 0x00;
    public int Dot { get; set; } = 0;
    public int[,] FrameBuffer { get; private set; }

    public PpuService(IMmuService mmuService, ITileService tileService, IEventService eventService)
    {
        _mmuService = mmuService;
        _tileService = tileService;
        _eventService = eventService;
        _eventService.TClock += OnTClock;
        FrameBuffer = new int[ScreenDimensions.HEIGHT, ScreenDimensions.WIDTH];
    }

    public void OnTClock(object? sender, ClockEventArgs e)
    {
        var stat = LcdStatusRegister.Clone();
        bool statInterruptRequested = false;
        Dot++;

        if (Dot > 455)
        {
            Dot = 0;
        }

        if (Dot == 0)
        {
            ScanLine++;
            if (ScanLine > 153)
            {
                ScanLine = 0;
            }

            LyRegister = (byte)ScanLine;

            // LYC=LY check
            stat.LycEqualsLy = LycRegister == LyRegister;
            if (stat.LycEqualsLy && stat.LycIntSelect)
            {
                statInterruptRequested = true;
            }

            if (ScanLine == 144)
            {
                VBlankInterruptRequest();
                VBlankStartInvoke(this, new VBlankEventArgs() { FrameBuffer = FrameBuffer });
            }
            else if (ScanLine == 0)
            {
                FrameBuffer = new int[ScreenDimensions.HEIGHT, ScreenDimensions.WIDTH];
            }
        }

        // Update PPU mode
        if (ScanLine >= 144)
        {
            Mode = PpuModes.VerticalBlank;
        }
        else if (Dot < 80)
        {
            Mode = PpuModes.OAMSearch;
            if (Dot == 0) ScanOAM();
        }
        else if (Dot < 252)
        {
            Mode = PpuModes.ActivePicture;
            var screenX = Dot - 80;
            if (screenX > 0 && screenX < 160)
            {
                RenderBackgroundAndWindow(ScanLine, screenX);
                RenderSprites(ScanLine, screenX);
            }
        }
        else
        {
            Mode = PpuModes.HorizontalBlank;
        }

        // Check if mode changed and if interrupt should be requested
        if (stat.PpuMode != Mode)
        {
            stat.PpuMode = Mode;
            switch (Mode)
            {
                case PpuModes.HorizontalBlank:
                    if (stat.Mode0IntSelect) statInterruptRequested = true;
                    break;
                case PpuModes.VerticalBlank:
                    if (stat.Mode1IntSelect) statInterruptRequested = true;
                    break;
                case PpuModes.OAMSearch:
                    if (stat.Mode2IntSelect) statInterruptRequested = true;
                    break;
            }
        }

        LcdStatusRegister = stat;

        // Trigger STAT interrupt if conditions are met
        if (statInterruptRequested)
        {
            InterruptRegister interruptRegister = _mmuService.ReadByte(AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS);
            interruptRegister.LCD = true;
            _mmuService.WriteByte(AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS, interruptRegister);
        }
    }

    public event VBlankStart VBlankStart;

    private void VBlankInterruptRequest()
    {
        InterruptRegister interruptRegister = _mmuService.ReadByte(AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS);
        interruptRegister.VBlank = true;
        _mmuService.WriteByte(AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS, interruptRegister);
    }

    public void VBlankStartInvoke(object? sender, VBlankEventArgs e)
    {
        VBlankStart?.Invoke(sender, e);
    }


    private void ScanOAM()
    {
        _oamObjectsThisScanLine = [];
        int spriteHeight = LcdControlRegister.SpriteSize ? 16 : 8;


        for (int i = 0; i < 40; i++)
        {
            var currentOamObject = OamObjects[i];
            int spriteY = currentOamObject.YPosition - 16;

            if (ScanLine >= spriteY && ScanLine < spriteY + spriteHeight)
            {
                _oamObjectsThisScanLine.Add(currentOamObject);

                if (_oamObjectsThisScanLine.Count == 10) break; // Max 10 sprites per scanline
            }
        }
    }

    private void RenderFullBackgroundAndWindow()
    {
        for (int y = 0; y < ScreenDimensions.HEIGHT; y++)
        {
            for (int x = 0; x < ScreenDimensions.WIDTH; x++)
            {
                RenderBackgroundAndWindow(y, x);
            }
        }
    }

    private void RenderBackgroundAndWindow(int screenY, int screenX)
    {
        var inWindow = LcdControlRegister.WindowDisplayEnable && WindowX < screenX && WindowY < screenY;

        var tileMap = (inWindow && LcdControlRegister.WindowTileMapDisplaySelect) ||
                      LcdControlRegister.BackgroundTileMapDisplaySelect
            ? ETileMap.TileMap1
            : ETileMap.TileMap0;
        var tileSet = LcdControlRegister.BackgroundWindowTileDataSelect
            ? ETileSet.TileSet1
            : ETileSet.TileSet0;
        var tileX = (byte)(screenX + ScrollX) / TILE_SIZE;
        var tilePixelX = ((screenX + ScrollX) % TILE_SIZE);
        var tileY = (byte)((byte)(screenY + ScrollY) / TILE_SIZE);
        var tilePixelY = (screenY + ScrollY) % TILE_SIZE;

        if (screenY < ScreenDimensions.HEIGHT && screenX < ScreenDimensions.WIDTH)
        {
            var colorIndex = _tileService.GetTilePixel(tileMap, tileSet, tileX, tileY, tilePixelX, tilePixelY);
            FrameBuffer[screenY, screenX] = ColorPaletteRegister.Colors[colorIndex];
        }
    }

    private void RenderSprites(int scanLine, int screenX)
    {
        if (!LcdControlRegister.SpriteDisplayEnable) return;

        if (_oamObjectsThisScanLine.Count == 0) return;

        foreach (var sprite in _oamObjectsThisScanLine.OrderByDescending(s => s.XPosition))
        {
            if (sprite.XPosition == 0 || sprite.XPosition >= 168) continue;
            if (!LcdControlRegister.SpriteSize && sprite.YPosition <= 7 || LcdControlRegister.SpriteSize && sprite.YPosition == 0 || sprite.YPosition >= 160) continue;

            var onScreenSpriteXPosition = sprite.XPosition - 8;
            var onScreenSpriteYPosition = sprite.YPosition - 16;


            int tilePixelX = screenX - onScreenSpriteXPosition;
            int tilePixelY = scanLine - onScreenSpriteYPosition;


            if (sprite.Flags.XFlip) tilePixelX = 7 - tilePixelX; // X flip
            if (sprite.Flags.YFlip) tilePixelY = 7 - tilePixelY; // Y flip

            int colorIndex = _tileService.GetSpritePixel(sprite.TileIndex, tilePixelX, tilePixelY, LcdControlRegister.SpriteSize);

            if (colorIndex != 0) // 0 is transparent for sprites
            {
                if (!sprite.Flags.Priority || FrameBuffer[scanLine, screenX] == 0)
                {
                    byte paletteRegister = sprite.Flags.DmgPalette ? _mmuService.ReadByte(0xFF49) : _mmuService.ReadByte(0xFF48);
                    int color = (paletteRegister >> (colorIndex * 2)) & 0x03;
                    FrameBuffer[scanLine, screenX] = color;
                }
            }
        }
    }
}