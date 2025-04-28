namespace HareDu.Core.Extensions;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class FilterExtensions
{
    /// <summary>
    /// Filters the elements of the provided source based on the given predicate.
    /// </summary>
    /// <param name="source">The source collection to filter.</param>
    /// <param name="predicate">A function defining the condition to filter the source elements.</param>
    /// <typeparam name="T">The type of elements in the source.</typeparam>
    /// <returns>A read-only list containing elements that satisfy the predicate.</returns>
    public static IReadOnlyList<T> Where<T>(this Results<T> source, Func<T, bool> predicate)
    {
        if (source is null)
            return new List<T>();

        return !source.HasData ? new List<T>() : Filter(source.Data, predicate);
    }

    /// <summary>
    /// Filters the elements in the result of an asynchronous task based on the provided predicate.
    /// </summary>
    /// <param name="source">A task representing the asynchronous result containing the data to filter.</param>
    /// <param name="predicate">A function that defines the filtering condition applied to each element in the result data.</param>
    /// <typeparam name="T">The type of elements contained in the result data.</typeparam>
    /// <returns>A read-only list containing elements from the result data that satisfy the predicate condition.</returns>
    public static IReadOnlyList<T> Where<T>(this Task<Results<T>> source, Func<T, bool> predicate)
    {
        if (source is null)
            return new List<T>();

        Results<T> result = source.Result;

        return !result.HasData ? new List<T>() : Filter(result.Data, predicate);
    }

    static IReadOnlyList<T> Filter<T>(IReadOnlyList<T> list, Func<T, bool> predicate)
    {
        var internalList = new List<T>();

        for (int i = 0; i < list.Count; i++)
        {
            if (predicate(list[i]))
                internalList.Add(list[i]);
        }
            
        return internalList;
    }
}