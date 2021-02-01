namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record GarbageCollectionMetrics
    {
        [JsonPropertyName("rate")]
        public decimal Rate { get; init; }
    }
}