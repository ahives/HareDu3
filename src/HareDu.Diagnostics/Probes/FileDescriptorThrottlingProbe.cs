namespace HareDu.Diagnostics.Probes;

using System;
using System.Collections.Generic;
using Core.Configuration;
using Core.Extensions;
using KnowledgeBase;
using Snapshotting.Model;

public class FileDescriptorThrottlingProbe :
    BaseDiagnosticProbe,
    DiagnosticProbe
{
    readonly DiagnosticsConfig _config;

    public DiagnosticProbeMetadata Metadata =>
        new()
        {
            Id = GetType().GetIdentifier(),
            Name = "File Descriptor Throttling Probe",
            Description = ""
        };
    public ComponentType ComponentType => ComponentType.OperatingSystem;
    public ProbeCategory Category => ProbeCategory.Throughput;

    public FileDescriptorThrottlingProbe(DiagnosticsConfig config, IKnowledgeBaseProvider kb)
        : base(kb)
    {
        _config = config;
    }

    public ProbeResult Execute<T>(T snapshot)
    {
        ProbeResult result;
        OperatingSystemSnapshot data = snapshot as OperatingSystemSnapshot;

        if (_config.IsNull() || _config.Probes.IsNull())
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.NA, out var article);
            result = new ProbeResult
            {
                Status = ProbeResultStatus.NA,
                Data = Array.Empty<ProbeData>(),
                ParentComponentId = data.IsNotNull() ? data.NodeIdentifier : null,
                ComponentId = data.IsNotNull() ? data.ProcessId : null,
                Id = Metadata.Id,
                Name = Metadata.Name,
                ComponentType = ComponentType,
                KB = article
            };

            NotifyObservers(result);

            return result;
        }
            
        ulong threshold = ComputeThreshold(data.FileDescriptors.Available);

        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "FileDescriptors.Available", PropertyValue = data.FileDescriptors.Available.ToString()},
            new () {PropertyName = "FileDescriptors.Used", PropertyValue = data.FileDescriptors.Used.ToString()},
            new () {PropertyName = "FileDescriptorUsageThresholdCoefficient", PropertyValue = _config.Probes.FileDescriptorUsageThresholdCoefficient.ToString()},
            new () {PropertyName = "CalculatedThreshold", PropertyValue = threshold.ToString()}
        };

        if (data.FileDescriptors.Used < threshold && threshold < data.FileDescriptors.Available)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Healthy, out var article);
            result = new ProbeResult
            {
                Status = ProbeResultStatus.Healthy,
                ParentComponentId = data.NodeIdentifier,
                ComponentId = data.ProcessId,
                Id = Metadata.Id,
                Name = Metadata.Name,
                ComponentType = ComponentType,
                Data = probeData,
                KB = article
            };
        }
        else if (data.FileDescriptors.Used == data.FileDescriptors.Available)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Unhealthy, out var article);
            result = new ProbeResult
            {
                Status = ProbeResultStatus.Unhealthy,
                ParentComponentId = data.NodeIdentifier,
                ComponentId = data.ProcessId,
                Id = Metadata.Id,
                Name = Metadata.Name,
                ComponentType = ComponentType,
                Data = probeData,
                KB = article
            };
        }
        else
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Warning, out var article);
            result = new ProbeResult
            {
                Status = ProbeResultStatus.Warning,
                ParentComponentId = data.NodeIdentifier,
                ComponentId = data.ProcessId,
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

    ulong ComputeThreshold(ulong fileDescriptorsAvailable)
        => _config.Probes.FileDescriptorUsageThresholdCoefficient >= 1
            ? fileDescriptorsAvailable
            : Convert.ToUInt64(Math.Ceiling(fileDescriptorsAvailable * _config.Probes.FileDescriptorUsageThresholdCoefficient));
}