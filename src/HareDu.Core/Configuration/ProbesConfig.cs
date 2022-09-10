namespace HareDu.Core.Configuration;

public record ProbesConfig
{
    public uint HighConnectionClosureRateThreshold { get; init; }
        
    public uint HighConnectionCreationRateThreshold { get; init; }
        
    public uint QueueHighFlowThreshold { get; init; }
        
    public uint QueueLowFlowThreshold { get; init; }
        
    public decimal MessageRedeliveryThresholdCoefficient { get; init; }

    public decimal SocketUsageThresholdCoefficient { get; init; }
        
    public decimal RuntimeProcessUsageThresholdCoefficient { get; init; }
        
    public decimal FileDescriptorUsageThresholdCoefficient { get; init; }
        
    public decimal ConsumerUtilizationThreshold { get; init; }
}