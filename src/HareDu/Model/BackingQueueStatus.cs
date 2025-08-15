namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the status of the backing queue for a RabbitMQ queue, providing detailed
/// metrics and configuration data about the underlying queue mechanism.
/// </summary>
public record BackingQueueStatus
{
    /// <summary>
    /// Gets the mode of the backing queue, indicating its operational behavior.
    /// Can be either <c>Default</c> for normal operation or <c>Lazy</c> to optimize resource usage
    /// at the cost of latency.
    /// </summary>
    [JsonPropertyName("mode")]
    public BackingQueueMode Mode { get; init; }

    /// <summary>
    /// Represents the number of messages that are currently in the queue but have not been delivered to consumers.
    /// Typically represents undelivered messages awaiting processing.
    /// </summary>
    [JsonPropertyName("q1")]
    public long Q1 { get; init; }

    /// <summary>
    /// Represents the number of messages in the backing queue's Q2 segment, which is used for storing transient messages
    /// awaiting transfer to other internal segments or consumers.
    /// </summary>
    [JsonPropertyName("q2")]
    public long Q2 { get; init; }

    /// <summary>
    /// Gets the delta information for the backing queue, representing a collection
    /// of delta values which may provide insight into the queue's state changes
    /// or transitional behavior over time.
    /// </summary>
    [JsonPropertyName("delta")]
    public IList<object> Delta { get; init; }

    /// <summary>
    /// Represents the number of messages in the third priority queue segment.
    /// This metric is used to analyze or monitor the distribution of messages
    /// across the internal queue structure.
    /// </summary>
    [JsonPropertyName("q3")]
    public long Q3 { get; init; }

    /// <summary>
    /// Represents the number of messages in the fourth quantile of the backing queue,
    /// providing insight into message distribution and timing within the queue.
    /// </summary>
    [JsonPropertyName("q4")]
    public long Q4 { get; init; }

    /// <summary>
    /// Gets the current length of the queue, representing the number of messages
    /// awaiting processing or retrieval.
    /// </summary>
    [JsonPropertyName("len")]
    public long Length { get; init; }

    /// <summary>
    /// Gets the target total number of messages to be kept in RAM for the backing queue.
    /// This value serves as a guideline for optimizing memory usage while balancing performance.
    /// </summary>
    [JsonPropertyName("target_ram_count")]
    public string TargetTotalMessagesInRAM { get; init; }

    /// <summary>
    /// Represents the identifier for the next sequence in the backing queue, used to track message order.
    /// </summary>
    [JsonPropertyName("next_seq_id")]
    public long NextSequenceId { get; init; }

    /// <summary>
    /// Gets the average ingress rate, representing the rate at which messages are entering
    /// the queue over a period of time, measured in messages per second.
    /// </summary>
    [JsonPropertyName("avg_ingress_rate")]
    public decimal AvgIngressRate { get; init; }

    /// <summary>
    /// Gets the average rate at which messages are being delivered from the queue (egress rate),
    /// measured in messages per second.
    /// </summary>
    [JsonPropertyName("avg_egress_rate")]
    public decimal AvgEgressRate { get; init; }

    /// <summary>
    /// Represents the average rate at which acknowledgements are being received by the queue.
    /// Measured in acknowledgements per second.
    /// </summary>
    [JsonPropertyName("avg_ack_ingress_rate")]
    public decimal AvgAcknowledgementIngressRate { get; init; }

    /// <summary>
    /// Gets the average rate at which acknowledgements are egressed (sent out) from the backing queue,
    /// expressed as a decimal value representing the rate per second.
    /// </summary>
    [JsonPropertyName("avg_ack_egress_rate")]
    public decimal AvgAcknowledgementEgressRate { get; init; }
}