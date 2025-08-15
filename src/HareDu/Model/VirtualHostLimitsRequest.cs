namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents the necessary detail required to define or update virtual host limits in the system.
/// </summary>
public record VirtualHostLimitsRequest
{
    /// <summary>
    /// Represents the numerical value associated with the virtual host limit configuration.
    /// </summary>
    [JsonPropertyName("value")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ulong Value { get; init; }
}