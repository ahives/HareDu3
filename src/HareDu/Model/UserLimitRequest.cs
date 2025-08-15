namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a request to define or update a limit for a user in the HareDu system.
/// </summary>
public record UserLimitRequest
{
    /// <summary>
    /// Specifies the value to be assigned when setting a user limit.
    /// </summary>
    /// <remarks>
    /// The value represents the required limit and is expected to be a non-negative integer.
    /// It is ignored during serialization when the default value of 0 is assigned.
    /// </remarks>
    [JsonPropertyName("value")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ulong Value { get; init; }
}