namespace HareDu;

/// <summary>
/// Represents the mode in which a RabbitMQ shovel deletes itself after meeting specific conditions.
/// </summary>
public enum DeleteShovelMode
{
    /// <summary>
    /// Specifies that the shovel should never delete itself, regardless of message count or conditions.
    /// </summary>
    Never,

    /// <summary>
    /// Specifies that the shovel should delete itself based on the length of the queue it is associated with.
    /// </summary>
    QueueLength
}