namespace HareDu.Diagnostics.Probes
{
    using System.Collections.Generic;
    using Core.Configuration;
    using Core.Extensions;
    using KnowledgeBase;
    using Snapshotting.Model;

    public class HighConnectionCreationRateProbe :
        BaseDiagnosticProbe,
        IUpdateProbeConfiguration,
        DiagnosticProbe
    {
        DiagnosticsConfig _config;
        
        public string Id => GetType().GetIdentifier();
        public string Name => "High Connection Creation Rate Probe";
        public string Description { get; }
        public ComponentType ComponentType => ComponentType.Connection;
        public ProbeCategory Category => ProbeCategory.Connectivity;

        public HighConnectionCreationRateProbe(DiagnosticsConfig config, IKnowledgeBaseProvider kb)
            : base(kb)
        {
            _config = config;
        }

        public ProbeResult Execute<T>(T snapshot)
        {
            ProbeResult result;
            BrokerConnectivitySnapshot data = snapshot as BrokerConnectivitySnapshot;

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
            
            var probeData = new List<ProbeData>
            {
                new () {PropertyName = "ConnectionsCreated.Rate", PropertyValue = data.ConnectionsCreated.Rate.ToString()},
                new () {PropertyName = "HighConnectionCreationRateThreshold", PropertyValue = _config.Probes.HighConnectionCreationRateThreshold.ToString()}
            };
            
            if (data.ConnectionsCreated.Rate >= _config.Probes.HighConnectionCreationRateThreshold)
            {
                _kb.TryGet(Id, ProbeResultStatus.Warning, out var article);
                result = new WarningProbeResult
                {
                    ParentComponentId = null,
                    ComponentId = null,
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
                    ParentComponentId = null,
                    ComponentId = null,
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
    }
}