namespace HareDu.Diagnostics.Probes
{
    using System;
    using System.Collections.Generic;
    using Core.Configuration;
    using Core.Extensions;
    using KnowledgeBase;
    using Snapshotting.Model;

    public class HighConnectionClosureRateProbe :
        BaseDiagnosticProbe,
        DiagnosticProbe
    {
        readonly DiagnosticsConfig _config;

        public DiagnosticProbeMetadata Metadata =>
            new()
            {
                Id = GetType().GetIdentifier(),
                Name = "High Connection Closure Rate Probe",
                Description = ""
            };
        public ComponentType ComponentType => ComponentType.Connection;
        public ProbeCategory Category => ProbeCategory.Connectivity;

        public HighConnectionClosureRateProbe(DiagnosticsConfig config, IKnowledgeBaseProvider kb)
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
                _kb.TryGet(Metadata.Id, ProbeResultStatus.NA, out var article);
                result = new ProbeResult
                {
                    Status = ProbeResultStatus.NA,
                    Data = Array.Empty<ProbeData>(),
                    ParentComponentId = null,
                    ComponentId = null,
                    Id = Metadata.Id,
                    Name = Metadata.Name,
                    ComponentType = ComponentType,
                    KB = article
                };

                NotifyObservers(result);

                return result;
            }

            var probeData = new List<ProbeData>
            {
                new () {PropertyName = "ConnectionsClosed.Rate", PropertyValue = data.ConnectionsClosed.Rate.ToString()},
                new () {PropertyName = "HighConnectionClosureRateThreshold", PropertyValue = _config.Probes.HighConnectionClosureRateThreshold.ToString()}
            };
            
            if (data.ConnectionsClosed.Rate >= _config.Probes.HighConnectionClosureRateThreshold)
            {
                _kb.TryGet(Metadata.Id, ProbeResultStatus.Warning, out var article);
                result = new ProbeResult
                {
                    Status = ProbeResultStatus.Warning,
                    ParentComponentId = null,
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
                    ParentComponentId = null,
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