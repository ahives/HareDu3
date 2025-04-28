namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public record VirtualHostMetadata
{
    [JsonPropertyName("description")]
    public string Description { get; init; }

    [JsonPropertyName("tags")]
    public List<string> Tags { get; init; }
}