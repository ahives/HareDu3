namespace HareDu.Diagnostics.Probes;

using System.Collections.Generic;
using System.Linq;
using Core.Extensions;
using KnowledgeBase;
using Snapshotting.Model;

public class NetworkPartitionProbe :
    BaseDiagnosticProbe<NodeSnapshot>,
    DiagnosticProbe
{
    public override DiagnosticProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "Network Partition Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Node;
    public override ProbeCategory Category => ProbeCategory.Connectivity;

    public NetworkPartitionProbe(IKnowledgeBaseProvider kb)
        : base(kb)
    {
    }

    public ProbeResult Execute<T>(T snapshot) => base.Execute(snapshot as NodeSnapshot);

    protected override ProbeResult GetProbeResult(NodeSnapshot data)
    {
        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "NetworkPartitions", PropertyValue = data.NetworkPartitions.ToString()}
        };

        ProbeResult result;
        
        if (data.NetworkPartitions.Any())
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