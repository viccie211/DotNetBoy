namespace DotNetBoy.Emulator.Models;

public class LcdControlRegister
{
    public bool BackgroundWindowDisplayPriority { get; set; }
    public bool SpriteDisplayEnable { get; set; }
    public bool SpriteSize { get; set; }
    public bool BackgroundTileMapDisplaySelect { get; set; }
    public bool BackgroundWindowTileDataSelect { get; set; }
    public bool WindowDisplayEnable { get; set; }
    public bool WindowTileMapDisplaySelect { get; set; }
    public bool LcdDisplayEnable { get; set; }
    
    
    public static implicit operator byte(LcdControlRegister lcdControlRegister)
    {
        byte result = 0;
        result = (byte)(lcdControlRegister.BackgroundWindowDisplayPriority ? 0b00000001 | result : result);
        result = (byte)(lcdControlRegister.SpriteDisplayEnable ? 0b00000010 | result : result);
        result = (byte)(lcdControlRegister.SpriteSize ? 0b00000100 | result : result);
        result = (byte)(lcdControlRegister.BackgroundTileMapDisplaySelect ? 0b00001000 | result : result);
        result = (byte)(lcdControlRegister.BackgroundWindowTileDataSelect ? 0b00010000 | result : result);
        result = (byte)(lcdControlRegister.WindowDisplayEnable ? 0b00100000 | result : result);
        result = (byte)(lcdControlRegister.WindowTileMapDisplaySelect ? 0b01000000 | result : result);
        result = (byte)(lcdControlRegister.LcdDisplayEnable ? 0b10000000 | result : result);
        
        return result;
    }
    
    public static implicit operator LcdControlRegister(byte input)
    {
        LcdControlRegister result = new LcdControlRegister
        {
            BackgroundWindowDisplayPriority = (0b00000001 & input) != 0,
            SpriteDisplayEnable = (0b00000010 & input) != 0,
            SpriteSize = (0b00000100 & input) != 0,
            BackgroundTileMapDisplaySelect = (0b00001000 & input) != 0,
            BackgroundWindowTileDataSelect = (0b00010000 & input) != 0,
            WindowDisplayEnable = (0b00100000 & input) != 0,
            WindowTileMapDisplaySelect = (0b01000000 & input) != 0,
            LcdDisplayEnable = (0b10000000 & input) != 0,
        };
        return result;
    }
}