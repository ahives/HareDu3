namespace HareDu.Core.Extensions;

using System;
using System.Security.Cryptography;
using System.Text;

public static class IdentifierGenerationExtensions
{
    /// <summary>
    /// Generates a unique identifier based on the fully qualified name of the specified type.
    /// </summary>
    /// <param name="type">The type object used to generate the unique identifier.</param>
    /// <returns>Returns a unique identifier as a string.</returns>
    public static string GetIdentifier(this Type type) => type.FullName.GetIdentifier();

    /// <summary>
    /// Generates a unique guid identifier based on the input string.
    /// </summary>
    /// <param name="value">The input string used to generate the identifier.</param>
    /// <returns>Returns a unique guid identifier as a string.</returns>
    public static string GetIdentifier(this string value)
    {
        using var algorithm = MD5.Create();
        byte[] bytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
                
        return new Guid(bytes).ToString();
    }
}