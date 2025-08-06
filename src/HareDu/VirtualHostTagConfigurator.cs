namespace HareDu;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Defines methods for configuring tags associated with a RabbitMQ virtual host.
/// </summary>
public interface VirtualHostTagConfigurator
{
    /// <summary>
    /// Adds a tag to the virtual host configuration.
    /// </summary>
    /// <param name="tag">The tag to be added. This value must not be null or whitespace.</param>
    void Add([NotNull] string tag);
}