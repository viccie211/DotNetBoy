namespace CoreBoy
{
    public class MMU
    {
        public MMU()
        {
            _MappedMemory[0] = 0x80;
            _MappedMemory[1] = 0xDA;
            _MappedMemory[2] = 0x00;
            _MappedMemory[3] = 0x00;
        }

        private byte[] _MappedMemory = new byte[0xFFFF];

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
    }
}