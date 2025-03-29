namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public record UserLimitsInfo
{
    [JsonPropertyName("user")]
    public string User { get; init; }

    [JsonPropertyName("value")]
    public IDictionary<string, ulong> Value { get; init; }
}
