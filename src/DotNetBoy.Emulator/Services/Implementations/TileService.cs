using DotNetBoy.Emulator.Enums;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.Services.Implementations;

public class TileService(IMmuService mmuService) : ITileService
{
    private const int TILE_MAP_WIDTH = 32;
    private const int TILE_BYTE_LENGTH = 16;
    private const int ADDRESS_OFFSET = 0x9000 - 0x8800;

    public int GetTilePixel(ETileMap tileMap, ETileSet tileSet, int tileX, int tileY, int tilePixelX, int tilePixelY)
    {
        var tileMapVram = mmuService.GetTileMap(tileMap);
        byte[] tile;
        var tileInTileMap = tileMapVram[tileY * TILE_MAP_WIDTH + tileX];
        var tileSetVram = mmuService.GetTileSet(tileSet);
        if (tileSet == ETileSet.TileSet1)
        {
            tile = tileSetVram[
                new Range(tileInTileMap * TILE_BYTE_LENGTH, tileInTileMap * TILE_BYTE_LENGTH + TILE_BYTE_LENGTH)];
        }
        else
        {
            if (tileInTileMap <= 127)
            {
                tile = tileSetVram[
                    new Range(ADDRESS_OFFSET + (tileInTileMap * TILE_BYTE_LENGTH), ADDRESS_OFFSET + (tileInTileMap * TILE_BYTE_LENGTH + TILE_BYTE_LENGTH))];
            }
            else
            {
                tile = tileSetVram[
                    new Range(tileInTileMap * TILE_BYTE_LENGTH - ADDRESS_OFFSET, tileInTileMap * TILE_BYTE_LENGTH + TILE_BYTE_LENGTH - ADDRESS_OFFSET)
                ];
            }
        }

        return ReadTilePixel(tilePixelX, tilePixelY, tile);
    }

    public int GetSpritePixel(int tileIndex, int pixelX, int pixelY, bool sixteenPxSprites)
    {
        const int TILE_BYTE_LENGTH = 16;
        var tileSetVram = mmuService.GetTileSet(ETileSet.TileSet1);
        byte[] tile;
        if (!sixteenPxSprites || pixelY <= 7)
        {
            tile = tileSetVram[
                new Range(tileIndex * TILE_BYTE_LENGTH, tileIndex * TILE_BYTE_LENGTH + TILE_BYTE_LENGTH)];
        }
        else
        {
            tile = tileSetVram[
                new Range(tileIndex + 1 * TILE_BYTE_LENGTH, tileIndex + 1 * TILE_BYTE_LENGTH + TILE_BYTE_LENGTH)];
        }

        var actualPixelY = !sixteenPxSprites || pixelY <= 7 ? pixelY : pixelY - 7;

        return ReadTilePixel(pixelX, actualPixelY, tile);
    }
    
    private int ReadTilePixel(int tilePixelX, int tilePixelY, byte[] tile)
    {
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