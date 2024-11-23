namespace HareDu.Diagnostics.Probes;

using System.Collections.Generic;
using Core.Extensions;
using KnowledgeBase;
using Snapshotting.Model;

public class DiskAlarmProbe :
    BaseDiagnosticProbe<DiskSnapshot>,
    DiagnosticProbe
{
    public override ProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "Disk Alarm Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Disk;
    public override ProbeCategory Category => ProbeCategory.Throughput;
    public bool HasExecuted { get; set; }

    public DiskAlarmProbe(IKnowledgeBaseProvider kb)
        : base(kb)
    {
    }

    public ProbeResult Execute<T>(T snapshot) => base.Execute(snapshot as DiskSnapshot);

    protected override ProbeResult GetProbeReadout(DiskSnapshot data)
    {
        ProbeResult result;

        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "Disk.FreeAlarm", PropertyValue = data.AlarmInEffect.ToString()},
            new () {PropertyName = "Disk.Limit", PropertyValue = data.Limit.ToString()},
            new () {PropertyName = "Disk.Capacity.Available", PropertyValue = data.Capacity.Available.ToString()}
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