namespace HareDu.Diagnostics.Probes;

using System.Collections.Generic;
using Core.Extensions;
using KnowledgeBase;
using Snapshotting.Model;

public class UnroutableMessageProbe :
    BaseDiagnosticProbe,
    DiagnosticProbe
{
    public DiagnosticProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "Unroutable Message Probe",
            Description = ""
        };
    public ComponentType ComponentType => ComponentType.Exchange;
    public ProbeCategory Category => ProbeCategory.Efficiency;

    public UnroutableMessageProbe(IKnowledgeBaseProvider kb)
        : base(kb)
    {
    }

    public ProbeResult Execute<T>(T snapshot)
    {
        ProbeResult result;
        BrokerQueuesSnapshot data = snapshot as BrokerQueuesSnapshot;
            
        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "Churn.NotRouted.Total", PropertyValue = data.Churn.NotRouted.Total.ToString()}
        };

        if (data.Churn.NotRouted.Total > 0)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Unhealthy, out var article);
            result = new ProbeResult
            {
                Status = ProbeResultStatus.Unhealthy,
                ParentComponentId = data.ClusterName,
                ComponentId = null,
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
                ParentComponentId = data.ClusterName,
                ComponentId = null,
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