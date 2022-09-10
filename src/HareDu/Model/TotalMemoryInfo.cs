namespace HareDu.Model;

using System.Text.Json.Serialization;

public record TotalMemoryInfo
{
    [JsonPropertyName("erlang")]
    public long Erlang { get; init; }
        
    [JsonPropertyName("rss")]
    public long Strategy { get; init; }
        
    [JsonPropertyName("allocated")]
    public long Allocated { get; init; }
}