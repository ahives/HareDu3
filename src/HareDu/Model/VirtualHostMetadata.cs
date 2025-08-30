namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents metadata associated with a virtual host.
/// </summary>
public record VirtualHostMetadata
{
    /// <summary>
    /// Provides a description of the virtual host.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; init; }

    /// <summary>
    /// Represents a collection of tags associated with a virtual host.
    /// </summary>
    [JsonPropertyName("tags")]
    public List<string> Tags { get; init; }

    /// <summary>
    /// Indicates the default type of queue associated with the virtual host.
    /// </summary>
    [JsonPropertyName("default_queue_type")]
    public DefaultQueueType DefaultQueueType { get; init; }
}