namespace HareDu.Snapshotting.Model
{
    public record BrokerRuntimeSnapshot :
        Snapshot
    {
        public string Identifier { get; init; }
        
        public string ClusterIdentifier { get; init; }
        
        public string Version { get; init; }

        public RuntimeProcessChurnMetrics Processes { get; init; }
        
        public RuntimeDatabase Database { get; init; }
        
        public GarbageCollection GC { get; init; }
    }
}