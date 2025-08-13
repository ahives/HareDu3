namespace HareDu.Shovel.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a request to configure a shovel in RabbitMQ.
/// </summary>
public record ShovelRequest
{
    /// <summary>
    /// Represents the parameters associated with the shovel request configuration.
    /// This property contains the detailed settings that define the source and destination
    /// of the shovel operation, including protocol, exchanges, queues, URIs, and other related attributes.
    /// </summary>
    [JsonPropertyName("value")]
    public ShovelRequestParams Value { get; init; }
}