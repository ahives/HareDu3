namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents an authentication mechanism used for securing access.
/// </summary>
public record AuthenticationMechanism
{
    /// <summary>
    /// Gets the name of the authentication mechanism.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// Gets the description of the authentication mechanism.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; init; }

    /// <summary>
    /// Indicates whether the authentication mechanism is enabled.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool IsEnabled { get; init; }
}