namespace HareDu.Diagnostics.Probes
{
    using System;
    using System.Collections.Generic;
    using Core.Configuration;
    using Core.Extensions;
    using KnowledgeBase;
    using Snapshotting.Model;

    public class SocketDescriptorThrottlingProbe :
        BaseDiagnosticProbe,
        DiagnosticProbe
    {
        readonly DiagnosticsConfig _config;

        public DiagnosticProbeMetadata Metadata =>
            new()
            {
                Id = GetType().GetIdentifier(),
                Name = "Socket Descriptor Throttling Probe",
                Description = "Checks network to see if the number of sockets currently in use is less than or equal to the number available."
            };
        public ComponentType ComponentType => ComponentType.Node;
        public ProbeCategory Category => ProbeCategory.Throughput;

        public SocketDescriptorThrottlingProbe(DiagnosticsConfig config, IKnowledgeBaseProvider kb)
            : base(kb)
        {
            _config = config;
        }

        public ProbeResult Execute<T>(T snapshot)
        {
            ProbeResult result;
            NodeSnapshot data = snapshot as NodeSnapshot;

            if (_config.IsNull() || _config.Probes.IsNull())
            {
                _kb.TryGet(Metadata.Id, ProbeResultStatus.NA, out var article);
                result = new ProbeResult
                {
                    Status = ProbeResultStatus.NA,
                    Data = Array.Empty<ProbeData>(),
                    ParentComponentId = data.IsNotNull() ? data.ClusterIdentifier : null,
                    ComponentId = data.IsNotNull() ? data.Identifier : null,
                    Id = Metadata.Id,
                    Name = Metadata.Name,
                    ComponentType = ComponentType,
                    KB = article
                };

                NotifyObservers(result);

                return result;
            }

            ulong warningThreshold = ComputeThreshold(data.OS.SocketDescriptors.Available);
            
            var probeData = new List<ProbeData>
            {
                new () {PropertyName = "OS.Sockets.Available", PropertyValue = data.OS.SocketDescriptors.Available.ToString()},
                new () {PropertyName = "OS.Sockets.Used", PropertyValue = data.OS.SocketDescriptors.Used.ToString()},
                new () {PropertyName = "ConsumerUtilization", PropertyValue = warningThreshold.ToString()}
            };

            if (data.OS.SocketDescriptors.Used < warningThreshold && warningThreshold < data.OS.SocketDescriptors.Available)
            {
                _kb.TryGet(Metadata.Id, ProbeResultStatus.Healthy, out var article);
                result = new ProbeResult
                {
                    Status = ProbeResultStatus.Healthy,
                    ParentComponentId = data.ClusterIdentifier,
                    ComponentId = data.Identifier,
                    Id = Metadata.Id,
                    Name = Metadata.Name,
                    ComponentType = ComponentType,
                    Data = probeData,
                    KB = article
                };
            }
            else if (data.OS.SocketDescriptors.Used == data.OS.SocketDescriptors.Available)
            {
                _kb.TryGet(Metadata.Id, ProbeResultStatus.Unhealthy, out var article);
                result = new ProbeResult
                {
                    Status = ProbeResultStatus.Unhealthy,
                    ParentComponentId = data.ClusterIdentifier,
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
                _kb.TryGet(Metadata.Id, ProbeResultStatus.Warning, out var article);
                result = new ProbeResult
                {
                    Status = ProbeResultStatus.Warning,
                    ParentComponentId = data.ClusterIdentifier,
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

        ulong ComputeThreshold(ulong socketsAvailable)
            => _config.Probes.SocketUsageThresholdCoefficient >= 1
                ? socketsAvailable
                : Convert.ToUInt64(Math.Ceiling(socketsAvailable * _config.Probes.SocketUsageThresholdCoefficient));
    }
}