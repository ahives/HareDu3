namespace HareDu.Diagnostics.Probes;

using System.Collections.Generic;
using Core.Extensions;
using KnowledgeBase;
using Model;
using Snapshotting.Model;

public class MessagePagingProbe :
    BaseDiagnosticProbe<QueueSnapshot>,
    DiagnosticProbe
{
    public override ProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "Message Paging Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Queue;
    public override ProbeCategory Category => ProbeCategory.Memory;
    public bool HasExecuted { get; set; }

    public MessagePagingProbe(IKnowledgeBaseProvider kb)
        : base(kb)
    {
    }

    public ProbeResult Execute<T>(T snapshot) => base.Execute(snapshot as QueueSnapshot);

    protected override ProbeResult GetProbeReadout(QueueSnapshot data)
    {
        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "Memory.PagedOut.Total", PropertyValue = data.Memory.PagedOut.Total.ToString()}
        };

        ProbeResult result;
        
        if (data.Memory.PagedOut.Total > 0)
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