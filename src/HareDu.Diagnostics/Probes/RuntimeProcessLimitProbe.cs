namespace HareDu.Diagnostics.Probes;

using System;
using System.Collections.Generic;
using Core.Configuration;
using Core.Extensions;
using KnowledgeBase;
using Snapshotting.Model;

public class RuntimeProcessLimitProbe :
    BaseDiagnosticProbe<BrokerRuntimeSnapshot>,
    DiagnosticProbe
{
    readonly DiagnosticsConfig _config;

    public override DiagnosticProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "Runtime Process Limit Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Runtime;
    public override ProbeCategory Category => ProbeCategory.Throughput;

    public RuntimeProcessLimitProbe(DiagnosticsConfig config, IKnowledgeBaseProvider kb)
        : base(kb)
    {
        _config = config;
    }

    public ProbeResult Execute<T>(T snapshot)
    {
        return base.Execute(snapshot as BrokerRuntimeSnapshot);
    }

    protected override ProbeResult GetProbeResult(BrokerRuntimeSnapshot data)
    {
        ProbeResult result;

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

        ulong threshold = ComputeThreshold(data.Processes.Limit);

        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "Processes.Limit", PropertyValue = data.Processes.Limit.ToString()},
            new () {PropertyName = "Processes.Used", PropertyValue = data.Processes.Used.ToString()},
            new () {PropertyName = "CalculatedThreshold", PropertyValue = threshold.ToString()}
        };

        if (data.Processes.Used >= data.Processes.Limit)
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
        else if (data.Processes.Used >= threshold && threshold < data.Processes.Limit)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Healthy, out var article);
            
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
        else
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

        NotifyObservers(result);

        return result;
    }

    ulong ComputeThreshold(ulong limit)
        => _config.Probes.RuntimeProcessUsageThresholdCoefficient >= 1
            ? limit
            : Convert.ToUInt64(Math.Ceiling(limit * _config.Probes.RuntimeProcessUsageThresholdCoefficient));
}