namespace HareDu.Diagnostics.Tests.Probes
{
    using System.Collections.Generic;
    using Core.Configuration;
    using Core.Extensions;
    using Diagnostics.Probes;
    using KnowledgeBase;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using Shouldly;
    using Snapshotting.Model;

    [TestFixture]
    public class HighConnectionCreationRateProbeTests
    {
        ServiceProvider _services;

        [OneTimeSetUp]
        public void Init()
        {
            _services = new ServiceCollection()
                .AddSingleton<IKnowledgeBaseProvider, KnowledgeBaseProvider>()
                .BuildServiceProvider();
        }

        [Test]
        public void Verify_probe_warning_condition_1()
        {
            HareDuConfig config = new () {Diagnostics = new () {Probes = new () {HighConnectionCreationRateThreshold = 100}}};
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new HighConnectionCreationRateProbe(config.Diagnostics, knowledgeBaseProvider);
            
            BrokerConnectivitySnapshot snapshot = GetSnapshot(102, 100);

            var result = probe.Execute(snapshot);
            
            result.Status.ShouldBe(ProbeResultStatus.Warning);
            result.KB.Id.ShouldBe(typeof(HighConnectionCreationRateProbe).GetIdentifier());
        }

        [Test]
        public void Verify_probe_warning_condition_2()
        {
            HareDuConfig config = new () {Diagnostics = new () {Probes = new () {HighConnectionCreationRateThreshold = 100}}};
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new HighConnectionCreationRateProbe(config.Diagnostics, knowledgeBaseProvider);
            
            BrokerConnectivitySnapshot snapshot = GetSnapshot(100, 100);

            var result = probe.Execute(snapshot);
            
            result.Status.ShouldBe(ProbeResultStatus.Warning);
            result.KB.Id.ShouldBe(typeof(HighConnectionCreationRateProbe).GetIdentifier());
        }

        [Test]
        public void Verify_probe_healthy_condition()
        {
            HareDuConfig config = new () {Diagnostics = new () {Probes = new () {HighConnectionCreationRateThreshold = 100}}};
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new HighConnectionCreationRateProbe(config.Diagnostics, knowledgeBaseProvider);
            
            BrokerConnectivitySnapshot snapshot = GetSnapshot(99, 100);

            var result = probe.Execute(snapshot);
            
            result.Status.ShouldBe(ProbeResultStatus.Healthy);
            result.KB.Id.ShouldBe(typeof(HighConnectionCreationRateProbe).GetIdentifier());
        }

        [Test]
        public void Verify_probe_na()
        {
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new HighConnectionCreationRateProbe(null, knowledgeBaseProvider);
            
            BrokerConnectivitySnapshot snapshot = GetSnapshot(99, 100);

            var result = probe.Execute(snapshot);
            
            result.Status.ShouldBe(ProbeResultStatus.NA);
        }

        BrokerConnectivitySnapshot GetSnapshot(decimal connectionsCreatedRate, decimal connectionsClosedRate) =>
            new()
            {
                ConnectionsCreated = new() {Total = 100000, Rate = connectionsCreatedRate},
                ConnectionsClosed = new() {Total = 174000, Rate = connectionsClosedRate},
                Connections = new List<ConnectionSnapshot>
                {
                    new()
                    {
                        Identifier = "Connection1",
                        OpenChannelsLimit = 6,
                        Channels = new List<ChannelSnapshot>
                        {
                            new()
                            {
                                PrefetchCount = 4,
                                UncommittedAcknowledgements = 2,
                                UncommittedMessages = 5,
                                UnconfirmedMessages = 8,
                                UnacknowledgedMessages = 6,
                                Consumers = 1,
                                Identifier = "Channel1"
                            },
                            new()
                            {
                                PrefetchCount = 4,
                                UncommittedAcknowledgements = 2,
                                UncommittedMessages = 5,
                                UnconfirmedMessages = 8,
                                UnacknowledgedMessages = 6,
                                Consumers = 1,
                                Identifier = "Channel2"
                            },
                            new()
                            {
                                PrefetchCount = 4,
                                UncommittedAcknowledgements = 2,
                                UncommittedMessages = 5,
                                UnconfirmedMessages = 8,
                                UnacknowledgedMessages = 6,
                                Consumers = 1,
                                Identifier = "Channel3"
                            },
                            new()
                            {
                                PrefetchCount = 4,
                                UncommittedAcknowledgements = 2,
                                UncommittedMessages = 5,
                                UnconfirmedMessages = 8,
                                UnacknowledgedMessages = 6,
                                Consumers = 1,
                                Identifier = "Channel4"
                            },
                            new()
                            {
                                PrefetchCount = 4,
                                UncommittedAcknowledgements = 2,
                                UncommittedMessages = 5,
                                UnconfirmedMessages = 8,
                                UnacknowledgedMessages = 6,
                                Consumers = 1,
                                Identifier = "Channel5"
                            },
                            new()
                            {
                                PrefetchCount = 4,
                                UncommittedAcknowledgements = 2,
                                UncommittedMessages = 5,
                                UnconfirmedMessages = 8,
                                UnacknowledgedMessages = 6,
                                Consumers = 1,
                                Identifier = "Channel6"
                            }
                        }
                    }
                }
            };
    }
}