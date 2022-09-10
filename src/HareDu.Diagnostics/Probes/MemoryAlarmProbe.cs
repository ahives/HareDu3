namespace HareDu.Diagnostics.Probes;

using System.Collections.Generic;
using Core.Extensions;
using KnowledgeBase;
using Snapshotting.Model;

public class MemoryAlarmProbe :
    BaseDiagnosticProbe,
    DiagnosticProbe
{
    public DiagnosticProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "Memory Alarm Probe",
            Description = ""
        };
    public ComponentType ComponentType => ComponentType.Memory;
    public ProbeCategory Category => ProbeCategory.Throughput;

    public MemoryAlarmProbe(IKnowledgeBaseProvider kb)
        : base(kb)
    {
    }

    public ProbeResult Execute<T>(T snapshot)
    {
        MemorySnapshot data = snapshot as MemorySnapshot;
        ProbeResult result;

        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "Memory.FreeAlarm", PropertyValue = data.AlarmInEffect.ToString()},
            new () {PropertyName = "Memory.Limit", PropertyValue = data.Limit.ToString()},
            new () {PropertyName = "Memory.Used", PropertyValue = data.Used.ToString()}
        };
            
        if (data.AlarmInEffect)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Unhealthy, out var article);
            result = new ProbeResult
            {
                Status = ProbeResultStatus.Unhealthy,
                ParentComponentId = data.NodeIdentifier,
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
                ParentComponentId = data.NodeIdentifier,
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