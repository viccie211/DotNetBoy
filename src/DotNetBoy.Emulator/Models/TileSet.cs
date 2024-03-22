using System.Diagnostics;

namespace DotNetBoy.Emulator.Models;

public class TileSet
{
    private const int TILE_BYTE_LENGTH = 16;
    public Tile[] Set { get; set; }

    public static implicit operator TileSet(byte[] vramTileSet)
    {
        var result = new TileSet()
        {
            Set = new Tile[vramTileSet.Length / TILE_BYTE_LENGTH]
        };

        for (int i = 0; i < result.Set.Length; i++)
        {
            result.Set[i] = new byte[] { vramTileSet[i * TILE_BYTE_LENGTH], vramTileSet[i * TILE_BYTE_LENGTH + 1], vramTileSet[i * TILE_BYTE_LENGTH + 2], vramTileSet[i * TILE_BYTE_LENGTH + 3] };
        }

        return result;
    }
}