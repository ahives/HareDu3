namespace HareDu.Diagnostics.Tests.Probes
{
    using Core.Extensions;
    using Diagnostics.Probes;
    using KnowledgeBase;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using Snapshotting.Model;

    [TestFixture]
    public class UnlimitedPrefetchCountProbeTests
    {
        ServiceProvider _services;

        [OneTimeSetUp]
        public void Init()
        {
            _services = new ServiceCollection()
                .AddSingleton<IKnowledgeBaseProvider, KnowledgeBaseProvider>()
                .BuildServiceProvider();
        }

        [Test(Description = "")]
        public void Verify_probe_warning_condition()
        {
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new UnlimitedPrefetchCountProbe(knowledgeBaseProvider);

            ChannelSnapshot snapshot = new () {PrefetchCount = 0};

            var result = probe.Execute(snapshot);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(ProbeResultStatus.Warning, result.Status);
                Assert.AreEqual(typeof(UnlimitedPrefetchCountProbe).GetIdentifier(), result.KB.Id);
            });
        }

        [Test]
        public void Verify_probe_inconclusive_condition_1()
        {
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new UnlimitedPrefetchCountProbe(knowledgeBaseProvider);

            ChannelSnapshot snapshot = new () {PrefetchCount = 5};

            var result = probe.Execute(snapshot);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(ProbeResultStatus.Inconclusive, result.Status);
                Assert.AreEqual(typeof(UnlimitedPrefetchCountProbe).GetIdentifier(), result.KB.Id);
            });
        }
    }
}