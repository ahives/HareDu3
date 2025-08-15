namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a request to create or update a virtual host in the RabbitMQ environment.
/// </summary>
public record VirtualHostRequest
{
    /// <summary>
    /// Indicates whether tracing is enabled for the virtual host.
    /// This property is typically used to configure diagnostic message tracing,
    /// allowing for enhanced observability and monitoring of broker operations.
    /// </summary>
    [JsonPropertyName("tracing")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Tracing { get; init; }

    /// <summary>
    /// Provides a description for the virtual host in the RabbitMQ broker.
    /// This property is typically used to supply additional information or context
    /// about the virtual host, aiding in its identification or documentation.
    /// </summary>
    [JsonPropertyName("description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Description { get; init; }

    /// <summary>
    /// Represents a list of tags associated with the virtual host in the RabbitMQ broker.
    /// These tags are used to categorize or identify the virtual host with custom-defined
    /// labels, providing additional context or metadata.
    /// </summary>
    [JsonPropertyName("tags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Tags { get; init; }
}