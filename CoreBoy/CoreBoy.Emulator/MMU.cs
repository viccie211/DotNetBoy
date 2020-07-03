using System.Linq;

namespace CoreBoy.Emulator
{
    public class MMU
    {
        public MMU()
        {
            //_MappedMemory[0xFF44] = 0x90;
        }

        private byte[] _MappedMemory = new byte[0xFFFF + 1];

        public byte ReadByte(ushort address)
        {
            return _MappedMemory[address];
        }

        public ushort ReadWord(ushort address)
        {
            return ByteUshortHelper.CombineBytes(ReadByte(address), ReadByte((ushort)(address + 1)));
        }

        public ushort ReadWordLSFirst(ushort address)
        {
            return ByteUshortHelper.CombineBytes(ReadByte((ushort)(address + 1)), ReadByte(address));
        }

        public void WriteByte(ushort address, byte value)
        {
            _MappedMemory[address] = value;
        }

        public byte[] GetTileSet0()
        {
            return _MappedMemory.Skip(0x8000).Take(4096).ToArray();
        }

        public byte[] GetTileMap0()
        {
            return _MappedMemory.Skip(0x9800).Take(1024).ToArray();
        }

        public void LoadRom(byte[] rom)
        {
            for (int i = 0; i < rom.Length && i < 32768; i++)
            {
                _MappedMemory[i] = rom[i];
            }
        }
    }
}