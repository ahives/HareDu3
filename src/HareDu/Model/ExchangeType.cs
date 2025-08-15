namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents the configuration of an exchange type.
/// Provides details about the exchange type's name, description, and whether it is enabled.
/// </summary>
public record ExchangeType
{
    /// <summary>
    /// Gets the name of the exchange type.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// Gets the description of the exchange type.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; init; }

    /// <summary>
    /// Indicates whether the exchange type is enabled.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool IsEnabled { get; init; }
}