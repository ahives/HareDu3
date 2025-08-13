namespace HareDu.Shovel;

using Model;

/// <summary>
/// Provides methods to configure a RabbitMQ dynamic shovel.
/// </summary>
public interface ShovelConfigurator
{
    /// <summary>
    /// The connection URI of the RabbitMQ broker.
    /// </summary>
    /// <param name="uri"></param>
    void Uri(string uri);

    /// <summary>
    /// The duration to wait before reconnecting to the brokers after being disconnected at either end.
    /// </summary>
    /// <param name="delayInSeconds"></param>
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