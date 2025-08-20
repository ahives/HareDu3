namespace HareDu.Diagnostics.Tests.Probes;

using Core.Configuration;
using Core.Extensions;
using Diagnostics.Probes;
using KnowledgeBase;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;
using Snapshotting.Model;

[TestFixture]
public class RuntimeProcessLimitProbeTests
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
    public void Verify_probe_unhealthy_condition_1()
    {
        HareDuConfig config = new () {Diagnostics = new () {Probes = new () {RuntimeProcessUsageThresholdCoefficient = 0.65M}}};
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new RuntimeProcessLimitProbe(config.Diagnostics, knowledgeBaseProvider);

        BrokerRuntimeSnapshot snapshot = new () {Processes = new () {Limit = 3, Used = 3, UsageRate = 3.2M}};

        var result = probe.Execute(snapshot);

        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(ProbeResultStatus.Unhealthy));
            Assert.That(result.KB.Id, Is.EqualTo(typeof(RuntimeProcessLimitProbe).GetIdentifier()));
        });
    }

    [Test(Description = "")]
    public void Verify_probe_unhealthy_condition_2()
    {
        HareDuConfig config = new () {Diagnostics = new () {Probes = new () {RuntimeProcessUsageThresholdCoefficient = 0.65M}}};
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new RuntimeProcessLimitProbe(config.Diagnostics, knowledgeBaseProvider);

        BrokerRuntimeSnapshot snapshot = new () {Processes = new () {Limit = 3, Used = 4, UsageRate = 3.2M}};

        var result = probe.Execute(snapshot);

        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(ProbeResultStatus.Unhealthy));
            Assert.That(result.KB.Id, Is.EqualTo(typeof(RuntimeProcessLimitProbe).GetIdentifier()));
        });
    }

    [Test]
    public void Verify_probe_healthy_condition()
    {
        HareDuConfig config = new () {Diagnostics = new () {Probes = new () {RuntimeProcessUsageThresholdCoefficient = 0.65M}}};
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new RuntimeProcessLimitProbe(config.Diagnostics, knowledgeBaseProvider);

        BrokerRuntimeSnapshot snapshot = new () {Processes = new () {Limit = 100, Used = 40, UsageRate = 3.2M}};

        var result = probe.Execute(snapshot);

        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(ProbeResultStatus.Healthy));
            Assert.That(result.KB.Id, Is.EqualTo(typeof(RuntimeProcessLimitProbe).GetIdentifier()));
        });
    }

    [Test]
    public void Verify_probe_warning_condition()
    {
        HareDuConfig config = new () {Diagnostics = new () {Probes = new () {RuntimeProcessUsageThresholdCoefficient = 0.65M}}};
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new RuntimeProcessLimitProbe(config.Diagnostics, knowledgeBaseProvider);

        BrokerRuntimeSnapshot snapshot = new () {Processes = new () {Limit = 4, Used = 3, UsageRate = 3.2M}};

        var result = probe.Execute(snapshot);

        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(ProbeResultStatus.Warning));
            Assert.That(result.KB.Id, Is.EqualTo(typeof(RuntimeProcessLimitProbe).GetIdentifier()));
        });
    }

    [Test]
    public void Verify_probe_na()
    {
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new RuntimeProcessLimitProbe(null, knowledgeBaseProvider);

        BrokerRuntimeSnapshot snapshot = new () {Processes = new () {Limit = 4, Used = 3, UsageRate = 3.2M}};

        var result = probe.Execute(snapshot);

        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(ProbeResultStatus.NA));
            Assert.That(result.KB.Id, Is.EqualTo(typeof(RuntimeProcessLimitProbe).GetIdentifier()));
        });
    }
}