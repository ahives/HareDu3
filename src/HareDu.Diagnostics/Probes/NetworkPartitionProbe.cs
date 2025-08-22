namespace HareDu.Diagnostics.Probes;

using System.Collections.Generic;
using System.Linq;
using KnowledgeBase;
using Model;
using Snapshotting.Model;

public class NetworkPartitionProbe :
    BaseDiagnosticProbe<NodeSnapshot>,
    DiagnosticProbe
{
    public override ProbeMetadata Metadata =>
        new()
        {
            Id = GetType().FullName,
            Name = "Network Partition Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Node;
    public override ProbeCategory Category => ProbeCategory.Connectivity;
    public bool HasExecuted { get; set; }

    public NetworkPartitionProbe(IKnowledgeBaseProvider kb)
        : base(kb)
    {
    }

    public ProbeResult Execute<T>(T snapshot) => base.Execute(snapshot as NodeSnapshot);

    protected override ProbeResult GetProbeReadout(NodeSnapshot data)
    {
        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "NetworkPartitions", PropertyValue = data.NetworkPartitions.ToString()}
        };

        ProbeResult result;
        
        if (data.NetworkPartitions.Any())
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Unhealthy, out var article);
            
            result = Probe.Unhealthy(data.ClusterIdentifier, data.Identifier, Metadata,
                ComponentType, probeData, article);
        }
        else
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Healthy, out var article);
            
            result = Probe.Healthy(data.ClusterIdentifier, data.Identifier, Metadata,
                ComponentType, probeData, article);
        }

        NotifyObservers(result);
        
        HasExecuted = true;

        return result;
    }
}