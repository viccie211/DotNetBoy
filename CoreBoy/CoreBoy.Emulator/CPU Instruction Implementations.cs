using System;
using System.Collections.Generic;

namespace CoreBoy.Emulator
{
    public partial class CPU
    {

        #region Instructions
        #region INC
        #region INC_R16
        public static void INC_BC(CPU cpu)
        {
            cpu._RegBC++;
            cpu._RegPC++;
        }

        public static void INC_DE(CPU cpu)
        {
            cpu._RegDE++;
            cpu._RegPC++;
        }

        public static void INC_HL(CPU cpu)
        {
            cpu._RegHL++;
            cpu._RegPC++;
        }

        public static void INC_SP(CPU cpu)
        {
            cpu._RegSP++;
            cpu._RegPC++;
        }
        #endregion

        #region INC_R8
        public static void INC_B(CPU cpu)
        {
            IncWithFlags(ref cpu._RegB, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void INC_C(CPU cpu)
        {
            IncWithFlags(ref cpu._RegC, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void INC_D(CPU cpu)
        {
            IncWithFlags(ref cpu._RegD, ref cpu._RegF);
            cpu._RegPC++;
        }
        public static void INC_E(CPU cpu)
        {
            IncWithFlags(ref cpu._RegE, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void INC_H(CPU cpu)
        {
            IncWithFlags(ref cpu._RegH, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void INC_L(CPU cpu)
        {
            IncWithFlags(ref cpu._RegL, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void INC_A(CPU cpu)
        {
            IncWithFlags(ref cpu._RegA, ref cpu._RegF);
            cpu._RegPC++;
        }
        #endregion
        #endregion

        #region DEC
        #region DEC_R16
        public static void DEC_BC(CPU cpu)
        {
            cpu._RegBC--;
            cpu._RegPC++;
        }

        public static void DEC_DE(CPU cpu)
        {
            cpu._RegDE--;
            cpu._RegPC++;
        }

        public static void DEC_HL(CPU cpu)
        {
            cpu._RegHL--;
            cpu._RegPC++;
        }

        public static void DEC_SP(CPU cpu)
        {
            cpu._RegSP--;
            cpu._RegPC++;
        }
        #endregion

        #region DEC_R8
        public static void DEC_B(CPU cpu)
        {
            DecWithFlags(ref cpu._RegB, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void DEC_C(CPU cpu)
        {
            DecWithFlags(ref cpu._RegC, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void DEC_D(CPU cpu)
        {
            DecWithFlags(ref cpu._RegD, ref cpu._RegF);
            cpu._RegPC++;
        }
        public static void DEC_E(CPU cpu)
        {
            DecWithFlags(ref cpu._RegE, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void DEC_H(CPU cpu)
        {
            DecWithFlags(ref cpu._RegH, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void DEC_L(CPU cpu)
        {
            DecWithFlags(ref cpu._RegL, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void DEC_A(CPU cpu)
        {
            DecWithFlags(ref cpu._RegA, ref cpu._RegF);
            cpu._RegPC++;
        }
        #endregion
        #endregion

        #region ADD
        public static void ADD_A_B(CPU cpu)
        {
            Add(ref cpu._RegA, cpu._RegB, ref cpu._RegF, ref cpu._RegPC);
        }

        public static void ADD_A_C(CPU cpu)
        {
            Add(ref cpu._RegA, cpu._RegC, ref cpu._RegF, ref cpu._RegPC);
        }

        public static void ADD_A_D(CPU cpu)
        {
            Add(ref cpu._RegA, cpu._RegD, ref cpu._RegF, ref cpu._RegPC);
        }

        public static void ADD_A_E(CPU cpu)
        {
            Add(ref cpu._RegA, cpu._RegE, ref cpu._RegF, ref cpu._RegPC);
        }

        public static void ADD_A_H(CPU cpu)
        {
            Add(ref cpu._RegA, cpu._RegH, ref cpu._RegF, ref cpu._RegPC);
        }
        #endregion


        #region CP

        public static void CP_A(CPU cpu)
        {
            Compare(cpu._RegA, cpu._RegA, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void CP_B(CPU cpu)
        {
            Compare(cpu._RegA, cpu._RegB, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void CP_C(CPU cpu)
        {
            Compare(cpu._RegA, cpu._RegC, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void CP_D(CPU cpu)
        {
            Compare(cpu._RegA, cpu._RegD, ref cpu._RegF);
            cpu._RegPC++;
        }
        public static void CP_E(CPU cpu)
        {
            Compare(cpu._RegA, cpu._RegE, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void CP_H(CPU cpu)
        {
            Compare(cpu._RegA, cpu._RegH, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void CP_L(CPU cpu)
        {
            Compare(cpu._RegA, cpu._RegL, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void CP_HL(CPU cpu)
        {
            Compare(cpu._RegA, cpu._MMU.ReadByte(cpu._RegHL), ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void CP_D8(CPU cpu)
        {
            Compare(cpu._RegA, cpu._MMU.ReadByte(((ushort)(cpu._RegPC + 1))), ref cpu._RegF);
            cpu._RegPC += 2;
        }
        #endregion

        #region SUB

        public static void SUB_A(CPU cpu)
        {
            Sub(ref cpu._RegA, cpu._RegA, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void SUB_B(CPU cpu)
        {
            Sub(ref cpu._RegA, cpu._RegB, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void SUB_C(CPU cpu)
        {
            Sub(ref cpu._RegA, cpu._RegC, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void SUB_D(CPU cpu)
        {
            Sub(ref cpu._RegA, cpu._RegD, ref cpu._RegF);
            cpu._RegPC++;
        }
        public static void SUB_E(CPU cpu)
        {
            Sub(ref cpu._RegA, cpu._RegE, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void SUB_H(CPU cpu)
        {
            Sub(ref cpu._RegA, cpu._RegH, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void SUB_L(CPU cpu)
        {
            Sub(ref cpu._RegA, cpu._RegL, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void SUB_HL(CPU cpu)
        {
            Sub(ref cpu._RegA, cpu._MMU.ReadByte(cpu._RegHL), ref cpu._RegF);
            cpu._RegPC++;
        }
        public static void SUB_D8(CPU cpu)
        {
            Sub(ref cpu._RegA, cpu._MMU.ReadByte(((ushort)(cpu._RegPC + 1))), ref cpu._RegF);
            cpu._RegPC += 2;
        }

        #endregion
        #region JP
        public static void JP(CPU cpu)
        {
            Jump(ref cpu._RegPC, cpu._MMU, true);
        }

        public static void JP_NZ(CPU cpu)
        {
            Jump(ref cpu._RegPC, cpu._MMU, !cpu._RegF.Zero);
        }

        public static void JP_Z(CPU cpu)
        {
            Jump(ref cpu._RegPC, cpu._MMU, cpu._RegF.Zero);
        }

        public static void JP_NC(CPU cpu)
        {
            Jump(ref cpu._RegPC, cpu._MMU, !cpu._RegF.Carry);
        }
        public static void JP_C(CPU cpu)
        {
            Jump(ref cpu._RegPC, cpu._MMU, cpu._RegF.Carry);
        }


        #endregion

        #region JR
        public static void JR(CPU cpu)
        {
            byte toAdd = cpu._MMU.ReadByte((ushort)(cpu._RegPC + 1));
            cpu._RegPC += toAdd;
        }

        public static void JR_NZ(CPU cpu)
        {
            byte toAdd = cpu._MMU.ReadByte((ushort)(cpu._RegPC + 1));
            JumpRelative(ref cpu._RegPC, toAdd, !cpu._RegF.Zero);
        }

        public static void JR_Z(CPU cpu)
        {
            byte toAdd = cpu._MMU.ReadByte((ushort)(cpu._RegPC + 1));
            JumpRelative(ref cpu._RegPC, toAdd, cpu._RegF.Zero);
        }

        public static void JR_NC(CPU cpu)
        {
            byte toAdd = cpu._MMU.ReadByte((ushort)(cpu._RegPC + 1));
            JumpRelative(ref cpu._RegPC, toAdd, !cpu._RegF.Carry);
        }

        public static void JR_C(CPU cpu)
        {
            byte toAdd = cpu._MMU.ReadByte((ushort)(cpu._RegPC + 1));
            JumpRelative(ref cpu._RegPC, toAdd, cpu._RegF.Carry);
        }

        #endregion

        #region LD
        #region LD_A
        public static void LD_A_A(CPU cpu)
        {
            LoadByte(ref cpu._RegA, cpu._RegA);
            cpu._RegPC++;
        }

        public static void LD_A_B(CPU cpu)
        {
            LoadByte(ref cpu._RegA, cpu._RegB);
            cpu._RegPC++;
        }

        public static void LD_A_C(CPU cpu)
        {
            LoadByte(ref cpu._RegA, cpu._RegC);
            cpu._RegPC++;
        }

        public static void LD_A_D(CPU cpu)
        {
            LoadByte(ref cpu._RegA, cpu._RegD);
            cpu._RegPC++;
        }

        public static void LD_A_E(CPU cpu)
        {
            LoadByte(ref cpu._RegA, cpu._RegE);
            cpu._RegPC++;
        }

        public static void LD_A_H(CPU cpu)
        {
            LoadByte(ref cpu._RegA, cpu._RegH);
            cpu._RegPC++;
        }

        public static void LD_A_L(CPU cpu)
        {
            LoadByte(ref cpu._RegA, cpu._RegL);
            cpu._RegPC++;
        }

        public static void LD_A_HL(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegA, cpu._RegHL, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_A_D8(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegA, (ushort)(cpu._RegPC + 1), cpu._MMU);
            cpu._RegPC += 2;
        }
        #endregion

        #region LD_B
        public static void LD_B_A(CPU cpu)
        {
            LoadByte(ref cpu._RegB, cpu._RegA);
            cpu._RegPC++;
        }

        public static void LD_B_B(CPU cpu)
        {
            LoadByte(ref cpu._RegB, cpu._RegB);
            cpu._RegPC++;
        }

        public static void LD_B_C(CPU cpu)
        {
            LoadByte(ref cpu._RegB, cpu._RegC);
            cpu._RegPC++;
        }

        public static void LD_B_D(CPU cpu)
        {
            LoadByte(ref cpu._RegB, cpu._RegD);
            cpu._RegPC++;
        }

        public static void LD_B_E(CPU cpu)
        {
            LoadByte(ref cpu._RegB, cpu._RegE);
            cpu._RegPC++;
        }

        public static void LD_B_H(CPU cpu)
        {
            LoadByte(ref cpu._RegB, cpu._RegH);
            cpu._RegPC++;
        }

        public static void LD_B_L(CPU cpu)
        {
            LoadByte(ref cpu._RegA, cpu._RegL);
            cpu._RegPC++;
        }

        public static void LD_B_HL(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegB, cpu._RegHL, cpu._MMU);
            cpu._RegPC++;
        }
        public static void LD_B_D8(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegB, (ushort)(cpu._RegPC + 1), cpu._MMU);
            cpu._RegPC += 2;
        }
        #endregion

        #region LD_C
        public static void LD_C_A(CPU cpu)
        {
            LoadByte(ref cpu._RegC, cpu._RegA);
            cpu._RegPC++;
        }

        public static void LD_C_B(CPU cpu)
        {
            LoadByte(ref cpu._RegC, cpu._RegB);
            cpu._RegPC++;
        }

        public static void LD_C_C(CPU cpu)
        {
            LoadByte(ref cpu._RegC, cpu._RegC);
            cpu._RegPC++;
        }

        public static void LD_C_D(CPU cpu)
        {
            LoadByte(ref cpu._RegC, cpu._RegD);
            cpu._RegPC++;
        }

        public static void LD_C_E(CPU cpu)
        {
            LoadByte(ref cpu._RegC, cpu._RegE);
            cpu._RegPC++;
        }

        public static void LD_C_H(CPU cpu)
        {
            LoadByte(ref cpu._RegC, cpu._RegH);
            cpu._RegPC++;
        }

        public static void LD_C_L(CPU cpu)
        {
            LoadByte(ref cpu._RegC, cpu._RegL);
            cpu._RegPC++;
        }

        public static void LD_C_HL(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegC, cpu._RegHL, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_C_D8(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegC, (ushort)(cpu._RegPC + 1), cpu._MMU);
            cpu._RegPC += 2;
        }
        #endregion

        #region LD_D
        public static void LD_D_A(CPU cpu)
        {
            LoadByte(ref cpu._RegD, cpu._RegA);
            cpu._RegPC++;
        }

        public static void LD_D_B(CPU cpu)
        {
            LoadByte(ref cpu._RegD, cpu._RegB);
            cpu._RegPC++;
        }

        public static void LD_D_C(CPU cpu)
        {
            LoadByte(ref cpu._RegD, cpu._RegC);
            cpu._RegPC++;
        }

        public static void LD_D_D(CPU cpu)
        {
            LoadByte(ref cpu._RegD, cpu._RegD);
            cpu._RegPC++;
        }

        public static void LD_D_E(CPU cpu)
        {
            LoadByte(ref cpu._RegD, cpu._RegE);
            cpu._RegPC++;
        }

        public static void LD_D_H(CPU cpu)
        {
            LoadByte(ref cpu._RegD, cpu._RegH);
            cpu._RegPC++;
        }

        public static void LD_D_L(CPU cpu)
        {
            LoadByte(ref cpu._RegD, cpu._RegL);
            cpu._RegPC++;
        }

        public static void LD_D_HL(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegD, cpu._RegHL, cpu._MMU);
            cpu._RegPC++;
        }


        public static void LD_D_D8(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegD, (ushort)(cpu._RegPC + 1), cpu._MMU);
            cpu._RegPC += 2;
        }
        #endregion

        #region LD_E
        public static void LD_E_A(CPU cpu)
        {
            LoadByte(ref cpu._RegE, cpu._RegA);
            cpu._RegPC++;
        }

        public static void LD_E_B(CPU cpu)
        {
            LoadByte(ref cpu._RegE, cpu._RegB);
            cpu._RegPC++;
        }

        public static void LD_E_C(CPU cpu)
        {
            LoadByte(ref cpu._RegE, cpu._RegC);
            cpu._RegPC++;
        }

        public static void LD_E_D(CPU cpu)
        {
            LoadByte(ref cpu._RegE, cpu._RegD);
            cpu._RegPC++;
        }

        public static void LD_E_E(CPU cpu)
        {
            LoadByte(ref cpu._RegE, cpu._RegE);
            cpu._RegPC++;
        }

        public static void LD_E_H(CPU cpu)
        {
            LoadByte(ref cpu._RegE, cpu._RegH);
            cpu._RegPC++;
        }

        public static void LD_E_L(CPU cpu)
        {
            LoadByte(ref cpu._RegE, cpu._RegL);
            cpu._RegPC++;
        }

        public static void LD_E_HL(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegE, cpu._RegHL, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_E_D8(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegE, (ushort)(cpu._RegPC + 1), cpu._MMU);
            cpu._RegPC += 2;
        }
        #endregion

        #region LD_H
        public static void LD_H_A(CPU cpu)
        {
            LoadByte(ref cpu._RegH, cpu._RegA);
            cpu._RegPC++;
        }

        public static void LD_H_B(CPU cpu)
        {
            LoadByte(ref cpu._RegH, cpu._RegB);
            cpu._RegPC++;
        }

        public static void LD_H_C(CPU cpu)
        {
            LoadByte(ref cpu._RegH, cpu._RegC);
            cpu._RegPC++;
        }

        public static void LD_H_D(CPU cpu)
        {
            LoadByte(ref cpu._RegH, cpu._RegD);
            cpu._RegPC++;
        }

        public static void LD_H_E(CPU cpu)
        {
            LoadByte(ref cpu._RegH, cpu._RegE);
            cpu._RegPC++;
        }

        public static void LD_H_H(CPU cpu)
        {
            LoadByte(ref cpu._RegH, cpu._RegH);
            cpu._RegPC++;
        }

        public static void LD_H_L(CPU cpu)
        {
            LoadByte(ref cpu._RegH, cpu._RegL);
            cpu._RegPC++;
        }

        public static void LD_H_HL(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegH, cpu._RegHL, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_H_D8(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegH, (ushort)(cpu._RegPC + 1), cpu._MMU);
            cpu._RegPC += 2;
        }
        #endregion

        #region LD_L
        public static void LD_L_A(CPU cpu)
        {
            LoadByte(ref cpu._RegL, cpu._RegA);
            cpu._RegPC++;
        }

        public static void LD_L_B(CPU cpu)
        {
            LoadByte(ref cpu._RegL, cpu._RegB);
            cpu._RegPC++;
        }

        public static void LD_L_C(CPU cpu)
        {
            LoadByte(ref cpu._RegL, cpu._RegC);
            cpu._RegPC++;
        }

        public static void LD_L_D(CPU cpu)
        {
            LoadByte(ref cpu._RegL, cpu._RegD);
            cpu._RegPC++;
        }

        public static void LD_L_E(CPU cpu)
        {
            LoadByte(ref cpu._RegL, cpu._RegE);
            cpu._RegPC++;
        }

        public static void LD_L_H(CPU cpu)
        {
            LoadByte(ref cpu._RegL, cpu._RegH);
            cpu._RegPC++;
        }

        public static void LD_L_L(CPU cpu)
        {
            LoadByte(ref cpu._RegL, cpu._RegL);
            cpu._RegPC++;
        }

        public static void LD_L_HL(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegL, cpu._RegHL, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_L_D8(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegL, (ushort)(cpu._RegPC + 1), cpu._MMU);
            cpu._RegPC += 2;
        }
        #endregion

        #region LD_HL
        public static void LD_HL_A(CPU cpu)
        {
            LoadByteToAddress(cpu._RegHL, cpu._RegA, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_HL_B(CPU cpu)
        {
            LoadByteToAddress(cpu._RegHL, cpu._RegB, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_HL_C(CPU cpu)
        {
            LoadByteToAddress(cpu._RegHL, cpu._RegC, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_HL_D(CPU cpu)
        {
            LoadByteToAddress(cpu._RegHL, cpu._RegD, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_HL_E(CPU cpu)
        {
            LoadByteToAddress(cpu._RegHL, cpu._RegE, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_HL_H(CPU cpu)
        {
            LoadByteToAddress(cpu._RegHL, cpu._RegH, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_HL_L(CPU cpu)
        {
            LoadByteToAddress(cpu._RegHL, cpu._RegL, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_HL_D8(CPU cpu)
        {
            byte value = 0;
            LoadFromAddress(ref value, (ushort)(cpu._RegPC + 1), cpu._MMU);
            LoadByteToAddress(cpu._RegHL, value, cpu._MMU);
            cpu._RegPC += 2;
        }

        #endregion

        #region LD_D16
        public static void LD_BC_D16(CPU cpu)
        {
            ushort target = 0;
            LoadWordFromAddress(ref target, (ushort)(cpu._RegPC + 1), cpu._MMU);
            cpu._RegBC = target;
            cpu._RegPC += 3;
        }

        public static void LD_DE_D16(CPU cpu)
        {
            ushort target = 0;
            LoadWordFromAddress(ref target, (ushort)(cpu._RegPC + 1), cpu._MMU);
            cpu._RegDE = target;
            cpu._RegPC += 3;
        }

        public static void LD_HL_D16(CPU cpu)
        {
            ushort target = 0;
            LoadWordFromAddress(ref target, (ushort)(cpu._RegPC + 1), cpu._MMU);
            cpu._RegHL = target;
            cpu._RegPC += 3;
        }

        public static void LD_SP_D16(CPU cpu)
        {
            ushort target = 0;
            LoadWordFromAddress(ref target, (ushort)(cpu._RegPC + 1), cpu._MMU);
            cpu._RegSP = target;
            cpu._RegPC += 3;
        }
        #endregion

        #region LD_R16_A
        public static void LD_BC_A(CPU cpu)
        {
            LoadByteToAddress(cpu._RegBC, cpu._RegA, cpu._MMU);
            cpu._RegPC++;
        }
        public static void LD_DE_A(CPU cpu)
        {
            LoadByteToAddress(cpu._RegDE, cpu._RegA, cpu._MMU);
            cpu._RegPC++;
        }
        #endregion

        #region LDH
        public static void LDH_A_A8(CPU cpu)
        {
            cpu._RegA = cpu._MMU.ReadByte((ushort)(0xFF00 + cpu._MMU.ReadByte((ushort)(cpu._RegPC + 1))));
            cpu._RegPC += 2;
        }

        public static void LDH_A8_A(CPU cpu)
        {
            cpu._MMU.WriteByte((ushort)(0xFF00 + cpu._MMU.ReadByte((ushort)(cpu._RegPC + 1))), cpu._RegA);
            cpu._RegPC += 2;
        }
        #endregion

        #endregion

        #region PUSH
        public static void PUSH_AF(CPU cpu)
        {
            Push(cpu._RegAF, cpu);
            cpu._RegPC++;
        }

        public static void PUSH_BC(CPU cpu)
        {
            Push(cpu._RegBC, cpu);
            cpu._RegPC++;
        }

        public static void PUSH_DE(CPU cpu)
        {
            Push(cpu._RegDE, cpu);
            cpu._RegPC++;
        }

        public static void PUSH_HL(CPU cpu)
        {
            Push(cpu._RegHL, cpu);
            cpu._RegPC++;
        }
        #endregion

        #region POP
        public static void POP_AF(CPU cpu)
        {
            ushort result = 0;
            Pop(ref result, cpu);
            cpu._RegAF = result;
            cpu._RegPC++;
        }

        public static void POP_BC(CPU cpu)
        {
            ushort result = 0;
            Pop(ref result, cpu);
            cpu._RegBC = result;
            cpu._RegPC++;
        }

        public static void POP_DE(CPU cpu)
        {
            ushort result = 0;
            Pop(ref result, cpu);
            cpu._RegDE = result;
            cpu._RegPC++;
        }

        public static void POP_HL(CPU cpu)
        {
            ushort result = 0;
            Pop(ref result, cpu);
            cpu._RegHL = result;
            cpu._RegPC++;
        }
        #endregion

        #region CALL
        public static void CALL(CPU cpu)
        {
            Call(cpu, true);
        }

        public static void CALL_Z(CPU cpu)
        {
            Call(cpu, cpu._RegF.Zero);
        }

        public static void CALL_NZ(CPU cpu)
        {
            Call(cpu, !cpu._RegF.Zero);
        }


        public static void CALL_C(CPU cpu)
        {
            Call(cpu, cpu._RegF.Carry);
        }

        public static void CALL_NC(CPU cpu)
        {
            Call(cpu, !cpu._RegF.Carry);
        }
        #endregion

        #region RET
        public static void RET(CPU cpu)
        {
            Ret(cpu, true);
        }

        public static void RET_Z(CPU cpu)
        {
            Ret(cpu, cpu._RegF.Zero);
        }

        public static void RET_NZ(CPU cpu)
        {
            Ret(cpu, !cpu._RegF.Zero);
        }


        public static void RET_C(CPU cpu)
        {
            Ret(cpu, cpu._RegF.Carry);
        }

        public static void RET_NC(CPU cpu)
        {
            Ret(cpu, !cpu._RegF.Carry);
        }
        #endregion

        #region RST
        public static void RST_00(CPU cpu)
        {
            Rst(cpu, 0x00);
        }
        public static void RST_08(CPU cpu)
        {
            Rst(cpu, 0x08);
        }
        public static void RST_10(CPU cpu)
        {
            Rst(cpu, 0x10);
        }
        public static void RST_18(CPU cpu)
        {
            Rst(cpu, 0x18);
        }

        public static void RST_20(CPU cpu)
        {
            Rst(cpu, 0x20);
        }
        public static void RST_28(CPU cpu)
        {
            Rst(cpu, 0x28);
        }
        public static void RST_30(CPU cpu)
        {
            Rst(cpu, 0x30);
        }
        public static void RST_38(CPU cpu)
        {
            Rst(cpu, 0x38);
        }
        #endregion

        #region AND

        public static void AND_B(CPU cpu)
        {
            And(ref cpu._RegA, cpu._RegB, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void AND_C(CPU cpu)
        {
            And(ref cpu._RegA, cpu._RegC, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void AND_D(CPU cpu)
        {
            And(ref cpu._RegA, cpu._RegD, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void AND_E(CPU cpu)
        {
            And(ref cpu._RegA, cpu._RegE, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void AND_H(CPU cpu)
        {
            And(ref cpu._RegA, cpu._RegH, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void AND_L(CPU cpu)
        {
            And(ref cpu._RegA, cpu._RegL, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void AND_A(CPU cpu)
        {
            And(ref cpu._RegA, cpu._RegA, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void AND_HL(CPU cpu)
        {
            And(ref cpu._RegA, cpu._MMU.ReadByte(cpu._RegHL), ref cpu._RegF);
            cpu._RegPC++;
        }
        public static void AND_D8(CPU cpu)
        {
            And(ref cpu._RegA, cpu._MMU.ReadByte((ushort)(cpu._RegPC + 1)), ref cpu._RegF);
            cpu._RegPC += 2;
        }
        #endregion

        #region OR

        public static void OR_B(CPU cpu)
        {
            Or(ref cpu._RegA, cpu._RegB, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void OR_C(CPU cpu)
        {
            Or(ref cpu._RegA, cpu._RegC, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void OR_D(CPU cpu)
        {
            Or(ref cpu._RegA, cpu._RegD, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void OR_E(CPU cpu)
        {
            Or(ref cpu._RegA, cpu._RegE, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void OR_H(CPU cpu)
        {
            Or(ref cpu._RegA, cpu._RegH, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void OR_L(CPU cpu)
        {
            Or(ref cpu._RegA, cpu._RegL, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void OR_A(CPU cpu)
        {
            Or(ref cpu._RegA, cpu._RegA, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void OR_HL(CPU cpu)
        {
            Or(ref cpu._RegA, cpu._MMU.ReadByte(cpu._RegHL), ref cpu._RegF);
            cpu._RegPC++;
        }
        public static void OR_D8(CPU cpu)
        {
            Or(ref cpu._RegA, cpu._MMU.ReadByte((ushort)(cpu._RegPC + 1)), ref cpu._RegF);
            cpu._RegPC += 2;
        }
        #endregion

        #region XOR
        public static void XOR_B(CPU cpu)
        {
            Xor(ref cpu._RegA, cpu._RegB, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void XOR_C(CPU cpu)
        {
            Xor(ref cpu._RegA, cpu._RegC, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void XOR_D(CPU cpu)
        {
            Xor(ref cpu._RegA, cpu._RegD, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void XOR_E(CPU cpu)
        {
            Xor(ref cpu._RegA, cpu._RegE, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void XOR_H(CPU cpu)
        {
            Xor(ref cpu._RegA, cpu._RegH, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void XOR_L(CPU cpu)
        {
            Xor(ref cpu._RegA, cpu._RegL, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void XOR_A(CPU cpu)
        {
            Xor(ref cpu._RegA, cpu._RegA, ref cpu._RegF);
            cpu._RegPC++;
        }

        public static void XOR_HL(CPU cpu)
        {
            Xor(ref cpu._RegA, cpu._MMU.ReadByte(cpu._RegHL), ref cpu._RegF);
            cpu._RegPC++;
        }
        public static void XOR_D8(CPU cpu)
        {
            Xor(ref cpu._RegA, cpu._MMU.ReadByte((ushort)(cpu._RegPC + 1)), ref cpu._RegF);
            cpu._RegPC += 2;
        }
        #endregion

        public static void NOP(CPU cpu)
        {
            cpu._RegPC++;
        }

        public static void HALT(CPU cpu)
        {
            cpu._Halted = true;
        }

        public static void STOP(CPU cpu)
        {
            cpu._Halted = true;
        }

        public static void CCF(CPU cpu)
        {
            cpu._RegF.Carry = !cpu._RegF.Carry;
            cpu._RegPC++;
        }

        public static void DI(CPU cpu)
        {
            Console.WriteLine("DI is called but interrupts aren't implemented yet");
            cpu._RegPC++;
        }


        #region Bitwise Logic
        public static void RRCA(CPU cpu)
        {
            cpu._RegF.Carry = (cpu._RegA & 0x01) == 1;
            cpu._RegA = (byte)(cpu._RegA >> 1);
            cpu._RegPC++;
        }

        #region BIT
        #region BIT0
        public static void BIT_0_A(CPU cpu)
        {
            Bit(cpu._RegA, 0, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_0_B(CPU cpu)
        {
            Bit(cpu._RegB, 0, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_0_C(CPU cpu)
        {
            Bit(cpu._RegC, 0, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_0_D(CPU cpu)
        {
            Bit(cpu._RegD, 0, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_0_E(CPU cpu)
        {
            Bit(cpu._RegE, 0, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_0_L(CPU cpu)
        {
            Bit(cpu._RegL, 0, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_0_H(CPU cpu)
        {
            Bit(cpu._RegH, 0, ref cpu._RegF);
            cpu._RegPC += 2;
        }

        public static void BIT_0_HL(CPU cpu)
        {
            Bit(cpu._RegH, 0, ref cpu._RegF);
            cpu._RegPC += 2;
        }

        #endregion
        #region BIT1
        public static void BIT_1_A(CPU cpu)
        {
            Bit(cpu._RegA, 1, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_1_B(CPU cpu)
        {
            Bit(cpu._RegB, 1, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_1_C(CPU cpu)
        {
            Bit(cpu._RegC, 1, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_1_D(CPU cpu)
        {
            Bit(cpu._RegD, 1, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_1_E(CPU cpu)
        {
            Bit(cpu._RegE, 1, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_1_L(CPU cpu)
        {
            Bit(cpu._RegL, 1, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_1_H(CPU cpu)
        {
            Bit(cpu._RegH, 1, ref cpu._RegF);
            cpu._RegPC += 2;
        }

        public static void BIT_1_HL(CPU cpu)
        {
            Bit(cpu._MMU.ReadByte(cpu._RegHL), 1, ref cpu._RegF);
            cpu._RegPC += 2;
        }

        #endregion
        #region BIT2
        public static void BIT_2_A(CPU cpu)
        {
            Bit(cpu._RegA, 2, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_2_B(CPU cpu)
        {
            Bit(cpu._RegB, 2, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_2_C(CPU cpu)
        {
            Bit(cpu._RegC, 2, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_2_D(CPU cpu)
        {
            Bit(cpu._RegD, 2, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_2_E(CPU cpu)
        {
            Bit(cpu._RegE, 2, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_2_L(CPU cpu)
        {
            Bit(cpu._RegL, 2, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_2_H(CPU cpu)
        {
            Bit(cpu._RegH, 2, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_2_HL(CPU cpu)
        {
            Bit(cpu._MMU.ReadByte(cpu._RegHL), 2, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        #endregion
        #region BIT3
        public static void BIT_3_A(CPU cpu)
        {
            Bit(cpu._RegA, 3, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_3_B(CPU cpu)
        {
            Bit(cpu._RegB, 3, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_3_C(CPU cpu)
        {
            Bit(cpu._RegC, 3, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_3_D(CPU cpu)
        {
            Bit(cpu._RegD, 3, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_3_E(CPU cpu)
        {
            Bit(cpu._RegE, 3, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_3_L(CPU cpu)
        {
            Bit(cpu._RegL, 3, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_3_H(CPU cpu)
        {
            Bit(cpu._RegH, 3, ref cpu._RegF);
            cpu._RegPC += 2;
        }

        public static void BIT_3_HL(CPU cpu)
        {
            Bit(cpu._MMU.ReadByte(cpu._RegHL), 3, ref cpu._RegF);
            cpu._RegPC += 2;
        }

        #endregion
        #region BIT4
        public static void BIT_4_A(CPU cpu)
        {
            Bit(cpu._RegA, 4, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_4_B(CPU cpu)
        {
            Bit(cpu._RegB, 4, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_4_C(CPU cpu)
        {
            Bit(cpu._RegC, 4, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_4_D(CPU cpu)
        {
            Bit(cpu._RegD, 4, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_4_E(CPU cpu)
        {
            Bit(cpu._RegE, 4, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_4_L(CPU cpu)
        {
            Bit(cpu._RegL, 4, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_4_H(CPU cpu)
        {
            Bit(cpu._RegH, 4, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_4_HL(CPU cpu)
        {
            Bit(cpu._MMU.ReadByte(cpu._RegHL), 4, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        #endregion
        #region BIT5
        public static void BIT_5_A(CPU cpu)
        {
            Bit(cpu._RegA, 5, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_5_B(CPU cpu)
        {
            Bit(cpu._RegB, 5, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_5_C(CPU cpu)
        {
            Bit(cpu._RegC, 5, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_5_D(CPU cpu)
        {
            Bit(cpu._RegD, 5, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_5_E(CPU cpu)
        {
            Bit(cpu._RegE, 5, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_5_L(CPU cpu)
        {
            Bit(cpu._RegL, 5, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_5_H(CPU cpu)
        {
            Bit(cpu._RegH, 5, ref cpu._RegF);
            cpu._RegPC += 2;
        }

        public static void BIT_5_HL(CPU cpu)
        {
            Bit(cpu._MMU.ReadByte(cpu._RegHL), 5, ref cpu._RegF);
            cpu._RegPC += 2;
        }

        #endregion
        #region BIT6
        public static void BIT_6_A(CPU cpu)
        {
            Bit(cpu._RegA, 6, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_6_B(CPU cpu)
        {
            Bit(cpu._RegB, 6, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_6_C(CPU cpu)
        {
            Bit(cpu._RegC, 6, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_6_D(CPU cpu)
        {
            Bit(cpu._RegD, 6, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_6_E(CPU cpu)
        {
            Bit(cpu._RegE, 6, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_6_L(CPU cpu)
        {
            Bit(cpu._RegL, 6, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_6_H(CPU cpu)
        {
            Bit(cpu._RegH, 6, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_6_HL(CPU cpu)
        {
            Bit(cpu._MMU.ReadByte(cpu._RegHL), 6, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        #endregion
        #region BIT7
        public static void BIT_7_A(CPU cpu)
        {
            Bit(cpu._RegA, 7, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_7_B(CPU cpu)
        {
            Bit(cpu._RegB, 7, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_7_C(CPU cpu)
        {
            Bit(cpu._RegC, 7, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_7_D(CPU cpu)
        {
            Bit(cpu._RegD, 7, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_7_E(CPU cpu)
        {
            Bit(cpu._RegE, 7, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_7_L(CPU cpu)
        {
            Bit(cpu._RegL, 7, ref cpu._RegF);
            cpu._RegPC += 2;
        }
        public static void BIT_7_H(CPU cpu)
        {
            Bit(cpu._RegH, 7, ref cpu._RegF);
            cpu._RegPC += 2;
        }

        public static void BIT_7_HL(CPU cpu)
        {
            Bit(cpu._MMU.ReadByte(cpu._RegHL), 7, ref cpu._RegF);
            cpu._RegPC += 2;
        }

        #endregion
        #endregion
        #endregion

        #endregion

        #region SubInstructions

        private static void IncWithFlags(ref byte target, ref FlagsRegister flagsRegister)
        {
            int newValue = target + 1;
            byte result = (byte)(newValue % 256);

            if (result == 0)
            {
                flagsRegister.Zero = true;
            }

            flagsRegister.Subtract = false;
            flagsRegister.HalfCarry = (target & 0xF) + (1 & 0xF) > 0xF;
            target = result;
        }
        private static void DecWithFlags(ref byte target, ref FlagsRegister flagsRegister)
        {
            int newValue = target - 1;
            byte result = (byte)(newValue % 256);

            if (result == 0)
            {
                flagsRegister.Zero = true;
            }

            flagsRegister.Subtract = true;
            flagsRegister.HalfCarry = (target & 0xF) + (1 & 0xF) > 0xF;
            target = result;
        }

        private static void Compare(byte target, byte source, ref FlagsRegister flagsRegister)
        {
            flagsRegister.Zero = target == source;
            flagsRegister.Subtract = true;
            flagsRegister.HalfCarry = (((target & 0xF) - (source & 0xF)) < 0);
            flagsRegister.Carry = target < source;
        }

        private static void Add(ref byte target, byte source, ref FlagsRegister flagsRegister, ref ushort programCounter)
        {
            int newValue = target + source;
            byte result = (byte)(newValue % 256);

            if (result == 0)
            {
                flagsRegister.Zero = true;
            }

            flagsRegister.Subtract = false;
            flagsRegister.Carry = newValue > 255;
            flagsRegister.HalfCarry = (target & 0xF) + (source & 0xF) > 0xF;
            target = result;
            programCounter++;
        }

        private static void Sub(ref byte target, byte source, ref FlagsRegister flagsRegister)
        {
            int newValue = target - source;
            byte result;

            if (newValue < 0)
            {
                result = (byte)(newValue + 256);
            }
            else
            {
                result = (byte)newValue;
            }

            flagsRegister.Zero = newValue == 0;
            flagsRegister.Subtract = true;
            flagsRegister.HalfCarry = (((target & 0xF) - (source & 0xF)) < 0);
            flagsRegister.Carry = target < source;
        }

        private static void Jump(ref ushort programCounter, MMU mmu, bool conditionMet)
        {
            if (!conditionMet)
            {
                programCounter += 3;
                return;
            }
            ushort newAddress = mmu.ReadWordLSFirst((ushort)(programCounter + 1));
            programCounter = newAddress;
        }

        private static void JumpRelative(ref ushort programCounter, byte toAdd, bool conditionMet)
        {
            ushort newAddress = (ushort)(programCounter + 2);
            if (conditionMet)
            {
                programCounter = (ushort)(newAddress + unchecked((sbyte)toAdd));
            }
            else
            {
                programCounter = newAddress;
            }
        }


        private static void LoadFromAddress(ref byte target, ushort address, MMU mmu)
        {
            LoadByte(ref target, mmu.ReadByte(address));
        }
        private static void LoadByte(ref byte target, byte source)
        {
            target = source;
        }

        private static void LoadWordFromAddress(ref ushort target, ushort address, MMU mmu)
        {
            LoadWord(ref target, mmu.ReadWordLSFirst(address));
        }

        private static void LoadWord(ref ushort target, ushort source)
        {
            target = source;
        }

        private static void LoadByteToAddress(ushort targetAddress, byte source, MMU mmu)
        {
            mmu.WriteByte(targetAddress, source);
        }

        private static void Push(ushort value, CPU cpu)
        {
            cpu._RegSP--;
            LoadByteToAddress(cpu._RegSP, ByteUshortHelper.UpperByteOfSixteenBits(value), cpu._MMU);
            cpu._RegSP--;
            LoadByteToAddress(cpu._RegSP, ByteUshortHelper.LowerByteOfSixteenBits(value), cpu._MMU);

        }

        private static void Pop(ref ushort target, CPU cpu)
        {
            byte leastSignificant = 0;
            byte mostSignificant = 0; ;
            LoadFromAddress(ref leastSignificant, cpu._RegSP, cpu._MMU);
            cpu._RegSP++;
            LoadFromAddress(ref mostSignificant, cpu._RegSP, cpu._MMU);
            cpu._RegSP++;
            target = ByteUshortHelper.CombineBytes(mostSignificant, leastSignificant);
        }

        private static void Call(CPU cpu, bool conditionMet)
        {
            ushort nextPC = (ushort)(cpu._RegPC + 3);

            if (conditionMet)
            {
                Push(nextPC, cpu);
                Jump(ref cpu._RegPC, cpu._MMU, conditionMet);
                return;
            }

            cpu._RegPC = nextPC;
        }

        private static void Rst(CPU cpu, ushort address)
        {
            Push((ushort)(cpu._RegPC + 1), cpu);
            cpu._RegPC = address;
        }

        private static void Ret(CPU cpu, bool conditionMet)
        {
            if (conditionMet)
            {
                Pop(ref cpu._RegPC, cpu);
                return;
            }

            cpu._RegPC++;
        }

        private static void And(ref byte target, byte source, ref FlagsRegister flagsRegister)
        {
            byte result = (byte)(target & source);
            flagsRegister.Carry = false;
            flagsRegister.Zero = result == 0;
            flagsRegister.Subtract = false;
            flagsRegister.HalfCarry = true;
            target = result;
        }

        private static void Or(ref byte target, byte source, ref FlagsRegister flagsRegister)
        {
            byte result = (byte)(target | source);
            flagsRegister.Carry = false;
            flagsRegister.Zero = result == 0;
            flagsRegister.Subtract = false;
            flagsRegister.HalfCarry = true;
            target = result;
        }

        private static void Xor(ref byte target, byte source, ref FlagsRegister flagsRegister)
        {
            byte result = (byte)(target ^ source);
            flagsRegister.Carry = false;
            flagsRegister.Zero = result == 0;
            flagsRegister.Subtract = false;
            flagsRegister.HalfCarry = false;
            target = result;
        }

        private static void Bit(byte target, byte bitNr, ref FlagsRegister flagsRegister)
        {
            int shifted = target >> bitNr;
            flagsRegister.Zero = (shifted & 1) == 1;
            flagsRegister.HalfCarry = true;
            flagsRegister.Subtract = false;
        }
        #endregion
    }

}