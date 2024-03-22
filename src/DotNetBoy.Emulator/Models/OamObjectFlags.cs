namespace DotNetBoy.Emulator.Models;

public class OamObjectFlags
{
    public bool DmgPalette { get; set; }
    public bool XFlip { get; set; }
    public bool YFlip { get; set; }
    public bool Priority { get; set; }
    
    
    public static implicit operator byte(OamObjectFlags lcdControlRegister)
    {
        byte result = 0;
        result = (byte)(lcdControlRegister.DmgPalette ? 0b00010000 | result : result);
        result = (byte)(lcdControlRegister.XFlip ? 0b00100000 | result : result);
        result = (byte)(lcdControlRegister.YFlip ? 0b01000000 | result : result);
        result = (byte)(lcdControlRegister.Priority ? 0b10000000 | result : result);
        
        return result;
    }
    
    public static implicit operator OamObjectFlags(byte input)
    {
        var result = new OamObjectFlags
        {
            DmgPalette = (0b00010000 & input) != 0,
            XFlip = (0b00100000 & input) != 0,
            YFlip = (0b01000000 & input) != 0,
            Priority = (0b10000000 & input) != 0,
        };
        return result;
    }
}