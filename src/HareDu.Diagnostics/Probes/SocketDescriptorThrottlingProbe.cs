namespace HareDu.Diagnostics.Probes;

using System;
using System.Collections.Generic;
using Core.Configuration;
using KnowledgeBase;
using Model;
using Snapshotting.Model;

public class SocketDescriptorThrottlingProbe :
    BaseDiagnosticProbe<NodeSnapshot>,
    DiagnosticProbe
{
    readonly DiagnosticsConfig _config;

    public override ProbeMetadata Metadata =>
        new()
        {
            Id = GetType().FullName,
            Name = "Socket Descriptor Throttling Probe",
            Description = "Checks network to see if the number of sockets currently in use is less than or equal to the number available."
        };
    public override ComponentType ComponentType => ComponentType.Node;
    public override ProbeCategory Category => ProbeCategory.Throughput;
    public bool HasExecuted { get; set; }

    public SocketDescriptorThrottlingProbe(DiagnosticsConfig config, IKnowledgeBaseProvider kb)
        : base(kb)
    {
        _config = config;
    }

    public ProbeResult Execute<T>(T snapshot) => base.Execute(snapshot as NodeSnapshot);

    protected override ProbeResult GetProbeReadout(NodeSnapshot data)
    {
        ProbeResult result;

        if (_config?.Probes is null)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.NA, out var article);

            result = Probe.NotAvailable(data?.ClusterIdentifier, data?.Identifier, Metadata,
                ComponentType, [], article);

            NotifyObservers(result);

            return result;
        }

        ulong warningThreshold = ComputeThreshold(data.OS.SocketDescriptors.Available);

        var probeData = new List<ProbeData>
        {
            new () {PropertyName = "OS.Sockets.Available", PropertyValue = data.OS.SocketDescriptors.Available.ToString()},
            new () {PropertyName = "OS.Sockets.Used", PropertyValue = data.OS.SocketDescriptors.Used.ToString()},
            new () {PropertyName = "ConsumerUtilization", PropertyValue = warningThreshold.ToString()}
        };

        if (data.OS.SocketDescriptors.Used < warningThreshold && warningThreshold < data.OS.SocketDescriptors.Available)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Healthy, out var article);

            result = Probe.Healthy(data.ClusterIdentifier, data.Identifier, Metadata,
                ComponentType, probeData, article);
        }
        else if (data.OS.SocketDescriptors.Used == data.OS.SocketDescriptors.Available)
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Unhealthy, out var article);

            result = Probe.Unhealthy(data.ClusterIdentifier, data.Identifier, Metadata,
                ComponentType, probeData, article);
        }
        else
        {
            _kb.TryGet(Metadata.Id, ProbeResultStatus.Warning, out var article);

            result = Probe.Warning(data.ClusterIdentifier, data.Identifier, Metadata,
                ComponentType, probeData, article);
        }

        NotifyObservers(result);
        
        HasExecuted = true;

        return result;
    }

    ulong ComputeThreshold(ulong socketsAvailable)
        => _config.Probes.SocketUsageThresholdCoefficient >= 1
            ? socketsAvailable
            : Convert.ToUInt64(Math.Ceiling(socketsAvailable * _config.Probes.SocketUsageThresholdCoefficient));
}