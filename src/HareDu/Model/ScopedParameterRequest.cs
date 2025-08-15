namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a request to manage a scoped parameter within a specified virtual host and component.
/// </summary>
/// <typeparam name="T">
/// Type of the parameter value.
/// </typeparam>
public record ScopedParameterRequest<T>
{
    /// <summary>
    /// Specifies the name of the virtual host associated with the scoped parameter.
    /// This property identifies the context in which the scoped parameter will be applied
    /// within the broker environment.
    /// </summary>
    [JsonPropertyName("vhost")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string VirtualHost { get; init; }

    /// <summary>
    /// Represents the component to which a scoped parameter is associated. This property specifies
    /// the logical grouping or feature within the system where the parameter will be used.
    /// </summary>
    [JsonPropertyName("component")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Component { get; init; }

    /// <summary>
    /// Specifies the name of the scoped parameter. This property identifies the parameter within a given scope,
    /// and it is used as a key in parameter-related operations.
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string ParameterName { get; init; }

    /// <summary>
    /// Represents the value assigned to a scoped parameter. This property can hold various types of data,
    /// depending on the specific parameter being used.
    /// </summary>
    /// <typeparam name="T">The type of the parameter value.</typeparam>
    [JsonPropertyName("value")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T ParameterValue { get; init; }
}