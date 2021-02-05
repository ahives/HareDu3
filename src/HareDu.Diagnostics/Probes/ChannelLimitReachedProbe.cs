namespace HareDu.Diagnostics.Probes
{
    using System;
    using System.Collections.Generic;
    using Core.Extensions;
    using KnowledgeBase;
    using Snapshotting.Model;

    public class ChannelLimitReachedProbe :
        BaseDiagnosticProbe,
        DiagnosticProbe
    {
        public string Id => GetType().GetIdentifier();
        public string Name => "Channel Limit Reached Probe";
        public string Description => "Measures actual number of channels to the defined limit on connection";
        public ComponentType ComponentType => ComponentType.Connection;
        public ProbeCategory Category => ProbeCategory.Throughput;

        public ChannelLimitReachedProbe(IKnowledgeBaseProvider kb)
            : base(kb)
        {
        }

        public ProbeResult Execute<T>(T snapshot)
        {
            ConnectionSnapshot data = snapshot as ConnectionSnapshot;
            ProbeResult result;

            var probeData = new List<ProbeData>
            {
                new () {PropertyName = "Channels.Count", PropertyValue = data.Channels.Count.ToString()},
                new () {PropertyName = "OpenChannelLimit", PropertyValue = data.OpenChannelsLimit.ToString()}
            };
            
            if (Convert.ToUInt64(data.Channels.Count) >= data.OpenChannelsLimit)
            {
                _kb.TryGet(Id, ProbeResultStatus.Unhealthy, out var article);
                result = new UnhealthyProbeResult
                {
                    ParentComponentId = data.NodeIdentifier,
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
                    ParentComponentId = data.NodeIdentifier,
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