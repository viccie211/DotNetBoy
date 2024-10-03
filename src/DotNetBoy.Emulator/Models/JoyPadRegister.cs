namespace DotNetBoy.Emulator.Models;

public class JoyPadRegister
{
    public bool AOrRight { get; set; }
    public bool BOrLeft { get; set; }
    public bool SelectOrUp { get; set; }
    public bool StartOrDown { get; set; }
    public bool SelectButtons { get; set; }

    public static implicit operator byte(JoyPadRegister lcdControlRegister)
    {
        byte result = 0;
        result = (byte)(lcdControlRegister.AOrRight ? 0b00000001 | result : result);
        result = (byte)(lcdControlRegister.BOrLeft ? 0b00000010 | result : result);
        result = (byte)(lcdControlRegister.SelectOrUp ? 0b00000100 | result : result);
        result = (byte)(lcdControlRegister.StartOrDown ? 0b00001000 | result : result);
        result = (byte)(lcdControlRegister.SelectButtons ? 0b00010000 | result : result);

        return result;
    }
    
    public static implicit operator JoyPadRegister(byte input)
    {
        JoyPadRegister result = new JoyPadRegister
        {
            AOrRight = (0b00000001 & input) != 0,
            BOrLeft = (0b00000010 & input) != 0,
            SelectOrUp = (0b00000100 & input) != 0,
            StartOrDown = (0b00001000 & input) != 0,
            SelectButtons = (0b00010000 & input) != 0,
        };
        return result;
    }
}