namespace HareDu;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Interface representing the configuration options for a RabbitMQ virtual host.
/// </summary>
public interface VirtualHostConfigurator
{
    /// <summary>
    /// Configuration setting that enables tracing.
    /// </summary>
    void WithTracingEnabled();

    /// <summary>
    /// Specifies the description for the virtual host.
    /// </summary>
    /// <param name="description">The description to associate with the virtual host.</param>
    void Description([NotNull] string description);

    /// <summary>
    /// Specifies the tags to associate with the virtual host.
    /// </summary>
    /// <param name="configurator">The action to configure tags using an implementation of <see cref="VirtualHostTagConfigurator"/>.</param>
    void Tags([NotNull] Action<VirtualHostTagConfigurator> configurator);
}