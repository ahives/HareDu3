namespace HareDu.Snapshotting.Model
{
    using System.Collections.Generic;

    public record NodeSnapshot :
        Snapshot
    {
        public OperatingSystemSnapshot OS { get; init; }

        public string RatesMode { get; init; }

        public long Uptime { get; init; }
        
        public long InterNodeHeartbeat { get; init; }

        public string Identifier { get; init; }
        
        public string ClusterIdentifier { get; init; }

        public string Type { get; init; }

        public bool IsRunning { get; init; }
        
        public ulong AvailableCoresDetected { get; init; }

        public IReadOnlyList<string> NetworkPartitions { get; init; }
        
        public DiskSnapshot Disk { get; init; }
        
        public BrokerRuntimeSnapshot Runtime { get; init; }
        
        public MemorySnapshot Memory { get; init; }

        public ContextSwitchingDetails ContextSwitching { get; init; }
    }
}