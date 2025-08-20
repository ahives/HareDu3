namespace HareDu.Diagnostics.Probes;

using System.Collections.Generic;
using Core.Extensions;
using KnowledgeBase;
using Model;
using Snapshotting.Model;

public class ChannelThrottlingProbe :
    BaseDiagnosticProbe<ChannelSnapshot>,
    DiagnosticProbe
{
    public override ProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "Channel Throttling Probe",
            Description = "Monitors connections to the RabbitMQ broker to determine whether channels are being throttled."
        };
    public override ComponentType ComponentType => ComponentType.Channel;
    public override ProbeCategory Category => ProbeCategory.Throughput;
    public bool HasExecuted { get; set; }

    public ChannelThrottlingProbe(IKnowledgeBaseProvider kb)
        : base(kb)
    {
    }

    public ProbeResult Execute<T>(T snapshot) => base.Execute(snapshot as ChannelSnapshot);

    protected override ProbeResult GetProbeReadout(ChannelSnapshot data)
    {
        ProbeResult result;

        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "UnacknowledgedMessages", PropertyValue = data.UnacknowledgedMessages.ToString()},
            new () {PropertyName = "PrefetchCount", PropertyValue = data.PrefetchCount.ToString()}
        };
            
        if (data.UnacknowledgedMessages > data.PrefetchCount)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Unhealthy, out var article);
            
            result = Probe.Unhealthy(data.ConnectionIdentifier, data.Identifier, Metadata,
                ComponentType, probeData, article);
        }
        else
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Healthy, out var article);
            
            result = Probe.Healthy(data.ConnectionIdentifier, data.Identifier, Metadata,
                ComponentType, probeData, article);
        }

        NotifyObservers(result);
        
        HasExecuted = true;

        return result;
    }
}