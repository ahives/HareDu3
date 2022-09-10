namespace HareDu.Model;

using System.Text.Json.Serialization;

public record ShovelRequest
{
    [JsonPropertyName("value")]
    public ShovelRequestParams Value { get; init; }
}