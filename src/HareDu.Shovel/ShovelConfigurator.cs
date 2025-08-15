namespace HareDu.Shovel;

using Model;

/// <summary>
/// Provides methods to configure a RabbitMQ dynamic shovel.
/// </summary>
public interface ShovelConfigurator
{
    /// <summary>
    /// Specifies the URI of the RabbitMQ broker the shovel will connect to.
    /// </summary>
    /// <param name="uri">The URI string that identifies the RabbitMQ broker.</param>
    void Uri(string uri);

    /// <summary>
    /// Specifies the delay, in seconds, before the shovel will attempt to reconnect
    /// after a disconnection occurs.
    /// </summary>
    /// <param name="delayInSeconds">The number of seconds to wait before reconnecting. The value must be greater than or equal to 1.</param>
    void ReconnectDelay(int delayInSeconds);

    /// <summary>
    /// Determine how the shovel should acknowledge consumed messages.
    /// </summary>
    /// <param name="mode">Define how the shovel will acknowledge consumed messages.</param>
    void AcknowledgementMode(ShovelAckMode mode);

    /// <summary>
    /// Configures the source for the RabbitMQ shovel.
    /// </summary>
    /// <param name="queue">The name of the source queue to shovel messages from.</param>
    /// <param name="protocol">The protocol to use for the source connection.</param>
    /// <param name="configurator">An optional callback to further configure the source settings.</param>
    void Source(string queue, ShovelProtocol protocol, Action<ShovelSourceConfigurator> configurator = null);

    /// <summary>
    /// Configures the destination of the RabbitMQ Shovel.
    /// </summary>
    /// <param name="queue">The name of the queue where the messages should be delivered.</param>
    /// <param name="protocol">The protocol used for shoveling messages to the destination.</param>
    /// <param name="configurator">An optional configuration action for setting up additional destination parameters.</param>
    void Destination(string queue, ShovelProtocol protocol, Action<ShovelDestinationConfigurator> configurator = null);

    /// <summary>
    /// Describes how the shovel is confirmed from the destination end.
    /// </summary>
    /// <param name="queue">The name of the destination queue.</param>
    /// <param name="configurator">Describes the destination shovel.</param>
    void Destination(string queue, Action<ShovelDestinationConfigurator> configurator = null);
}