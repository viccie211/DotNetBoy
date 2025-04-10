namespace DotNetBoy.Emulator.Models;

public class JoyPadRegister
{
    public bool AOrRight { get; set; }
    public bool BOrLeft { get; set; }
    public bool SelectOrUp { get; set; }
    public bool StartOrDown { get; set; }

    public bool SelectDPad { get; set; }
    public bool SelectButtons { get; set; }
    public bool Bit6 => true;
    public bool Bit7 => true;

    public static implicit operator byte(JoyPadRegister joyPadRegister)
    {
        byte result = 0x0F;
        result = (byte)(!joyPadRegister.AOrRight ? 0b00000001 | result : result);
        result = (byte)(!joyPadRegister.BOrLeft ? 0b00000010 | result : result);
        result = (byte)(!joyPadRegister.SelectOrUp ? 0b00000100 | result : result);
        result = (byte)(!joyPadRegister.StartOrDown ? 0b00001000 | result : result);
        result = (byte)(!joyPadRegister.SelectDPad ? 0b00010000 | result : result);
        result = (byte)(!joyPadRegister.SelectButtons ? 0b00100000 | result : result);
        result = (byte)(joyPadRegister.Bit6 ? 0b01000000 | result : result);
        result = (byte)(joyPadRegister.Bit7 ? 0b10000000 | result : result);
        return result;
    }

    public static implicit operator JoyPadRegister(byte input)
    {
        JoyPadRegister result = new JoyPadRegister
        {
            AOrRight = (0b00000001 & input) == 0,
            BOrLeft = (0b00000010 & input) == 0,
            SelectOrUp = (0b00000100 & input) == 0,
            StartOrDown = (0b00001000 & input) == 0,
            SelectDPad = (0b00010000 & input) == 0,
            SelectButtons = (0b00100000 & input) == 0,
        };
        return result;
    }
}