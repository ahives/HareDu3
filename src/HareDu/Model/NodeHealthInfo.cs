namespace HareDu.Model;

using System.Text.Json.Serialization;

public record NodeHealthInfo
{
    [JsonPropertyName("status")]
    public NodeStatus Status { get; init; }

    [JsonPropertyName("reason")]
    public long Reason { get; init; }
}