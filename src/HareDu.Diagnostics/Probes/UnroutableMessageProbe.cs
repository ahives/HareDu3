namespace HareDu.Diagnostics.Probes;

using System.Collections.Generic;
using KnowledgeBase;
using Model;
using Snapshotting.Model;

public class UnroutableMessageProbe :
    BaseDiagnosticProbe<BrokerQueuesSnapshot>,
    DiagnosticProbe
{
    public override ProbeMetadata Metadata =>
        new()
        {
            Id = GetType().FullName,
            Name = "Unroutable Message Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Exchange;
    public override ProbeCategory Category => ProbeCategory.Efficiency;
    public bool HasExecuted { get; set; }

    public UnroutableMessageProbe(IKnowledgeBaseProvider kb)
        : base(kb)
    {
    }

    public ProbeResult Execute<T>(T snapshot) => base.Execute(snapshot as BrokerQueuesSnapshot);

    protected override ProbeResult GetProbeReadout(BrokerQueuesSnapshot data)
    {
        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "Churn.NotRouted.Total", PropertyValue = data.Churn.NotRouted.Total.ToString()}
        };

        ProbeResult result;

        if (data.Churn.NotRouted.Total > 0)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Unhealthy, out var article);
            result = Probe.Unhealthy(data.ClusterName, null, Metadata,
                ComponentType, probeData, article);
        }
        else
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Healthy, out var article);
            result = Probe.Healthy(data.ClusterName, null, Metadata,
                ComponentType, probeData, article);
        }

        NotifyObservers(result);

        HasExecuted = true;

        return result;
    }
}