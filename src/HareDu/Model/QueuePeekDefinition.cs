namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record QueuePeekDefinition
    {
        [JsonPropertyName("count")]
        public uint Take { get; init; }
        
        [JsonPropertyName("encoding")]
        public string Encoding { get; init; }
        
        [JsonPropertyName("truncate")]
        public ulong TruncateMessageThreshold { get; init; }
        
        [JsonPropertyName("ackmode")]
        public string RequeueMode { get; init; }
    }
}