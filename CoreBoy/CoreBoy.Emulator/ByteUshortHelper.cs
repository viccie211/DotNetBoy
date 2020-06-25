namespace CoreBoy.Emulator
{
    public static class ByteUshortHelper
    {
        public static ushort CombineBytes(byte upper, byte lower)
        {
            ushort result = (ushort)(upper << 8);
            result = (ushort)(result | lower);
            return result;
        }

        public static byte LowerByteOfSixteenBits(ushort input)
        {
            return (byte)(0x00FF & input);
        }

        public static byte UpperByteOfSixteenBits(ushort input)
        {
            return (byte)(input >> 8);
        }

    }
}