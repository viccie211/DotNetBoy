using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.Services.Implementations;

public class ByteUshortService :IByteUshortService
{
    public ushort CombineBytes(byte upper, byte lower)
    {
        ushort result = (ushort)(upper << 8);
        result = (ushort)(result | lower);
        return result;
    }

    public byte LowerByteOfSixteenBits(ushort input)
    {
        return (byte)(0x00FF & input);
    }

    public byte UpperByteOfSixteenBits(ushort input)
    {
        return (byte)(input >> 8);
    }
}