namespace HareDu.Snapshotting.Model;

public record QueueInternals
{
    public Reductions Reductions { get; init; }
        
    public long TargetCountOfMessagesAllowedInRAM { get; init; }
        
    public decimal ConsumerUtilization { get; init; }

    public long Q1 { get; init; }
        
    public long Q2 { get; init; }
        
    public long Q3 { get; init; }
        
    public long Q4 { get; init; }
        
    public decimal AvgIngressRate { get; init; }
        
    public decimal AvgEgressRate { get; init; }
        
    public decimal AvgAcknowledgementIngressRate { get; init; }
        
    public decimal AvgAcknowledgementEgressRate { get; init; }
}