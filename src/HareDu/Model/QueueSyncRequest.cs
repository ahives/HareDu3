namespace HareDu.Model
{
    using System.Text.Json.Serialization;
    using Serialization;

    public record QueueSyncRequest
    {
        [JsonPropertyName("action")]
        [JsonConverter(typeof(QueueSyncActionEnumConverter))]
        public QueueSyncAction Action { get; init; }
    }
}