namespace HareDu;

using System;

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
    void Description(string description);

    /// <summary>
    /// Specifies the tags to associate with the virtual host.
    /// </summary>
    /// <param name="configurator">The action to configure tags using an implementation of <see cref="VirtualHostTagConfigurator"/>.</param>
    void Tags(Action<VirtualHostTagConfigurator> configurator);
}