namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a request to create or manage a global parameter.
/// </summary>
public record GlobalParameterRequest
{
    /// <summary>
    /// Represents the name of the global parameter to be created or updated.
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Name { get; init; }

    /// <summary>
    /// Represents the value associated with the global parameter to be created or updated.
    /// </summary>
    [JsonPropertyName("value")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public object Value { get; init; }
}