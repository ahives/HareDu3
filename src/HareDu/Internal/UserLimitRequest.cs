namespace HareDu.Internal;

using System.Text.Json.Serialization;

public record UserLimitRequest
{
    [JsonPropertyName("value")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ulong Value { get; init; }
}