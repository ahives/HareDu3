namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record ServerHealthInfo
    {
        [JsonPropertyName("status")]
        public string Status { get; init; }
        
        [JsonPropertyName("reason")]
        public string Reason { get; init; }
    }
}