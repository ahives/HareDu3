namespace HareDu.Shovel;

using System.Text.Json.Serialization;
using Model;

/// <summary>
/// Represents the parameters for configuring a Shovel in a messaging system.
/// Provides detailed attributes for defining source and destination configurations,
/// message delivery protocols, behaviors, and additional options.
/// </summary>
public record ShovelRequestParams
{
    /// <summary>
    /// Represents the protocol used for the source in a RabbitMQ Shovel configuration.
    /// This property specifies how the source is accessed and communicates during the
    /// data transfer process. Possible values for the source protocol include
    /// AMQP 0.9.1 or AMQP 1.0, depending on the supported versions of the protocols.
    /// </summary>
    [JsonPropertyName("src-protocol")]
    public ShovelProtocol SourceProtocol { get; init; }

    /// <summary>
    /// Specifies the URI used for the source in a RabbitMQ Shovel configuration.
    /// This property defines the connection endpoint for the source from which messages
    /// will be retrieved during the data transfer process.
    /// </summary>
    [JsonPropertyName("src-uri")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string SourceUri { get; init; }

    /// <summary>
    /// Represents the name of the source queue in a RabbitMQ Shovel configuration.
    /// This property specifies the queue from which messages are consumed during
    /// the data transfer process. The value is typically the name of an existing
    /// queue on the source broker.
    /// </summary>
    [JsonPropertyName("src-queue")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string SourceQueue { get; init; }

    /// <summary>
    /// Represents the protocol used for the destination in a RabbitMQ Shovel configuration.
    /// This property defines the communication protocol for transferring messages to the destination.
    /// Supported protocol values include AMQP 0.9.1 and AMQP 1.0, depending on the capabilities of the destination.
    /// </summary>
    [JsonPropertyName("dest-protocol")]
    public ShovelProtocol DestinationProtocol { get; init; }

    /// <summary>
    /// Represents the URI of the destination in a RabbitMQ Shovel configuration.
    /// This property specifies the target endpoint where data is forwarded or routed
    /// during the Shovel's operation. The URI typically includes protocol, host,
    /// port, and other connection-specific details required to connect to the
    /// destination.
    /// </summary>
    [JsonPropertyName("dest-uri")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string DestinationUri { get; init; }

    /// <summary>
    /// Represents the name of the destination queue in a RabbitMQ Shovel configuration.
    /// This property determines the target queue to which messages are forwarded during
    /// the data transfer process.
    /// </summary>
    [JsonPropertyName("dest-queue")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string DestinationQueue { get; init; }

    /// <summary>
    /// Specifies the delay, in seconds, before attempting to reconnect when a connection
    /// failure occurs in a RabbitMQ Shovel configuration. This property helps control
    /// the interval between reconnection attempts, contributing to the reliability of the
    /// data transfer process in case of transient disruptions.
    /// </summary>
    [JsonPropertyName("reconnect-delay")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int ReconnectDelay { get; init; }

    /// <summary>
    /// Defines the acknowledgment mode used by the RabbitMQ shovel
    /// when processing messages. This property determines how message
    /// acknowledgments are handled during transfer from the source
    /// to the destination. Possible values include OnPublish, OnConfirm, and NoAck,
    /// indicating different acknowledgment strategies.
    /// </summary>
    [JsonPropertyName("ack-mode")]
    public ShovelAckMode AcknowledgeMode { get; init; }

    /// <summary>
    /// Specifies the time period or condition after which the source of the RabbitMQ Shovel is deleted.
    /// This property can be used to define a duration (e.g., never, an interval like 1 hour, etc.)
    /// or other criteria for cleaning up the source once the Shovel has completed its operations.
    /// The value is dependent on the configuration and how the Shovel is designed to handle
    /// source cleanup behavior.
    /// </summary>
    [JsonPropertyName("src-delete-after")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public object SourceDeleteAfter { get; init; }

    /// <summary>
    /// Specifies the maximum number of messages to prefetch from the source
    /// queue in a RabbitMQ Shovel configuration. This property determines
    /// how many messages can be transmitted and held in memory before being
    /// processed, helping control throughput and memory usage in the data
    /// transfer process.
    /// </summary>
    [JsonPropertyName("src-prefetch-count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ulong SourcePrefetchCount { get; init; }

    /// <summary>
    /// Specifies the name of the source exchange in a RabbitMQ Shovel configuration.
    /// This property represents the exchange from which messages are retrieved as the
    /// starting point of the data transfer process.
    /// </summary>
    [JsonPropertyName("src-exchange")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string SourceExchange { get; init; }

    /// <summary>
    /// Defines the routing key used for message routing from the source exchange in a RabbitMQ Shovel configuration.
    /// This property specifies the routing key that determines which messages are selected when transferring
    /// from the source exchange to the destination.
    /// </summary>
    [JsonPropertyName("src-exchange-key")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string SourceExchangeRoutingKey { get; init; }

    /// <summary>
    /// Specifies the destination exchange in a RabbitMQ Shovel configuration.
    /// This property identifies the target exchange where the messages should be routed
    /// during the Shovel operation. It enables message routing to the appropriate destination
    /// exchange as part of the inter-broker or intra-broker transfer process.
    /// </summary>
    [JsonPropertyName("dest-exchange")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string DestinationExchange { get; init; }

    /// <summary>
    /// Specifies the routing key used for the destination exchange in a RabbitMQ Shovel configuration.
    /// This property determines how messages are routed to the appropriate queue within the
    /// destination exchange during the data transfer process.
    /// </summary>
    [JsonPropertyName("dest-exchange-key")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string DestinationExchangeKey { get; init; }

    /// <summary>
    /// Represents the properties used for publishing messages to the destination in a RabbitMQ Shovel configuration.
    /// This property defines specific settings or attributes that are applied to messages as they are published
    /// to the destination, ensuring that the messages meet the desired operational or protocol requirements.
    /// </summary>
    [JsonPropertyName("dest-publish-properties")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string DestinationPublishProperties { get; init; }

    /// <summary>
    /// Indicates whether forward headers are added to the destination message in a RabbitMQ Shovel configuration.
    /// This property, when enabled, ensures that headers describing the original message source
    /// are included at the destination during the data transfer process.
    /// </summary>
    [JsonPropertyName("dest-add-forward-headers")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool DestinationAddForwardHeaders { get; init; }

    /// <summary>
    /// Indicates whether a timestamp header should be added to messages forwarded
    /// to the destination in a RabbitMQ Shovel configuration. If set to true,
    /// the current timestamp will be appended to each message as a header
    /// upon reaching the destination queue or exchange. This can be useful for
    /// tracking and auditing purposes.
    /// </summary>
    [JsonPropertyName("dest-add-timestamp-header")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool DestinationAddTimestampHeader { get; init; }
}