namespace HareDu.Model;

using System.Text.Json.Serialization;

public class Rate
{
    [JsonPropertyName("rate")]
    public decimal Value { get; init; }
}