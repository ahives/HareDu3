namespace HareDu.Diagnostics.Probes;

using System;
using System.Collections.Generic;
using Core.Configuration;
using Core.Extensions;
using KnowledgeBase;
using Snapshotting.Model;

public class HighConnectionClosureRateProbe :
    BaseDiagnosticProbe<BrokerConnectivitySnapshot>,
    DiagnosticProbe
{
    readonly DiagnosticsConfig _config;

    public override DiagnosticProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "High Connection Closure Rate Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Connection;
    public override ProbeCategory Category => ProbeCategory.Connectivity;

    public HighConnectionClosureRateProbe(DiagnosticsConfig config, IKnowledgeBaseProvider kb) : base(kb)
    {
        _config = config;
    }

    public ProbeResult Execute<T>(T snapshot) => base.Execute(snapshot as BrokerConnectivitySnapshot);

    protected override ProbeResult GetProbeResult(BrokerConnectivitySnapshot data)
    {
        ProbeResult result;

        if (_config?.Probes is null)
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