namespace HareDu.Core.Extensions;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class ProjectionExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="projector"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public static TResult Select<T, TResult>(this Task<Result<T>> source, Func<Result<T>, TResult> projector)
    {
        if (source is null || projector is null)
            return default;
                
        Result<T> result = source.GetResult();

        return result is not null && result.HasData ? projector(result) : default;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="projector"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public static TResult Select<T, TResult>(this Result<T> source, Func<Result<T>, TResult> projector)
    {
        if (source is null || !source.HasData || projector is null)
            return default;
            
        return source.HasData ? projector(source) : default;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="projector"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public static TResult Select<T, TResult>(this Results<T> source, Func<Results<T>, TResult> projector)
    {
        if (source is null || !source.HasData || projector is null)
            return default;
            
        return source.HasData ? projector(source) : default;
    }
        
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="projector"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public static TResult Select<T, TResult>(this Task<Result<IReadOnlyList<T>>> source, Func<Result<IReadOnlyList<T>>, TResult> projector)
    {
        if (source is null || projector is null)
            return default;
                
        Result<IReadOnlyList<T>> result = source.GetResult();

        return result is not null && result.HasData ? projector(result) : default;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="projector"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IReadOnlyList<T> Select<T>(this Result<IReadOnlyList<T>> source, Func<Result<IReadOnlyList<T>>, IReadOnlyList<T>> projector)
    {
        if (source is null || !source.HasData || projector is null)
            return Array.Empty<T>();

        return source.HasData ? projector(source) : Array.Empty<T>();
    }
        
    /// <summary>
    /// Safely attempts to unwrap the specified object and returns the resultant value. If the object is NULL, then the default object value will be returned.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="projection"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <returns></returns>
    public static T Select<T, U>(this U obj, Func<U, T> projection)
        => obj is null
            ? default
            : projection(obj);
}