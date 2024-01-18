namespace DotNetBoy.Emulator.Services.Interfaces;

public interface IByteUshortService
{
    ushort CombineBytes(byte upper, byte lower);
    byte LowerByteOfSixteenBits(ushort input);
    byte UpperByteOfSixteenBits(ushort input);
}