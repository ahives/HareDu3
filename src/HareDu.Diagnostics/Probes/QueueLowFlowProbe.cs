namespace HareDu.Diagnostics.Probes;

using System;
using System.Collections.Generic;
using Core.Configuration;
using Core.Extensions;
using KnowledgeBase;
using Model;
using Snapshotting.Model;

public class QueueLowFlowProbe :
    BaseDiagnosticProbe<QueueSnapshot>,
    DiagnosticProbe
{
    readonly DiagnosticsConfig _config;

    public override ProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "Queue Low Flow Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Queue;
    public override ProbeCategory Category => ProbeCategory.Throughput;
    public bool HasExecuted { get; set; }

    public QueueLowFlowProbe(DiagnosticsConfig config, IKnowledgeBaseProvider kb)
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
            
            result = Probe.NotAvailable(null, null, Metadata,
                ComponentType, Array.Empty<ProbeData>(), article);

            NotifyObservers(result);

            return result;
        }
           
        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "Messages.Incoming.Total", PropertyValue = data.Messages.Incoming.Total.ToString()},
            new () {PropertyName = "QueueLowFlowThreshold", PropertyValue = _config.Probes.QueueLowFlowThreshold.ToString()}
        };
            
        if (data.Messages.Incoming.Total <= _config.Probes.QueueLowFlowThreshold)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Unhealthy, out var article);
            
            result = Probe.Unhealthy(data.Node, data.Identifier, Metadata,
                ComponentType, probeData, article);
        }
        else
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Healthy, out var article);
            
            result = Probe.Healthy(data.Node, data.Identifier, Metadata,
                ComponentType, probeData, article);
        }

        NotifyObservers(result);

        HasExecuted = true;

        return result;
    }
}