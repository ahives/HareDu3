namespace HareDu.Model;

using System.Text.Json.Serialization;

public record GlobalParameterInfo
{
    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("value")]
    public object Value { get; init; }
}