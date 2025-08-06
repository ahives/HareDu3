namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents permission details for a specific user on a virtual host in RabbitMQ.
/// </summary>
public record VirtualHostPermissionInfo
{
    /// <summary>
    /// Gets the name of the virtual host associated with the permissions.
    /// </summary>
    [JsonPropertyName("vhost")]
    public string VirtualHost { get; init; }

    /// <summary>
    /// Gets the name of the user associated with the permissions.
    /// </summary>
    [JsonPropertyName("user")]
    public string User { get; init; }

    /// <summary>
    /// Gets the permissions that define which resources can be configured in the virtual host.
    /// </summary>
    [JsonPropertyName("configure")]
    public string Configure { get; init; }

    /// <summary>
    /// Gets the expression defining write access permissions for the virtual host.
    /// </summary>
    [JsonPropertyName("write")]
    public string Write { get; init; }

    /// <summary>
    /// Gets the permissions granted for reading from the virtual host.
    /// </summary>
    [JsonPropertyName("read")]
    public string Read { get; init; }
}