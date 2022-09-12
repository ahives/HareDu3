namespace HareDu.Diagnostics.Probes;

using System.Collections.Generic;
using Core.Extensions;
using KnowledgeBase;
using Snapshotting.Model;

public class UnlimitedPrefetchCountProbe :
    BaseDiagnosticProbe<ChannelSnapshot>,
    DiagnosticProbe
{
    public override DiagnosticProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "Unlimited Prefetch Count Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Channel;
    public override ProbeCategory Category => ProbeCategory.Throughput;

    public UnlimitedPrefetchCountProbe(IKnowledgeBaseProvider kb)
        : base(kb)
    {
    }

    public ProbeResult Execute<T>(T snapshot)
    {
        return base.Execute(snapshot as ChannelSnapshot);
    }

    protected override ProbeResult GetProbeResult(ChannelSnapshot data)
    {
        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "PrefetchCount", PropertyValue = data.PrefetchCount.ToString()}
        };

        ProbeResult result;
        
        if (data.PrefetchCount == 0)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Warning, out var article);
            
            result = new ProbeResult
            {
                Status = ProbeResultStatus.Warning,
                ParentComponentId = data.ConnectionIdentifier,
                ComponentId = data.Identifier,
                Id = Metadata.Id,
                Name = Metadata.Name,
                ComponentType = ComponentType,
                Data = probeData,
                KB = article
            };
        }
        else
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Inconclusive, out var article);
            
            result = new ProbeResult
            {
                Status = ProbeResultStatus.Inconclusive,
                ParentComponentId = data.ConnectionIdentifier,
                ComponentId = data.Identifier,
                Id = Metadata.Id,
                Name = Metadata.Name,
                ComponentType = ComponentType,
                Data = probeData,
                KB = article
            };
        }

        NotifyObservers(result);
                
        return result;
    }
}