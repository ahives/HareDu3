namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public record ScopedParameterInfo
{
    [JsonPropertyName("vhost")]
    public string VirtualHost { get; init; }

    [JsonPropertyName("component")]
    public string Component { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("value")]
    public IDictionary<string, object> Value { get; init; }
}