namespace HareDu;

using System.Diagnostics.CodeAnalysis;
using Model;

/// <summary>
/// Represents options available for configuring user access.
/// </summary>
public interface UserAccessOptions
{
    /// <summary>
    /// Adds a specific user access tag.
    /// </summary>
    /// <param name="tag">The user access tag to be added.</param>
    void AddTag([NotNull] UserAccessTag tag);
}