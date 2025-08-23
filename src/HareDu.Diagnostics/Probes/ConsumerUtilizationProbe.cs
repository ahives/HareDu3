namespace HareDu.Diagnostics.Probes;

using System.Collections.Generic;
using Core.Configuration;
using KnowledgeBase;
using Model;
using Snapshotting.Model;

public class ConsumerUtilizationProbe :
    BaseDiagnosticProbe<QueueSnapshot>,
    DiagnosticProbe
{
    readonly DiagnosticsConfig _config;

    public override ProbeMetadata Metadata =>
        new()
        {
            Id = GetType().FullName,
            Name = "Consumer Utilization Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Queue;
    public override ProbeCategory Category => ProbeCategory.Throughput;
    public bool HasExecuted { get; set; }

    public ConsumerUtilizationProbe(DiagnosticsConfig config, IKnowledgeBaseProvider kb)
        : base(kb)
    {
        _config = config;
    }

    public ProbeResult Execute<T>(T snapshot) => base.Execute(snapshot as QueueSnapshot);

    protected override ProbeResult GetProbeReadout(QueueSnapshot data)
    {
        ProbeResult result;

        if (_config?.Probes is null)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.NA, out var article);
            result = Probe.NotAvailable(data?.Node, data?.Identifier, Metadata, ComponentType, [], article);

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
            result = Probe.Warning(data.Node, data.Identifier, Metadata, ComponentType, probeData, article);
        }
        else if (data.ConsumerUtilization < _config.Probes.ConsumerUtilizationThreshold
                 && _config.Probes.ConsumerUtilizationThreshold <= 1.0M)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Unhealthy, out var article);
            result = Probe.Unhealthy(data.Node, data.Identifier, Metadata, ComponentType, probeData, article);
        }
        else
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Healthy, out var article);
            result = Probe.Healthy(data.Node, data.Identifier, Metadata, ComponentType, probeData, article);
        }

        NotifyObservers(result);

        HasExecuted = true;

        return result;
    }
}