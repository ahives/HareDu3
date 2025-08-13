namespace HareDu.Model;

/// <summary>
/// Represents the acknowledgement modes for consumed messages in a message transfer system.
/// These modes define when consumed messages should be acknowledged back to the source.
/// </summary>
public enum AckMode
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