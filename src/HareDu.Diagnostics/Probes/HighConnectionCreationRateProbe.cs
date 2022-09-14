namespace HareDu.Diagnostics.Probes;

using System;
using System.Collections.Generic;
using Core.Configuration;
using Core.Extensions;
using KnowledgeBase;
using Snapshotting.Model;

public class HighConnectionCreationRateProbe :
    BaseDiagnosticProbe<BrokerConnectivitySnapshot>,
    DiagnosticProbe
{
    readonly DiagnosticsConfig _config;

    public override DiagnosticProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "High Connection Creation Rate Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Connection;
    public override ProbeCategory Category => ProbeCategory.Connectivity;

    public HighConnectionCreationRateProbe(DiagnosticsConfig config, IKnowledgeBaseProvider kb)
        : base(kb)
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
            new () {PropertyName = "ConnectionsCreated.Rate", PropertyValue = data.ConnectionsCreated.Rate.ToString()},
            new () {PropertyName = "HighConnectionCreationRateThreshold", PropertyValue = _config.Probes.HighConnectionCreationRateThreshold.ToString()}
        };
            
        if (data.ConnectionsCreated.Rate >= _config.Probes.HighConnectionCreationRateThreshold)
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