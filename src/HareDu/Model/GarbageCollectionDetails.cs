namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents detailed information regarding garbage collection for RabbitMQ processes such as connections, queues, or channels.
/// </summary>
public record GarbageCollectionDetails
{
    /// <summary>
    /// Gets the total count of minor garbage collections that have occurred.
    /// </summary>
    [JsonPropertyName("minor_gcs")]
    public long MinorGarbageCollection { get; init; }

    /// <summary>
    /// Represents the number of garbage collection cycles after which a full sweep will be performed.
    /// </summary>
    [JsonPropertyName("fullsweep_after")]
    public long FullSweepAfter { get; init; }

    /// <summary>
    /// Gets the minimum heap size in bytes for the garbage collection process.
    /// </summary>
    [JsonPropertyName("min_heap_size")]
    public long MinimumHeapSize { get; init; }

    /// <summary>
    /// Gets the minimum size of the binary virtual heap, which determines the threshold for garbage collection of binaries.
    /// </summary>
    [JsonPropertyName("min_bin_vheap_size")]
    public long MinimumBinaryVirtualHeapSize { get; init; }

    /// <summary>
    /// Gets the maximum heap size allocated during garbage collection.
    /// </summary>
    [JsonPropertyName("max_heap_size")]
    public long MaximumHeapSize { get; init; }
}