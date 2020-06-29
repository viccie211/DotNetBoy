namespace CoreBoy.Emulator.PPUModels
{
    public class Tile
    {
        public int[,] Pixels { get; set; }
        public Tile()
        {
            this.Pixels = new int[8, 8];
        }
        public Tile(byte[] tileData)
        {
            this.Pixels = new int[8, 8];

            for (int i = 0; i < tileData.Length - 1; i += 2)
            {
                byte byte1 = tileData[i];
                byte byte2 = tileData[i + 1];
                byte mask = 0b10000000;
                int j = 0;
                while (mask > 0)
                {
                    bool bit1 = ((byte1 & mask) == mask);
                    bool bit2 = ((byte2 & mask) == mask);
                    if (!bit1 && !bit2)
                    {
                        Pixels[i / 2, j] = 0;
                    }
                    if (!bit1 && bit2)
                    {
                        Pixels[i / 2, j] = 1;
                    }
                    if (bit1 && !bit2)
                    {
                        Pixels[i / 2, j] = 2;
                    }
                    if (bit1 && bit2)
                    {
                        Pixels[i / 2, j] = 3;
                    }
                    mask = (byte)(mask >> 1);
                    j++;
                }
            }

        }
    }
}