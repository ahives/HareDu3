namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents information about permissions assigned to a specific RabbitMQ user within a virtual host.
/// </summary>
public record UserPermissionsInfo
{
    /// <summary>
    /// Represents the username associated with the permissions object.
    /// This property identifies the user for whom specific permissions have been configured,
    /// including configuration, write, and read access within a particular virtual host.
    /// </summary>
    [JsonPropertyName("user")]
    public string User { get; init; }

    /// <summary>
    /// Represents the virtual host associated with the user's permissions.
    /// This property specifies the context within which the user has been granted permissions
    /// for configuration, write, and read operations.
    /// </summary>
    [JsonPropertyName("vhost")]
    public string VirtualHost { get; init; }

    /// <summary>
    /// Gets the user-specific configuration permissions for the associated virtual host.
    /// This property defines the scope of configure access, typically expressed as a regular expression,
    /// which determines what resources a user can configure within the RabbitMQ virtual host.
    /// </summary>
    [JsonPropertyName("configure")]
    public string Configure { get; init; }

    /// <summary>
    /// Gets the user-specific write permission pattern for the associated virtual host.
    /// This property indicates the write permissions granted to the user and is typically a regular expression
    /// that defines the scope of write access within the RabbitMQ virtual host.
    /// </summary>
    [JsonPropertyName("write")]
    public string Write { get; init; }

    /// <summary>
    /// Defines the read access permissions for a specific user within a virtual host.
    /// This property specifies the scope of resources the user is permitted to read within the RabbitMQ system.
    /// </summary>
    [JsonPropertyName("read")]
    public string Read { get; init; }
}