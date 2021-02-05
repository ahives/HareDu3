namespace HareDu.Diagnostics.Probes
{
    using System.Collections.Generic;
    using Core.Extensions;
    using KnowledgeBase;
    using Snapshotting.Model;

    public class ChannelThrottlingProbe :
        BaseDiagnosticProbe,
        DiagnosticProbe
    {
        public string Id => GetType().GetIdentifier();
        public string Name => "Channel Throttling Probe";
        public string Description => "Monitors connections to the RabbitMQ broker to determine whether channels are being throttled.";
        public ComponentType ComponentType => ComponentType.Channel;
        public ProbeCategory Category => ProbeCategory.Throughput;

        public ChannelThrottlingProbe(IKnowledgeBaseProvider kb)
            : base(kb)
        {
        }

        public ProbeResult Execute<T>(T snapshot)
        {
            ChannelSnapshot data = snapshot as ChannelSnapshot;
            ProbeResult result;
            
            var probeData = new List<ProbeData>
            {
                new () {PropertyName = "UnacknowledgedMessages", PropertyValue = data.UnacknowledgedMessages.ToString()},
                new () {PropertyName = "PrefetchCount", PropertyValue = data.PrefetchCount.ToString()}
            };
            
            if (data.UnacknowledgedMessages > data.PrefetchCount)
            {
                _kb.TryGet(Id, ProbeResultStatus.Unhealthy, out var article);
                result = new UnhealthyProbeResult
                {
                    ParentComponentId = data.ConnectionIdentifier,
                    ComponentId = data.Identifier,
                    Id = Id,
                    Name = Name,
                    ComponentType = ComponentType,
                    Data = probeData,
                    KB = article
                };
            }
            else
            {
                _kb.TryGet(Id, ProbeResultStatus.Healthy, out var article);
                result = new HealthyProbeResult
                {
                    ParentComponentId = data.ConnectionIdentifier,
                    ComponentId = data.Identifier,
                    Id = Id,
                    Name = Name,
                    ComponentType = ComponentType,
                    Data = probeData,
                    KB = article
                };
            }

            NotifyObservers(result);
                
            return result;
        }
    }
}