namespace HareDu.Diagnostics;

/// <summary>
/// Represents metadata for a diagnostic scanner, providing identification information
/// and other relevant details about the scanner's characteristics.
/// </summary>
public record ScannerMetadata
{
    /// <summary>
    /// Represents the unique identifier for the diagnostic scanner.
    /// This property is primarily used to associate specific diagnostic
    /// results with the corresponding scanner.
    /// </summary>
    public string Identifier { get; init; }
}