namespace HareDu;

using System.Diagnostics.CodeAnalysis;
using Model;

/// <summary>
/// Provides functionality to configure various policy arguments for a RabbitMQ policy.
/// </summary>
public interface PolicyArgumentConfigurator
{
    /// <summary>
    /// Sets the expiry duration of the policy in milliseconds.
    /// </summary>
    /// <param name="milliseconds">The duration in milliseconds after which the resource will expire.</param>
    void SetExpiry(ulong milliseconds);

    /// <summary>
    /// Configures the federation upstream set argument for a RabbitMQ policy with the specified value.
    /// </summary>
    /// <param name="value">The name of the federation upstream set to be assigned as the argument.</param>
    void SetFederationUpstreamSet([NotNull] string value);

    /// <summary>
    /// Sets the federation upstream argument on the policy.
    /// </summary>
    /// <param name="value">The name of the federation-upstream to be set.</param>
    void SetFederationUpstream([NotNull] string value);

    /// <summary>
    /// Sets the time-to-live for messages in the queue, in milliseconds.
    /// </summary>
    /// <param name="milliseconds">The duration in milliseconds after which messages in the queue will expire and be removed.</param>
    void SetMessageTimeToLive(ulong milliseconds);

    /// <summary>
    /// Sets the maximum message size in bytes for the policy.
    /// </summary>
    /// <param name="value">The maximum size in bytes that a message can have before it is rejected or discarded.</param>
    void SetMessageMaxSizeInBytes(ulong value);

    /// <summary>
    /// Sets the maximum size of a message for the policy.
    /// </summary>
    /// <param name="value">The maximum size of a message in bytes.</param>
    void SetMessageMaxSize(ulong value);

    /// <summary>
    /// Sets the dead letter exchange argument for the policy with the specified value.
    /// </summary>
    /// <param name="name">The name of the exchange to be used as the dead letter exchange.</param>
    void SetDeadLetterExchangeName([NotNull] string name);

    /// <summary>
    /// Sets the dead letter routing key argument for a RabbitMQ policy with the specified value.
    /// </summary>
    /// <param name="value">The routing key that will be used for the dead letter exchange.</param>
    void SetDeadLetterRoutingKey([NotNull] string value);

    /// <summary>
    /// Sets the mode of the queue to define its behavior and storage characteristics.
    /// </summary>
    /// <param name="mode">The mode to set for the queue, specifying its attributes such as storage strategy.</param>
    void SetQueueMode(QueueMode mode);

    /// <summary>
    /// Sets the alternate exchange argument for a RabbitMQ policy with the specified value.
    /// </summary>
    /// <param name="value">The name of the alternate exchange to be assigned as the argument.</param>
    void SetAlternateExchange([NotNull] string value);

    /// <summary>
    /// Sets the queue master locator policy argument with the specified key.
    /// </summary>
    /// <param name="locator">The key identifying the queue master locator to be assigned as the argument.</param>
    void SetQueueMasterLocator([NotNull] string locator);

    /// <summary>
    /// Sets the delivery limit argument on the policy with the provided value.
    /// </summary>
    /// <param name="limit">The maximum number of deliveries allowed for a message before it is dropped or dead-lettered.</param>
    void SetDeliveryLimit(uint limit);

    /// <summary>
    /// Configures the queue overflow behavior for a RabbitMQ policy.
    /// </summary>
    /// <param name="behavior">The behavior to apply when the queue exceeds its maximum capacity.</param>
    void SetQueueOverflowBehavior(QueueOverflowBehavior behavior);

    /// <summary>
    /// Sets the queue leader locator strategy for determining the leader of the RabbitMQ queue.
    /// </summary>
    /// <param name="locator">The strategy to be used for locating the queue leader.</param>
    void SetQueueLeaderLocator(QueueLeaderLocator locator);

    /// <summary>
    /// Sets the consumer timeout duration in milliseconds.
    /// </summary>
    /// <param name="timeout">The duration in milliseconds after which the consumer will time out.</param>
    void SetConsumerTimeout(uint timeout);

    /// <summary>
    /// Configures the strategy for routing messages to a dead-letter queue during the dead-lettering process.
    /// </summary>
    /// <param name="strategy">The strategy to use for routing messages, indicating how messages should be handled when dead-lettered.</param>
    void SetDeadLetterQueueStrategy(DeadLetterQueueStrategy strategy);

    /// <summary>
    /// Sets the maximum age for a message in the queue.
    /// </summary>
    /// <param name="duration">The value representing the duration of the maximum age.</param>
    /// <param name="units">The unit of time (e.g., seconds, minutes, hours) for the specified duration.</param>
    void SetMaxAge(uint duration, TimeUnit units);
}