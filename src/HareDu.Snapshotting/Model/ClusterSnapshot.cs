namespace HareDu.Snapshotting.Model;

using System.Collections.Generic;

public record ClusterSnapshot :
    Snapshot
{
    public string BrokerVersion { get; init; }
        
    public string ClusterName { get; init; }
        
    public IReadOnlyList<NodeSnapshot> Nodes { get; init; }
}