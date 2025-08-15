namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a record containing information about total memory usage in a system or application.
/// </summary>
public record TotalMemoryInfo
{
    /// <summary>
    /// Represents the total memory used by the Erlang virtual machine in the system.
    /// </summary>
    [JsonPropertyName("erlang")]
    public long Erlang { get; init; }

    /// <summary>
    /// Represents the memory management strategy used by the system.
    /// </summary>
    [JsonPropertyName("rss")]
    public long Strategy { get; init; }

    /// <summary>
    /// Represents the total amount of memory allocated in the system.
    /// </summary>
    [JsonPropertyName("allocated")]
    public long Allocated { get; init; }
}