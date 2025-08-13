namespace HareDu.Shovel.Model;

/// <summary>
/// Defines the protocols used by RabbitMQ for shoveling messages between source and destination.
/// </summary>
public enum ShovelProtocol
{
    /// <summary>
    /// Represents the AMQP 0-9-1 protocol for shoveling messages between the source and destination in RabbitMQ.
    /// </summary>
    Amqp091,

    /// <summary>
    /// Represents the AMQP 1.0 protocol for shoveling messages between the source and destination in RabbitMQ.
    /// </summary>
    Amqp10
}