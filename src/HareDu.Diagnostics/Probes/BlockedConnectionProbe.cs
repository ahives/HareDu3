namespace HareDu.Diagnostics.Probes;

using System.Collections.Generic;
using Core.Extensions;
using KnowledgeBase;
using Model;
using Snapshotting.Model;

public class BlockedConnectionProbe :
    BaseDiagnosticProbe<ConnectionSnapshot>,
    DiagnosticProbe
{
    public override DiagnosticProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "Blocked Connection Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Connection;
    public override ProbeCategory Category => ProbeCategory.Throughput;

    public BlockedConnectionProbe(IKnowledgeBaseProvider kb)
        : base(kb)
    {
    }

    public ProbeResult Execute<T>(T snapshot) => base.Execute(snapshot as ConnectionSnapshot);

    protected override ProbeResult GetProbeResult(ConnectionSnapshot data)
    {
        ProbeResult result;

        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "State", PropertyValue = data.State.ToString()}
        };
            
        if (data.State == BrokerConnectionState.Blocked)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Unhealthy, out var article);
            
            result = new ProbeResult
            {
                Status = ProbeResultStatus.Unhealthy,
                ParentComponentId = data.NodeIdentifier,
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
                ParentComponentId = data.NodeIdentifier,
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