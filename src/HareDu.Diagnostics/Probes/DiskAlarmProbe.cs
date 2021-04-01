namespace HareDu.Diagnostics.Probes
{
    using System.Collections.Generic;
    using Core.Extensions;
    using KnowledgeBase;
    using Snapshotting.Model;

    public class DiskAlarmProbe :
        BaseDiagnosticProbe,
        DiagnosticProbe
    {
        public DiagnosticProbeMetadata Metadata =>
            new()
            {
                Id = GetType().GetIdentifier(),
                Name = "Disk Alarm Probe",
                Description = ""
            };
        public ComponentType ComponentType => ComponentType.Disk;
        public ProbeCategory Category => ProbeCategory.Throughput;

        public DiskAlarmProbe(IKnowledgeBaseProvider kb)
            : base(kb)
        {
        }

        public ProbeResult Execute<T>(T snapshot)
        {
            DiskSnapshot data = snapshot as DiskSnapshot;
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
}