namespace HareDu;

/// <summary>
/// Defines the configuration options for deleting a queue in the RabbitMQ broker.
/// </summary>
public interface QueueDeletionConfigurator
{
    /// <summary>
    /// Prevent delete actions on the specified queue from being successful if the queue has active consumers.
    /// </summary>
    void WhenHasNoConsumers();

    /// <summary>
    /// Prevent delete actions on the specified queue from being successful if the queue contains messages.
    /// </summary>
    void WhenEmpty();
}