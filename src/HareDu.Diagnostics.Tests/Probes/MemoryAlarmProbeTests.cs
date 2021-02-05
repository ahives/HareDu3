namespace HareDu.Diagnostics.Tests.Probes
{
    using Core.Extensions;
    using Diagnostics.Probes;
    using KnowledgeBase;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using Shouldly;
    using Snapshotting.Model;

    [TestFixture]
    public class MemoryAlarmProbeTests
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
            var probe = new MemoryAlarmProbe(knowledgeBaseProvider);
            
            MemorySnapshot snapshot = new () {Used = 103283, Limit = 823983, AlarmInEffect = true};

            var result = probe.Execute(snapshot);
            
            result.Status.ShouldBe(ProbeResultStatus.Unhealthy);
            result.KB.Id.ShouldBe(typeof(MemoryAlarmProbe).GetIdentifier());
        }

        [Test]
        public void Verify_probe_healthy_condition()
        {
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new MemoryAlarmProbe(knowledgeBaseProvider);

            MemorySnapshot snapshot = new () {Used = 103283, Limit = 823983, AlarmInEffect = false};

            var result = probe.Execute(snapshot);
            
            result.Status.ShouldBe(ProbeResultStatus.Healthy);
            result.KB.Id.ShouldBe(typeof(MemoryAlarmProbe).GetIdentifier());
        }
    }
}