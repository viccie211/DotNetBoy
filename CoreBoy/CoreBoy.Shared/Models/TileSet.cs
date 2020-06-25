using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreBoy.Models
{
    public class TileSet
    {
        private List<KeyValuePair<ushort, Tile>> _set0;

        public TileSet()
        {
            _set0 = new List<KeyValuePair<ushort, Tile>>();
        }

        public TileSet(bool addTestTiles)
        {
            _set0 = new List<KeyValuePair<ushort, Tile>>();
            Tile value1 = new Tile(new byte[] { 0x00, 0x00, 0x18, 0x18, 0x24, 0x24, 0x42, 0x42, 0x42, 0x42, 0x7e, 0x7e, 0x42, 0x42, 0x42, 0x42 });
            KeyValuePair<ushort, Tile> Tile1 = new KeyValuePair<ushort, Tile>(0x8000, value1);
            _set0.Add(Tile1);

            Tile value2 = new Tile(new byte[] { 0x00, 0x00, 0xfc, 0x7c, 0xce, 0x42, 0xc6, 0x42, 0xfc, 0x7c, 0x4e, 0xc2, 0x46, 0xc2, 0x7c, 0xfc });
            KeyValuePair<ushort, Tile> Tile2 = new KeyValuePair<ushort, Tile>(0x8010, value2);
            _set0.Add(Tile2);

            Tile value3 = new Tile(new byte[] { 0x00, 0x00, 0x3c, 0x3c, 0xc6, 0x7a, 0xc0, 0x40, 0x40, 0xc0, 0x40, 0xc0, 0x46, 0xc2, 0x7c, 0xbc });
            KeyValuePair<ushort, Tile> Tile3 = new KeyValuePair<ushort, Tile>(0x8020, value3);
            _set0.Add(Tile3);

            Tile value4 = new Tile(new byte[] { 0x00, 0x00, 0x7c, 0x7c, 0xc6, 0x7a, 0xc6, 0x42, 0x42, 0xc6, 0x42, 0xc6, 0x42, 0xc6, 0x7c, 0xbc });
            KeyValuePair<ushort, Tile> Tile4 = new KeyValuePair<ushort, Tile>(0x8030, value4);
            _set0.Add(Tile4);
        }

        public TileSet(byte[] vramTileSet)
        {
            _set0 = new List<KeyValuePair<ushort, Tile>>();
            for (int i = 0; i < vramTileSet.Length; i += 16)
            {
                List<byte> tileData;
                if (i + 1 < vramTileSet.Length)
                {
                    tileData = vramTileSet.Skip(i).Take(16).ToList();
                }
                else
                {
                    tileData = new List<byte>();
                }
                while (tileData.Count < 16)
                {
                    tileData.Add(0xFF);
                }
                Tile value = new Tile(tileData.ToArray());
                KeyValuePair<ushort, Tile> keyValuePair = new KeyValuePair<ushort, Tile>((ushort)(0x8000 + i), value);
                _set0.Add(keyValuePair);
            }
        }

        public Tile GetByTileAndSetNr(int tileNr, int setNr)
        {
            if (setNr == 0)
            {
                if (tileNr < _set0.Count)
                {
                    return _set0[tileNr].Value;
                }
                else
                {
                    return new Tile();
                }
            }
            else
            {
                return new Tile();
            }
        }
    }
}