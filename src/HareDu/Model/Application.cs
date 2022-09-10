namespace HareDu.Model;

using System.Text.Json.Serialization;

public record Application
{
    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("description")]
    public string Description { get; init; }

    [JsonPropertyName("version")]
    public string Version { get; init; }
}