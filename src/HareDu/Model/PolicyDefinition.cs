namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents the definition of a policy in the system, including attributes that control queue behavior,
/// message processing, and other advanced configurations within the messaging infrastructure.
/// </summary>
public record PolicyDefinition
{
    /// <summary>
    /// Specifies the maximum number of redelivery attempts for a message before it is
    /// discarded or routed to a dead-letter queue. This property is used to control
    /// the number of delivery retries when a message fails to be successfully processed.
    /// </summary>
    [JsonPropertyName("delivery-limit")]
    public uint DeliveryLimit { get; init; }

    /// <summary>
    /// Specifies the name of the exchange to which messages are routed when they
    /// are dead-lettered. Dead-lettering occurs when a message cannot be processed
    /// or delivered due to specific conditions, such as exceeding its Time-To-Live (TTL)
    /// or being rejected by a consumer. This property is used to configure the handling
    /// of undeliverable messages.
    /// </summary>
    [JsonPropertyName("dead-letter-exchange")]
    public string DeadLetterExchangeName { get; init; }

    /// <summary>
    /// Defines the maximum number of messages that a queue can hold before exceeding its capacity.
    /// Once this limit is reached, the behavior of the queue is dictated by its overflow policy,
    /// such as discarding older messages or rejecting new ones.
    /// This property is useful for managing memory usage and ensuring queues do not grow indefinitely.
    /// </summary>
    [JsonPropertyName("max-length")]
    public ulong MaxLength { get; init; }

    /// <summary>
    /// Specifies the maximum age, as a duration, for a message to remain in a queue before it is considered expired.
    /// Messages older than this duration may be discarded or handled based on the queue's configurations.
    /// This property is useful for ensuring that stale messages do not remain in the system indefinitely.
    /// </summary>
    [JsonPropertyName("max-age")]
    public string MaxAge { get; init; }

    /// <summary>
    /// Specifies the maximum total size, in bytes, of all messages that a queue can hold.
    /// When the limit is reached, the queue's behavior will depend on the configured overflow policy.
    /// This property is useful for controlling memory usage and ensuring resource constraints are respected.
    /// </summary>
    [JsonPropertyName("max-length-bytes")]
    public ulong MaxLengthBytes { get; init; }

    /// <summary>
    /// Specifies the behavior of a queue when its maximum length or maximum length in bytes is reached.
    /// This property determines how the queue handles overflow scenarios, such as dropping messages
    /// or rejecting further message publications. Proper configuration of this property helps in managing
    /// resource usage and ensuring predictable handling of message surpluses.
    /// </summary>
    [JsonPropertyName("overflow")]
    public QueueOverflowBehavior OverflowBehavior { get; init; }

    /// <summary>
    /// Specifies the mechanism for determining which node in a clustered environment should be the leader
    /// responsible for managing a specific queue. This property helps ensure queues are optimally distributed
    /// across nodes to balance load and enhance availability.
    /// </summary>
    [JsonPropertyName("queue-leader-locator")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public QueueLeaderLocator QueueLeaderLocator { get; init; }

    /// <summary>
    /// Represents the time (in milliseconds) a consumer can remain inactive before being disconnected.
    /// This property is used to manage and monitor the behavior of inactive consumers in a queue.
    /// </summary>
    [JsonPropertyName("consumer-timeout")]
    public uint ConsumerTimeout { get; init; }

    /// <summary>
    /// Specifies the duration (in milliseconds) after which a queue can be automatically expired if it has no consumers.
    /// This property is used to control the lifetime of idle queues, ensuring they do not persist indefinitely.
    /// </summary>
    [JsonPropertyName("expires")]
    public ulong AutoExpire { get; init; }

    /// <summary>
    /// Defines the routing key to be used when a message is moved to a dead-letter queue.
    /// This property is used to control how messages are routed to the dead-letter queue's
    /// corresponding exchange based on this specified routing key.
    /// </summary>
    [JsonPropertyName("dead-letter-routing-key")]
    public string DeadLetterRoutingKey { get; init; }

    /// <summary>
    /// Specifies the strategy to be used for dead-lettering messages in a queue.
    /// This property defines how messages that cannot be processed successfully
    /// or are rejected by consumers are handled when they are moved to a dead-letter queue.
    /// </summary>
    [JsonPropertyName("dead-letter-strategy")]
    public DeadLetterQueueStrategy DeadLetterQueueStrategy { get; init; }

    /// <summary>
    /// Defines the time-to-live (TTL) for messages within a queue.
    /// This property specifies the maximum duration, in milliseconds,
    /// that a message can remain in the queue before expiring.
    /// Expired messages are removed automatically and are no longer accessible.
    /// </summary>
    [JsonPropertyName("message-ttl")]
    public ulong MessageTimeToLive { get; init; }

    /// <summary>
    /// Represents the name of a predefined set of federation upstream connections.
    /// This property is used to specify a group of upstreams for message federation,
    /// enabling the exchange of messages between brokers in federated configurations.
    /// </summary>
    [JsonPropertyName("federation-upstream-set")]
    public string FederationUpstreamSet { get; init; }

    /// <summary>
    /// Specifies the upstream configuration for message federation.
    /// Federation upstream is used to define an external source from
    /// which messages are federated into the current broker or cluster.
    /// This facilitates message distribution and integration between brokers.
    /// </summary>
    [JsonPropertyName("federation-upstream")]
    public string FederationUpstream { get; init; }

    /// <summary>
    /// Specifies the operational mode of the queue, determining its behavior in terms of message storage.
    /// The available modes include:
    /// - Default: The queue operates in the standard mode, optimized for performance with messages held in memory.
    /// - Lazy: The queue prioritizes persistence by storing messages on disk, suitable for scenarios with larger message backlogs.
    /// </summary>
    [JsonPropertyName("queue-mode")]
    public QueueMode QueueMode { get; init; }

    /// <summary>
    /// Defines the alternate exchange for the queue or exchange that messages will be routed to
    /// in case the primary destination lacks a suitable binding for the messages being published.
    /// This property enables configuring a failover mechanism to handle unroutable messages.
    /// </summary>
    [JsonPropertyName("alternate-exchange")]
    public string AlternateExchange { get; init; }

    /// <summary>
    /// Specifies the location or behavior of the queue master in a clustered RabbitMQ environment.
    /// This property defines how the queue master will be determined within the cluster and can influence
    /// performance and message routing based on specific configurations.
    /// </summary>
    [JsonPropertyName("queue-master-locator")]
    public string QueueMasterLocator { get; init; }
}