namespace DotNetBoy.Emulator.Models;

public class OamObject
{
    public byte YPosition { get; set; }
    public byte XPosition { get; set; }
    public byte TileIndex { get; set; }
    public OamObjectFlags Flags { get; set; }

    public static implicit operator byte[](OamObject oamObject)
    {
        var result = new byte[4];
        result[0] = oamObject.YPosition;
        result[1] = oamObject.XPosition;
        result[2] = oamObject.TileIndex;
        result[3] = oamObject.Flags;
        return result;
    }

    public static implicit operator OamObject(byte[] oamRam)
    {
        return new OamObject()
        {
            YPosition = oamRam.Length >= 1 ? oamRam[0] : (byte)0x00,
            XPosition = oamRam.Length >= 2 ? oamRam[1] : (byte)0x00,
            TileIndex = oamRam.Length >= 3 ? oamRam[2] : (byte)0x00,
            Flags = oamRam.Length >= 4 ? oamRam[3] : (byte)0x00
        };
    }
}