namespace HareDu.Snapshotting.Model;

using System.Collections.Generic;

public record BrokerConnectivitySnapshot :
    Snapshot
{
    public string BrokerVersion { get; init; }
        
    public string ClusterName { get; init; }
        
    public ChurnMetrics ChannelsClosed { get; init; }

    public ChurnMetrics ChannelsCreated { get; init; }

    public ChurnMetrics ConnectionsClosed { get; init; }

    public ChurnMetrics ConnectionsCreated { get; init; }
        
    public IReadOnlyList<ConnectionSnapshot> Connections { get; init; }
}