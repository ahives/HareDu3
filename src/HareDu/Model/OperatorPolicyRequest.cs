namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public record OperatorPolicyRequest
{
    [JsonPropertyName("pattern")]
    public string Pattern { get; init; }

    [JsonPropertyName("definition")]
    public IDictionary<string, ulong> Arguments { get; init; }

    [JsonPropertyName("priority")]
    public int Priority { get; init; }

    [JsonPropertyName("apply-to")]
    public OperatorPolicyAppliedTo ApplyTo { get; init; }
}