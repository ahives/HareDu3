namespace HareDu.Diagnostics.Tests.Probes;

using Core.Extensions;
using Diagnostics.Probes;
using KnowledgeBase;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;
using Snapshotting.Model;

[TestFixture]
public class QueueGrowthProbeTests
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
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new QueueGrowthProbe(knowledgeBaseProvider);
            
        QueueSnapshot snapshot = new () {Messages = new () {Incoming = new () {Total = 103283, Rate = 8734.5M}, Acknowledged = new () {Total = 823983, Rate = 8423.5M}}};

        var result = probe.Execute(snapshot);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(ProbeResultStatus.Warning));
            Assert.That(result.KB.Id, Is.EqualTo(typeof(QueueGrowthProbe).GetIdentifier()));
        });
    }

    [Test]
    public void Verify_probe_healthy_condition()
    {
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new QueueGrowthProbe(knowledgeBaseProvider);
            
        QueueSnapshot snapshot = new () {Messages = new () {Incoming = new () {Total = 103283, Rate = 8423.5M}, Acknowledged = new () {Total = 823983, Rate = 8734.5M}}};

        var result = probe.Execute(snapshot);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(ProbeResultStatus.Healthy));
            Assert.That(result.KB.Id, Is.EqualTo(typeof(QueueGrowthProbe).GetIdentifier()));
        });
    }
}