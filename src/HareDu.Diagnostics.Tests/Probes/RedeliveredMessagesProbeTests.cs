namespace HareDu.Diagnostics.Tests.Probes;

using Core.Configuration;
using Core.Extensions;
using Diagnostics.Probes;
using KnowledgeBase;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Snapshotting.Model;

[TestFixture]
public class RedeliveredMessagesProbeTests
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
        HareDuConfig config = new () {Diagnostics = new () {Probes = new () {MessageRedeliveryThresholdCoefficient = 0.50M}}};
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new RedeliveredMessagesProbe(config.Diagnostics, knowledgeBaseProvider);
            
        QueueSnapshot snapshot = new () {Messages = new () {Incoming = new (){Total = 100, Rate = 54.4M}, Redelivered = new (){Total = 90, Rate = 32.3M}}};

        var result = probe.Execute(snapshot);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(ProbeResultStatus.Warning));
            Assert.That(result.KB.Id, Is.EqualTo(typeof(RedeliveredMessagesProbe).GetIdentifier()));
        });
    }

    [Test]
    public void Verify_probe_healthy_condition()
    {
        HareDuConfig config = new () {Diagnostics = new () {Probes = new () {MessageRedeliveryThresholdCoefficient = 0.50M}}};
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new RedeliveredMessagesProbe(config.Diagnostics, knowledgeBaseProvider);
            
        QueueSnapshot snapshot = new () {Messages = new () {Incoming = new (){Total = 100, Rate = 54.4M}, Redelivered = new (){Total = 40, Rate = 32.3M}}};

        var result = probe.Execute(snapshot);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(ProbeResultStatus.Healthy));
            Assert.That(result.KB.Id, Is.EqualTo(typeof(RedeliveredMessagesProbe).GetIdentifier()));
        });
    }

    [Test]
    public void Verify_probe_na()
    {
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new RedeliveredMessagesProbe(null, knowledgeBaseProvider);
            
        QueueSnapshot snapshot = new () {Messages = new () {Incoming = new (){Total = 100, Rate = 54.4M}, Redelivered = new (){Total = 90, Rate = 32.3M}}};

        var result = probe.Execute(snapshot);
            
        Assert.That(result.Status, Is.EqualTo(ProbeResultStatus.NA));
    }
}