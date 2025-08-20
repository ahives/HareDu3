namespace HareDu;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Provides methods to configure optional arguments for a queue, allowing fine-grained customization of queue properties such as expiration,
/// message time-to-live, dead-lettering settings, and alternate exchange configuration.
/// </summary>
public interface QueueArgumentConfigurator
{
    /// <summary>
    /// Configures a specific argument for the queue with a given value.
    /// </summary>
    /// <param name="arg">The name of the argument to be configured.</param>
    /// <param name="value">The value to assign to the specified argument.</param>
    /// <typeparam name="T">The type of the value being assigned to the argument.</typeparam>
    void Set<T>([NotNull] string arg, [NotNull] T value);

    /// <summary>
    /// Sets the expiration time for the queue, after which the queue will be automatically deleted if it is no longer in use.
    /// </summary>
    /// <param name="milliseconds">The expiration time in milliseconds. The value must be greater than 0.</param>
    void SetQueueExpiration(ulong milliseconds);

    /// <summary>
    /// Configures the queue to set a time-to-live for each individual message.
    /// </summary>
    /// <param name="milliseconds">The time-to-live for each message in the queue, specified in milliseconds.</param>
    void SetPerQueuedMessageExpiration(ulong milliseconds);

    /// <summary>
    /// Configures the dead-letter exchange for the queue, where messages will be routed when they cannot be delivered to their intended destination.
    /// </summary>
    /// <param name="exchange">The name of the dead-letter exchange to assign to the queue.</param>
    void SetDeadLetterExchange([NotNull] string exchange);

    /// <summary>
    /// Sets the routing key for the dead letter exchange.
    /// </summary>
    /// <param name="routingKey">The routing key to be associated with the dead letter exchange.</param>
    void SetDeadLetterExchangeRoutingKey([NotNull] string routingKey);

    /// <summary>
    /// Sets an alternate exchange for the queue. Messages that cannot be routed
    /// to any queue from the primary exchange will be routed to this alternate exchange.
    /// </summary>
    /// <param name="exchange">The name of the alternate exchange to be set for the queue.</param>
    void SetAlternateExchange([NotNull] string exchange);
}