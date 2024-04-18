namespace DotNetBoy.Emulator.Consts;

public static class AddressConsts
{
    public const ushort INTERRUPT_REQUEST_REGISTER_ADDRESS = 0xFF0F;
    public const ushort INTERRUPT_ENABLE_REGISTER_ADDRESS = 0xFFFF;
    public const ushort VBLANK_INTERRUPT_VECTOR = 0x0040;
    public const ushort LCD_INTERRUPT_VECTOR = 0x0048;
    public const ushort TIMER_INTERRUPT_VECTOR = 0x0050;
    public const ushort SERIAL_INTERRUPT_VECTOR = 0x0058;
    public const ushort JOYPAD_INTERRUPT_VECTOR = 0x0060;
    public const ushort LCD_CONTROL_REGISTER_ADDRESS = 0xFF40;
    public const ushort LCD_STATUS_REGISTER_ADDRESS = 0xFF41;
    public const ushort LY_REGISTER_ADDRESS = 0xFF44;
    public const ushort LYC_REGISTER_ADDRESS = 0xFF45;
    public const ushort SCY_ADDRESS = 0xFF42;
    public const ushort SCX_ADDRESS = 0xFF43;
    public const ushort COLOR_PALETTE_REGISTER_ADDRESS = 0xFF47;
    public const ushort WY_ADDRESS = 0xFF4A;
    public const ushort WX_ADDRESS = 0xFF4B;
}