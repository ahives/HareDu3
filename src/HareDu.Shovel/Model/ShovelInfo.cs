namespace HareDu.Shovel.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents information about a RabbitMQ shovel, including its details, virtual host, component, and name.
/// </summary>
public record ShovelInfo
{
    /// <summary>
    /// Gets the details of the RabbitMQ shovel.
    /// This property represents the configuration and behavior of the shovel,
    /// including source and destination protocols, URIs, queues, and other settings.
    /// </summary>
    [JsonPropertyName("value")]
    public ShovelDetails Details { get; init; }

    /// <summary>
    /// Gets the name of the virtual host associated with the shovel.
    /// This property identifies the RabbitMQ virtual host where the shovel is configured and operates.
    /// </summary>
    [JsonPropertyName("vhost")]
    public string VirtualHost { get; init; }

    /// <summary>
    /// Gets the component associated with the RabbitMQ shovel.
    /// This property identifies the logical grouping or category
    /// that the shovel belongs to within the RabbitMQ environment.
    /// </summary>
    [JsonPropertyName("component")]
    public string Component { get; init; }

    /// <summary>
    /// Gets the name of the RabbitMQ shovel.
    /// Represents the unique identifier for the shovel used to distinguish it
    /// from other shovels within the specified virtual host.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }
}