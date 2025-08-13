namespace HareDu.Shovel.Model;

/// <summary>
/// Defines the acknowledgment mode for a message shovel, determining
/// how and when messages are acknowledged as they are transferred
/// between source and destination.
/// </summary>
public enum ShovelAckMode
{
    /// <summary>
    /// Acknowledge consumed messages only after confirmation from the destination.
    /// Typically used when a message transfer requires guarantees that the message
    /// has been processed by the destination before acknowledging back to the source.
    /// </summary>
    OnConfirm,

    /// <summary>
    /// Acknowledge consumed messages immediately upon being published to the destination.
    /// Suitable for scenarios where immediate acknowledgment is required without
    /// waiting for confirmation from the destination.
    /// </summary>
    OnPublish,

    /// <summary>
    /// Indicates that messages are not acknowledged when consumed.
    /// This mode disables the acknowledgment mechanism, meaning messages can be delivered without
    /// requiring confirmation from the consumer. It is typically used in scenarios where
    /// high throughput is more critical than reliability or message delivery guarantees.
    /// </summary>
    NoAck
}