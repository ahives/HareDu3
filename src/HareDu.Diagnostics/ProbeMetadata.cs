namespace HareDu.Diagnostics;

public record ProbeMetadata
{
    public string Id { get; init; }
        
    public string Name { get; init; }
        
    public string Description { get; init; }
}