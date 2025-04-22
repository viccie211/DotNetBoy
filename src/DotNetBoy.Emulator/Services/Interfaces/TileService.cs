using DotNetBoy.Emulator.Enums;

namespace DotNetBoy.Emulator.Services.Interfaces;

public interface ITileService
{
    int GetTilePixel(ETileMap tileMap, ETileSet tileSet, int tileX, int tileY, int tilePixelX, int tilePixelY);
    int GetSpritePixel(int tileIndex, int pixelX, int pixelY,bool sixteenPxSprites);
}