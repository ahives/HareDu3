namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record VirtualHostLimitsDefinition
    {
        [JsonPropertyName("max-connections")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ulong MaxConnectionLimit { get; init; }
        
        [JsonPropertyName("max-queues")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ulong MaxQueueLimit { get; init; }
    }
}