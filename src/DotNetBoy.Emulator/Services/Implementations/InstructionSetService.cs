using DotNetBoy.Emulator.InstructionSet;
using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.BitInstructions;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.RotateInstructions;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.SetBitInstructions;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ShiftInstructions;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.Services.Implementations;

public class InstructionSetService(
    RotateInstructions rotateInstructions,
    JumpInstructions jumpInstructions,
    MiscellaneousInstructions miscellaneousInstructions,
    LoadInstructions loadInstructions,
    IncrementInstructions incrementInstructions,
    DecrementInstructions decrementInstructions,
    LogicInstructions logicInstructions,
    StoreInstructions storeInstructions,
    PushPopInstructions pushPopInstructions,
    LoadBetweenRegistersInstructions loadBetweenRegistersInstructions,
    ArithmeticInstructions arithmeticInstructions,
    ShiftRightLogicalInstructions shiftRightLogicalInstructions,
    ShiftRightArithmeticInstructions shiftRightArithmeticInstructions,
    ShiftLeftArithmeticInstructions shiftLeftArithmeticInstructions,
    BitInBRegisterInstructions bitInBRegisterInstructions,
    BitInCRegisterInstructions bitInCRegisterInstructions,
    BitInDRegisterInstructions bitInDRegisterInstructions,
    BitInERegisterInstructions bitInERegisterInstructions,
    BitInHRegisterInstructions bitInHRegisterInstructions,
    BitInLRegisterInstructions bitInLRegisterInstructions,
    BitAtAddressHLInstructions bitAtAddressHlInstructions,
    BitInARegisterInstructions bitInARegisterInstructions,
    ResetBitInBRegisterInstructions resetBitInBRegisterInstructions,
    ResetBitInCRegisterInstructions resetBitInCRegisterInstructions,
    ResetBitInDRegisterInstructions resetBitInDRegisterInstructions,
    ResetBitInERegisterInstructions resetBitInERegisterInstructions,
    ResetBitInHRegisterInstructions resetBitInHRegisterInstructions,
    ResetBitInLRegisterInstructions resetBitInLRegisterInstructions,
    ResetBitAtAddressHLInstructions resetBitAtAddressHlInstructions,
    ResetBitInARegisterInstructions resetBitInARegisterInstructions,
    SetBitInBRegisterInstructions setBitInBRegisterInstructions,
    SetBitInCRegisterInstructions setBitInCRegisterInstructions,
    SetBitInDRegisterInstructions setBitInDRegisterInstructions,
    SetBitInERegisterInstructions setBitInERegisterInstructions,
    SetBitInHRegisterInstructions setBitInHRegisterInstructions,
    SetBitInLRegisterInstructions setBitInLRegisterInstructions,
    SetBitAtAddressHLInstructions setBitAtAddressHlInstructions,
    SetBitInARegisterInstructions setBitInARegisterInstructions,
    RotateRightInstructions rotateRightInstructions,
    RotateLeftInstructions rotateLeftInstructions,
    RotateLeftThroughCarryInstructions rotateLeftThroughCarryInstructions,
    RotateRightThroughCarryInstructions rotateRightThroughCarryInstructions,
    SwapInstructions swapInstructions) : IInstructionSetService
{
    public void NonPrefixedInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x07:
            case 0x0F:
            case 0x17:
            case 0x1F:
                rotateInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x09:
            case 0x19:
            case 0x29:
            case 0x39:
            case 0x80:
            case 0x81:
            case 0x82:
            case 0x83:
            case 0x84:
            case 0x85:
            case 0x86:
            case 0x87:
            case 0x88:
            case 0x89:
            case 0x8A:
            case 0x8B:
            case 0x8C:
            case 0x8D:
            case 0x8E:
            case 0x8F:
            case 0x90:
            case 0x91:
            case 0x92:
            case 0x93:
            case 0x94:
            case 0x95:
            case 0x96:
            case 0x97:
            case 0x98:
            case 0x99:
            case 0x9A:
            case 0x9B:
            case 0x9C:
            case 0x9D:
            case 0x9E:
            case 0x9F:
            case 0xC6:
            case 0xCE:
            case 0xD6:
            case 0xDE:
            case 0xE8:
                arithmeticInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x18:
            case 0x20:
            case 0x28:
            case 0x30:
            case 0x38:
            case 0xC0:
            case 0xC2:
            case 0xC3:
            case 0xC4:
            case 0xC7:
            case 0xC8:
            case 0xC9:
            case 0xCA:
            case 0xCC:
            case 0xCD:
            case 0xCF:
            case 0xD0:
            case 0xD2:
            case 0xD4:
            case 0xD7:
            case 0xD8:
            case 0xD9:
            case 0xDA:
            case 0xDC:
            case 0xDF:
            case 0xE7:
            case 0xE9:
            case 0xEF:
            case 0xF7:
            case 0xFF:
                jumpInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x05:
            case 0x0B:
            case 0x0D:
            case 0x15:
            case 0x1B:
            case 0x1D:
            case 0x25:
            case 0x2B:
            case 0x2D:
            case 0x35:
            case 0x3B:
            case 0x3D:
                decrementInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x03:
            case 0x04:
            case 0x0C:
            case 0x13:
            case 0x14:
            case 0x1C:
            case 0x23:
            case 0x24:
            case 0x2C:
            case 0x33:
            case 0x34:
            case 0x3C:
                incrementInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x40:
            case 0x41:
            case 0x42:
            case 0x43:
            case 0x44:
            case 0x45:
            case 0x47:
            case 0x48:
            case 0x49:
            case 0x4A:
            case 0x4B:
            case 0x4C:
            case 0x4D:
            case 0x4F:
            case 0x50:
            case 0x51:
            case 0x52:
            case 0x53:
            case 0x54:
            case 0x55:
            case 0x57:
            case 0x58:
            case 0x59:
            case 0x5A:
            case 0x5B:
            case 0x5C:
            case 0x5D:
            case 0x5F:
            case 0x60:
            case 0x61:
            case 0x62:
            case 0x63:
            case 0x64:
            case 0x65:
            case 0x67:
            case 0x68:
            case 0x69:
            case 0x6A:
            case 0x6B:
            case 0x6C:
            case 0x6D:
            case 0x6F:
            case 0x78:
            case 0x79:
            case 0x7A:
            case 0x7B:
            case 0x7C:
            case 0x7D:
            case 0x7F:
            case 0xF9:
                loadBetweenRegistersInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x01:
            case 0x06:
            case 0x0E:
            case 0x11:
            case 0x16:
            case 0x0A:
            case 0x1A:
            case 0x1E:
            case 0x21:
            case 0x26:
            case 0x2A:
            case 0x3A:
            case 0x2E:
            case 0x3E:
            case 0x31:
            case 0x46:
            case 0x4E:
            case 0x56:
            case 0x5E:
            case 0x66:
            case 0x6E:
            case 0x7E:
            case 0xF0:
            case 0xF2:
            case 0xF8:
            case 0xFA:
                loadInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0xA0:
            case 0xA1:
            case 0xA2:
            case 0xA3:
            case 0xA4:
            case 0xA5:
            case 0xA6:
            case 0xA7:
            case 0xA8:
            case 0xA9:
            case 0xAA:
            case 0xAB:
            case 0xAC:
            case 0xAD:
            case 0xAE:
            case 0xAF:
            case 0xB0:
            case 0xB1:
            case 0xB2:
            case 0xB3:
            case 0xB4:
            case 0xB5:
            case 0xB6:
            case 0xB7:
            case 0xB8:
            case 0xB9:
            case 0xBA:
            case 0xBB:
            case 0xBC:
            case 0xBD:
            case 0xBE:
            case 0xBF:
            case 0xE6:
            case 0xEE:
            case 0xF6:
            case 0xFE:
                logicInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x00:
            case 0x10:
            case 0x27:
            case 0x2F:
            case 0x37:
            case 0x3F:
            case 0x76:
            case 0xCB:
            case 0xD3:
            case 0xDB:
            case 0xDD:
            case 0xE3:
            case 0xE4:
            case 0xEB:
            case 0xEC:
            case 0xED:
            case 0xF3:
            case 0xF4:
            case 0xFB:
            case 0xFC:
            case 0xFD:
                miscellaneousInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0xC1:
            case 0xC5:
            case 0xD1:
            case 0xD5:
            case 0xE1:
            case 0xE5:
            case 0xF1:
            case 0xF5:
                pushPopInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x08:
            case 0x02:
            case 0x12:
            case 0x22:
            case 0x32:
            case 0x36:
            case 0x70:
            case 0x71:
            case 0x72:
            case 0x73:
            case 0x74:
            case 0x75:
            case 0x77:
            case 0xE0:
            case 0xE2:
            case 0xEA:
                storeInstructions.ExecuteInstruction(opCode, registers);
                break;

            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not handled in Non Prefixed Instructions.");
        }
    }

    public void PrefixedInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x46:
            case 0x4E:
            case 0x56:
            case 0x5E:
            case 0x66:
            case 0x6E:
            case 0x76:
            case 0x7E:
                bitAtAddressHlInstructions.ExecuteInstruction(opCode, registers);
                break;
            case 0x47:
            case 0x4F:
            case 0x57:
            case 0x5F:
            case 0x67:
            case 0x6F:
            case 0x77:
            case 0x7F:
                bitInARegisterInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x40:
            case 0x48:
            case 0x50:
            case 0x58:
            case 0x60:
            case 0x68:
            case 0x70:
            case 0x78:
                bitInBRegisterInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x41:
            case 0x49:
            case 0x51:
            case 0x59:
            case 0x61:
            case 0x69:
            case 0x71:
            case 0x79:
                bitInCRegisterInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x42:
            case 0x4A:
            case 0x52:
            case 0x5A:
            case 0x62:
            case 0x6A:
            case 0x72:
            case 0x7A:
                bitInDRegisterInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x43:
            case 0x4B:
            case 0x53:
            case 0x5B:
            case 0x63:
            case 0x6B:
            case 0x73:
            case 0x7B:
                bitInERegisterInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x44:
            case 0x4C:
            case 0x54:
            case 0x5C:
            case 0x64:
            case 0x6C:
            case 0x74:
            case 0x7C:
                bitInHRegisterInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x45:
            case 0x4D:
            case 0x55:
            case 0x5D:
            case 0x65:
            case 0x6D:
            case 0x75:
            case 0x7D:
                bitInLRegisterInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x86:
            case 0x8E:
            case 0x96:
            case 0x9E:
            case 0xA6:
            case 0xAE:
            case 0xB6:
            case 0xBE:
                resetBitAtAddressHlInstructions.ExecuteInstruction(opCode, registers);
                break;
            
            case 0x87:
            case 0x8F:
            case 0x97:
            case 0x9F:
            case 0xA7:
            case 0xAF:
            case 0xB7:
            case 0xBF:
                resetBitInARegisterInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x80:
            case 0x88:
            case 0x90:
            case 0x98:
            case 0xA0:
            case 0xA8:
            case 0xB0:
            case 0xB8:
                resetBitInBRegisterInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x81:
            case 0x89:
            case 0x91:
            case 0x99:
            case 0xA1:
            case 0xA9:
            case 0xB1:
            case 0xB9:
                resetBitInCRegisterInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x82:
            case 0x8A:
            case 0x92:
            case 0x9A:
            case 0xA2:
            case 0xAA:
            case 0xB2:
            case 0xBA:
                resetBitInDRegisterInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x83:
            case 0x8B:
            case 0x93:
            case 0x9B:
            case 0xA3:
            case 0xAB:
            case 0xB3:
            case 0xBB:
                resetBitInERegisterInstructions.ExecuteInstruction(opCode, registers);
                break;
            
            case 0x84:
            case 0x8C:
            case 0x94:
            case 0x9C:
            case 0xA4:
            case 0xAC:
            case 0xB4:
            case 0xBC:
                resetBitInHRegisterInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x85:
            case 0x8D:
            case 0x95:
            case 0x9D:
            case 0xA5:
            case 0xAD:
            case 0xB5:
            case 0xBD:
                resetBitInLRegisterInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x00:
            case 0x01:
            case 0x02:
            case 0x03:
            case 0x04:
            case 0x05:
            case 0x06:
            case 0x07:
                rotateLeftInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x10:
            case 0x11:
            case 0x12:
            case 0x13:
            case 0x14:
            case 0x15:
            case 0x16:
            case 0x17:
                rotateLeftThroughCarryInstructions.ExecuteInstruction(opCode, registers);
                break;
            
            case 0x08:
            case 0x09:
            case 0x0A:
            case 0x0B:
            case 0x0C:
            case 0x0D:
            case 0x0E:
            case 0x0F:
                rotateRightInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x18:
            case 0x19:
            case 0x1A:
            case 0x1B:
            case 0x1C:
            case 0x1D:
            case 0x1E:
            case 0x1F:
                rotateRightThroughCarryInstructions.ExecuteInstruction(opCode, registers);
                break;
            
            case 0xC6:
            case 0xCE:
            case 0xD6:
            case 0xDE:
            case 0xE6:
            case 0xEE:
            case 0xF6:
            case 0xFE:
                setBitAtAddressHlInstructions.ExecuteInstruction(opCode, registers);
                break;
            
            case 0xC7:
            case 0xCF:
            case 0xD7:
            case 0xDF:
            case 0xE7:
            case 0xEF:
            case 0xF7:
            case 0xFF:
                setBitInARegisterInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0xC0:
            case 0xC8:
            case 0xD0:
            case 0xD8:
            case 0xE0:
            case 0xE8:
            case 0xF0:
            case 0xF8:
                setBitInBRegisterInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0xC1:
            case 0xC9:
            case 0xD1:
            case 0xD9:
            case 0xE1:
            case 0xE9:
            case 0xF1:
            case 0xF9:
                setBitInCRegisterInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0xC2:
            case 0xCA:
            case 0xD2:
            case 0xDA:
            case 0xE2:
            case 0xEA:
            case 0xF2:
            case 0xFA:
                setBitInDRegisterInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0xC3:
            case 0xCB:
            case 0xD3:
            case 0xDB:
            case 0xE3:
            case 0xEB:
            case 0xF3:
            case 0xFB:
                setBitInERegisterInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0xC4:
            case 0xCC:
            case 0xD4:
            case 0xDC:
            case 0xE4:
            case 0xEC:
            case 0xF4:
            case 0xFC:
                setBitInHRegisterInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0xC5:
            case 0xCD:
            case 0xD5:
            case 0xDD:
            case 0xE5:
            case 0xED:
            case 0xF5:
            case 0xFD:
                setBitInLRegisterInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x20:
            case 0x21:
            case 0x22:
            case 0x23:
            case 0x24:
            case 0x25:
            case 0x26:
            case 0x27:
                shiftLeftArithmeticInstructions.ExecuteInstruction(opCode, registers);
                break;

            case 0x28:
            case 0x29:
            case 0x2A:
            case 0x2B:
            case 0x2C:
            case 0x2D:
            case 0x2E:
            case 0x2F:
                shiftRightArithmeticInstructions.ExecuteInstruction(opCode, registers);
                break;
            
            case 0x38:
            case 0x39:
            case 0x3A:
            case 0x3B:
            case 0x3C:
            case 0x3D:
            case 0x3E:
            case 0x3F:
                shiftRightLogicalInstructions.ExecuteInstruction(opCode, registers);
                break;
            
            case 0x30:
            case 0x31:
            case 0x32:
            case 0x33:
            case 0x34:
            case 0x35:
            case 0x36:
            case 0x37:
                swapInstructions.ExecuteInstruction(opCode, registers);
                break;



            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not handled in Prefixed Instructions.");
        }
    }
}