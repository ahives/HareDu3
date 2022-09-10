namespace HareDu.Diagnostics.Probes;

using System.Collections.Generic;
using Core.Extensions;
using KnowledgeBase;
using Snapshotting.Model;

public class MessagePagingProbe :
    BaseDiagnosticProbe,
    DiagnosticProbe
{
    public DiagnosticProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "Message Paging Probe",
            Description = ""
        };
    public ComponentType ComponentType => ComponentType.Queue;
    public ProbeCategory Category => ProbeCategory.Memory;

    public MessagePagingProbe(IKnowledgeBaseProvider kb)
        : base(kb)
    {
    }

    public ProbeResult Execute<T>(T snapshot)
    {
        QueueSnapshot data = snapshot as QueueSnapshot;
        ProbeResult result;
            
        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "Memory.PagedOut.Total", PropertyValue = data.Memory.PagedOut.Total.ToString()}
        };
            
        if (data.Memory.PagedOut.Total > 0)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Unhealthy, out var article);
            result = new ProbeResult
            {
                Status = ProbeResultStatus.Unhealthy,
                ParentComponentId = data.Node,
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
                ParentComponentId = data.Node,
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