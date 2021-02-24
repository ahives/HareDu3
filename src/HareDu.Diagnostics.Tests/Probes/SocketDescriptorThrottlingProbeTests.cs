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
    public class SocketDescriptorThrottlingProbeTests
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
            HareDuConfig config = new () {Diagnostics = new () {Probes = new () {SocketUsageThresholdCoefficient = 0.60M}}};
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new SocketDescriptorThrottlingProbe(config.Diagnostics, knowledgeBaseProvider);

            NodeSnapshot snapshot = new (){OS = new () {SocketDescriptors = new () {Available = 10, Used = 9, UsageRate = 4.2M}}};

            var result = probe.Execute(snapshot);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(ProbeResultStatus.Warning, result.Status);
                Assert.AreEqual(typeof(SocketDescriptorThrottlingProbe).GetIdentifier(), result.KB.Id);
            });
        }

        [Test(Description = "When sockets used >= calculated high watermark and calculated high watermark >= max sockets available")]
        public void Verify_probe_unhealthy_condition()
        {
            HareDuConfig config = new () {Diagnostics = new () {Probes = new () {SocketUsageThresholdCoefficient = 0.60M}}};
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new SocketDescriptorThrottlingProbe(config.Diagnostics, knowledgeBaseProvider);

            NodeSnapshot snapshot = new (){OS = new () {SocketDescriptors = new () {Available = 10, Used = 10, UsageRate = 4.2M}}};

            var result = probe.Execute(snapshot);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(ProbeResultStatus.Unhealthy, result.Status);
                Assert.AreEqual(typeof(SocketDescriptorThrottlingProbe).GetIdentifier(), result.KB.Id);
            });
        }

        [Test]
        public void Verify_probe_healthy_condition()
        {
            HareDuConfig config = new () {Diagnostics = new () {Probes = new () {SocketUsageThresholdCoefficient = 0.60M}}};
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new SocketDescriptorThrottlingProbe(config.Diagnostics, knowledgeBaseProvider);
            
            NodeSnapshot snapshot = new (){OS = new () {SocketDescriptors = new () {Available = 10, Used = 4, UsageRate = 4.2M}}};

            var result = probe.Execute(snapshot);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(ProbeResultStatus.Healthy, result.Status);
                Assert.AreEqual(typeof(SocketDescriptorThrottlingProbe).GetIdentifier(), result.KB.Id);
            });
        }

        [Test]
        public void Verify_probe_na()
        {
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new SocketDescriptorThrottlingProbe(null, knowledgeBaseProvider);
            
            NodeSnapshot snapshot = new (){OS = new () {SocketDescriptors = new () {Available = 10, Used = 4, UsageRate = 4.2M}}};

            var result = probe.Execute(snapshot);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(ProbeResultStatus.NA, result.Status);
                Assert.AreEqual(typeof(SocketDescriptorThrottlingProbe).GetIdentifier(), result.KB.Id);
            });
        }
    }
}