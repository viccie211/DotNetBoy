using DotNetBoy.Emulator.Enums;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.Services.Implementations;

public class TileService(IMmuService mmuService) : ITileService
{
    private const int TILE_MAP_WIDTH = 32;
    private const int TILE_BYTE_LENGTH = 16;

    public int GetPixel(ETileMap tileMap, ETileSet tileSet, int tileX, int tileY, int tilePixelX, int tilePixelY)
    {
        var tileMapVram = mmuService.GetTileMap(tileMap);
        var tileInTileMap = tileMapVram[tileY * TILE_MAP_WIDTH + tileX];
        var tileSetVram = mmuService.GetTileSet(tileSet);
        var tile = tileSetVram[
            new Range(tileInTileMap * TILE_BYTE_LENGTH, tileInTileMap * TILE_BYTE_LENGTH + TILE_BYTE_LENGTH)];

        var tileRow = tile[new Range(tilePixelY * 2, tilePixelY * 2 + 2)];
        var bitMask = (byte)(0x01 << 7 - tilePixelX);
        var maskedBit0 = tileRow[0] & bitMask;
        var maskedBit1 = tileRow[1] & bitMask;
        var shiftedBit0 = maskedBit0 > 0;
        var shiftedBit1 = maskedBit1 > 0;

        if (!shiftedBit0 && !shiftedBit1)
            return 0;
        if (shiftedBit0 && !shiftedBit1)
            return 1;
        if (!shiftedBit0 && shiftedBit1)
            return 2;
        if (shiftedBit0 && shiftedBit1)
            return 3;
        return 0;
    }
}