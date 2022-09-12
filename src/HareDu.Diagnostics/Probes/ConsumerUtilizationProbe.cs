namespace HareDu.Diagnostics.Probes;

using System;
using System.Collections.Generic;
using Core.Configuration;
using Core.Extensions;
using KnowledgeBase;
using Snapshotting.Model;

public class ConsumerUtilizationProbe :
    BaseDiagnosticProbe<QueueSnapshot>,
    DiagnosticProbe
{
    readonly DiagnosticsConfig _config;

    public override DiagnosticProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "Consumer Utilization Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Queue;
    public override ProbeCategory Category => ProbeCategory.Throughput;

    public ConsumerUtilizationProbe(DiagnosticsConfig config, IKnowledgeBaseProvider kb)
        : base(kb)
    {
        _config = config;
    }

    public ProbeResult Execute<T>(T snapshot)
    {
        return base.Execute(snapshot as QueueSnapshot);
    }

    protected override ProbeResult GetProbeResult(QueueSnapshot data)
    {
        ProbeResult result;

        if (_config.IsNull() || _config.Probes.IsNull())
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.NA, out var article);
            
            result = new ProbeResult
            {
                Status = ProbeResultStatus.NA,
                Data = Array.Empty<ProbeData>(),
                ParentComponentId = data.IsNotNull() ? data.Node : null,
                ComponentId = data.IsNotNull() ? data.Identifier : null,
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
            new () {PropertyName = "ConsumerUtilization", PropertyValue = data.ConsumerUtilization.ToString()},
            new () {PropertyName = "ConsumerUtilizationThreshold", PropertyValue = _config.Probes.ConsumerUtilizationThreshold.ToString()}
        };
            
        if (data.ConsumerUtilization >= _config.Probes.ConsumerUtilizationThreshold
            && data.ConsumerUtilization < 1.0M
            && _config.Probes.ConsumerUtilizationThreshold <= 1.0M)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Warning, out var article);
            
            result = new ProbeResult
            {
                Status = ProbeResultStatus.Warning,
                ParentComponentId = data.Node,
                ComponentId = data.Identifier,
                Id = Metadata.Id,
                Name = Metadata.Name,
                ComponentType = ComponentType,
                Data = probeData,
                KB = article
            };
        }
        else if (data.ConsumerUtilization < _config.Probes.ConsumerUtilizationThreshold
                 && _config.Probes.ConsumerUtilizationThreshold <= 1.0M)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Unhealthy, out var article);
            
            result = new ProbeResult
            {
                Status = ProbeResultStatus.Unhealthy,
                ParentComponentId = data.Node,
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
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Healthy, out var article);
            
            result = new ProbeResult
            {
                Status = ProbeResultStatus.Healthy,
                ParentComponentId = data.Node,
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
}