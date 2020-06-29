using CoreBoy.Emulator.PPUModels;

namespace CoreBoy.Emulator
{
    public class PPU
    {
        public const int _width = 160;
        public const int _height = 144;
        private const int _tileSize = 8;

        private const ushort _lyAddress = 0xFF44;
        public int[,] _frameData;
        private TileSet _tileSet;
        private TileMap _tileMap;
        private MMU _MMU;

        private int _mode = 0;
        private int _modeclock = 0;
        public int Line = 0;

        public int XOffset = 0;
        public int YOffset = 0;
        public int TileMapNr = 0;
        public int TileSetNr = 0;

        public PPU(MMU MMU)
        {
            _frameData = new int[_height, _width];
            _MMU = MMU;
        }

        public void Step()
        {
            if (Line == 0)
            {
                _tileSet = new TileSet(_MMU.GetTileSet0());
                _tileMap = new TileMap(_MMU.GetTileMap0());
            }
            if (Line < _height)
            {
                RenderScan(Line);
            }
            _MMU.WriteByte(_lyAddress, (byte)Line);
            Line++;
        }

        private void RenderScan(int line)
        {
            int TileY = (line + YOffset) / _tileSize;
            int yInTile = (line + YOffset) % _tileSize;

            for (int screenX = 0; screenX < _width; screenX++)
            {
                int TileX = (screenX + XOffset) / _tileSize;
                int xInTile = (screenX + XOffset) % _tileSize;
                Tile tile = _tileSet.GetByTileAndSetNr(_tileMap.GetTileNrFromTileMap(TileY, TileX, TileMapNr), TileSetNr);
                _frameData[line, screenX] = tile.Pixels[yInTile, xInTile];
            }
        }

    }
}