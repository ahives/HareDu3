namespace HareDu.Diagnostics.Probes
{
    using System;
    using System.Collections.Generic;
    using Core.Configuration;
    using Core.Extensions;
    using KnowledgeBase;
    using Snapshotting.Model;

    public class RuntimeProcessLimitProbe :
        BaseDiagnosticProbe,
        DiagnosticProbe
    {
        readonly DiagnosticsConfig _config;
        
        public string Id => GetType().GetIdentifier();
        public string Name => "Runtime Process Limit Probe";
        public string Description { get; }
        public ComponentType ComponentType => ComponentType.Runtime;
        public ProbeCategory Category => ProbeCategory.Throughput;

        public RuntimeProcessLimitProbe(DiagnosticsConfig config, IKnowledgeBaseProvider kb)
            : base(kb)
        {
            _config = config;
        }

        public ProbeResult Execute<T>(T snapshot)
        {
            ProbeResult result;
            BrokerRuntimeSnapshot data = snapshot as BrokerRuntimeSnapshot;

            if (_config.IsNull() || _config.Probes.IsNull())
            {
                _kb.TryGet(Id, ProbeResultStatus.NA, out var article);
                result = new NotApplicableProbeResult
                {
                    ParentComponentId = null,
                    ComponentId = null,
                    Id = Id,
                    Name = Name,
                    ComponentType = ComponentType,
                    KB = article
                };

                NotifyObservers(result);

                return result;
            }

            ulong threshold = ComputeThreshold(data.Processes.Limit);

            var probeData = new List<ProbeData>
            {
                new () {PropertyName = "Processes.Limit", PropertyValue = data.Processes.Limit.ToString()},
                new () {PropertyName = "Processes.Used", PropertyValue = data.Processes.Used.ToString()},
                new () {PropertyName = "CalculatedThreshold", PropertyValue = threshold.ToString()}
            };

            if (data.Processes.Used >= data.Processes.Limit)
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
            else if (data.Processes.Used >= threshold && threshold < data.Processes.Limit)
            {
                _kb.TryGet(Id, ProbeResultStatus.Healthy, out var article);
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
            else
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

            NotifyObservers(result);

            return result;
        }

        ulong ComputeThreshold(ulong limit)
            => _config.Probes.RuntimeProcessUsageThresholdCoefficient >= 1
                ? limit
                : Convert.ToUInt64(Math.Ceiling(limit * _config.Probes.RuntimeProcessUsageThresholdCoefficient));
    }
}