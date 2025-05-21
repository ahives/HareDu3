namespace HareDu.Diagnostics.Tests.Probes;

using Core.Extensions;
using Diagnostics.Probes;
using KnowledgeBase;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Snapshotting.Model;

[TestFixture]
public class MessagePagingProbeTests
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
    public void Verify_analyzer_unhealthy_condition()
    {
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new MessagePagingProbe(knowledgeBaseProvider);

        QueueSnapshot snapshot = new() {Memory = new() {PagedOut = new() {Total = 3}}};

        var result = probe.Execute(snapshot);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(ProbeResultStatus.Unhealthy));
            Assert.That(result.KB.Id, Is.EqualTo(typeof(MessagePagingProbe).GetIdentifier()));
        });
    }

    [Test]
    public void Verify_analyzer_healthy_condition()
    {
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new MessagePagingProbe(knowledgeBaseProvider);
            
        QueueSnapshot snapshot = new() {Memory = new() {PagedOut = new() {Total = 0}}};

        var result = probe.Execute(snapshot);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(ProbeResultStatus.Healthy));
            Assert.That(result.KB.Id, Is.EqualTo(typeof(MessagePagingProbe).GetIdentifier()));
        });
    }
}