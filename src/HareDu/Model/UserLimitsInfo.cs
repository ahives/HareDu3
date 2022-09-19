namespace HareDu.Model;

using System.Text.Json.Serialization;

public record UserLimitsInfo
{
    [JsonPropertyName("value")]
    public int Value { get; init; }
}