namespace HareDu.Diagnostics.Probes
{
    using System;
    using System.Collections.Generic;
    using Core.Configuration;
    using Core.Extensions;
    using KnowledgeBase;
    using Snapshotting.Model;

    public class RedeliveredMessagesProbe :
        BaseDiagnosticProbe,
        IUpdateProbeConfiguration,
        DiagnosticProbe
    {
        DiagnosticsConfig _config;
        
        public string Id => GetType().GetIdentifier();
        public string Name => "Redelivered Messages Probe";
        public string Description { get; }
        public ComponentType ComponentType => ComponentType.Queue;
        public ProbeCategory Category => ProbeCategory.FaultTolerance;

        public RedeliveredMessagesProbe(DiagnosticsConfig config, IKnowledgeBaseProvider kb)
            : base(kb)
        {
            _config = config;
        }

        public ProbeResult Execute<T>(T snapshot)
        {
            ProbeResult result;
            QueueSnapshot data = snapshot as QueueSnapshot;
            
            if (_config.IsNull() || _config.Probes.IsNull())
            {
                _kb.TryGet(Id, ProbeResultStatus.NA, out var article);
                result = new NotApplicableProbeResult
                {
                    ParentComponentId = !data.IsNull() ? data.Node : string.Empty,
                    ComponentId = !data.IsNull() ? data.Identifier : string.Empty,
                    Id = Id,
                    Name = Name,
                    ComponentType = ComponentType,
                    KB = article
                };

                NotifyObservers(result);

                return result;
            }
            
            ulong warningThreshold = ComputeThreshold(data.Messages.Incoming.Total);
            
            var probeData = new List<ProbeData>
            {
                new () {PropertyName = "Messages.Incoming.Total", PropertyValue = data.Messages.Incoming.Total.ToString()},
                new () {PropertyName = "Messages.Redelivered.Total", PropertyValue = data.Messages.Redelivered.Total.ToString()},
                new () {PropertyName = "MessageRedeliveryThresholdCoefficient", PropertyValue = _config.Probes.MessageRedeliveryThresholdCoefficient.ToString()},
                new () {PropertyName = "CalculatedThreshold", PropertyValue = warningThreshold.ToString()}
            };
            
            if (data.Messages.Redelivered.Total >= warningThreshold
                && data.Messages.Redelivered.Total < data.Messages.Incoming.Total
                && warningThreshold < data.Messages.Incoming.Total)
            {
                _kb.TryGet(Id, ProbeResultStatus.Warning, out var article);
                result = new WarningProbeResult
                {
                    ParentComponentId = data.Node,
                    ComponentId = data.Identifier,
                    Id = Id,
                    Name = Name,
                    ComponentType = ComponentType,
                    Data = probeData,
                    KB = article
                };
            }
            else if (data.Messages.Redelivered.Total >= data.Messages.Incoming.Total)
            {
                _kb.TryGet(Id, ProbeResultStatus.Unhealthy, out var article);
                result = new UnhealthyProbeResult
                {
                    ParentComponentId = data.Node,
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
                    ParentComponentId = data.Node,
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

        ulong ComputeThreshold(ulong total)
            => _config.Probes.MessageRedeliveryThresholdCoefficient >= 1
                ? total
                : Convert.ToUInt64(Math.Ceiling(total * _config.Probes.MessageRedeliveryThresholdCoefficient));
    }
}