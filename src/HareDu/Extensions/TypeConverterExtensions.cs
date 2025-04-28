namespace HareDu.Extensions;

public static class TypeConverterExtensions
{
    /// <summary>
    /// Converts a string representation of a number to an unsigned long (ulong).
    /// If the string is null, empty, whitespace, or "infinity", or if the conversion fails, it returns ulong.MaxValue.
    /// </summary>
    /// <param name="value">The string value to be converted to ulong.</param>
    /// <returns>The converted unsigned long value, or ulong.MaxValue if the input is invalid or cannot be converted.</returns>
    public static ulong ToLong(this string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Equals("infinity"))
            return ulong.MaxValue;

        return ulong.TryParse(value, out ulong result) ? result : ulong.MaxValue;
    }
}