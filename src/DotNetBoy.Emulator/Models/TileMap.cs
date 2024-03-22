namespace DotNetBoy.Emulator.Models;

public class TileMap
{
    private const int MAP_SIDE = 32;
    public int[,] Map { get; set; }

    public static implicit operator TileMap(byte[] vramTileMap)
    {
        var result = new TileMap()
        {
            Map = new int[MAP_SIDE, MAP_SIDE]
        };
        int i = 0;

        for (int y = 0; y < MAP_SIDE; y++)
        {
            for (int x = 0; x < MAP_SIDE; x++)
            {
                if (i < vramTileMap.Length)
                {
                    result.Map[y, x] = vramTileMap[i];
                }
                else
                {
                    result.Map[y, x] = 0;
                }

                i++;
            }
        }

        return result;
    }
}