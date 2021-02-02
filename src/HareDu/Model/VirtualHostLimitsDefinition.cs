namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record VirtualHostLimitsDefinition
    {
        [JsonPropertyName("max-connections")]
        public ulong MaxConnectionLimit { get; init; }
        
        [JsonPropertyName("max-queues")]
        public ulong MaxQueueLimit { get; init; }
    }
}