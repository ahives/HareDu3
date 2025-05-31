namespace HareDu.Core.Extensions;

using System;

public static class DataConversionExtensions
{
    /// <summary>
    /// Converts the specified integer value to a 32-bit unsigned integer.
    /// </summary>
    /// <param name="value">The integer value to be converted.</param>
    /// <returns>A 32-bit unsigned integer representation of the specified value.</returns>
    public static uint ConvertTo(this int value) => Convert.ToUInt32(value);
}