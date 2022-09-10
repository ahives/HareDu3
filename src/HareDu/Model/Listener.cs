namespace HareDu.Model;

using System.Text.Json.Serialization;
using Serialization.Converters;

public record Listener
{
    [JsonPropertyName("node")]
    public string Node { get; init; }
        
    [JsonPropertyName("protocol")]
    public string Protocol { get; init; }
        
    [JsonPropertyName("ip_address")]
    public string IPAddress { get; init; }
        
    [JsonPropertyName("port")]
    public string Port { get; init; }
        
    [JsonPropertyName("socket_opts")]
    [JsonConverter(typeof(InconsistentObjectDataConverter))]
    public SocketOptions SocketOptions { get; init; }
}