using DotNetBoy.Emulator.Enums;

namespace DotNetBoy.Emulator.Services.Interfaces;

public interface ITileService
{
    int GetPixel(ETileMap tileMap, ETileSet tileSet, int tileX, int tileY, int tilePixelX, int tilePixelY);
}