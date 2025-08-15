namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents the health status of a node, providing information regarding its operational state and any issues encountered.
/// </summary>
public record NodeHealthInfo
{
    /// <summary>
    /// Indicates the current operational state or health status of the node.
    /// </summary>
    [JsonPropertyName("status")]
    public NodeStatus Status { get; init; }

    /// <summary>
    /// Represents an identifier or code describing the underlying reason for the node health status.
    /// </summary>
    [JsonPropertyName("reason")]
    public long Reason { get; init; }
}