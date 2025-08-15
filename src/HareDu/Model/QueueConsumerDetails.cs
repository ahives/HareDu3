namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents details about a consumer's associated queue in the messaging system.
/// </summary>
public record QueueConsumerDetails
{
    /// <summary>
    /// Gets the name of the virtual host associated with the queue consumer.
    /// </summary>
    [JsonPropertyName("vhost")]
    public string VirtualHost { get; init; }

    /// <summary>
    /// Gets the name of the queue consumer.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }
}