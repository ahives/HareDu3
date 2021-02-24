namespace HareDu.Diagnostics.Tests.Probes
{
    using Core.Configuration;
    using Core.Extensions;
    using Diagnostics.Probes;
    using KnowledgeBase;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using Snapshotting.Model;

    [TestFixture]
    public class ConsumerUtilizationProbeTests
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
        public void Verify_probe_warning_condition()
        {
            HareDuConfig config = new () {Diagnostics = new () {Probes = new () {ConsumerUtilizationThreshold = 0.50M}}};
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new ConsumerUtilizationProbe(config.Diagnostics, knowledgeBaseProvider);

            QueueSnapshot snapshot = new () {ConsumerUtilization = 0.5M};

            var result = probe.Execute(snapshot);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(ProbeResultStatus.Warning, result.Status);
                Assert.AreEqual(typeof(ConsumerUtilizationProbe).GetIdentifier(), result.KB.Id);
            });
        }

        [Test]
        public void Verify_probe_unhealthy_condition()
        {
            HareDuConfig config = new () {Diagnostics = new () {Probes = new () {ConsumerUtilizationThreshold = 0.50M}}};
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new ConsumerUtilizationProbe(config.Diagnostics, knowledgeBaseProvider);

            QueueSnapshot snapshot = new () {ConsumerUtilization = 0.4M};

            var result = probe.Execute(snapshot);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(ProbeResultStatus.Unhealthy, result.Status);
                Assert.AreEqual(typeof(ConsumerUtilizationProbe).GetIdentifier(), result.KB.Id);
            });
        }

        [Test]
        public void Verify_probe_healthy_condition()
        {
            HareDuConfig config = new () {Diagnostics = new () {Probes = new () {ConsumerUtilizationThreshold = 0.50M}}};
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new ConsumerUtilizationProbe(config.Diagnostics, knowledgeBaseProvider);

            QueueSnapshot snapshot = new () {ConsumerUtilization = 1.0M};

            var result = probe.Execute(snapshot);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(ProbeResultStatus.Healthy, result.Status);
                Assert.AreEqual(typeof(ConsumerUtilizationProbe).GetIdentifier(), result.KB.Id);
            });
        }
    }
}