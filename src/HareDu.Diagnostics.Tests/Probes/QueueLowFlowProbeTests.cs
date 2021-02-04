namespace HareDu.Diagnostics.Tests.Probes
{
    using Core.Configuration;
    using Core.Extensions;
    using Diagnostics.Probes;
    using KnowledgeBase;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using Shouldly;
    using Snapshotting.Model;

    [TestFixture]
    public class QueueLowFlowProbeTests
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
            HareDuConfig config = new () {Diagnostics = new () {Probes = new () {QueueLowFlowThreshold = 20}}};
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new QueueLowFlowProbe(config.Diagnostics, knowledgeBaseProvider);

            QueueSnapshot snapshot = new () {Messages = new () {Incoming = new () {Total = 20}}};

            var result = probe.Execute(snapshot);
            
            result.Status.ShouldBe(ProbeResultStatus.Unhealthy);
            result.KB.Id.ShouldBe(typeof(QueueLowFlowProbe).GetIdentifier());
        }

        [Test]
        public void Verify_probe_healthy_condition()
        {
            HareDuConfig config = new () {Diagnostics = new () {Probes = new () {QueueLowFlowThreshold = 20}}};
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new QueueLowFlowProbe(config.Diagnostics, knowledgeBaseProvider);
            
            QueueSnapshot snapshot = new () {Messages = new () {Incoming = new () {Total = 100}}};

            var result = probe.Execute(snapshot);
            
            result.Status.ShouldBe(ProbeResultStatus.Healthy);
            result.KB.Id.ShouldBe(typeof(QueueLowFlowProbe).GetIdentifier());
        }

        [Test]
        public void Verify_probe_na()
        {
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new QueueLowFlowProbe(null, knowledgeBaseProvider);
            
            QueueSnapshot snapshot = new () {Messages = new () {Incoming = new () {Total = 100}}};

            var result = probe.Execute(snapshot);
            
            result.Status.ShouldBe(ProbeResultStatus.NA);
            result.KB.Id.ShouldBe(typeof(QueueLowFlowProbe).GetIdentifier());
        }
    }
}