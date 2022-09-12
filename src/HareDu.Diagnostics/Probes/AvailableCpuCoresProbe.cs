namespace HareDu.Diagnostics.Probes;

using System.Collections.Generic;
using Core.Extensions;
using KnowledgeBase;
using Snapshotting.Model;

public class AvailableCpuCoresProbe :
    BaseDiagnosticProbe<NodeSnapshot>,
    DiagnosticProbe
{
    public override DiagnosticProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "Available CPU Cores Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Node;
    public override ProbeCategory Category => ProbeCategory.Throughput;

    public AvailableCpuCoresProbe(IKnowledgeBaseProvider kb)
        : base(kb)
    {
    }

    public ProbeResult Execute<T>(T snapshot)
    {
        return base.Execute(snapshot as NodeSnapshot);
    }

    protected override ProbeResult GetProbeResult(NodeSnapshot data)
    {
        ProbeResult result;

        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "AvailableCoresDetected", PropertyValue = data.AvailableCoresDetected.ToString()}
        };
            
        if (data.AvailableCoresDetected <= 0)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Unhealthy, out var article);
            
            result = new ProbeResult
            {
                Status = ProbeResultStatus.Unhealthy,
                ParentComponentId = data.ClusterIdentifier,
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
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Healthy, out var article);
            
            result = new ProbeResult
            {
                Status = ProbeResultStatus.Healthy,
                ParentComponentId = data.ClusterIdentifier,
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