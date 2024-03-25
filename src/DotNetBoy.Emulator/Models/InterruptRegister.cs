namespace DotNetBoy.Emulator.Models;

public class InterruptRegister
{
    public bool VBlank { get; set; }
    public bool LCD { get; set; }
    public bool Timer { get; set; }
    public bool Serial { get; set; }
    public bool Joypad { get; set; }
    
    
    public static implicit operator byte(InterruptRegister lcdControlRegister)
    {
        byte result = 0;
        result = (byte)(lcdControlRegister.VBlank ? 0b00000001 | result : result);
        result = (byte)(lcdControlRegister.LCD ? 0b00000010 | result : result);
        result = (byte)(lcdControlRegister.Timer ? 0b00000100 | result : result);
        result = (byte)(lcdControlRegister.Serial ? 0b00001000 | result : result);
        result = (byte)(lcdControlRegister.Joypad ? 0b00010000 | result : result);
        
        return result;
    }
    
    public static implicit operator InterruptRegister(byte input)
    {
        InterruptRegister result = new InterruptRegister
        {
            VBlank = (0b00000001 & input) != 0,
            LCD = (0b00000010 & input) != 0,
            Timer = (0b00000100 & input) != 0,
            Serial = (0b00001000 & input) != 0,
            Joypad = (0b00010000 & input) != 0,
        };
        return result;
    }
}