namespace HareDu.Diagnostics.Probes;

using System.Collections.Generic;
using Core.Extensions;
using KnowledgeBase;
using Model;
using Snapshotting.Model;

public class QueueNoFlowProbe :
    BaseDiagnosticProbe<QueueSnapshot>,
    DiagnosticProbe
{
    public override ProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "Queue No Flow Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Queue;
    public override ProbeCategory Category => ProbeCategory.Throughput;
    public bool HasExecuted { get; set; }

    public QueueNoFlowProbe(IKnowledgeBaseProvider kb)
        : base(kb)
    {
    }

    public ProbeResult Execute<T>(T snapshot) => base.Execute(snapshot as QueueSnapshot);

    protected override ProbeResult GetProbeReadout(QueueSnapshot data)
    {
        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "Messages.Incoming.Total", PropertyValue = data.Messages.Incoming.Total.ToString()}
        };

        ProbeResult result;
        
        if (data.Messages.Incoming.Total == 0)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Unhealthy, out var article);
            
            result = Probe.Unhealthy(data.Node, data.Identifier, Metadata,
                ComponentType, probeData, article);
        }
        else
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Healthy, out var article);
            
            result = Probe.Healthy(data.Node, data.Identifier, Metadata,
                ComponentType, probeData, article);
        }

        NotifyObservers(result);

        HasExecuted = true;

        return result;
    }
}