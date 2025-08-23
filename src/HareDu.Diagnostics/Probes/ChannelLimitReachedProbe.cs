namespace HareDu.Diagnostics.Probes;

using System;
using System.Collections.Generic;
using KnowledgeBase;
using Model;
using Snapshotting.Model;

public class ChannelLimitReachedProbe :
    BaseDiagnosticProbe<ConnectionSnapshot>,
    DiagnosticProbe
{
    public override ProbeMetadata Metadata =>
        new()
        {
            Id = GetType().FullName,
            Name = "Channel Limit Reached Probe",
            Description = "Measures actual number of channels to the defined limit on connection"
        };
    public override ComponentType ComponentType => ComponentType.Connection;
    public override ProbeCategory Category => ProbeCategory.Throughput;
    public bool HasExecuted { get; set; }

    public ChannelLimitReachedProbe(IKnowledgeBaseProvider kb)
        : base(kb)
    {
    }

    public ProbeResult Execute<T>(T snapshot) => base.Execute(snapshot as ConnectionSnapshot);

    protected override ProbeResult GetProbeReadout(ConnectionSnapshot data)
    {
        ProbeResult result;

        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "Channels.Count", PropertyValue = data.Channels.Count.ToString()},
            new () {PropertyName = "OpenChannelLimit", PropertyValue = data.OpenChannelsLimit.ToString()}
        };
            
        if (Convert.ToUInt64(data.Channels.Count) >= data.OpenChannelsLimit)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Unhealthy, out var article);
            result = Probe.Unhealthy(data.NodeIdentifier, data.Identifier, Metadata,
                ComponentType, probeData, article);
        }
        else
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Healthy, out var article);
            result = Probe.Healthy(data.NodeIdentifier, data.Identifier, Metadata,
                ComponentType, probeData, article);
        }

        NotifyObservers(result);

        HasExecuted = true;

        return result;
    }
}