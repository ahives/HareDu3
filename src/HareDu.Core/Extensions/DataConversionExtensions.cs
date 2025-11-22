namespace HareDu.Core.Extensions;

using System;

public static class DataConversionExtensions
{
    /// <summary>
    /// Extensions for converting data types.
    /// </summary>
    /// <param name="value">The integer value to be converted.</param>
    extension(int value)
    {
        /// <summary>
        /// Converts the specified integer value to a 32-bit unsigned integer.
        /// </summary>
        /// <returns>A 32-bit unsigned integer representation of the specified value.</returns>
        public uint ConvertTo() => Convert.ToUInt32(value);
    }
}