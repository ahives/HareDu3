namespace HareDu.Snapshotting.Model;

public record PersistenceDetails
{
    public ulong Total { get; init; }
        
    public decimal Rate { get; init; }
}