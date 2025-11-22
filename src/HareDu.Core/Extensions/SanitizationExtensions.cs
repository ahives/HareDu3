namespace HareDu.Core.Extensions;

public static class SanitizationExtensions
{
    /// <summary>
    /// Extension methods for sanitizing strings.
    /// </summary>
    /// <param name="value">The original virtual host name to be sanitized.</param>
    extension(string value)
    {
        public string SanitizePropertiesKey() =>
            string.IsNullOrWhiteSpace(value) ? string.Empty : value.Replace("%5F", "%255F");

        /// <summary>
        /// Converts the provided virtual host name into a sanitized format suitable for use in API paths.
        /// </summary>
        /// <returns>
        /// A sanitized version of the virtual host name. If the input is null, empty, or consists only of white spaces, an empty string is returned.
        /// If the input is "/", it is replaced with "%2f".
        /// </returns>
        public string ToSanitizedName()
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            return value == @"/" ? value.Replace("/", "%2f") : value;
        }
    }
}