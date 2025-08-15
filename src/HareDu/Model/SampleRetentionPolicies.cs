namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the sample retention policies for various data types during RabbitMQ operations.
/// </summary>
public record SampleRetentionPolicies
{
    /// <summary>
    /// Represents the global retention policies for a specific feature or entity.
    /// It contains a collection of values indicating predetermined retention durations or settings.
    /// </summary>
    [JsonPropertyName("global")]
    public IList<ulong> Global { get; init; }

    /// <summary>
    /// Represents basic retention policies defining a collection of values,
    /// typically used to specify retention durations or thresholds for a particular entity or feature.
    /// </summary>
    [JsonPropertyName("basic")]
    public IList<ulong> Basic { get; init; }

    /// <summary>
    /// Represents a collection of detailed retention policies as numeric values.
    /// It specifies precise retention periods or configurations for a given entity.
    /// </summary>
    [JsonPropertyName("detailed")]
    public IList<ulong> Detailed { get; init; }
}