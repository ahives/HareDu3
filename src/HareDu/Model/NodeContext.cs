namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public record NodeContext
{
    [JsonPropertyName("description")]
    public string Description { get; init; }

    [JsonPropertyName("path")]
    public string Path { get; init; }
        
    [JsonPropertyName("port")]
    public string Port { get; init; }
        
    [JsonPropertyName("cowboy_opts")]
    public string ServerOptions { get; init; }
        
    [JsonPropertyName("ssl_opts")]
    public IList<string> SslOptions { get; init; }
        
    [JsonPropertyName("node")]
    public string Node { get; init; }
}