using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Mono.Cecil.Cil;
using MonoMod.Cil;

namespace Avalon.Hooks;

public static class Utilities
{
    public static void OutputIL(ILContext il)
    {
        var c = new ILCursor(il);
        foreach (Instruction instruction in c.Instrs)
        {
            object obj = (instruction.Operand == null ? string.Empty : instruction.Operand.ToString()) ?? string.Empty;
            ExxoAvalonOrigins.Mod.Logger.Debug(
                $"{instruction.Offset.ToString(CultureInfo.InvariantCulture)} | {instruction.OpCode.ToString()} | {obj}");
        }
    }

    public static List<Instruction> FromCursorToInstruction(ILCursor c, Func<Instruction, bool> predicate)
    {
        List<Instruction> instructions = new();
        while (!predicate.Invoke(c.Next))
        {
            instructions.Add(c.Next);
            c.Index++;
        }

        return instructions;
    }

    public static void EmitInstructions(ILCursor c, IEnumerable<Instruction> instructions)
    {
        foreach (Instruction instruction in instructions)
        {
            c.Emit(instruction.OpCode, instruction.Operand);
        }
    }

    public static void RemoveUntilInstruction(ILCursor c, Func<Instruction, bool> predicate)
    {
        while (!predicate.Invoke(c.Next))
        {
            c.Remove();
        }
    }

    public static void AddAlternativeIdChecks(ILContext il, ushort origId, Func<ushort, bool> predicate)
    {
        var c = new ILCursor(il);

        while (c.TryGotoNext(i =>
                   (i.MatchBeq(out _) || i.MatchBneUn(out _)) && i.Offset != 0 && i.Previous.MatchLdcI4(origId)))
        {
            c.Index--;
            c.EmitDelegate<Func<ushort, ushort>>(id => predicate.Invoke(id) ? origId : id);
            c.Index += 2;
        }
    }
    public static void ReplaceIDIfMatch(ILContext il, ushort val1, ushort val2, FieldInfo field, int matchInt)
    {
        var c = new ILCursor(il);

        while (c.TryGotoNext(i => i.MatchLdcI4(val1)))
        {
            // Ensure not replacing anything added by an IL hook
            if (c.Next.Offset != 0 && !(c.Prev.MatchCall(out _) && c.Prev.Offset == 0))
            {
                c.Remove();
                ILLabel endif = c.DefineLabel();
                ILLabel path = c.DefineLabel();
                c.Emit(OpCodes.Ldsfld, field);
                c.Emit(OpCodes.Ldc_I4, matchInt);
                c.Emit(OpCodes.Beq, path);
                c.Emit(OpCodes.Ldc_I4, val1);
                c.Emit(OpCodes.Br, endif);
                c.MarkLabel(path);
                c.Emit(OpCodes.Ldc_I4, val2);
                c.MarkLabel(endif);
            }
        }
    }
    public static void SoftReplaceAllMatchingInstructions(ILContext il, Instruction i1, Instruction i2)
    {
        var c = new ILCursor(il);

        while (c.TryGotoNext(i => i.OpCode == i1.OpCode && (i1.Operand == null || i.Operand == i1.Operand)))
        {
            // Ensure not replacing anything added by an IL hook
            if (c.Next.Offset != 0)
            {
                c.Index++;
                c.Emit(OpCodes.Pop);
                c.Emit(i2.OpCode, i2.Operand);
            }
        }
    }

    public static void SoftReplaceAllMatchingInstructionsWithMethod(ILContext il, Instruction i1, MethodBase method)
    {
        var c = new ILCursor(il);

        while (c.TryGotoNext(i => i.OpCode == i1.OpCode && (i1.Operand == null || i.Operand == i1.Operand)))
        {
            // Ensure not replacing anything added by an IL hook
            if (c.Next.Offset != 0)
            {
                c.Index++;
                c.Emit(OpCodes.Pop);
                c.Emit(OpCodes.Call, method);
            }
        }
    }
}
