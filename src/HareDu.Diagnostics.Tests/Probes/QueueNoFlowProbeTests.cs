namespace HareDu.Diagnostics.Tests.Probes;

using Core.Extensions;
using Diagnostics.Probes;
using KnowledgeBase;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Snapshotting.Model;

[TestFixture]
public class QueueNoFlowProbeTests
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
        var probe = new QueueNoFlowProbe(knowledgeBaseProvider);

        QueueSnapshot snapshot = new () {Messages = new () {Incoming = new (){Total = 0}}};

        var result = probe.Execute(snapshot);
            
        Assert.Multiple(() =>
        {
            Assert.AreEqual(ProbeResultStatus.Unhealthy, result.Status);
            Assert.AreEqual(typeof(QueueNoFlowProbe).GetIdentifier(), result.KB.Id);
        });
    }

    [Test]
    public void Verify_probe_healthy_condition()
    {
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new QueueNoFlowProbe(knowledgeBaseProvider);
            
        QueueSnapshot snapshot = new () {Messages = new () {Incoming = new () {Total = 1}}};

        var result = probe.Execute(snapshot);
            
        Assert.Multiple(() =>
        {
            Assert.AreEqual(ProbeResultStatus.Healthy, result.Status);
            Assert.AreEqual(typeof(QueueNoFlowProbe).GetIdentifier(), result.KB.Id);
        });
    }
}