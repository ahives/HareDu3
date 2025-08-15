namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents memory usage information for a node within the RabbitMQ cluster.
/// </summary>
public record NodeMemoryUsageInfo
{
    /// <summary>
    /// Represents the memory usage details of a node, encapsulating various memory consumption metrics within the system.
    /// </summary>
    [JsonPropertyName("memory")]
    public MemoryInfo Memory { get; init; }
}