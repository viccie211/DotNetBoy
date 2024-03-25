namespace DotNetBoy.Emulator.Models;

public class ColorPaletteRegister
{
    public int[] Colors { get; set; } = new int[4];

    public static implicit operator ColorPaletteRegister(byte input)
    {
        var colors = new int[4];

        for (int i = 0; i < 4; i++)
        {
            var shifted = input >> i*2;
            var masked = shifted & 0b00000011;
            colors[i] = masked;
        }

        return new ColorPaletteRegister()
        {
            Colors = colors
        };
    }

    public static implicit operator byte(ColorPaletteRegister input)
    {
        var result = (byte)0;

        for (int i = 0; i < 4; i++)
        {
            var unshifted = input.Colors[i];
            var shifted = (byte)(unshifted << i);
            result += shifted;
        }

        return result;
    }
}