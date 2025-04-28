namespace HareDu.Core.Extensions;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class ProjectionExtensions
{
    /// <summary>
    /// Applies the specified projector function to the given result object and returns the transformed value.
    /// If the source is null, lacks data, or the projector is null, a default value is returned.
    /// </summary>
    /// <param name="source">The asynchronous result object containing data to project.</param>
    /// <param name="projector">The function used to transform the source data.</param>
    /// <typeparam name="T">The type of the data in the source result.</typeparam>
    /// <typeparam name="TResult">The type of the transformed value produced by the projector.</typeparam>
    /// <returns>The projected value if the source has data and the projector is not null; otherwise, the default value of TResult.</returns>
    public static TResult Select<T, TResult>(this Task<Result<T>> source, Func<Result<T>, TResult> projector)
    {
        if (source is null || projector is null)
            return default;

        Result<T> result = source.GetResult();

        return result is not null && result.HasData ? projector(result) : default;
    }

    /// <summary>
    /// Applies the specified projector function to the given result object and returns the transformed value. If the source is null, lacks data, or the projector is null, a default value is returned.
    /// </summary>
    /// <param name="source">The result object containing data to project.</param>
    /// <param name="projector">The function used to transform the source data.</param>
    /// <typeparam name="T">The type of the data in the source result.</typeparam>
    /// <typeparam name="TResult">The type of the transformed value produced by the projector.</typeparam>
    /// <returns>The projected value if the source has data and the projector is not null; otherwise, the default value of TResult.</returns>
    public static TResult Select<T, TResult>(this Result<T> source, Func<Result<T>, TResult> projector)
    {
        if (source is null || !source.HasData || projector is null)
            return default;

        return source.HasData ? projector(source) : default;
    }

    /// <summary>
    /// Projects the specified source object to a new result using the provided projector function. Returns the default value of TResult if the source is null, lacks data, or if the projector is null.
    /// </summary>
    /// <param name="source">The source object containing the data to be projected.</param>
    /// <param name="projector">The function used to project the source object to a new result.</param>
    /// <typeparam name="T">The type of the source data.</typeparam>
    /// <typeparam name="TResult">The type of the projection result.</typeparam>
    /// <returns>The projected result of type TResult, or the default value of TResult if the source or projector are invalid.</returns>
    public static TResult Select<T, TResult>(this Results<T> source, Func<Results<T>, TResult> projector)
    {
        if (source is null || !source.HasData || projector is null)
            return default;

        return source.HasData ? projector(source) : default;
    }

    /// <summary>
    /// Applies the specified projector function to the given result object and returns the transformed value.
    /// If the source is null, lacks data, or the projector is null, a default value is returned.
    /// </summary>
    /// <param name="source">The asynchronous result object containing data to project.</param>
    /// <param name="projector">The function used to transform the source data.</param>
    /// <typeparam name="T">The type of the data in the source result.</typeparam>
    /// <typeparam name="TResult">The type of the transformed value produced by the projector.</typeparam>
    /// <returns>The projected value if the source has data and the projector is not null; otherwise, the default value of TResult.</returns>
    public static TResult Select<T, TResult>(this Task<Result<IReadOnlyList<T>>> source,
        Func<Result<IReadOnlyList<T>>, TResult> projector)
    {
        if (source is null || projector is null)
            return default;

        Result<IReadOnlyList<T>> result = source.GetResult();

        return result is not null && result.HasData ? projector(result) : default;
    }

    /// <summary>
    /// Applies the specified projector function to the given result object and returns the transformed value. If the source is null, lacks data, or the projector is null, a default value is returned.
    /// </summary>
    /// <param name="source">The result object containing data to project.</param>
    /// <param name="projector">The function used to transform the source data.</param>
    /// <typeparam name="T">The type of the data in the source result.</typeparam>
    /// <typeparam name="TResult">The type of the transformed value produced by the projector.</typeparam>
    /// <returns>The projected value if the source has data and the projector is not null; otherwise, the default value of TResult.</returns>
    public static IReadOnlyList<T> Select<T>(this Result<IReadOnlyList<T>> source,
        Func<Result<IReadOnlyList<T>>, IReadOnlyList<T>> projector)
    {
        if (source is null || !source.HasData || projector is null)
            return Array.Empty<T>();

        return source.HasData ? projector(source) : Array.Empty<T>();
    }

    /// <summary>
    /// Projects an object of type U into an object of type T using the specified projection function.
    /// </summary>
    /// <param name="obj">The object to be projected.</param>
    /// <param name="projection">The function used to transform the object of type U into an object of type T.</param>
    /// <typeparam name="T">The type of the projected result.</typeparam>
    /// <typeparam name="U">The type of the object to project from.</typeparam>
    /// <returns>The projected object of type T if the input object is not null; otherwise, the default value of type T.</returns>
    public static T Select<T, U>(this U obj, Func<U, T> projection)
        => obj is null
            ? default
            : projection(obj);
}