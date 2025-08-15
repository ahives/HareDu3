namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents an application that includes metadata such as its name, description, and version.
/// </summary>
public record Application
{
    /// <summary>
    /// Gets the name of the application.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// Gets the description of the application.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; init; }

    /// <summary>
    /// Gets the version of the application.
    /// </summary>
    [JsonPropertyName("version")]
    public string Version { get; init; }
}