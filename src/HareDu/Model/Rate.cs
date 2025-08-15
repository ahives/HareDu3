namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents the rate-related details for specific operations or metrics in a message broker.
/// </summary>
public class Rate
{
    /// <summary>
    /// Gets the rate value associated with a specific measurement or metric.
    /// </summary>
    [JsonPropertyName("rate")]
    public decimal Value { get; init; }
}