namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents information about a user's topic permissions within a RabbitMQ system.
/// </summary>
public record TopicPermissionsInfo
{
    /// <summary>
    /// Represents the name of the user to whom the topic permissions are assigned.
    /// </summary>
    [JsonPropertyName("user")]
    public string User { get; init; }

    /// <summary>
    /// Represents the virtual host to which the topic permissions are assigned.
    /// </summary>
    [JsonPropertyName("vhost")]
    public string VirtualHost { get; init; }

    /// <summary>
    /// Represents the exchange to which the topic permissions apply in a specific virtual host.
    /// </summary>
    [JsonPropertyName("exchange")]
    public string Exchange { get; init; }

    /// <summary>
    /// Represents the write permissions for a specific user on a given virtual host and exchange.
    /// </summary>
    [JsonPropertyName("write")]
    public string Write { get; init; }

    /// <summary>
    /// Represents the read permissions associated with a specific exchange in a virtual host.
    /// </summary>
    [JsonPropertyName("read")]
    public string Read { get; init; }
}