namespace HareDu;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Provides functionality to configure various policy arguments for a RabbitMQ policy.
/// </summary>
public interface PolicyArgumentConfigurator
{
    /// <summary>
    /// Sets the specified argument on the policy with the provided value.
    /// </summary>
    /// <param name="arg">The name of the policy argument to be set.</param>
    /// <param name="value">The value to assign to the specified argument.</param>
    /// <typeparam name="T">The type of the value being assigned to the argument.</typeparam>
    void Set<T>([NotNull] string arg, [NotNull] T value);

    /// <summary>
    /// Sets the expiry duration of the policy in milliseconds.
    /// </summary>
    /// <param name="milliseconds">The duration in milliseconds after which the resource will expire.</param>
    void SetExpiry([NotNull] ulong milliseconds);

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
    /// Configures the high availability mode for the policy.
    /// </summary>
    /// <param name="mode">The high availability mode to be applied to the policy.</param>
    void SetHighAvailabilityMode([NotNull] HighAvailabilityModes mode);

    /// <summary>
    /// Sets the high availability parameter to the specified value. This is used to configure the number of nodes for high availability in policies.
    /// </summary>
    /// <param name="value">The number of nodes or specific high availability parameter to be assigned.</param>
    void SetHighAvailabilityParams([NotNull] uint value);

    /// <summary>
    /// Sets the synchronization mode for high availability.
    /// </summary>
    /// <param name="mode">The synchronization mode to be set, defining whether it is manual or automatic.</param>
    void SetHighAvailabilitySyncMode([NotNull] HighAvailabilitySyncMode mode);

    /// <summary>
    /// Sets the time-to-live for messages in the queue, in milliseconds.
    /// </summary>
    /// <param name="milliseconds">The duration in milliseconds after which messages in the queue will expire and be removed.</param>
    void SetMessageTimeToLive([NotNull] ulong milliseconds);

    /// <summary>
    /// Sets the maximum message size in bytes for the policy.
    /// </summary>
    /// <param name="value">The maximum size in bytes that a message can have before it is rejected or discarded.</param>
    void SetMessageMaxSizeInBytes([NotNull] ulong value);

    /// <summary>
    /// Sets the maximum size of a message for the policy.
    /// </summary>
    /// <param name="value">The maximum size of a message in bytes.</param>
    void SetMessageMaxSize([NotNull] ulong value);

    /// <summary>
    /// Sets the dead letter exchange argument for the policy with the specified value.
    /// </summary>
    /// <param name="value">The name of the exchange to be used as the dead letter exchange.</param>
    void SetDeadLetterExchange([NotNull] string value);

    /// <summary>
    /// Sets the dead letter routing key argument for a RabbitMQ policy with the specified value.
    /// </summary>
    /// <param name="value">The routing key that will be used for the dead letter exchange.</param>
    void SetDeadLetterRoutingKey([NotNull] string value);

    /// <summary>
    /// Sets the mode of the queue to define its behavior and storage characteristics.
    /// </summary>
    /// <param name="mode">The mode to set for the queue, specifying its attributes such as storage strategy.</param>
    void SetQueueMode([NotNull] QueueMode mode);

    /// <summary>
    /// Sets the alternate exchange argument for a RabbitMQ policy with the specified value.
    /// </summary>
    /// <param name="value">The name of the alternate exchange to be assigned as the argument.</param>
    void SetAlternateExchange([NotNull] string value);

    /// <summary>
    /// Sets the queue master locator policy argument with the specified key.
    /// </summary>
    /// <param name="key">The key identifying the queue master locator to be assigned as the argument.</param>
    void SetQueueMasterLocator([NotNull] string key);

    /// <summary>
    /// Configures the queue promotion behavior during a shutdown operation.
    /// </summary>
    /// <param name="mode">Specifies the desired promotion mode for the queue during shutdown.</param>
    void SetQueuePromotionOnShutdown([NotNull] QueuePromotionShutdownMode mode);

    /// <summary>
    /// Configures the queue promotion behavior when a failure occurs, using the specified mode.
    /// </summary>
    /// <param name="mode">The promotion failure behavior mode to set. This determines how the system handles promotion failures.</param>
    void SetQueuePromotionOnFailure([NotNull] QueuePromotionFailureMode mode);

    /// <summary>
    /// Sets the delivery limit argument on the policy with the provided value.
    /// </summary>
    /// <param name="limit">The maximum number of deliveries allowed for a message before it is dropped or dead-lettered.</param>
    void SetDeliveryLimit([NotNull] ulong limit);
}