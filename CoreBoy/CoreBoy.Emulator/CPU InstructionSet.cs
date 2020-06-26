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
                0xF3,OpcodeNames.CCF
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
            {
                0x18, OpcodeNames.JR
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

            #region Bitwise Logic
            {
                0xF0,OpcodeNames.RRCA
            }
            #endregion
            
        };

        public Dictionary<byte, string> PrefixedInstructions = new Dictionary<byte, string>()
        {

        };

        public Dictionary<string, Action<CPU>> InstructionSet;
        // public Dictionary<string, Action<CPU>> InstructionSet = new Dictionary<string, Action<CPU>>()
        // {
        //     {
        //         OpcodeNames.NOP,NOP
        //     },
        //     {
        //         OpcodeNames.HALT,HALT
        //     },
        //     {
        //         OpcodeNames.STOP,STOP
        //     },
        //     {
        //         OpcodeNames.CCF,CCF
        //     },


        //     #region INC
        //     #region INC_R16
        //     {
        //         OpcodeNames.INC_BC,INC_BC
        //     },
        //     {
        //         OpcodeNames.INC_DE,INC_DE
        //     },
        //     {
        //         OpcodeNames.INC_HL,INC_HL
        //     },
        //     {
        //         OpcodeNames.INC_SP,INC_SP
        //     },
        //     #endregion
        //     #region INC_R8
        //     {
        //         OpcodeNames.INC_B,INC_B
        //     },
        //     {
        //         OpcodeNames.INC_C,INC_C
        //     },
        //     {
        //         OpcodeNames.INC_D,INC_D
        //     },
        //     {
        //         OpcodeNames.INC_E,INC_E
        //     },
        //     {
        //         OpcodeNames.INC_H,INC_H
        //     },
        //     {
        //         OpcodeNames.INC_L,INC_L
        //     },
        //     {
        //         OpcodeNames.INC_A,INC_A
        //     },
        //     #endregion
        //     #endregion

        //     #region DEC
        //     #region DEC_R16
        //     {
        //         OpcodeNames.DEC_BC,DEC_BC
        //     },
        //     {
        //         OpcodeNames.DEC_DE,DEC_DE
        //     },
        //     {
        //         OpcodeNames.DEC_HL,DEC_HL
        //     },
        //     {
        //         OpcodeNames.DEC_SP,DEC_SP
        //     },
        //     #endregion
        //     #region DEC_R8
        //     {
        //         OpcodeNames.DEC_B,DEC_B
        //     },
        //     {
        //         OpcodeNames.DEC_C,DEC_C
        //     },
        //     {
        //         OpcodeNames.DEC_D,DEC_D
        //     },
        //     {
        //         OpcodeNames.DEC_E,DEC_E
        //     },
        //     {
        //         OpcodeNames.DEC_H,DEC_H
        //     },
        //     {
        //         OpcodeNames.DEC_L,DEC_L
        //     },
        //     {
        //         OpcodeNames.DEC_A,DEC_A
        //     },
        //     #endregion
        //     #endregion

        //     #region LD_D8
        //     {
        //         OpcodeNames.LD_B_D8,LD_B_D8
        //     },
        //     {
        //         OpcodeNames.LD_C_D8,LD_C_D8
        //     },
        //     {
        //         OpcodeNames.LD_D_D8,LD_D_D8
        //     },
        //     {
        //         OpcodeNames.LD_E_D8,LD_E_D8
        //     },
        //     {
        //         OpcodeNames.LD_H_D8,LD_H_D8
        //     },
        //     {
        //         OpcodeNames.LD_L_D8,LD_L_D8
        //     },
        //     {
        //         OpcodeNames.LD_HL_D8,LD_HL_D8
        //     },
        //     {
        //         OpcodeNames.LD_A_D8,LD_A_D8
        //     },
        //     #endregion
        //     #region LD_B
        //     {
        //         OpcodeNames.LD_B_B,LD_B_B
        //     },
        //     {
        //         OpcodeNames.LD_B_C,LD_B_C
        //     },
        //     {
        //         OpcodeNames.LD_B_D,LD_B_D
        //     },
        //     {
        //         OpcodeNames.LD_B_E,LD_B_E
        //     },
        //     {
        //         OpcodeNames.LD_B_H,LD_B_H
        //     },
        //     {
        //         OpcodeNames.LD_B_L,LD_B_L
        //     },
        //     {
        //         OpcodeNames.LD_B_HL,LD_B_HL
        //     },
        //     {
        //         OpcodeNames.LD_B_A,LD_B_A
        //     },
        //     #endregion
        //     #region LD_C
        //     {
        //         OpcodeNames.LD_C_B,LD_C_B
        //     },
        //     {
        //         OpcodeNames.LD_C_C,LD_C_C
        //     },
        //     {
        //         OpcodeNames.LD_C_D,LD_C_D
        //     },
        //     {
        //         OpcodeNames.LD_C_E,LD_C_E
        //     },
        //     {
        //         OpcodeNames.LD_C_H,LD_C_H
        //     },
        //     {
        //         OpcodeNames.LD_C_L,LD_C_L
        //     },
        //     {
        //         OpcodeNames.LD_C_HL,LD_C_HL
        //     },
        //     {
        //         OpcodeNames.LD_C_A,LD_C_A
        //     },
        //     #endregion
        //     #region LD_D
        //     {
        //         OpcodeNames.LD_D_B,LD_D_B
        //     },
        //     {
        //         OpcodeNames.LD_D_C,LD_D_C
        //     },
        //     {
        //         OpcodeNames.LD_D_D,LD_D_D
        //     },
        //     {
        //         OpcodeNames.LD_D_E,LD_D_E
        //     },
        //     {
        //         OpcodeNames.LD_D_H,LD_D_H
        //     },
        //     {
        //         OpcodeNames.LD_D_L,LD_D_L
        //     },
        //     {
        //         OpcodeNames.LD_D_HL,LD_D_HL
        //     },
        //     {
        //         OpcodeNames.LD_D_A,LD_D_A
        //     },
        //     #endregion
        //     #region LD_E
        //     {
        //         OpcodeNames.LD_E_B,LD_E_B
        //     },
        //     {
        //         OpcodeNames.LD_E_C,LD_E_C
        //     },
        //     {
        //         OpcodeNames.LD_E_D,LD_E_D
        //     },
        //     {
        //         OpcodeNames.LD_E_E,LD_E_E
        //     },
        //     {
        //         OpcodeNames.LD_E_H,LD_E_H
        //     },
        //     {
        //         OpcodeNames.LD_E_L,LD_E_L
        //     },
        //     {
        //         OpcodeNames.LD_E_HL,LD_E_HL
        //     },
        //     {
        //         OpcodeNames.LD_E_A,LD_E_A
        //     },
        //     #endregion
        //     #region LD_H
        //     {
        //         OpcodeNames.LD_H_B,LD_H_B
        //     },
        //     {
        //         OpcodeNames.LD_H_C,LD_H_C
        //     },
        //     {
        //         OpcodeNames.LD_H_D,LD_H_D
        //     },
        //     {
        //         OpcodeNames.LD_H_E,LD_H_E
        //     },
        //     {
        //         OpcodeNames.LD_H_H,LD_H_H
        //     },
        //     {
        //         OpcodeNames.LD_H_L,LD_H_L
        //     },
        //     {
        //         OpcodeNames.LD_H_HL,LD_H_HL
        //     },
        //     {
        //         OpcodeNames.LD_H_A,LD_H_A
        //     },
        //     #endregion
        //     #region LD_L
        //     {
        //         OpcodeNames.LD_L_B,LD_L_B
        //     },
        //     {
        //         OpcodeNames.LD_L_C,LD_L_C
        //     },
        //     {
        //         OpcodeNames.LD_L_D,LD_L_D
        //     },
        //     {
        //         OpcodeNames.LD_L_E,LD_L_E
        //     },
        //     {
        //         OpcodeNames.LD_L_H,LD_L_H
        //     },
        //     {
        //         OpcodeNames.LD_L_L,LD_L_L
        //     },
        //     {
        //         OpcodeNames.LD_L_HL,LD_L_HL
        //     },
        //     {
        //         OpcodeNames.LD_L_A,LD_L_A
        //     },
        //     #endregion

        //     #region LD_HL
        //     {
        //         OpcodeNames.LD_HL_B,LD_HL_B
        //     },
        //     {
        //         OpcodeNames.LD_HL_C,LD_HL_C
        //     },
        //     {
        //         OpcodeNames.LD_HL_D,LD_HL_D
        //     },
        //     {
        //         OpcodeNames.LD_HL_E,LD_HL_E
        //     },
        //     {
        //         OpcodeNames.LD_HL_H,LD_HL_H
        //     },
        //     {
        //         OpcodeNames.LD_HL_L,LD_HL_L
        //     },
        //     {
        //         OpcodeNames.LD_HL_A,LD_HL_A
        //     },
        //     #endregion

        //     #region LD_R16_D16
        //     {
        //         OpcodeNames.LD_BC_D16, LD_BC_D16
        //     },
        //     {
        //         OpcodeNames.LD_DE_D16, LD_DE_D16
        //     },
        //     {
        //         OpcodeNames.LD_HL_D16, LD_HL_D16
        //     },
        //     {
        //         OpcodeNames.LD_SP_D16, LD_SP_D16
        //     },
        //     #endregion

        //     #region LD_(R16)_A
        //     {
        //         OpcodeNames.LD_BC_A,LD_BC_A
        //     },
        //     {
        //         OpcodeNames.LD_DE_A,LD_DE_A
        //     },
        //     #endregion

        //     #region ADD
        //     {
        //         OpcodeNames.ADD_A_B, ADD_A_B
        //     },
        //     {
        //         OpcodeNames.ADD_A_C, ADD_A_C
        //     },
        //     {
        //         OpcodeNames.ADD_A_D, ADD_A_D
        //     },
        //     {
        //         OpcodeNames.ADD_A_E, ADD_A_E
        //     },
        //     {
        //         OpcodeNames.ADD_A_H, ADD_A_H
        //     },
        //     #endregion

        //     #region CP
        //     {
        //         OpcodeNames.CP_A,CP_A
        //     },
        //     {
        //         OpcodeNames.CP_B,CP_B
        //     },
        //     {
        //         OpcodeNames.CP_C,CP_C
        //     },
        //     {
        //         OpcodeNames.CP_D,CP_D
        //     },
        //     {
        //         OpcodeNames.CP_E,CP_E
        //     },
        //     {
        //         OpcodeNames.CP_H,CP_H
        //     },
        //     {
        //         OpcodeNames.CP_L,CP_L
        //     },
        //     {
        //         OpcodeNames.CP_HL,CP_HL
        //     },
        //     {
        //         OpcodeNames.CP_D8,CP_D8
        //     },
        //     #endregion


        //     #region JP
        //     {
        //         OpcodeNames.JP,JP
        //     },
        //     {
        //         OpcodeNames.JP_NZ,JP_NZ
        //     },
        //     {
        //         OpcodeNames.JP_NC,JP_NC
        //     },
        //     {
        //         OpcodeNames.JP_Z,JP_Z
        //     },
        //     {
        //         OpcodeNames.JP_C,JP_C
        //     },
        //     {
        //         OpcodeNames.JR,JR
        //     },
        //     #endregion
        //     #region POP
        //     {
        //         OpcodeNames.POP_AF,POP_AF
        //     },
        //     {
        //         OpcodeNames.POP_BC,POP_BC
        //     },
        //     {
        //         OpcodeNames.POP_DE,POP_DE
        //     },
        //     {
        //         OpcodeNames.POP_HL,POP_HL
        //     },
        //     #endregion

        //     #region PUSH
        //     {
        //         OpcodeNames.PUSH_AF,PUSH_AF
        //     },
        //     {
        //         OpcodeNames.PUSH_BC,PUSH_BC
        //     },
        //     {
        //         OpcodeNames.PUSH_DE,PUSH_DE
        //     },
        //     {
        //         OpcodeNames.PUSH_HL,PUSH_HL
        //     },
        //     #endregion
        //     #region CALL
        //     {
        //         OpcodeNames.CALL,CALL_ALWAYS
        //     },
        //     {
        //         OpcodeNames.CALL_C,CALL_C
        //     },
        //     {
        //         OpcodeNames.CALL_NC,CALL_NC
        //     },
        //     {
        //         OpcodeNames.CALL_Z,CALL_Z
        //     },
        //     {
        //         OpcodeNames.CALL_NZ,CALL_NZ
        //     },
        //     #endregion
        //     #region RET
        //     {
        //         OpcodeNames.RET,RET_ALWAYS
        //     },
        //     {
        //         OpcodeNames.RET_C,RET_C
        //     },
        //     {
        //         OpcodeNames.RET_NC,RET_NC
        //     },
        //     {
        //         OpcodeNames.RET_Z,RET_Z
        //     },
        //     {
        //         OpcodeNames.RET_NZ,RET_NZ
        //     },
        //     #endregion

        //     #region RST
        //     {
        //         OpcodeNames.RST_00,RST_00
        //     },
        //     {
        //         OpcodeNames.RST_08,RST_08
        //     },
        //     {
        //         OpcodeNames.RST_10,RST_10
        //     },
        //     {
        //         OpcodeNames.RST_18,RST_18
        //     },
        //     {
        //         OpcodeNames.RST_20,RST_20
        //     },
        //     {
        //         OpcodeNames.RST_28,RST_28
        //     },
        //     {
        //         OpcodeNames.RST_30,RST_30
        //     },
        //     {
        //         OpcodeNames.RST_38,RST_38
        //     },
        //     #endregion

        //     #region Bitwise logic
        //     {
        //         OpcodeNames.RRCA,RRCA
        //     }
        //     #endregion

        // };
        #endregion

    }

}