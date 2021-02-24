namespace HareDu.Diagnostics.Tests.Probes
{
    using System.Collections.Generic;
    using Core.Extensions;
    using Diagnostics.Probes;
    using KnowledgeBase;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using Snapshotting.Model;

    [TestFixture]
    public class ChannelLimitReachedProbeTests
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
        public void Verify_probe_unhealthy_condition_1()
        {
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new ChannelLimitReachedProbe(knowledgeBaseProvider);
            
            ConnectionSnapshot snapshot = new ()
            {
                Identifier = "Connection1",
                OpenChannelsLimit = 2,
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
                        Identifier = "Channel0"
                    },
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
                    }
                }
            };

            var result = probe.Execute(snapshot);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(ProbeResultStatus.Unhealthy, result.Status);
                Assert.AreEqual(typeof(ChannelLimitReachedProbe).GetIdentifier(), result.KB.Id);
            });
        }

        [Test]
        public void Verify_probe_unhealthy_condition_2()
        {
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new ChannelLimitReachedProbe(knowledgeBaseProvider);
            
            ConnectionSnapshot snapshot = new ()
            {
                Identifier = "Connection1",
                OpenChannelsLimit = 3,
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
                        Identifier = "Channel0"
                    },
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
                    }
                }
            };

            var result = probe.Execute(snapshot);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(ProbeResultStatus.Unhealthy, result.Status);
                Assert.AreEqual(typeof(ChannelLimitReachedProbe).GetIdentifier(), result.KB.Id);
            });
        }

        [Test]
        public void Verify_probe_healthy_condition()
        {
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new ChannelLimitReachedProbe(knowledgeBaseProvider);

            ConnectionSnapshot snapshot = new ()
            {
                Identifier = "Connection1",
                OpenChannelsLimit = 3,
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
                        Identifier = "Channel0"
                    },
                    new()
                    {
                        PrefetchCount = 4,
                        UncommittedAcknowledgements = 2,
                        UncommittedMessages = 5,
                        UnconfirmedMessages = 8,
                        UnacknowledgedMessages = 6,
                        Consumers = 1,
                        Identifier = "Channel1"
                    }
                }
            };

            var result = probe.Execute(snapshot);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(ProbeResultStatus.Healthy, result.Status);
                Assert.AreEqual(typeof(ChannelLimitReachedProbe).GetIdentifier(), result.KB.Id);
            });
        }
    }
}