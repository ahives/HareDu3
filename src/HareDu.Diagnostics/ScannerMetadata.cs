namespace HareDu.Diagnostics;

public record ScannerMetadata
{
    /// <summary>
    /// Identifier of the diagnostic scanner.
    /// </summary>
    public string Identifier { get; init; }
}