namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public record VirtualHostLimitsInfo
{
    [JsonPropertyName("vhost")]
    public string VirtualHostName { get; init; }
        
    [JsonPropertyName("value")]
    public IDictionary<string, ulong> Limits { get; init; }
}