namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents detailed information about RabbitMQ user limits.
/// </summary>
public record UserLimitsInfo
{
    /// <summary>
    /// Gets the identifier for the user associated with the specific limits.
    /// Typically represents the name or unique username of the user.
    /// </summary>
    [JsonPropertyName("user")]
    public string User { get; init; }

    /// <summary>
    /// Gets the collection of key-value pairs representing specific limits associated with the user.
    /// The key is a string denoting the type or name of the limit, and the value is an unsigned long representing the limit value.
    /// </summary>
    [JsonPropertyName("value")]
    public IDictionary<string, ulong> Value { get; init; }
}
