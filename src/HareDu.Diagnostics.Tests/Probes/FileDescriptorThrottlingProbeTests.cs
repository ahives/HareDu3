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
public class FileDescriptorThrottlingProbeTests
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
        HareDuConfig config = new () {Diagnostics = new () {Probes = new () {FileDescriptorUsageThresholdCoefficient = 0.65M}}};
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new FileDescriptorThrottlingProbe(config.Diagnostics, knowledgeBaseProvider);

        OperatingSystemSnapshot snapshot = new () {FileDescriptors = new () {Available = 100, Used = 90}};

        var result = probe.Execute(snapshot);

        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(ProbeResultStatus.Warning));
            Assert.That(result.KB.Id, Is.EqualTo(typeof(FileDescriptorThrottlingProbe).GetIdentifier()));
        });
    }

    [Test]
    public void Verify_probe_unhealthy_condition()
    {
        HareDuConfig config = new () {Diagnostics = new () {Probes = new () {FileDescriptorUsageThresholdCoefficient = 0.65M}}};
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new FileDescriptorThrottlingProbe(config.Diagnostics, knowledgeBaseProvider);

        OperatingSystemSnapshot snapshot = new () {FileDescriptors = new () {Available = 100, Used = 100}};

        var result = probe.Execute(snapshot);

        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(ProbeResultStatus.Unhealthy));
            Assert.That(result.KB.Id, Is.EqualTo(typeof(FileDescriptorThrottlingProbe).GetIdentifier()));
        });
    }

    [Test]
    public void Verify_probe_healthy_condition()
    {
        HareDuConfig config = new () {Diagnostics = new () {Probes = new () {FileDescriptorUsageThresholdCoefficient = 0.65M}}};
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new FileDescriptorThrottlingProbe(config.Diagnostics, knowledgeBaseProvider);

        OperatingSystemSnapshot snapshot = new () {FileDescriptors = new () {Available = 100, Used = 60}};

        var result = probe.Execute(snapshot);

        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(ProbeResultStatus.Healthy));
            Assert.That(result.KB.Id, Is.EqualTo(typeof(FileDescriptorThrottlingProbe).GetIdentifier()));
        });
    }

    [Test]
    public void Verify_probe_na()
    {
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new FileDescriptorThrottlingProbe(null, knowledgeBaseProvider);

        OperatingSystemSnapshot snapshot = new () {FileDescriptors = new () {Available = 100, Used = 60}};

        var result = probe.Execute(snapshot);

        Assert.That(result.Status, Is.EqualTo(ProbeResultStatus.NA));
    }
}