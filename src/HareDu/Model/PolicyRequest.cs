namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public record PolicyRequest
{
    [JsonPropertyName("pattern")]
    public string Pattern { get; init; }

    [JsonPropertyName("definition")]
    public IDictionary<string, string> Arguments { get; init; }

    [JsonPropertyName("priority")]
    public int Priority { get; init; }

    [JsonPropertyName("apply-to")]
    public PolicyAppliedTo ApplyTo { get; init; }
}