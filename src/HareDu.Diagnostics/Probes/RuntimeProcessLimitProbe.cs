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

    public override ProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "Runtime Process Limit Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Runtime;
    public override ProbeCategory Category => ProbeCategory.Throughput;
    public bool HasExecuted { get; set; }

    public RuntimeProcessLimitProbe(DiagnosticsConfig config, IKnowledgeBaseProvider kb)
        : base(kb)
    {
        _config = config;
    }

    public ProbeResult Execute<T>(T snapshot) => base.Execute(snapshot as BrokerRuntimeSnapshot);

    protected override ProbeResult GetProbeReadout(BrokerRuntimeSnapshot data)
    {
        ProbeResult result;

        if (_config?.Probes is null)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.NA, out var article);
            
            result = Probe.NotAvailable(null, null, Metadata,
                ComponentType, Array.Empty<ProbeData>(), article);

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
            
            result = Probe.Unhealthy(data.ClusterIdentifier, data.Identifier, Metadata,
                ComponentType, probeData, article);
        }
        else if (data.Processes.Used >= threshold && threshold < data.Processes.Limit)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Healthy, out var article);
            
            result = Probe.Warning(data.ClusterIdentifier, data.Identifier, Metadata,
                ComponentType, probeData, article);
        }
        else
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Healthy, out var article);
            
            result = Probe.Healthy(data.ClusterIdentifier, data.Identifier, Metadata,
                ComponentType, probeData, article);
        }

        NotifyObservers(result);

        HasExecuted = true;

        return result;
    }

    ulong ComputeThreshold(ulong limit)
        => _config.Probes.RuntimeProcessUsageThresholdCoefficient >= 1
            ? limit
            : Convert.ToUInt64(Math.Ceiling(limit * _config.Probes.RuntimeProcessUsageThresholdCoefficient));
}