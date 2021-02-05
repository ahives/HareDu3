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
        IUpdateProbeConfiguration,
        DiagnosticProbe
    {
        DiagnosticsConfig _config;
        
        public string Id => GetType().GetIdentifier();
        public string Name => "Socket Descriptor Throttling Probe";
        public string Description =>
            "Checks network to see if the number of sockets currently in use is less than or equal to the number available.";
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
                _kb.TryGet(Id, ProbeResultStatus.NA, out var article);
                result = new NotApplicableProbeResult
                {
                    ParentComponentId = !data.IsNull() ? data.ClusterIdentifier : null,
                    ComponentId = !data.IsNull() ? data.Identifier : null,
                    Id = Id,
                    Name = Name,
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
                _kb.TryGet(Id, ProbeResultStatus.Healthy, out var article);
                result = new HealthyProbeResult
                {
                    ParentComponentId = data.ClusterIdentifier,
                    ComponentId = data.Identifier,
                    Id = Id,
                    Name = Name,
                    ComponentType = ComponentType,
                    Data = probeData,
                    KB = article
                };
            }
            else if (data.OS.SocketDescriptors.Used == data.OS.SocketDescriptors.Available)
            {
                _kb.TryGet(Id, ProbeResultStatus.Unhealthy, out var article);
                result = new UnhealthyProbeResult
                {
                    ParentComponentId = data.ClusterIdentifier,
                    ComponentId = data.Identifier,
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
                    ParentComponentId = data.ClusterIdentifier,
                    ComponentId = data.Identifier,
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

        ulong ComputeThreshold(ulong socketsAvailable)
            => _config.Probes.SocketUsageThresholdCoefficient >= 1
                ? socketsAvailable
                : Convert.ToUInt64(Math.Ceiling(socketsAvailable * _config.Probes.SocketUsageThresholdCoefficient));
    }
}