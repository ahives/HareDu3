namespace HareDu;

using System.Text.Json.Serialization;

public record VirtualHostPermissionInfo
{
    [JsonPropertyName("vhost")]
    public string VirtualHost { get; init; }

    [JsonPropertyName("user")]
    public string User { get; init; }

    [JsonPropertyName("configure")]
    public string Configure { get; init; }

    [JsonPropertyName("write")]
    public string Write { get; init; }

    [JsonPropertyName("read")]
    public string Read { get; init; }
}