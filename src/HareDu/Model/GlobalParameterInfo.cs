namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents the information of a global parameter in the context of a RabbitMQ system.
/// </summary>
public record GlobalParameterInfo
{
    /// <summary>
    /// Represents the name of the global parameter in the context of RabbitMQ.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// Represents the value associated with the global parameter in the context of RabbitMQ.
    /// </summary>
    [JsonPropertyName("value")]
    public object Value { get; init; }
}