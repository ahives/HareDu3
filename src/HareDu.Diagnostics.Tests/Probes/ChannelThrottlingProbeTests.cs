namespace HareDu.Diagnostics.Tests.Probes
{
    using Core.Extensions;
    using Diagnostics.Probes;
    using KnowledgeBase;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using Snapshotting.Model;

    [TestFixture]
    public class ChannelThrottlingProbeTests
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
        public void Verify_probe_unhealthy_condition()
        {
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new ChannelThrottlingProbe(knowledgeBaseProvider);

            ChannelSnapshot snapshot = new ()
            {
                PrefetchCount = 4,
                UncommittedAcknowledgements = 2,
                UncommittedMessages = 5,
                UnconfirmedMessages = 8,
                UnacknowledgedMessages = 6,
                Consumers = 1,
                Identifier = "Channel1"
            };

            var result = probe.Execute(snapshot);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(ProbeResultStatus.Unhealthy, result.Status);
                Assert.AreEqual(typeof(ChannelThrottlingProbe).GetIdentifier(), result.KB.Id);
            });
        }

        [Test]
        public void Verify_probe_healthy_condition()
        {
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new ChannelThrottlingProbe(knowledgeBaseProvider);

            ChannelSnapshot snapshot = new ChannelSnapshot()
            {
                PrefetchCount = 6,
                UncommittedAcknowledgements = 2,
                UncommittedMessages = 5,
                UnconfirmedMessages = 8,
                UnacknowledgedMessages = 4,
                Consumers = 1,
                Identifier = "Channel1"
            };

            var result = probe.Execute(snapshot);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(ProbeResultStatus.Healthy, result.Status);
                Assert.AreEqual(typeof(ChannelThrottlingProbe).GetIdentifier(), result.KB.Id);
            });
        }
    }
}