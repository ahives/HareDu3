namespace HareDu.Snapshotting.Model
{
    using System.Collections.Generic;

    public record BrokerQueuesSnapshot :
        Snapshot
    {
        public string ClusterName { get; init; }
        
        public BrokerQueueChurnMetrics Churn { get; init; }
        
        public IReadOnlyList<QueueSnapshot> Queues { get; init; }
    }
}