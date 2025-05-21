namespace HareDu.Diagnostics.Tests.Probes;

using Core.Extensions;
using Diagnostics.Probes;
using KnowledgeBase;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Snapshotting.Model;

[TestFixture]
public class UnroutableMessageProbeTests
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
        var probe = new UnroutableMessageProbe(knowledgeBaseProvider);

        BrokerQueuesSnapshot snapshot = new() {Churn = new() {NotRouted = new() {Total = 2}}};

        var result = probe.Execute(snapshot);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(ProbeResultStatus.Unhealthy));
            Assert.That(result.KB.Id, Is.EqualTo(typeof(UnroutableMessageProbe).GetIdentifier()));
        });
    }

    [Test]
    public void Verify_probe_healthy_condition()
    {
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new UnroutableMessageProbe(knowledgeBaseProvider);

        BrokerQueuesSnapshot snapshot = new() {Churn = new() {NotRouted = new() {Total = 0}}};

        var result = probe.Execute(snapshot);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(ProbeResultStatus.Healthy));
            Assert.That(result.KB.Id, Is.EqualTo(typeof(UnroutableMessageProbe).GetIdentifier()));
        });
    }
}