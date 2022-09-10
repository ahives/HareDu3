namespace HareDu.Model;

using System.Text.Json.Serialization;

public record ConnectionDetails
{
    [JsonPropertyName("peer_host")]
    public string PeerHost { get; init; }

    [JsonPropertyName("peer_port")]
    public long PeerPort { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }
}