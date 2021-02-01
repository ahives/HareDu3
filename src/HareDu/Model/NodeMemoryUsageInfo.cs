namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record NodeMemoryUsageInfo
    {
        [JsonPropertyName("memory")]
        public MemoryInfo Memory { get; init; }
    }
}