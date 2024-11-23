namespace HareDu.Diagnostics.Probes;

using System.Collections.Generic;
using Core.Extensions;
using KnowledgeBase;
using Snapshotting.Model;

public class MemoryAlarmProbe :
    BaseDiagnosticProbe<MemorySnapshot>,
    DiagnosticProbe
{
    public override ProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "Memory Alarm Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Memory;
    public override ProbeCategory Category => ProbeCategory.Throughput;
    public bool HasExecuted { get; set; }

    public MemoryAlarmProbe(IKnowledgeBaseProvider kb)
        : base(kb)
    {
    }

    public ProbeResult Execute<T>(T snapshot) => base.Execute(snapshot as MemorySnapshot);

    protected override ProbeResult GetProbeReadout(MemorySnapshot data)
    {
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
            
            result = Probe.Unhealthy(data.NodeIdentifier, null, Metadata,
                ComponentType, probeData, article);
        }
        else
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Healthy, out var article);
            
            result = Probe.Healthy(data.NodeIdentifier, null, Metadata,
                ComponentType, probeData, article);
        }

        NotifyObservers(result);
        
        HasExecuted = true;

        return result;
    }
}