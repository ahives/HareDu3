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
    public class DiskAlarmProbeTests
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
            var probe = new DiskAlarmProbe(knowledgeBaseProvider);
            
            DiskSnapshot snapshot = new () {AlarmInEffect = true, Capacity = new () {Available = 10283, Rate = 83.9M}};

            var result = probe.Execute(snapshot);
            
            result.Status.ShouldBe(ProbeResultStatus.Unhealthy);
            result.KB.Id.ShouldBe(typeof(DiskAlarmProbe).GetIdentifier());
        }

        [Test]
        public void Verify_probe_healthy_condition()
        {
            var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
            var probe = new DiskAlarmProbe(knowledgeBaseProvider);

            DiskSnapshot snapshot = new () {AlarmInEffect = false, Capacity = new () {Available = 10283, Rate = 74.3M}};

            var result = probe.Execute(snapshot);
            
            result.Status.ShouldBe(ProbeResultStatus.Healthy);
            result.KB.Id.ShouldBe(typeof(DiskAlarmProbe).GetIdentifier());
        }
    }
}