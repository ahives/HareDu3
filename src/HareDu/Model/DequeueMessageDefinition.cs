namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record DequeueMessageDefinition
    {
        [JsonPropertyName("count")]
        public uint Take { get; init; }
        
        [JsonPropertyName("encoding")]
        public string Encoding { get; init; }
        
        [JsonPropertyName("truncate")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ulong TruncateMessageThreshold { get; init; }
        
        [JsonPropertyName("ackmode")]
        public string RequeueMode { get; init; }
    }
}