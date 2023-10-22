using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Mono.Cecil.Cil;
using MonoMod.Cil;

namespace Avalon.Hooks;

public static class Utilities
{
    public static Func<TResult> CreatePropertyReader<TInstance, TResult>(PropertyInfo propertyInfo)
    {
        var instanceParameter = Expression.Parameter(typeof(TInstance));
        return Expression.Lambda<Func<TResult>>(Expression.Property(instanceParameter, propertyInfo), instanceParameter).Compile();
    }

    public static Func<TResult> CreatePropertyReader<TResult>(PropertyInfo propertyInfo) => Expression.Lambda<Func<TResult>>(Expression.Property(null, propertyInfo)).Compile();

    public static Func<TResult> CreateFieldReader<TInstance, TResult>(FieldInfo fieldInfo)
    {
        var instanceParameter = Expression.Parameter(typeof(TInstance));
        return Expression.Lambda<Func<TResult>>(Expression.Field(instanceParameter, fieldInfo), instanceParameter).Compile();
    }

    public static Func<TResult> CreateFieldReader<TResult>(FieldInfo fieldInfo) => Expression.Lambda<Func<TResult>>(Expression.Field(null, fieldInfo)).Compile();
    

    public static TDelegate CacheInstanceMethod<TDelegate>(MethodInfo methodInfo)
    {
        var parameterInfos = methodInfo.GetParameters();
        var delegateParameterInfos = typeof(TDelegate).GetMethod("Invoke")!.GetParameters();

        var parameterExpressions = delegateParameterInfos.Select(p => Expression.Parameter(p.ParameterType, p.Name)).ToArray();
        var expressions = parameterExpressions[1..].Select((p, i) => delegateParameterInfos[i].ParameterType == parameterInfos[i].ParameterType ? p as Expression : Expression.Convert(p, parameterInfos[i].ParameterType));

        var methodCallExpression = Expression.Call(parameterExpressions[0], methodInfo, expressions);
        return Expression.Lambda<TDelegate>(methodCallExpression, parameterExpressions).Compile();
    }

    public static TDelegate CacheStaticMethod<TDelegate>(MethodInfo methodInfo)
    {
        var parameterInfos = methodInfo.GetParameters();
        var delegateParameterInfos = typeof(TDelegate).GetMethod("Invoke")!.GetParameters();

        var parameterExpressions = delegateParameterInfos.Select(p => Expression.Parameter(p.ParameterType, p.Name)).ToArray();
        var expressions = parameterExpressions.Select((p, i) => delegateParameterInfos[i].ParameterType == parameterInfos[i].ParameterType ? p as Expression : Expression.Convert(p, parameterInfos[i].ParameterType));

        var methodCallExpression = Expression.Call(methodInfo, expressions);
        return Expression.Lambda<TDelegate>(methodCallExpression, parameterExpressions).Compile();
    }

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
