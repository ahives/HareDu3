namespace HareDu.Model;

using System.Text.Json.Serialization;

public record VirtualHostLimitsRequest
{
    [JsonPropertyName("value")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ulong Value { get; init; }
}