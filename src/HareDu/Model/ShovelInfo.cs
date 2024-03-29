namespace HareDu.Model;

using System;
using System.Text.Json.Serialization;

public record ShovelInfo
{
    [JsonPropertyName("node")]
    public string Node { get; init; }

    [JsonPropertyName("timestamp")]
    public DateTimeOffset Timestamp { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("vhost")]
    public string VirtualHost { get; init; }

    [JsonPropertyName("type")]
    public ShovelType Type { get; init; }

    [JsonPropertyName("state")]
    public ShovelState State { get; init; }
}