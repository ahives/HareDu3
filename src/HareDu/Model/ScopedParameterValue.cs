namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents the value of a scoped parameter in RabbitMQ, including its specific properties such as pattern,
/// definition, priority, and application scope.
/// </summary>
public record ScopedParameterValue
{
    /// <summary>
    /// Gets the pattern used to match the parameter value.
    /// </summary>
    /// <remarks>
    /// This property defines the regular expression or matching criterion that determines how the parameter is applied.
    /// </remarks>
    [JsonPropertyName("pattern")]
    public string Pattern { get; init; }

    /// <summary>
    /// Gets the definition associated with the scoped parameter value.
    /// </summary>
    /// <remarks>
    /// This property specifies the configuration or rule that is applied based on the given pattern.
    /// </remarks>
    [JsonPropertyName("definition")]
    public string Definition { get; init; }

    /// <summary>
    /// Gets the priority level associated with the scoped parameter value.
    /// </summary>
    /// <remarks>
    /// This property determines the precedence or importance given to the parameter's rule
    /// when multiple patterns or definitions are evaluated.
    /// </remarks>
    [JsonPropertyName("priority")]
    public int Priority { get; init; }

    /// <summary>
    /// Gets the target scope or entity where the parameter value should be applied.
    /// </summary>
    /// <remarks>
    /// This property specifies the context or object type (such as users, queues, etc.)
    /// to which the parameter's definition and pattern are relevant.
    /// </remarks>
    [JsonPropertyName("apply-to")]
    public string ApplyTo { get; init; }
}