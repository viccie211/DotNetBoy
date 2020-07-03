using System;
using System.Collections.Generic;

namespace CoreBoy.Emulator
{
    public partial class CPU
    {
        #region InstructionSet
        public void NonPrefixedInstructions(byte opcode)
        {
            switch (opcode)
            {
                case
                0x00:
                    NOP(this);
                    break;
                case
                0x10:
                    STOP(this);
                    break;
                case
                0x76:
                    HALT(this);
                    break;
                case
                0x3F:
                    CCF(this);
                    break;
                case
                0xF3:
                    DI(this);
                    break;
                case 0xFB:
                    EI(this);
                    break;

                #region INC
                #region INC_R16
                case
                0x03:
                    INC_BC(this);
                    break;
                case
                0x13:
                    INC_DE(this);
                    break;
                case
                0x23:
                    INC_HL(this);
                    break;
                case
                0x33:
                    INC_SP(this);
                    break;
                #endregion
                #region INC_R8
                case
                0x04:
                    INC_B(this);
                    break;
                case
                0x0C:
                    INC_C(this);
                    break;
                case
                0x14:
                    INC_D(this);
                    break;
                case
                0x1C:
                    INC_E(this);
                    break;
                case
                0x24:
                    INC_H(this);
                    break;
                case
                0x2C:
                    INC_L(this);
                    break;
                case
                0x3C:
                    INC_A(this);
                    break;
                #endregion

                case 0x34:
                    INC_AT_HL(this);
                    break;
                #endregion

                #region DEC
                #region DEC_R16
                case
                0x0B:
                    DEC_BC(this);
                    break;
                case
                0x1B:
                    DEC_DE(this);
                    break;
                case
                0x2B:
                    DEC_HL(this);
                    break;
                case
                0x3B:
                    DEC_SP(this);
                    break;
                #endregion
                #region DEC_R8
                case
                0x05:
                    DEC_B(this);
                    break;
                case
                0x0D:
                    DEC_C(this);
                    break;
                case
                0x15:
                    DEC_D(this);
                    break;
                case
                0x1D:
                    DEC_E(this);
                    break;
                case
                0x25:
                    DEC_H(this);
                    break;
                case
                0x2D:
                    DEC_L(this);
                    break;
                case
                0x3D:
                    DEC_A(this);
                    break;
                #endregion
                case 0x35:
                    DEC_AT_HL(this);
                    break;
                #endregion

                #region LD
                #region LD_D8
                case
                0x06:
                    LD_B_D8(this);
                    break;
                case
                0x0E:
                    LD_C_D8(this);
                    break;
                case
                0x16:
                    LD_D_D8(this);
                    break;
                case
                0x1E:
                    LD_E_D8(this);
                    break;
                case
                0x26:
                    LD_H_D8(this);
                    break;
                case
                0x2E:
                    LD_L_D8(this);
                    break;
                case
                0x36:
                    LD_HL_D8(this);
                    break;
                case
                0x3E:
                    LD_A_D8(this);
                    break;
                case
                0x0A:
                    LD_A_BC(this);
                    break;
                case
                0x1A:
                    LD_A_DE(this);
                    break;

                #endregion

                #region LD_B
                case
                0x40:
                    LD_B_B(this);
                    break;
                case
                0x41:
                    LD_B_C(this);
                    break;
                case
                0x42:
                    LD_B_D(this);
                    break;
                case
                0x43:
                    LD_B_E(this);
                    break;
                case
                0x44:
                    LD_B_H(this);
                    break;
                case
                0x45:
                    LD_B_L(this);
                    break;
                case
                0x46:
                    LD_B_HL(this);
                    break;
                case
                0x47:
                    LD_B_A(this);
                    break;
                #endregion

                #region LD_C
                case
                0x48:
                    LD_C_B(this);
                    break;
                case
                0x49:
                    LD_C_C(this);
                    break;
                case
                0x4A:
                    LD_C_D(this);
                    break;
                case
                0x4B:
                    LD_C_E(this);
                    break;
                case
                0x4C:
                    LD_C_H(this);
                    break;
                case
                0x4D:
                    LD_C_L(this);
                    break;
                case
                0x4E:
                    LD_C_HL(this);
                    break;
                case
                0x4F:
                    LD_C_A(this);
                    break;
                #endregion

                #region LD_D
                case
                0x50:
                    LD_D_B(this);
                    break;
                case
                0x51:
                    LD_D_C(this);
                    break;
                case
                0x52:
                    LD_D_D(this);
                    break;
                case
                0x53:
                    LD_D_E(this);
                    break;
                case
                0x54:
                    LD_D_H(this);
                    break;
                case
                0x55:
                    LD_D_L(this);
                    break;
                case
                0x56:
                    LD_D_HL(this);
                    break;
                case
                0x57:
                    LD_D_A(this);
                    break;
                #endregion

                #region LD_E
                case
                0x58:
                    LD_E_B(this);
                    break;
                case
                0x59:
                    LD_E_C(this);
                    break;
                case
                0x5A:
                    LD_E_D(this);
                    break;
                case
                0x5B:
                    LD_E_E(this);
                    break;
                case
                0x5C:
                    LD_E_H(this);
                    break;
                case
                0x5D:
                    LD_E_L(this);
                    break;
                case
                0x5E:
                    LD_E_HL(this);
                    break;
                case
                0x5F:
                    LD_E_A(this);
                    break;
                #endregion

                #region LD_H
                case
                0x60:
                    LD_H_B(this);
                    break;
                case
                0x61:
                    LD_H_C(this);
                    break;
                case
                0x62:
                    LD_H_D(this);
                    break;
                case
                0x63:
                    LD_H_E(this);
                    break;
                case
                0x64:
                    LD_H_H(this);
                    break;
                case
                0x65:
                    LD_H_L(this);
                    break;
                case
                0x66:
                    LD_H_HL(this);
                    break;
                case
                0x67:
                    LD_H_A(this);
                    break;
                #endregion

                #region LD_L
                case
                0x68:
                    LD_L_B(this);
                    break;
                case
                0x69:
                    LD_L_C(this);
                    break;
                case
                0x6A:
                    LD_L_D(this);
                    break;
                case
                0x6B:
                    LD_L_E(this);
                    break;
                case
                0x6C:
                    LD_L_H(this);
                    break;
                case
                0x6D:
                    LD_L_L(this);
                    break;
                case
                0x6E:
                    LD_L_HL(this);
                    break;
                case
                0x6F:
                    LD_L_A(this);
                    break;
                #endregion

                #region LD_HL
                case
                0x70:
                    LD_HL_B(this);
                    break;
                case
                0x71:
                    LD_HL_C(this);
                    break;
                case
                0x72:
                    LD_HL_D(this);
                    break;
                case
                0x73:
                    LD_HL_E(this);
                    break;
                case
                0x74:
                    LD_HL_H(this);
                    break;
                case
                0x75:
                    LD_HL_L(this);
                    break;
                case
                0x77:
                    LD_HL_A(this);
                    break;
                case
                0x22:
                    LD_HL_A_INC(this);
                    break;
                case
                0x32:
                    LD_HL_A_DEC(this);
                    break;
                #endregion

                #region LD_A
                case
                0x78:
                    LD_A_B(this);
                    break;
                case
                0x79:
                    LD_A_C(this);
                    break;
                case
                0x7A:
                    LD_A_D(this);
                    break;
                case
                0x7B:
                    LD_A_E(this);
                    break;
                case
                0x7C:
                    LD_A_H(this);
                    break;
                case
                0x7D:
                    LD_A_L(this);
                    break;
                case
                0x7E:
                    LD_A_HL(this);
                    break;
                case
                0x7F:
                    LD_A_A(this);
                    break;
                case 0x2A:
                    LD_A_HL_INC(this);
                    break;
                case 0x3A:
                    LD_A_HL_DEC(this);
                    break;

                #endregion

                #region LD_R16_D16
                case
                0x01:
                    LD_BC_D16(this);
                    break;

                case
                0x11:
                    LD_DE_D16(this);
                    break;
                case
                0x21:
                    LD_HL_D16(this);
                    break;

                case
                0x31:
                    LD_SP_D16(this);
                    break;
                #endregion

                #region LD_(R16)_A
                case
                0x02:
                    LD_BC_A(this);
                    break;
                case
                0x12:
                    LD_DE_A(this);
                    break;
                #endregion

                #region LDH
                case
                0xE0:
                    LDH_A8_A(this);
                    break;
                case
                0xF0:
                    LDH_A_A8(this);
                    break;
                #endregion

                case 0xEA:
                    LD_A16_A(this);
                    break;
                case 0xFA:
                    LD_A_A16(this);
                    break;
                case 0xE2:
                    LDH_C_A(this);
                    break;
                case 0xF2:
                    LDH_A_C(this);
                    break;
                case 0xF8:
                    LD_HL_SP_R8(this);
                    break;
                #endregion

                #region ADD
                case
                0x80:
                    ADD_A_B(this);
                    break;
                case
                0x81:
                    ADD_A_C(this);
                    break;
                case
                0x82:
                    ADD_A_D(this);
                    break;
                case
                0x83:
                    ADD_A_E(this);
                    break;
                case
                0x84:
                    ADD_A_H(this);
                    break;
                case 0x85:
                    ADD_A_L(this);
                    break;

                case 0x86:
                    ADD_A_HL(this);
                    break;
                case 0x87:
                    ADD_A_A(this);
                    break;
                case 0xC6:
                    ADD_A_D8(this);
                    break;
                case 0x09:
                    ADD_HL_BC(this);
                    break;

                case 0x19:
                    ADD_HL_DE(this);
                    break;
                case 0x29:
                    ADD_HL_HL(this);
                    break;
                case 0x39:
                    ADD_HL_SP(this);
                    break;
                #endregion

                #region CP
                case
                0xBF:
                    CP_A(this);
                    break;
                case
                0xB8:
                    CP_B(this);
                    break;
                case
                0xB9:
                    CP_C(this);
                    break;
                case
                0xBA:
                    CP_D(this);
                    break;
                case
                0xBB:
                    CP_E(this);
                    break;
                case
                0xBC:
                    CP_H(this);
                    break;
                case
                0xBD:
                    CP_L(this);
                    break;
                case
                0xBE:
                    CP_HL(this);
                    break;
                case
                0xFE:
                    CP_D8(this);
                    break;
                #endregion

                #region SUB
                case
                0x9F:
                    SUB_A(this);
                    break;
                case
                0x98:
                    SUB_B(this);
                    break;
                case
                0x99:
                    SUB_C(this);
                    break;
                case
                0x9A:
                    SUB_D(this);
                    break;
                case
                0x9B:
                    SUB_E(this);
                    break;
                case
                0x9C:
                    SUB_H(this);
                    break;
                case
                0x9D:
                    SUB_L(this);
                    break;
                case
                0x9E:
                    SUB_HL(this);
                    break;
                case
                0xD6:
                    SUB_D8(this);
                    break;
                #endregion

                #region JP
                case
                0xC2:
                    JP_NZ(this);
                    break;
                case
                0xC3:
                    JP(this);
                    break;
                case
                0xCA:
                    JP_Z(this);
                    break;
                case
                0xD2:
                    JP_NC(this);
                    break;
                case
                0xDA:
                    JP_C(this);
                    break;
                case 0xE9:
                    JP_HL(this);
                    break;
                #endregion
                #region JR
                case
                0x18:
                    JR(this);
                    break;
                case
                0x20:
                    JR_NZ(this);
                    break;
                case
                0x30:
                    JR_NC(this);
                    break;
                case
                0x28:
                    JR_Z(this);
                    break;
                case
                0x38:
                    JR_C(this);
                    break;
                #endregion
                #region POP
                case
                0xC1:
                    POP_BC(this);
                    break;
                case
                0xD1:
                    POP_DE(this);
                    break;
                case
                0xE1:
                    POP_HL(this);
                    break;
                case
                0xF1:
                    POP_AF(this);
                    break;
                #endregion

                #region PUSH
                case
                0xC5:
                    PUSH_BC(this);
                    break;
                case
                0xD5:
                    PUSH_DE(this);
                    break;
                case
                0xE5:
                    PUSH_HL(this);
                    break;
                case
                0xF5:
                    PUSH_AF(this);
                    break;
                #endregion

                #region CALL
                case
                0xC4:
                    CALL_NZ(this);
                    break;
                case
                0xCC:
                    CALL_Z(this);
                    break;
                case
                0xCD:
                    CALL(this);
                    break;
                case
                0xD4:
                    CALL_NC(this);
                    break;
                case
                0xDC:
                    CALL_C(this);
                    break;
                #endregion

                #region RET
                case
                0xC0:
                    RET_NZ(this);
                    break;
                case
                0xC8:
                    RET_Z(this);
                    break;
                case
                0xC9:
                    RET(this);
                    break;
                case
                0xD0:
                    RET_NC(this);
                    break;
                case
                0xD8:
                    RET_C(this);
                    break;
                case 0xD9:
                    RETI(this);
                    break;
                #endregion

                #region RST
                case
                0xC7:
                    RST_00(this);
                    break;
                case
                0xCF:
                    RST_08(this);
                    break;
                case
                0xD7:
                    RST_10(this);
                    break;
                case
                0xDF:
                    RST_18(this);
                    break;
                case
                0xE7:
                    RST_20(this);
                    break;
                case
                0xEF:
                    RST_28(this);
                    break;
                case
                0xF7:
                    RST_30(this);
                    break;
                case
                0xFF:
                    RST_38(this);
                    break;
                #endregion

                #region AND
                case
                0xE6:
                    AND_D8(this);
                    break;
                case
                0xA0:
                    AND_B(this);
                    break;
                case
                0xA1:
                    AND_C(this);
                    break;
                case
                0xA2:
                    AND_D(this);
                    break;
                case
                0xA3:
                    AND_E(this);
                    break;
                case
                0xA4:
                    AND_H(this);
                    break;
                case
                0xA5:
                    AND_L(this);
                    break;
                case
                0xA6:
                    AND_HL(this);
                    break;
                case
                0xA7:
                    AND_A(this);
                    break;
                #endregion

                #region OR
                case
                0xF6:
                    OR_D8(this);
                    break;
                case
                0xB0:
                    OR_B(this);
                    break;
                case
                0xB1:
                    OR_C(this);
                    break;
                case
                0xB2:
                    OR_D(this);
                    break;
                case
                0xB3:
                    OR_E(this);
                    break;
                case
                0xB4:
                    OR_H(this);
                    break;
                case
                0xB5:
                    OR_L(this);
                    break;
                case
                0xB6:
                    OR_HL(this);
                    break;
                case
                0xB7:
                    OR_A(this);
                    break;
                #endregion

                #region XOR
                case
                0xEE:
                    XOR_D8(this);
                    break;
                case
                0xA8:
                    XOR_B(this);
                    break;
                case
                0xA9:
                    XOR_C(this);
                    break;
                case
                0xAA:
                    XOR_D(this);
                    break;
                case
                0xAB:
                    XOR_E(this);
                    break;
                case
                0xAC:
                    XOR_H(this);
                    break;
                case
                0xAD:
                    XOR_L(this);
                    break;
                case
                0xAE:
                    XOR_HL(this);
                    break;
                case
                0xAF:
                    XOR_A(this);
                    break;
                #endregion

                #region Bitwise Logic

                case
                0x0F:
                    RRCA(this);
                    break;
                case 0x1F:
                    RR_A_nonprefix(this);
                    break;
                case 0x2F:
                    CPL(this);
                    break;
                #endregion

                default:
                    throw new NotImplementedException($"nonprefixed:{opcode.ToString("x2")}");
            }
        }
        public void PrefixedInstructions(byte opcode)
        {
            switch (opcode)
            {
                #region RR
                case 0x18:
                    RR_B(this);
                    break;
                case 0x19:
                    RR_C(this);
                    break;
                case 0x1A:
                    RR_D(this);
                    break;
                case 0x1B:
                    RR_E(this);
                    break;
                case 0x1C:
                    RR_H(this);
                    break;
                case 0x1D:
                    RR_L(this);
                    break;
                case 0x1E:
                    RR_HL(this);
                    break;
                case 0x1F:
                    RR_A(this);
                    break;
                #endregion

                #region SRL
                case 0x38:
                    SRL_B(this);
                    break;
                case 0x39:
                    SRL_C(this);
                    break;
                case 0x3A:
                    SRL_D(this);
                    break;
                case 0x3B:
                    SRL_E(this);
                    break;
                case 0x3C:
                    SRL_H(this);
                    break;
                case 0x3D:
                    SRL_L(this);
                    break;
                case 0x3E:
                    SRL_HL(this);
                    break;
                case 0x3F:
                    SRL_A(this);
                    break;
                #endregion

                #region Bit
                #region Bit 0
                case
                0x47:
                    BIT_0_A(this);
                    break;
                case
                0x40:
                    BIT_0_B(this);
                    break;
                case
                0x41:
                    BIT_0_C(this);
                    break;
                case
                0x42:
                    BIT_0_D(this);
                    break;
                case
                0x43:
                    BIT_0_E(this);
                    break;
                case
                0x44:
                    BIT_0_H(this);
                    break;
                case
                0x45:
                    BIT_0_L(this);
                    break;
                case
                0x46:
                    BIT_0_HL(this);
                    break;
                #endregion
                #region Bit 1
                case
                0x4F:
                    BIT_1_A(this);
                    break;
                case
                0x48:
                    BIT_1_B(this);
                    break;
                case
                0x49:
                    BIT_1_C(this);
                    break;
                case
                0x4A:
                    BIT_1_D(this);
                    break;
                case
                0x4B:
                    BIT_1_E(this);
                    break;
                case
                0x4C:
                    BIT_1_H(this);
                    break;
                case
                0x4D:
                    BIT_1_L(this);
                    break;
                case
                0x4E:
                    BIT_1_HL(this);
                    break;
                #endregion
                #region Bit 2
                case
                0x57:
                    BIT_2_A(this);
                    break;
                case
                0x50:
                    BIT_2_B(this);
                    break;
                case
                0x51:
                    BIT_2_C(this);
                    break;
                case
                0x52:
                    BIT_2_D(this);
                    break;
                case
                0x53:
                    BIT_2_E(this);
                    break;
                case
                0x54:
                    BIT_2_H(this);
                    break;
                case
                0x55:
                    BIT_2_L(this);
                    break;
                case
                0x56:
                    BIT_2_HL(this);
                    break;
                #endregion
                #region Bit 3
                case
                0x5F:
                    BIT_3_A(this);
                    break;
                case
                0x58:
                    BIT_3_B(this);
                    break;
                case
                0x59:
                    BIT_3_C(this);
                    break;
                case
                0x5A:
                    BIT_3_D(this);
                    break;
                case
                0x5B:
                    BIT_3_E(this);
                    break;
                case
                0x5C:
                    BIT_3_H(this);
                    break;
                case
                0x5D:
                    BIT_3_L(this);
                    break;
                case
                0x5E:
                    BIT_3_HL(this);
                    break;
                #endregion
                #region Bit 4
                case
                0x67:
                    BIT_4_A(this);
                    break;
                case
                0x60:
                    BIT_4_B(this);
                    break;
                case
                0x61:
                    BIT_4_C(this);
                    break;
                case
                0x62:
                    BIT_4_D(this);
                    break;
                case
                0x63:
                    BIT_4_E(this);
                    break;
                case
                0x64:
                    BIT_4_H(this);
                    break;
                case
                0x65:
                    BIT_4_L(this);
                    break;
                case
                0x66:
                    BIT_4_HL(this);
                    break;
                #endregion
                #region Bit 5
                case
                0x6F:
                    BIT_5_A(this);
                    break;
                case
                0x68:
                    BIT_5_B(this);
                    break;
                case
                0x69:
                    BIT_5_C(this);
                    break;
                case
                0x6A:
                    BIT_5_D(this);
                    break;
                case
                0x6B:
                    BIT_5_E(this);
                    break;
                case
                0x6C:
                    BIT_5_H(this);
                    break;
                case
                0x6D:
                    BIT_5_L(this);
                    break;
                case
                0x6E:
                    BIT_5_HL(this);
                    break;
                #endregion
                #region Bit 6
                case
                0x77:
                    BIT_6_A(this);
                    break;
                case
                0x70:
                    BIT_6_B(this);
                    break;
                case
                0x71:
                    BIT_6_C(this);
                    break;
                case
                0x72:
                    BIT_6_D(this);
                    break;
                case
                0x73:
                    BIT_6_E(this);
                    break;
                case
                0x74:
                    BIT_6_H(this);
                    break;
                case
                0x75:
                    BIT_6_L(this);
                    break;
                case
                0x76:
                    BIT_6_HL(this);
                    break;
                #endregion
                #region Bit 7
                case
                0x7F:
                    BIT_7_A(this);
                    break;
                case
                0x78:
                    BIT_7_B(this);
                    break;
                case
                0x79:
                    BIT_7_C(this);
                    break;
                case
                0x7A:
                    BIT_7_D(this);
                    break;
                case
                0x7B:
                    BIT_7_E(this);
                    break;
                case
                0x7C:
                    BIT_7_H(this);
                    break;
                case
                0x7D:
                    BIT_7_L(this);
                    break;
                case
                0x7E:
                    BIT_7_HL(this);
                    break;
                #endregion

                #region SWAP
                case 0x37:
                    SWAP_A(this);
                    break;
                case 0x30:
                    SWAP_B(this);
                    break;
                case 0x31:
                    SWAP_C(this);
                    break;
                case 0x32:
                    SWAP_C(this);
                    break;
                case 0x33:
                    SWAP_D(this);
                    break;
                case 0x34:
                    SWAP_E(this);
                    break;
                case 0x35:
                    SWAP_H(this);
                    break;
                case 0x36:
                    SWAP_HL(this);
                    break;
                #endregion
                #endregion

                #region RES
                #region RES_0
                case 0x80:
                    RES_0_B(this);
                    break;
                case 0x81:
                    RES_0_C(this);
                    break;
                case 0x82:
                    RES_0_D(this);
                    break;
                case 0x83:
                    RES_0_E(this);
                    break;
                case 0x84:
                    RES_0_H(this);
                    break;
                case 0x85:
                    RES_0_L(this);
                    break;
                case 0x86:
                    RES_0_HL(this);
                    break;
                case 0x87:
                    RES_0_A(this);
                    break;
                #endregion

                #region RES_1
                case 0x88:
                    RES_1_B(this);
                    break;
                case 0x89:
                    RES_1_C(this);
                    break;
                case 0x8A:
                    RES_1_D(this);
                    break;
                case 0x8B:
                    RES_1_E(this);
                    break;
                case 0x8C:
                    RES_1_H(this);
                    break;
                case 0x8D:
                    RES_1_L(this);
                    break;
                case 0x8E:
                    RES_1_HL(this);
                    break;
                case 0x8F:
                    RES_1_A(this);
                    break;
                #endregion
                #region RES_2
                case 0x90:
                    RES_2_B(this);
                    break;
                case 0x91:
                    RES_2_C(this);
                    break;
                case 0x92:
                    RES_2_D(this);
                    break;
                case 0x93:
                    RES_2_E(this);
                    break;
                case 0x94:
                    RES_2_H(this);
                    break;
                case 0x95:
                    RES_2_L(this);
                    break;
                case 0x96:
                    RES_2_HL(this);
                    break;
                case 0x97:
                    RES_2_A(this);
                    break;
                #endregion

                #region RES_3
                case 0x98:
                    RES_3_B(this);
                    break;
                case 0x99:
                    RES_3_C(this);
                    break;
                case 0x9A:
                    RES_3_D(this);
                    break;
                case 0x9B:
                    RES_3_E(this);
                    break;
                case 0x9C:
                    RES_3_H(this);
                    break;
                case 0x9D:
                    RES_3_L(this);
                    break;
                case 0x9E:
                    RES_3_HL(this);
                    break;
                case 0x9F:
                    RES_3_A(this);
                    break;
                #endregion
                #region RES_4
                case 0xA0:
                    RES_4_B(this);
                    break;
                case 0xA1:
                    RES_4_C(this);
                    break;
                case 0xA2:
                    RES_4_D(this);
                    break;
                case 0xA3:
                    RES_4_E(this);
                    break;
                case 0xA4:
                    RES_4_H(this);
                    break;
                case 0xA5:
                    RES_4_L(this);
                    break;
                case 0xA6:
                    RES_4_HL(this);
                    break;
                case 0xA7:
                    RES_4_A(this);
                    break;
                #endregion

                #region RES_5
                case 0xA8:
                    RES_5_B(this);
                    break;
                case 0xA9:
                    RES_5_C(this);
                    break;
                case 0xAA:
                    RES_5_D(this);
                    break;
                case 0xAB:
                    RES_5_E(this);
                    break;
                case 0xAC:
                    RES_5_H(this);
                    break;
                case 0xAD:
                    RES_5_L(this);
                    break;
                case 0xAE:
                    RES_5_HL(this);
                    break;
                case 0xAF:
                    RES_5_A(this);
                    break;
                #endregion
                #region RES_6
                case 0xB0:
                    RES_6_B(this);
                    break;
                case 0xB1:
                    RES_6_C(this);
                    break;
                case 0xB2:
                    RES_6_D(this);
                    break;
                case 0xB3:
                    RES_6_E(this);
                    break;
                case 0xB4:
                    RES_6_H(this);
                    break;
                case 0xB5:
                    RES_6_L(this);
                    break;
                case 0xB6:
                    RES_6_HL(this);
                    break;
                case 0xB7:
                    RES_6_A(this);
                    break;
                #endregion

                #region RES_7
                case 0xB8:
                    RES_7_B(this);
                    break;
                case 0xB9:
                    RES_7_C(this);
                    break;
                case 0xBA:
                    RES_7_D(this);
                    break;
                case 0xBB:
                    RES_7_E(this);
                    break;
                case 0xBC:
                    RES_7_H(this);
                    break;
                case 0xBD:
                    RES_7_L(this);
                    break;
                case 0xBE:
                    RES_7_HL(this);
                    break;
                case 0xBF:
                    RES_7_A(this);
                    break;
                #endregion
                #endregion

                default:
                    throw new NotImplementedException($"prefixed:{opcode.ToString("x2")}");
            }
        }

        public Dictionary<string, Action<CPU>> InstructionSet;

        #endregion

    }

}