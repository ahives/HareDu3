namespace HareDu.Diagnostics.Probes;

using System.Collections.Generic;
using Core.Extensions;
using KnowledgeBase;
using Snapshotting.Model;

public class UnroutableMessageProbe :
    BaseDiagnosticProbe<BrokerQueuesSnapshot>,
    DiagnosticProbe
{
    public override DiagnosticProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "Unroutable Message Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Exchange;
    public override ProbeCategory Category => ProbeCategory.Efficiency;

    public UnroutableMessageProbe(IKnowledgeBaseProvider kb)
        : base(kb)
    {
    }

    public ProbeResult Execute<T>(T snapshot)
    {
        return base.Execute(snapshot as BrokerQueuesSnapshot);
    }

    protected override ProbeResult GetProbeResult(BrokerQueuesSnapshot data)
    {
        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "Churn.NotRouted.Total", PropertyValue = data.Churn.NotRouted.Total.ToString()}
        };

        ProbeResult result;
        
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