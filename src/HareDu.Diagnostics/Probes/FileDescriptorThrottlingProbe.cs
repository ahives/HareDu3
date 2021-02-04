namespace HareDu.Diagnostics.Probes
{
    using System;
    using System.Collections.Generic;
    using Core.Configuration;
    using Core.Extensions;
    using KnowledgeBase;
    using Snapshotting.Model;

    public class FileDescriptorThrottlingProbe :
        BaseDiagnosticProbe,
        IUpdateProbeConfiguration,
        DiagnosticProbe
    {
        DiagnosticsConfig _config;
        
        public string Id => GetType().GetIdentifier();
        public string Name => "File Descriptor Throttling Probe";
        public string Description { get; }
        public ComponentType ComponentType => ComponentType.OperatingSystem;
        public ProbeCategory Category => ProbeCategory.Throughput;

        public FileDescriptorThrottlingProbe(DiagnosticsConfig config, IKnowledgeBaseProvider kb)
            : base(kb)
        {
            _config = config;
        }

        public ProbeResult Execute<T>(T snapshot)
        {
            ProbeResult result;
            OperatingSystemSnapshot data = snapshot as OperatingSystemSnapshot;

            if (_config.IsNull() || _config.Probes.IsNull())
            {
                _kb.TryGet(Id, ProbeResultStatus.NA, out var article);
                result = new NotApplicableProbeResult
                {
                    ParentComponentId = !data.IsNull() ? data.NodeIdentifier : null,
                    ComponentId = !data.IsNull() ? data.ProcessId : null,
                    Id = Id,
                    Name = Name,
                    ComponentType = ComponentType,
                    KB = article
                };

                NotifyObservers(result);

                return result;
            }
            
            ulong threshold = ComputeThreshold(data.FileDescriptors.Available);

            var probeData = new List<ProbeData>
            {
                new () {PropertyName = "FileDescriptors.Available", PropertyValue = data.FileDescriptors.Available.ToString()},
                new () {PropertyName = "FileDescriptors.Used", PropertyValue = data.FileDescriptors.Used.ToString()},
                new () {PropertyName = "FileDescriptorUsageThresholdCoefficient", PropertyValue = _config.Probes.FileDescriptorUsageThresholdCoefficient.ToString()},
                new () {PropertyName = "CalculatedThreshold", PropertyValue = threshold.ToString()}
            };

            if (data.FileDescriptors.Used < threshold && threshold < data.FileDescriptors.Available)
            {
                _kb.TryGet(Id, ProbeResultStatus.Healthy, out var article);
                result = new HealthyProbeResult
                {
                    ParentComponentId = data.NodeIdentifier,
                    ComponentId = data.ProcessId,
                    Id = Id,
                    Name = Name,
                    ComponentType = ComponentType,
                    Data = probeData,
                    KB = article
                };
            }
            else if (data.FileDescriptors.Used == data.FileDescriptors.Available)
            {
                _kb.TryGet(Id, ProbeResultStatus.Unhealthy, out var article);
                result = new UnhealthyProbeResult
                {
                    ParentComponentId = data.NodeIdentifier,
                    ComponentId = data.ProcessId,
                    Id = Id,
                    Name = Name,
                    ComponentType = ComponentType,
                    Data = probeData,
                    KB = article
                };
            }
            else
            {
                _kb.TryGet(Id, ProbeResultStatus.Warning, out var article);
                result = new WarningProbeResult
                {
                    ParentComponentId = data.NodeIdentifier,
                    ComponentId = data.ProcessId,
                    Id = Id,
                    Name = Name,
                    ComponentType = ComponentType,
                    Data = probeData,
                    KB = article
                };
            }

            NotifyObservers(result);

            return result;
        }

        public void UpdateConfiguration(DiagnosticsConfig config)
        {
            DiagnosticsConfig current = _config;
            _config = config;
            
            NotifyObservers(Id, Name, current, config);
        }

        ulong ComputeThreshold(ulong fileDescriptorsAvailable)
            => _config.Probes.FileDescriptorUsageThresholdCoefficient >= 1
                ? fileDescriptorsAvailable
                : Convert.ToUInt64(Math.Ceiling(fileDescriptorsAvailable * _config.Probes.FileDescriptorUsageThresholdCoefficient));
    }
}