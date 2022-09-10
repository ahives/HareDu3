namespace HareDu.Model;

using System.Text.Json.Serialization;
using Serialization.Converters;

public record QueueSyncRequest
{
    [JsonPropertyName("action")]
    [JsonConverter(typeof(QueueSyncActionEnumConverter))]
    public QueueSyncAction Action { get; init; }
}