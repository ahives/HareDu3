namespace HareDu.Diagnostics.Probes;

using System;
using System.Collections.Generic;
using Core.Configuration;
using Core.Extensions;
using KnowledgeBase;
using Snapshotting.Model;

public class RedeliveredMessagesProbe :
    BaseDiagnosticProbe<QueueSnapshot>,
    DiagnosticProbe
{
    readonly DiagnosticsConfig _config;

    public override DiagnosticProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "Redelivered Messages Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.Queue;
    public override ProbeCategory Category => ProbeCategory.FaultTolerance;

    public RedeliveredMessagesProbe(DiagnosticsConfig config, IKnowledgeBaseProvider kb)
        : base(kb)
    {
        _config = config;
    }

    public ProbeResult Execute<T>(T snapshot) => base.Execute(snapshot as QueueSnapshot);

    protected override ProbeResult GetProbeResult(QueueSnapshot data)
    {
        ProbeResult result;
        
        if (_config?.Probes is null)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.NA, out var article);
            
            result = new ProbeResult
            {
                Status = ProbeResultStatus.NA,
                Data = Array.Empty<ProbeData>(),
                ParentComponentId = data is not null ? data.Node : string.Empty,
                ComponentId = data is not null ? data.Identifier : string.Empty,
                Id = Metadata.Id,
                Name = Metadata.Name,
                ComponentType = ComponentType,
                KB = article
            };

            NotifyObservers(result);

            return result;
        }
            
        ulong warningThreshold = ComputeThreshold(data.Messages.Incoming.Total);
            
        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "Messages.Incoming.Total", PropertyValue = data.Messages.Incoming.Total.ToString()},
            new () {PropertyName = "Messages.Redelivered.Total", PropertyValue = data.Messages.Redelivered.Total.ToString()},
            new () {PropertyName = "MessageRedeliveryThresholdCoefficient", PropertyValue = _config.Probes.MessageRedeliveryThresholdCoefficient.ToString()},
            new () {PropertyName = "CalculatedThreshold", PropertyValue = warningThreshold.ToString()}
        };
            
        if (data.Messages.Redelivered.Total >= warningThreshold
            && data.Messages.Redelivered.Total < data.Messages.Incoming.Total
            && warningThreshold < data.Messages.Incoming.Total)
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
        else if (data.Messages.Redelivered.Total >= data.Messages.Incoming.Total)
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

    ulong ComputeThreshold(ulong total)
        => _config.Probes.MessageRedeliveryThresholdCoefficient >= 1
            ? total
            : Convert.ToUInt64(Math.Ceiling(total * _config.Probes.MessageRedeliveryThresholdCoefficient));
}