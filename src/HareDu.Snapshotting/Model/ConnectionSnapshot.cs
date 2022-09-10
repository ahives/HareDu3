namespace HareDu.Snapshotting.Model;

using System.Collections.Generic;
using HareDu.Model;

public record ConnectionSnapshot :
    Snapshot
{
    public string Identifier { get; init; }
        
    public NetworkTrafficSnapshot NetworkTraffic { get; init; }
        
    public ulong OpenChannelsLimit { get; init; }
        
    public string NodeIdentifier { get; init; }
        
    public string VirtualHost { get; init; }
        
    public BrokerConnectionState State { get; init; }

    public IReadOnlyList<ChannelSnapshot> Channels { get; init; }
}