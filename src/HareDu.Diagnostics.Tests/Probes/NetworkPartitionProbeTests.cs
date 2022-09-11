namespace HareDu.Diagnostics.Tests.Probes;

using System.Collections.Generic;
using Core.Extensions;
using Diagnostics.Probes;
using KnowledgeBase;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Snapshotting.Model;

[TestFixture]
public class NetworkPartitionProbeTests
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
        var probe = new NetworkPartitionProbe(knowledgeBaseProvider);

        NodeSnapshot snapshot = new () {NetworkPartitions = new List<string>()
        {
            "node1@rabbitmq",
            "node2@rabbitmq",
            "node3@rabbitmq"
        }};

        var result = probe.Execute(snapshot);
            
        Assert.Multiple(() =>
        {
            Assert.AreEqual(ProbeResultStatus.Unhealthy, result.Status);
            Assert.AreEqual(typeof(NetworkPartitionProbe).GetIdentifier(), result.KB.Id);
        });
    }

    [Test]
    public void Verify_probe_healthy_condition()
    {
        var knowledgeBaseProvider = _services.GetService<IKnowledgeBaseProvider>();
        var probe = new NetworkPartitionProbe(knowledgeBaseProvider);

        NodeSnapshot snapshot = new () {NetworkPartitions = new List<string>()};

        var result = probe.Execute(snapshot);
            
        Assert.Multiple(() =>
        {
            Assert.AreEqual(ProbeResultStatus.Healthy, result.Status);
            Assert.AreEqual(typeof(NetworkPartitionProbe).GetIdentifier(), result.KB.Id);
        });
    }
}