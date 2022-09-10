namespace HareDu.Diagnostics.Probes;

public record DiagnosticProbeMetadata
{
    public string Id { get; init; }
        
    public string Name { get; init; }
        
    public string Description { get; init; }
}