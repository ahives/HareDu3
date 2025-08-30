namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents overview information related to a RabbitMQ virtual host.
/// </summary>
public record VirtualHostOverviewInfo
{
    /// <summary>
    /// Represents the name of the virtual host, used to uniquely identify it within the broker environment.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// Provides a textual description of the virtual host, often used to give additional context or information
    /// about its purpose or characteristics.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; init; }

    /// <summary>
    /// Provides metadata associated with the virtual host, including attributes such as description and tags.
    /// Represents additional contextual information that characterizes the virtual host.
    /// </summary>
    [JsonPropertyName("metadata")]
    public VirtualHostMetadata Metadata { get; init; }

    /// <summary>
    /// Represents a collection of tags associated with a virtual host.
    /// These tags are used for categorization or providing additional metadata.
    /// </summary>
    [JsonPropertyName("tags")]
    public IList<string> Tags { get; init; }
}