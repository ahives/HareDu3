namespace HareDu;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Defines the configuration of topic permissions, allowing for the specification
/// of exchange bindings and patterns for allowable read and write operations.
/// </summary>
public interface TopicPermissionsConfigurator
{
    /// <summary>
    /// Specify the exchange on which this set of permissions applies.
    /// </summary>
    /// <param name="exchange">The name of the exchange.</param>
    void Exchange([NotNull] string exchange);

    /// <summary>
    /// Specify the pattern of what types of writes are allowable for this permission.
    /// </summary>
    /// <param name="pattern">The pattern used to define allowable write types.</param>
    void UsingWritePattern([NotNull] string pattern);

    /// <summary>
    /// Specify the pattern of what types of reads are allowable for this permission.
    /// </summary>
    /// <param name="pattern">The pattern used to define allowable read types.</param>
    void UsingReadPattern([NotNull] string pattern);
}