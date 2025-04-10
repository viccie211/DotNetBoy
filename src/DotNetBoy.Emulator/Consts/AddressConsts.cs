namespace DotNetBoy.Emulator.Consts;

public static class AddressConsts
{
    public const ushort ROM_BANK_0_BASE_ADDRESS = 0x0000;
    public const ushort ROM_BANK_0_UPPER_ADDRESS = 0x3FFF;
    public const ushort CARTRIDGE_TYPE_HEADER_ADDRESS = 0x0147;
    public const ushort CARTRIDGE_SIZE_HEADER_ADDRESS = 0x0148;
    public const ushort ROM_BANK_1_BASE_ADDRESS = 0x4000;
    public const ushort ROM_BANK_1_UPPER_ADDRESS = 0x7FFF;
    public const ushort CARTRIDGE_RAM_BASE_ADDRESS = 0xA000;
    public const ushort CARTRIDGE_RAM_UPPER_ADDRESS = 0xBFFF;
    public const ushort INTERRUPT_REQUEST_REGISTER_ADDRESS = 0xFF0F;
    public const ushort INTERRUPT_ENABLE_REGISTER_ADDRESS = 0xFFFF;
    public const ushort VBLANK_INTERRUPT_VECTOR = 0x0040;
    public const ushort LCD_INTERRUPT_VECTOR = 0x0048;
    public const ushort TIMER_INTERRUPT_VECTOR = 0x0050;
    public const ushort SERIAL_INTERRUPT_VECTOR = 0x0058;
    public const ushort JOYPAD_INTERRUPT_VECTOR = 0x0060;
    public const ushort JOYPAD_INPUT_REGISTER = 0xFF00;
    public const ushort DIV_REGISTER = 0xFF04;
    public const ushort TIMA_REGISTER = 0xFF05;
    public const ushort TMA_REGISTER = 0xFF06;
    public const ushort TAC_REGISTER = 0xFF07;
    public const ushort LCD_CONTROL_REGISTER_ADDRESS = 0xFF40;
    public const ushort LCD_STATUS_REGISTER_ADDRESS = 0xFF41;
    public const ushort LY_REGISTER_ADDRESS = 0xFF44;
    public const ushort LYC_REGISTER_ADDRESS = 0xFF45;
    public const ushort SCY_ADDRESS = 0xFF42;
    public const ushort SCX_ADDRESS = 0xFF43;
    public const ushort DMA_REGISTER = 0xFF46;
    public const ushort COLOR_PALETTE_REGISTER_ADDRESS = 0xFF47;
    public const ushort WY_ADDRESS = 0xFF4A;
    public const ushort WX_ADDRESS = 0xFF4B;
}