namespace HareDu.Diagnostics.Probes;

using System.Collections.Generic;
using Core.Extensions;
using KnowledgeBase;
using Model;
using Snapshotting.Model;

public class UnlimitedPrefetchCountProbe :
    BaseDiagnosticProbe<ChannelSnapshot>,
    DiagnosticProbe
{
    public override ProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "Unlimited Prefetch Count Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Channel;
    public override ProbeCategory Category => ProbeCategory.Throughput;
    public bool HasExecuted { get; set; }

    public UnlimitedPrefetchCountProbe(IKnowledgeBaseProvider kb)
        : base(kb)
    {
    }

    public ProbeResult Execute<T>(T snapshot) => base.Execute(snapshot as ChannelSnapshot);

    protected override ProbeResult GetProbeReadout(ChannelSnapshot data)
    {
        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "PrefetchCount", PropertyValue = data.PrefetchCount.ToString()}
        };

        ProbeResult result;
        
        if (data.PrefetchCount == 0)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Warning, out var article);
            
            result = Probe.Warning(data.ConnectionIdentifier, data.Identifier, Metadata,
                ComponentType, probeData, article);
        }
        else
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Inconclusive, out var article);
            
            result = Probe.Inconclusive(data.ConnectionIdentifier, data.Identifier, Metadata,
                ComponentType, probeData, article);
        }

        NotifyObservers(result);
        
        HasExecuted = true;

        return result;
    }
}