namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a request to create, update, or manage topic permissions within a RabbitMQ environment.
/// This model is typically used to define the permissions for publishing to and consuming from specific
/// exchanges and patterns.
/// </summary>
public record TopicPermissionsRequest
{
    /// <summary>
    /// Represents the name of the exchange to which the topic permissions are applied.
    /// </summary>
    [JsonPropertyName("exchange")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Exchange { get; init; }

    /// <summary>
    /// Specifies the write permission pattern for the topic.
    /// This pattern determines which routing keys the user or application is allowed to write to for the given exchange.
    /// </summary>
    [JsonPropertyName("write")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Write { get; init; }

    /// <summary>
    /// Represents the pattern that defines the read permissions for a topic in the associated exchange.
    /// </summary>
    [JsonPropertyName("read")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Read { get; init; }
}