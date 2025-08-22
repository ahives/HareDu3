namespace HareDu.Diagnostics.Model;

/// <summary>
/// Represents metadata information associated with a diagnostic probe.
/// </summary>
public record ProbeMetadata
{
    /// <summary>
    /// Gets the unique identifier of the probe, which serves as a key for associating diagnostic information
    /// specific to the probe and retrieving related metadata or knowledge base definitions.
    /// </summary>
    public string Id { get; init; }

    /// <summary>
    /// Gets the name of the probe, which is intended to provide a textual representation
    /// of the purpose or type of diagnostic probe being represented.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Provides a textual description of the diagnostic probe used to convey its purpose
    /// or operational details in a human-readable format.
    /// </summary>
    public string Description { get; init; }
}