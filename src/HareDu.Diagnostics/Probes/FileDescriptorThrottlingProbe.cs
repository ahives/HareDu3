namespace HareDu.Diagnostics.Probes;

using System;
using System.Collections.Generic;
using Core.Configuration;
using KnowledgeBase;
using Model;
using Snapshotting.Model;

public class FileDescriptorThrottlingProbe :
    BaseDiagnosticProbe<OperatingSystemSnapshot>,
    DiagnosticProbe
{
    readonly DiagnosticsConfig _config;

    public override ProbeMetadata Metadata =>
        new()
        {
            Id = GetType().FullName,
            Name = "File Descriptor Throttling Probe",
            Description = ""
        };
    public override ComponentType ComponentType => ComponentType.OperatingSystem;
    public override ProbeCategory Category => ProbeCategory.Throughput;
    public bool HasExecuted { get; set; }

    public FileDescriptorThrottlingProbe(DiagnosticsConfig config, IKnowledgeBaseProvider kb)
        : base(kb)
    {
        _config = config;
    }

    public ProbeResult Execute<T>(T snapshot) => base.Execute(snapshot as OperatingSystemSnapshot);

    protected override ProbeResult GetProbeReadout(OperatingSystemSnapshot data)
    {
        ProbeResult result;

        if (_config?.Probes is null)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.NA, out var article);
            
            result = Probe.NotAvailable(data?.NodeIdentifier, data?.ProcessId, Metadata,
                ComponentType, [], article);

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
            
            result = Probe.Healthy(data.NodeIdentifier, data.ProcessId, Metadata,
                ComponentType, probeData, article);
        }
        else if (data.FileDescriptors.Used == data.FileDescriptors.Available)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Unhealthy, out var article);
            
            result = Probe.Unhealthy(data.NodeIdentifier, data.ProcessId, Metadata,
                ComponentType, probeData, article);
        }
        else
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Warning, out var article);
            
            result = Probe.Warning(data.NodeIdentifier, data.ProcessId, Metadata,
                ComponentType, probeData, article);
        }

        NotifyObservers(result);

        HasExecuted = true;

        return result;
    }

    ulong ComputeThreshold(ulong fileDescriptorsAvailable)
        => _config.Probes.FileDescriptorUsageThresholdCoefficient >= 1
            ? fileDescriptorsAvailable
            : Convert.ToUInt64(Math.Ceiling(fileDescriptorsAvailable * _config.Probes.FileDescriptorUsageThresholdCoefficient));
}