using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Avalon.Reflection;

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
}
