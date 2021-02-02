namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record QueueConsumerDetails
    {
        [JsonPropertyName("vhost")]
        public string VirtualHost { get; init; }
        
        [JsonPropertyName("name")]
        public string Name { get; init; }
    }
}