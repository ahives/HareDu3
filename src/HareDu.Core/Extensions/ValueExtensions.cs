namespace HareDu.Core.Extensions;

using System.Collections.Generic;
using System.Threading.Tasks;

public static class ValueExtensions
{
    /// <summary>
    /// Determines whether all values in the specified list are null, empty, or whitespace.
    /// </summary>
    /// <param name="values">The list of string values to evaluate.</param>
    /// <returns>True if all values in the list are null, empty, or whitespace; otherwise, false.</returns>
    public static bool IsEmpty(this IList<string> values)
    {
        for (int i = 0; i < values.Count; i++)
        {
            if (!string.IsNullOrWhiteSpace(values[i]))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Returns true if at least one value in the specified list is not null, empty, or whitespace, otherwise, false.
    /// </summary>
    /// <param name="values">The list of string values to evaluate.</param>
    /// <returns>True if the list contains at least one non-null, non-empty, and non-whitespace value; otherwise, false.</returns>
    public static bool IsNotEmpty(this IList<string> values) => !IsEmpty(values);

    /// <summary>
    /// Returns the original list if it is not null; otherwise, returns an empty list.
    /// </summary>
    /// <param name="data">The original list to check.</param>
    /// <typeparam name="T">The type of data contained in the list.</typeparam>
    /// <returns>An IReadOnlyList containing the original data if it is not null; otherwise, an empty list.</returns>
    public static IReadOnlyList<T> GetDataOrEmpty<T>(this List<T> data) => data ?? new List<T>();

    /// <summary>
    /// Returns the specified data if it is not null; otherwise, returns the default value of the type.
    /// </summary>
    /// <param name="data">The object to evaluate or return.</param>
    /// <typeparam name="T">The type of the object being evaluated.</typeparam>
    /// <returns>The original object if it is not null; otherwise, the default value of the specified type.</returns>
    public static T GetDataOrDefault<T>(this T data) => data is null ? default : data;

    /// <summary>
    /// Attempts to retrieve a value at the specified index from the data contained in the source result.
    /// </summary>
    /// <param name="source">The result containing the data to access.</param>
    /// <param name="index">The zero-based index of the value to retrieve.</param>
    /// <param name="value">The value at the specified index if found; otherwise, the default value for the type.</param>
    /// <typeparam name="T">The type of data contained in the result.</typeparam>
    /// <returns>True if a value exists at the specified index and was successfully retrieved; otherwise, false.</returns>
    public static bool TryGetValue<T>(this Result<IReadOnlyList<T>> source, int index, out T value)
    {
        if (source is null || !source.HasData || index < 0 || index >= source.Data.Count)
        {
            value = default;
            return false;
        }

        value = source.Data[index];
        return true;
    }

    /// <summary>
    /// Attempts to retrieve a value from the specified result at the given index.
    /// </summary>
    /// <param name="source">The asynchronous result containing a read-only list of data.</param>
    /// <param name="index">The index of the value to retrieve.</param>
    /// <param name="value">The value retrieved from the list at the specified index, or the default value if retrieval fails.</param>
    /// <typeparam name="T">The type of the elements in the list contained in the result.</typeparam>
    /// <returns>True if the value is successfully retrieved; otherwise, false.</returns>
    public static bool TryGetValue<T>(this Task<Result<IReadOnlyList<T>>> source, int index, out T value)
    {
        if (source is null || index < 0)
        {
            value = default;
            return false;
        }
            
        Result<IReadOnlyList<T>> result = source.GetAwaiter().GetResult();

        if (result?.Data is null || !result.HasData || result.HasFaulted || index >= result.Data.Count)
        {
            value = default;
            return false;
        }

        value = result.Data[index];
        return true;
    }
}