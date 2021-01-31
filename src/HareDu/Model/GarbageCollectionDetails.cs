namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record GarbageCollectionDetails
    {
        [JsonPropertyName("minor_gcs")]
        public long MinorGarbageCollection { get; init; }

        [JsonPropertyName("fullsweep_after")]
        public long FullSweepAfter { get; init; }

        [JsonPropertyName("min_heap_size")]
        public long MinimumHeapSize { get; init; }

        [JsonPropertyName("min_bin_vheap_size")]
        public long MinimumBinaryVirtualHeapSize { get; init; }

        [JsonPropertyName("max_heap_size")]
        public long MaximumHeapSize { get; init; }
    }
}