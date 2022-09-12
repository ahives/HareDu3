namespace HareDu.Diagnostics.Probes;

using System;
using System.Collections.Generic;
using Core.Configuration;
using Core.Extensions;
using KnowledgeBase;
using Snapshotting.Model;

public class QueueHighFlowProbe :
    BaseDiagnosticProbe<QueueSnapshot>,
    DiagnosticProbe
{
    readonly DiagnosticsConfig _config;

    public override DiagnosticProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "Queue High Flow Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Queue;
    public override ProbeCategory Category => ProbeCategory.Throughput;

    public QueueHighFlowProbe(DiagnosticsConfig config, IKnowledgeBaseProvider kb)
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
            new () {PropertyName = "Messages.Incoming.Total", PropertyValue = data.Messages.Incoming.Total.ToString()},
            new () {PropertyName = "QueueHighFlowThreshold", PropertyValue = _config.Probes.QueueHighFlowThreshold.ToString()}
        };
            
        if (data.Messages.Incoming.Total >= _config.Probes.QueueHighFlowThreshold)
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