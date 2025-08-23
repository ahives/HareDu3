namespace HareDu.Diagnostics.Probes;

using System.Collections.Generic;
using KnowledgeBase;
using Model;
using Snapshotting.Model;

public class QueueGrowthProbe :
    BaseDiagnosticProbe<QueueSnapshot>,
    DiagnosticProbe
{
    public override ProbeMetadata Metadata =>
        new()
        {
            Id = GetType().FullName,
            Name = "Queue Growth Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Queue;
    public override ProbeCategory Category => ProbeCategory.Throughput;
    public bool HasExecuted { get; set; }

    public QueueGrowthProbe(IKnowledgeBaseProvider kb)
        : base(kb)
    {
    }

    public ProbeResult Execute<T>(T snapshot) => base.Execute(snapshot as QueueSnapshot);

    protected override ProbeResult GetProbeReadout(QueueSnapshot data)
    {
        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "Messages.Incoming.Rate", PropertyValue = data.Messages.Incoming.Rate.ToString()},
            new () {PropertyName = "Messages.Acknowledged.Rate", PropertyValue = data.Messages.Acknowledged.Rate.ToString()}
        };

        ProbeResult result;
        
        if (data.Messages.Incoming.Rate > data.Messages.Acknowledged.Rate)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Warning, out var article);
            result = Probe.Warning(data.Node, data.Identifier, Metadata, ComponentType, probeData, article);
        }
        else
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Healthy, out var article);
            result = Probe.Healthy(data.Node, data.Identifier, Metadata, ComponentType, probeData, article);
        }

        NotifyObservers(result);

        return result;
    }
}