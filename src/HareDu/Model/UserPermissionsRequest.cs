namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a request to define user permissions on a RabbitMQ virtual host. Permissions control what resources
/// a user can access and how they can interact with those resources.
/// </summary>
public record UserPermissionsRequest
{
    /// <summary>
    /// Gets the pattern that specifies permissions for configuring resources.
    /// </summary>
    [JsonPropertyName("configure")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Configure { get; init; }

    /// <summary>
    /// Gets the pattern that defines permissions for writing resources.
    /// </summary>
    [JsonPropertyName("write")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Write { get; init; }

    /// <summary>
    /// Gets the pattern that defines permissions for reading resources.
    /// </summary>
    [JsonPropertyName("read")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Read { get; init; }
}