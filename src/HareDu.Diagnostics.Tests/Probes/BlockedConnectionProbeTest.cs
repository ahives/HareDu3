namespace HareDu.Diagnostics.Tests.Probes;

using Core.Extensions;
using Diagnostics.Probes;
using HareDu.Model;
using KnowledgeBase;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;
using Snapshotting.Model;

[TestFixture]
public class BlockedConnectionProbeTest
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
        var probe = new BlockedConnectionProbe(knowledgeBaseProvider);

        ConnectionSnapshot snapshot = new () {State = BrokerConnectionState.Blocked};

        var result = probe.Execute(snapshot);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(ProbeResultStatus.Unhealthy));
            Assert.That(result.KB.Id, Is.EqualTo(typeof(BlockedConnectionProbe).GetIdentifier()));
        });
    }

    [Test]
    public void Verify_probe_healthy_condition()
    {
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new BlockedConnectionProbe(knowledgeBaseProvider);
            
        ConnectionSnapshot snapshot = new () {State = BrokerConnectionState.Running};

        var result = probe.Execute(snapshot);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(ProbeResultStatus.Healthy));
            Assert.That(result.KB.Id, Is.EqualTo(typeof(BlockedConnectionProbe).GetIdentifier()));
        });
    }
}