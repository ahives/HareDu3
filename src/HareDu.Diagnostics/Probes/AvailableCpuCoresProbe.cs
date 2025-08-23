namespace HareDu.Diagnostics.Probes;

using System.Collections.Generic;
using KnowledgeBase;
using Model;
using Snapshotting.Model;

public class AvailableCpuCoresProbe :
    BaseDiagnosticProbe<NodeSnapshot>,
    DiagnosticProbe
{
    public override ProbeMetadata Metadata =>
        new()
        {
            Id = GetType().FullName,
            Name = "Available CPU Cores Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Node;
    public override ProbeCategory Category => ProbeCategory.Throughput;

    public AvailableCpuCoresProbe(IKnowledgeBaseProvider kb)
        : base(kb)
    {
    }

    public ProbeResult Execute<T>(T snapshot) => base.Execute(snapshot as NodeSnapshot);

    protected override ProbeResult GetProbeReadout(NodeSnapshot data)
    {
        ProbeResult result;

        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "AvailableCoresDetected", PropertyValue = data.AvailableCoresDetected.ToString()}
        };

        if (data.AvailableCoresDetected <= 0)
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

        return result;
    }
}