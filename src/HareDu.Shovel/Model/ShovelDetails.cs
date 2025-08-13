namespace HareDu.Shovel.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents the details of a RabbitMQ shovel. A shovel is used to transfer messages from a source to a destination.
/// This class contains configuration properties related to the source, destination, and shoveling behavior.
/// </summary>
public record ShovelDetails
{
    /// <summary>
    /// Gets the protocol used for the source in the message shoveling process.
    /// This indicates the communication protocol (such as AMQP 0-9-1 or AMQP 1.0)
    /// used between the shovel and the source system.
    /// </summary>
    [JsonPropertyName("src-protocol")]
    public ShovelProtocol SourceProtocol { get; init; }

    /// <summary>
    /// Gets the URI of the source from which messages are being shovelled.
    /// This specifies the connection details of the source system in the message shoveling process.
    /// </summary>
    [JsonPropertyName("src-uri")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string SourceUri { get; init; }

    /// <summary>
    /// Gets the name of the source queue from which messages are consumed
    /// during the shoveling process. This indicates the specific queue
    /// on the source side that is read by the shovel.
    /// </summary>
    [JsonPropertyName("src-queue")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string SourceQueue { get; init; }

    /// <summary>
    /// Represents the condition under which the source queue should be deleted after the completion
    /// of the shoveling process. This can specify "never", "queue-length", or a time duration
    /// indicating when the source queue will be removed.
    /// </summary>
    [JsonPropertyName("src-delete-after")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public object SourceDeleteAfter { get; init; }

    /// <summary>
    /// Gets the maximum number of messages that can be prefetched from the source queue
    /// during the message shoveling process. This defines the upper limit of unacknowledged
    /// messages allowed to be sent to the shovel for processing.
    /// </summary>
    [JsonPropertyName("src-prefetch-count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ulong SourcePrefetchCount { get; init; }

    /// <summary>
    /// Gets the protocol used for the destination in the message shoveling process.
    /// This specifies the communication protocol (such as AMQP 0-9-1 or AMQP 1.0)
    /// employed between the shovel and the destination system.
    /// </summary>
    [JsonPropertyName("dest-protocol")]
    public ShovelProtocol DestinationProtocol { get; init; }

    /// <summary>
    /// Gets the URI that specifies the destination in the message shoveling process.
    /// This URI represents the target system or endpoint where messages are forwarded.
    /// </summary>
    [JsonPropertyName("dest-uri")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string DestinationUri { get; init; }

    /// <summary>
    /// Gets the name of the destination queue where messages are to be transferred.
    /// Specifies the queue within the destination system that will receive the shovelled messages.
    /// </summary>
    [JsonPropertyName("dest-queue")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string DestinationQueue { get; init; }

    /// <summary>
    /// Gets the acknowledgment mode used for consumed messages during the shoveling process.
    /// Specifies how and when message acknowledgments are returned to the source,
    /// determining the reliability of message delivery between the source and destination.
    /// </summary>
    [JsonPropertyName("ack-mode")]
    public ShovelAckMode AckMode { get; init; }
}