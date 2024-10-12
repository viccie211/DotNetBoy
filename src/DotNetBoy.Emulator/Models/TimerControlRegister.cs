namespace DotNetBoy.Emulator.Models;

public class TimerControlRegister
{
    public uint TimerInputDivisionFactor { get; set; }
    public bool TimerEnable { get; set; }

    public static implicit operator TimerControlRegister(byte input)
    {
        var clockSelect = input & 0x03;
        uint divisionFactor = 1;
        switch (clockSelect)
        {
            case 0x00:
                divisionFactor = 64;
                break;
            case 0x01:
                divisionFactor = 1;
                break;
            case 0x02:
                divisionFactor = 4;
                break;
            case 0x03:
                divisionFactor = 16;
                break;
        }

        return new TimerControlRegister()
        {
            TimerInputDivisionFactor = divisionFactor,
            TimerEnable = (input & 0x04) == 0x04,
        };
    }
}