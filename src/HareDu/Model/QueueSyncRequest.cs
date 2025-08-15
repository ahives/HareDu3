namespace HareDu.Model;

using System.Text.Json.Serialization;
using Serialization.Converters;

/// <summary>
/// Represents the payload used to perform synchronization operations on a queue within the system.
/// This includes triggering synchronization and cancellation actions for a specific queue.
/// </summary>
public record QueueSyncRequest
{
    /// <summary>
    /// Gets the action to be performed on the queue, such as synchronizing or canceling synchronization.
    /// </summary>
    /// <remarks>
    /// The value of the Action property is represented by the <see cref="QueueSyncAction"/> enum, which defines the possible actions:
    /// Sync - synchronize the queue.
    /// CancelSync - cancel the synchronization of the queue.
    /// </remarks>
    [JsonPropertyName("action")]
    [JsonConverter(typeof(QueueSyncActionEnumConverter))]
    public QueueSyncAction Action { get; init; }
}