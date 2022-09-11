namespace HareDu.Diagnostics.Tests.Probes;

using Core.Configuration;
using Core.Extensions;
using Diagnostics.Probes;
using KnowledgeBase;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Snapshotting.Model;

[TestFixture]
public class QueueHighFlowProbesTests
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
        HareDuConfig config = new () {Diagnostics = new () {Probes = new () {QueueHighFlowThreshold = 100}}};
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new QueueHighFlowProbe(config.Diagnostics, knowledgeBaseProvider);

        QueueSnapshot snapshot = new () {Messages = new () {Incoming = new () {Total = 105}}};

        var result = probe.Execute(snapshot);
            
        Assert.Multiple(() =>
        {
            Assert.AreEqual(ProbeResultStatus.Unhealthy, result.Status);
            Assert.AreEqual(typeof(QueueHighFlowProbe).GetIdentifier(), result.KB.Id);
        });
    }

    [Test]
    public void Verify_probe_healthy_condition()
    {
        HareDuConfig config = new () {Diagnostics = new () {Probes = new () {QueueHighFlowThreshold = 100}}};
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new QueueHighFlowProbe(config.Diagnostics, knowledgeBaseProvider);

        QueueSnapshot snapshot = new () {Messages = new () {Incoming = new () {Total = 90}}};

        var result = probe.Execute(snapshot);
            
        Assert.Multiple(() =>
        {
            Assert.AreEqual(ProbeResultStatus.Healthy, result.Status);
            Assert.AreEqual(typeof(QueueHighFlowProbe).GetIdentifier(), result.KB.Id);
        });
    }

    [Test]
    public void Verify_probe_na()
    {
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new QueueHighFlowProbe(null, knowledgeBaseProvider);

        QueueSnapshot snapshot = new () {Messages = new () {Incoming = new () {Total = 90}}};

        var result = probe.Execute(snapshot);
            
        Assert.AreEqual(ProbeResultStatus.NA, result.Status);
    }
}