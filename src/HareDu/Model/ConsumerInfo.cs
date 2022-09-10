namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public record ConsumerInfo
{
    [JsonPropertyName("prefetch_count")]
    public ulong PreFetchCount { get; init; }
        
    [JsonPropertyName("arguments")]
    public IDictionary<string, object> Arguments { get; init; }
        
    [JsonPropertyName("ack_required")]
    public bool AcknowledgementRequired { get; init; }
        
    [JsonPropertyName("exclusive")]
    public bool Exclusive { get; init; }
        
    [JsonPropertyName("consumer_tag")]
    public string ConsumerTag { get; init; }
        
    [JsonPropertyName("channel_details")]
    public ChannelDetails ChannelDetails { get; init; }
        
    [JsonPropertyName("queue")]
    public QueueConsumerDetails QueueConsumerDetails { get; init; }
}