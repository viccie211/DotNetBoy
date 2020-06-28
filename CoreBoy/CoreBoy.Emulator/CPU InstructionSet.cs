using System;
using System.Collections.Generic;

namespace CoreBoy.Emulator
{
    public partial class CPU
    {
        #region Properties

        public Dictionary<byte, string> NonPrefixedInstructions = new Dictionary<byte, string>()
        {
            {
                0x00, OpcodeNames.NOP
            },
            {
                0x10,OpcodeNames.STOP
            },
            {
                0x76,OpcodeNames.HALT
            },
            {
                0x3F,OpcodeNames.CCF
            },
            {
                0xF3,OpcodeNames.DI
            },


            #region INC
            #region INC_R16
            {
                0x03,OpcodeNames.INC_BC
            },
            {
                0x13,OpcodeNames.INC_DE
            },
            {
                0x23,OpcodeNames.INC_HL
            },
            {
                0x33,OpcodeNames.INC_SP
            },
            #endregion
            #region INC_R8
            {
                0x04,OpcodeNames.INC_B
            },
            {
                0x0C,OpcodeNames.INC_C
            },
            {
                0x14,OpcodeNames.INC_D
            },
            {
                0x1C,OpcodeNames.INC_E
            },
            {
                0x24,OpcodeNames.INC_H
            },
            {
                0x2C,OpcodeNames.INC_L
            },
            {
                0x3C,OpcodeNames.INC_A
            },
            #endregion
            #endregion

            #region DEC
            #region DEC_R16
            {
                0x0B,OpcodeNames.DEC_BC
            },
            {
                0x1B,OpcodeNames.DEC_DE
            },
            {
                0x2B,OpcodeNames.DEC_HL
            },
            {
                0x3B,OpcodeNames.DEC_SP
            },
            #endregion
            #region DEC_R8
            {
                0x05,OpcodeNames.DEC_B
            },
            {
                0x0D,OpcodeNames.DEC_C
            },
            {
                0x15,OpcodeNames.DEC_D
            },
            {
                0x1D,OpcodeNames.DEC_E
            },
            {
                0x25,OpcodeNames.DEC_H
            },
            {
                0x2D,OpcodeNames.DEC_L
            },
            {
                0x3D,OpcodeNames.DEC_A
            },
            #endregion
            #endregion

            #region LD
            #region LD_D8
            {
                0x06, OpcodeNames.LD_B_D8
            },
            {
                0x0E, OpcodeNames.LD_C_D8
            },
            {
                0x16, OpcodeNames.LD_D_D8
            },
            {
                0x1E, OpcodeNames.LD_E_D8
            },
            {
                0x26, OpcodeNames.LD_H_D8
            },
            {
                0x2E, OpcodeNames.LD_L_D8
            },
            {
                0x36, OpcodeNames.LD_HL_D8
            },
            {
                0x3E, OpcodeNames.LD_A_D8
            },
            #endregion

    	    #region LD_B
            {
                0x40,OpcodeNames.LD_B_B
            },
            {
                0x41,OpcodeNames.LD_B_C
            },
            {
                0x42,OpcodeNames.LD_B_D
            },
            {
                0x43,OpcodeNames.LD_B_E
            },
            {
                0x44,OpcodeNames.LD_B_H
            },
            {
                0x45,OpcodeNames.LD_B_L
            },
            {
                0x46,OpcodeNames.LD_B_HL
            },
            {
                0x47,OpcodeNames.LD_B_A
            },
            #endregion

            #region LD_C
            {
                0x48,OpcodeNames.LD_C_B
            },
            {
                0x49,OpcodeNames.LD_C_C
            },
            {
                0x4A,OpcodeNames.LD_C_D
            },
            {
                0x4B,OpcodeNames.LD_C_E
            },
            {
                0x4C,OpcodeNames.LD_C_H
            },
            {
                0x4D,OpcodeNames.LD_C_L
            },
            {
                0x4E,OpcodeNames.LD_C_HL
            },
            {
                0x4F,OpcodeNames.LD_C_A
            },
            #endregion

            #region LD_D
            {
                0x50,OpcodeNames.LD_D_B
            },
            {
                0x51,OpcodeNames.LD_D_C
            },
            {
                0x52,OpcodeNames.LD_D_D
            },
            {
                0x53,OpcodeNames.LD_D_E
            },
            {
                0x54,OpcodeNames.LD_D_H
            },
            {
                0x55,OpcodeNames.LD_D_L
            },
            {
                0x56,OpcodeNames.LD_D_HL
            },
            {
                0x57,OpcodeNames.LD_D_A
            },
            #endregion

            #region LD_E
            {
                0x58,OpcodeNames.LD_E_B
            },
            {
                0x59,OpcodeNames.LD_E_C
            },
            {
                0x5A,OpcodeNames.LD_E_D
            },
            {
                0x5B,OpcodeNames.LD_E_E
            },
            {
                0x5C,OpcodeNames.LD_E_H
            },
            {
                0x5D,OpcodeNames.LD_E_L
            },
            {
                0x5E,OpcodeNames.LD_E_HL
            },
            {
                0x5F,OpcodeNames.LD_E_A
            },
            #endregion

            #region LD_H
            {
                0x60,OpcodeNames.LD_H_B
            },
            {
                0x61,OpcodeNames.LD_H_C
            },
            {
                0x62,OpcodeNames.LD_H_D
            },
            {
                0x63,OpcodeNames.LD_H_E
            },
            {
                0x64,OpcodeNames.LD_H_H
            },
            {
                0x65,OpcodeNames.LD_H_L
            },
            {
                0x66,OpcodeNames.LD_H_HL
            },
            {
                0x67,OpcodeNames.LD_H_A
            },
            #endregion

            #region LD_L
            {
                0x68,OpcodeNames.LD_L_B
            },
            {
                0x69,OpcodeNames.LD_L_C
            },
            {
                0x6A,OpcodeNames.LD_L_D
            },
            {
                0x6B,OpcodeNames.LD_L_E
            },
            {
                0x6C,OpcodeNames.LD_L_H
            },
            {
                0x6D,OpcodeNames.LD_L_L
            },
            {
                0x6E,OpcodeNames.LD_L_HL
            },
            {
                0x6F,OpcodeNames.LD_L_A
            },
            #endregion

            #region LD_HL
            {
                0x70,OpcodeNames.LD_HL_B
            },
            {
                0x71,OpcodeNames.LD_HL_C
            },
            {
                0x72,OpcodeNames.LD_HL_D
            },
            {
                0x73,OpcodeNames.LD_HL_E
            },
            {
                0x74,OpcodeNames.LD_HL_H
            },
            {
                0x75,OpcodeNames.LD_HL_L
            },
            {
                0x77,OpcodeNames.LD_HL_A
            },
            #endregion

            #region LD_A
            {
                0x78,OpcodeNames.LD_A_B
            },
            {
                0x79,OpcodeNames.LD_A_C
            },
            {
                0x7A,OpcodeNames.LD_A_D
            },
            {
                0x7B,OpcodeNames.LD_A_E
            },
            {
                0x7C,OpcodeNames.LD_A_H
            },
            {
                0x7D,OpcodeNames.LD_A_L
            },
            {
                0x7E,OpcodeNames.LD_A_HL
            },
            {
                0x7F,OpcodeNames.LD_A_A
            },
            #endregion
            
            #region LD_R16_D16
            {
                0x01, OpcodeNames.LD_BC_D16
            },

                        {
                0x11, OpcodeNames.LD_DE_D16
            },
                        {
                0x21, OpcodeNames.LD_HL_D16
            },

                        {
                0x31, OpcodeNames.LD_SP_D16
            },
            #endregion
            
            #region LD_(R16)_A
            {
                0x02,OpcodeNames.LD_BC_A
            },
            {
                0x12,OpcodeNames.LD_DE_A
            },
            #endregion
            
            #region LDH
            {
                0xE0, OpcodeNames.LDH_A8_A
            },
            {
                0xF0, OpcodeNames.LDH_A_A8
            },
            #endregion

            #endregion            
            
            #region ADD
            {
                0x80, OpcodeNames.ADD_A_B
            },
            {
                0x81, OpcodeNames.ADD_A_C
            },
            {
                0x82, OpcodeNames.ADD_A_D
            },
            {
                0x83, OpcodeNames.ADD_A_E
            },
            {
                0x84, OpcodeNames.ADD_A_H
            },
            #endregion

            #region CP
            {
                0xBF, OpcodeNames.CP_A
            },
            {
                0xB8, OpcodeNames.CP_B
            },
            {
                0xB9, OpcodeNames.CP_C
            },
            {
                0xBA, OpcodeNames.CP_D
            },
            {
                0xBB, OpcodeNames.CP_E
            },
            {
                0xBC, OpcodeNames.CP_H
            },
            {
                0xBD, OpcodeNames.CP_L
            },
            {
                0xBE, OpcodeNames.CP_HL
            },
            {
                0xFE, OpcodeNames.CP_D8
            },
            #endregion

            #region SUB
            {
                0x9F, OpcodeNames.SUB_A
            },
            {
                0x98, OpcodeNames.SUB_B
            },
            {
                0x99, OpcodeNames.SUB_C
            },
            {
                0x9A, OpcodeNames.SUB_D
            },
            {
                0x9B, OpcodeNames.SUB_E
            },
            {
                0x9C, OpcodeNames.SUB_H
            },
            {
                0x9D, OpcodeNames.SUB_L
            },
            {
                0x9E, OpcodeNames.SUB_HL
            },
            {
                0xD6, OpcodeNames.SUB_D8
            },
            #endregion

            #region JP
            {
                0xC2, OpcodeNames.JP_NZ
            },
            {
                0xC3, OpcodeNames.JP
            },
            {
                0xCA, OpcodeNames.JP_Z
            },
            {
                0xD2, OpcodeNames.JP_NC
            },
            {
                0xDA, OpcodeNames.JP_C
            },
            #endregion
            #region JR
            {
                0x18, OpcodeNames.JR
            },
            {
                0x20,OpcodeNames.JR_NZ
            },
            {
                0x30,OpcodeNames.JR_NC
            },
            {
                0x28,OpcodeNames.JR_Z
            },
            {
                0x38,OpcodeNames.JR_C
            },
            #endregion
            #region POP
            {
                0xC1,OpcodeNames.POP_BC
            },
            {
                0xD1,OpcodeNames.POP_DE
            },
            {
                0xE1,OpcodeNames.POP_HL
            },
            {
                0xF1,OpcodeNames.POP_AF
            },
            #endregion

            #region PUSH
            {
                0xC5,OpcodeNames.PUSH_BC
            },
            {
                0xD5,OpcodeNames.PUSH_DE
            },
            {
                0xE5,OpcodeNames.PUSH_HL
            },
            {
                0xF5,OpcodeNames.PUSH_AF
            },
            #endregion

            #region CALL
            {
                0xC4, OpcodeNames.CALL_NZ
            },
            {
                0xCC, OpcodeNames.CALL_Z
            },
            {
                0xCD, OpcodeNames.CALL
            },
            {
                0xD4, OpcodeNames.CALL_NC
            },
            {
                0xDC, OpcodeNames.CALL_C
            },
            #endregion

            #region RET
            {
                0xC0, OpcodeNames.RET_NZ
            },
            {
                0xC8, OpcodeNames.RET_Z
            },
            {
                0xC9, OpcodeNames.RET
            },
            {
                0xD0, OpcodeNames.RET_NC
            },
            {
                0xD8, OpcodeNames.RET_C
            },
            #endregion

            #region RST
            {
                0xC7,OpcodeNames.RST_00
            },
            {
                0xCF,OpcodeNames.RST_08
            },
            {
                0xD7,OpcodeNames.RST_10
            },
            {
                0xDF,OpcodeNames.RST_18
            },
            {
                0xE7,OpcodeNames.RST_20
            },
            {
                0xEF,OpcodeNames.RST_28
            },
            {
                0xF7,OpcodeNames.RST_30
            },
            {
                0xFF,OpcodeNames.RST_38
            },
            #endregion
           
            #region AND
             {
                0xE6,OpcodeNames.AND_D8
            },
            {
                0xA0, OpcodeNames.AND_B
            },
            {
                0xA1, OpcodeNames.AND_C
            },
            {
                0xA2, OpcodeNames.AND_D
            },
            {
                0xA3, OpcodeNames.AND_E
            },
            {
                0xA4, OpcodeNames.AND_H
            },
            {
                0xA5, OpcodeNames.AND_L
            },
            {
                0xA6, OpcodeNames.AND_HL
            },
            {
                0xA7, OpcodeNames.AND_A
            },
            #endregion

            #region OR
             {
                0xF6,OpcodeNames.OR_D8
            },
            {
                0xB0, OpcodeNames.OR_B
            },
            {
                0xB1, OpcodeNames.OR_C
            },
            {
                0xB2, OpcodeNames.OR_D
            },
            {
                0xB3, OpcodeNames.OR_E
            },
            {
                0xB4, OpcodeNames.OR_H
            },
            {
                0xB5, OpcodeNames.OR_L
            },
            {
                0xB6, OpcodeNames.OR_HL
            },
            {
                0xB7, OpcodeNames.OR_A
            },
            #endregion

            #region XOR
             {
                0xEE,OpcodeNames.XOR_D8
            },
            {
                0xA8, OpcodeNames.XOR_B
            },
            {
                0xA9, OpcodeNames.XOR_C
            },
            {
                0xAA, OpcodeNames.XOR_D
            },
            {
                0xAB, OpcodeNames.XOR_E
            },
            {
                0xAC, OpcodeNames.XOR_H
            },
            {
                0xAD, OpcodeNames.XOR_L
            },
            {
                0xAE, OpcodeNames.XOR_HL
            },
            {
                0xAF, OpcodeNames.XOR_A
            },
            #endregion

            #region Bitwise Logic

            {
                0x0F,OpcodeNames.RRCA
            }
            #endregion
            
        };

        public Dictionary<byte, string> PrefixedInstructions = new Dictionary<byte, string>()
        {
            #region Bit
            #region Bit 0
            {
                0x47, OpcodeNames.BIT_0_A
            },
            {
                0x40, OpcodeNames.BIT_0_B
            },
            {
                0x41, OpcodeNames.BIT_0_C
            },
            {
                0x42, OpcodeNames.BIT_0_D
            },
            {
                0x43, OpcodeNames.BIT_0_E
            },
            {
                0x44, OpcodeNames.BIT_0_H
            },
            {
                0x45, OpcodeNames.BIT_0_L
            },
            {
                0x46, OpcodeNames.BIT_0_HL
            },
            #endregion
            #region Bit 1
            {
                0x4F, OpcodeNames.BIT_1_A
            },
            {
                0x48, OpcodeNames.BIT_1_B
            },
            {
                0x49, OpcodeNames.BIT_1_C
            },
            {
                0x4A, OpcodeNames.BIT_1_D
            },
            {
                0x4B, OpcodeNames.BIT_1_E
            },
            {
                0x4C, OpcodeNames.BIT_1_H
            },
            {
                0x4D, OpcodeNames.BIT_1_L
            },
            {
                0x4E, OpcodeNames.BIT_1_HL
            },
            #endregion
            #region Bit 2
            {
                0x57, OpcodeNames.BIT_2_A
            },
            {
                0x50, OpcodeNames.BIT_2_B
            },
            {
                0x51, OpcodeNames.BIT_2_C
            },
            {
                0x52, OpcodeNames.BIT_2_D
            },
            {
                0x53, OpcodeNames.BIT_2_E
            },
            {
                0x54, OpcodeNames.BIT_2_H
            },
            {
                0x55, OpcodeNames.BIT_2_L
            },
            {
                0x56, OpcodeNames.BIT_2_HL
            },
            #endregion
            #region Bit 3
            {
                0x5F, OpcodeNames.BIT_3_A
            },
            {
                0x58, OpcodeNames.BIT_3_B
            },
            {
                0x59, OpcodeNames.BIT_3_C
            },
            {
                0x5A, OpcodeNames.BIT_3_D
            },
            {
                0x5B, OpcodeNames.BIT_3_E
            },
            {
                0x5C, OpcodeNames.BIT_3_H
            },
            {
                0x5D, OpcodeNames.BIT_3_L
            },
            {
                0x5E, OpcodeNames.BIT_3_HL
            },
            #endregion
            #region Bit 4
            {
                0x67, OpcodeNames.BIT_4_A
            },
            {
                0x60, OpcodeNames.BIT_4_B
            },
            {
                0x61, OpcodeNames.BIT_4_C
            },
            {
                0x62, OpcodeNames.BIT_4_D
            },
            {
                0x63, OpcodeNames.BIT_4_E
            },
            {
                0x64, OpcodeNames.BIT_4_H
            },
            {
                0x65, OpcodeNames.BIT_4_L
            },
            {
                0x66, OpcodeNames.BIT_4_HL
            },
            #endregion
            #region Bit 5
            {
                0x6F, OpcodeNames.BIT_5_A
            },
            {
                0x68, OpcodeNames.BIT_5_B
            },
            {
                0x69, OpcodeNames.BIT_5_C
            },
            {
                0x6A, OpcodeNames.BIT_5_D
            },
            {
                0x6B, OpcodeNames.BIT_5_E
            },
            {
                0x6C, OpcodeNames.BIT_5_H
            },
            {
                0x6D, OpcodeNames.BIT_5_L
            },
            {
                0x6E, OpcodeNames.BIT_5_HL
            },
            #endregion
            #region Bit 6
            {
                0x77, OpcodeNames.BIT_6_A
            },
            {
                0x70, OpcodeNames.BIT_6_B
            },
            {
                0x71, OpcodeNames.BIT_6_C
            },
            {
                0x72, OpcodeNames.BIT_6_D
            },
            {
                0x73, OpcodeNames.BIT_6_E
            },
            {
                0x74, OpcodeNames.BIT_6_H
            },
            {
                0x75, OpcodeNames.BIT_6_L
            },
            {
                0x76, OpcodeNames.BIT_6_HL
            },
            #endregion
            #region Bit 7
            {
                0x7F, OpcodeNames.BIT_7_A
            },
            {
                0x78, OpcodeNames.BIT_7_B
            },
            {
                0x79, OpcodeNames.BIT_7_C
            },
            {
                0x7A, OpcodeNames.BIT_7_D
            },
            {
                0x7B, OpcodeNames.BIT_7_E
            },
            {
                0x7C, OpcodeNames.BIT_7_H
            },
            {
                0x7D, OpcodeNames.BIT_7_L
            },
            {
                0x7E, OpcodeNames.BIT_7_HL
            },
            #endregion
            #endregion
        };

        public Dictionary<string, Action<CPU>> InstructionSet;

        #endregion

    }

}