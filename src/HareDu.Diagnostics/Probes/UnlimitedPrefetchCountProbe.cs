namespace HareDu.Diagnostics.Probes
{
    using System.Collections.Generic;
    using Core.Extensions;
    using KnowledgeBase;
    using Snapshotting.Model;

    public class UnlimitedPrefetchCountProbe :
        BaseDiagnosticProbe,
        DiagnosticProbe
    {
        public string Id => GetType().GetIdentifier();
        public string Name => "Unlimited Prefetch Count Probe";
        public string Description { get; }
        public ComponentType ComponentType => ComponentType.Channel;
        public ProbeCategory Category => ProbeCategory.Throughput;

        public UnlimitedPrefetchCountProbe(IKnowledgeBaseProvider kb)
            : base(kb)
        {
        }

        public ProbeResult Execute<T>(T snapshot)
        {
            ChannelSnapshot data = snapshot as ChannelSnapshot;
            ProbeResult result;
            
            var probeData = new List<ProbeData>
            {
                new () {PropertyName = "PrefetchCount", PropertyValue = data.PrefetchCount.ToString()}
            };

            if (data.PrefetchCount == 0)
            {
                _kb.TryGet(Id, ProbeResultStatus.Warning, out var article);
                result = new WarningProbeResult
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
                _kb.TryGet(Id, ProbeResultStatus.Inconclusive, out var article);
                result = new InconclusiveProbeResult
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