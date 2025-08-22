namespace HareDu.Diagnostics.Model;

/// <summary>
/// Represents diagnostic probe data with specific property details.
/// </summary>
public record ProbeData
{
    /// <summary>
    /// Gets the name of the property associated with the probe data.
    /// </summary>
    /// <remarks>
    /// This property represents a key or identifier that corresponds to a specific diagnostic measure
    /// or metadata collected by a diagnostic probe.
    /// </remarks>
    public string PropertyName { get; init; }

    /// <summary>
    /// Gets the value associated with a specific property in the probe data.
    /// </summary>
    /// <remarks>
    /// This property represents the corresponding value for a key or identifier encapsulated in the diagnostic information.
    /// </remarks>
    public string PropertyValue { get; init; }
}