namespace HareDu.Diagnostics.Scanners
{
    public record DiagnosticScannerMetadata
    {
        /// <summary>
        /// Identifier of the diagnostic scanner.
        /// </summary>
        public string Identifier { get; init; }
    }
}