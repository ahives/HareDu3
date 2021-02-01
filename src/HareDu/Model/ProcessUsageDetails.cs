namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record ProcessUsageDetails
    {
        [JsonPropertyName("rate")]
        public decimal Rate { get; init; }
    }
}