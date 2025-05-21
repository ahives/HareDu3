namespace HareDu.Diagnostics.Tests.Analyzers;

using System;
using System.Collections.Generic;
using System.Linq;
using Core.Extensions;
using Diagnostics.Probes;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using NUnit.Framework;
using Snapshotting.Model;

[TestFixture]
public class ScannerResultAnalyzerTests
{
    ServiceProvider _services;

    [OneTimeSetUp]
    public void Init()
    {
        _services = new ServiceCollection()
            .AddHareDu()
            .BuildServiceProvider();
    }

    [Test]
    public void Verify_can_observe_analysis()
    {
        BrokerQueuesSnapshot snapshot = new ()
        {
            ClusterName = "Cluster 1",
            Churn = new ()
            {
                NotRouted = new () {Total = 5}
            },
            Queues = GetQueues().ToList()
        };

        var analyzer = _services.GetService<IScannerResultAnalyzer>();
        // .RegisterObserver(new FakeScannerAnalyzerObserver());
            
        var summary = _services.GetService<IScanner>()
            .Scan(snapshot)
            .Analyze(analyzer, x => x.ComponentType.ToString())
            .ScreenDump();
    }
        
    [Test]
    public void Verify_can_analyze_by_component_type()
    {
        BrokerQueuesSnapshot snapshot = new ()
        {
            ClusterName = "Cluster 1",
            Churn = new ()
            {
                NotRouted = new () {Total = 5}
            },
            Queues = GetQueues().ToList()
        };

        var analyzer = _services.GetService<IScannerResultAnalyzer>();
        var summary = _services.GetService<IScanner>()
            .Scan(snapshot)
            .Analyze(analyzer, x => x.ComponentType.ToString());
            
        Assert.Multiple(() =>
        {
            Assert.That(summary, Is.Not.Null);
            Assert.That(summary.Count, Is.EqualTo(2));
            Assert.That(summary.Any(x => x.Id == "Queue"), Is.True);

            var queueSummary = summary.SingleOrDefault(x => x.Id == "Queue");

            Assert.That(queueSummary, Is.Not.Null);
            Assert.That(queueSummary.Healthy.Total, Is.EqualTo(24));
            Assert.That(Decimal.Round(queueSummary.Healthy.Percentage, 2), Is.EqualTo(42.86M));
            Assert.That(queueSummary.Unhealthy.Total, Is.EqualTo(32));
            Assert.That(Decimal.Round(queueSummary.Unhealthy.Percentage, 2), Is.EqualTo(57.14M));
            Assert.That(queueSummary.Warning.Total, Is.EqualTo(0));
            Assert.That(queueSummary.Warning.Percentage, Is.EqualTo(0));
            Assert.That(queueSummary.Inconclusive.Total, Is.EqualTo(0));
            Assert.That(queueSummary.Inconclusive.Percentage, Is.EqualTo(0));
            Assert.That(summary.Any(x => x.Id == "Exchange"), Is.True);

            var exchangeSummary = summary.SingleOrDefault(x => x.Id == "Exchange");

            Assert.That(exchangeSummary, Is.Not.Null);
            Assert.That(exchangeSummary.Healthy.Total, Is.EqualTo(0));
            Assert.That(exchangeSummary.Healthy.Percentage, Is.EqualTo(0));
            Assert.That(exchangeSummary.Unhealthy.Total, Is.EqualTo(1));
            Assert.That(exchangeSummary.Unhealthy.Percentage, Is.EqualTo(100));
            Assert.That(exchangeSummary.Warning.Total, Is.EqualTo(0));
            Assert.That(exchangeSummary.Warning.Percentage, Is.EqualTo(0));
            Assert.That(exchangeSummary.Inconclusive.Total, Is.EqualTo(0));
            Assert.That(exchangeSummary.Inconclusive.Percentage, Is.EqualTo(0));
        });
    }
        
    [Test]
    public void Verify_can_analyze_by_parent_component()
    {
        BrokerQueuesSnapshot snapshot = new ()
        {
            ClusterName = "Cluster 1",
            Churn = new ()
            {
                NotRouted = new () {Total = 5}
            },
            Queues = GetQueues().ToList()
        };

        var summary = _services.GetService<IScanner>()
            .Scan(snapshot)
            .Analyze(_services.GetService<IScannerResultAnalyzer>(), x => x.ParentComponentId);
            
        Assert.Multiple(() =>
        {
            Assert.That(summary, Is.Not.Null);
            Assert.That(summary.Count, Is.EqualTo(2));
            Assert.That(summary.Any(x => x.Id == "Cluster 1"), Is.True);
            Assert.That(summary.Any(x => x.Id == "Node0"), Is.True);

            var nodeSummary = summary.SingleOrDefault(x => x.Id == "Node0");

            Assert.That(nodeSummary, Is.Not.Null);
            Assert.That(nodeSummary.Healthy.Total, Is.EqualTo(24));
            Assert.That(Decimal.Round(nodeSummary.Healthy.Percentage, 2), Is.EqualTo(42.86M));
            Assert.That(nodeSummary.Unhealthy.Total, Is.EqualTo(32));
            Assert.That(Decimal.Round(nodeSummary.Unhealthy.Percentage, 2), Is.EqualTo(57.14M));
            Assert.That(nodeSummary.Warning.Total, Is.EqualTo(0));
            Assert.That(nodeSummary.Warning.Percentage, Is.EqualTo(0));
            Assert.That(nodeSummary.Inconclusive.Total, Is.EqualTo(0));
            Assert.That(nodeSummary.Inconclusive.Percentage, Is.EqualTo(0));

            var clusterSummary = summary.SingleOrDefault(x => x.Id == "Cluster 1");

            Assert.That(clusterSummary, Is.Not.Null);
            Assert.That(clusterSummary.Healthy.Total, Is.EqualTo(0));
            Assert.That(clusterSummary.Healthy.Percentage, Is.EqualTo(0));
            Assert.That(clusterSummary.Unhealthy.Total, Is.EqualTo(1));
            Assert.That(clusterSummary.Unhealthy.Percentage, Is.EqualTo(100));
            Assert.That(clusterSummary.Warning.Total, Is.EqualTo(0));
            Assert.That(clusterSummary.Warning.Percentage, Is.EqualTo(0));
            Assert.That(clusterSummary.Inconclusive.Total, Is.EqualTo(0));
            Assert.That(clusterSummary.Inconclusive.Percentage, Is.EqualTo(0));
        });
    }
        
    [Test]
    public void Verify_can_analyze_by_probe()
    {
        BrokerQueuesSnapshot snapshot = new ()
        {
            ClusterName = "Cluster 1",
            Churn = new ()
            {
                NotRouted = new () {Total = 5}
            },
            Queues = GetQueues().ToList()
        };

        var summary = _services.GetService<IScanner>()
            .Scan(snapshot)
            .Analyze(_services.GetService<IScannerResultAnalyzer>(), x => x.Id);

        Assert.Multiple(() =>
        {
            Assert.That(summary, Is.Not.Null);
            Assert.That(summary.Count, Is.EqualTo(8));
            Assert.That(summary.Any(x => x.Id == typeof(UnroutableMessageProbe).GetIdentifier()), Is.True);

            var unroutableSummary = summary.SingleOrDefault(x => x.Id == typeof(UnroutableMessageProbe).GetIdentifier());

            Assert.That(unroutableSummary, Is.Not.Null);
            Assert.That(unroutableSummary.Healthy.Total, Is.EqualTo(0));
            Assert.That(unroutableSummary.Healthy.Percentage, Is.EqualTo(0));
            Assert.That(unroutableSummary.Unhealthy.Total, Is.EqualTo(1));
            Assert.That(unroutableSummary.Unhealthy.Percentage, Is.EqualTo(100));
            Assert.That(unroutableSummary.Warning.Total, Is.EqualTo(0));
            Assert.That(unroutableSummary.Warning.Percentage, Is.EqualTo(0));
            Assert.That(unroutableSummary.Inconclusive.Total, Is.EqualTo(0));
            Assert.That(unroutableSummary.Inconclusive.Percentage, Is.EqualTo(0));
            Assert.That(summary.Any(x => x.Id == typeof(MessagePagingProbe).GetIdentifier()), Is.True);

            var memoryPagedOutSummary = summary.SingleOrDefault(x => x.Id == typeof(MessagePagingProbe).GetIdentifier());

            Assert.That(memoryPagedOutSummary, Is.Not.Null);
            Assert.That(memoryPagedOutSummary.Healthy.Total, Is.EqualTo(5));
            Assert.That(memoryPagedOutSummary.Healthy.Percentage, Is.EqualTo(62.5M));
            Assert.That(memoryPagedOutSummary.Unhealthy.Total, Is.EqualTo(3));
            Assert.That(memoryPagedOutSummary.Unhealthy.Percentage, Is.EqualTo(37.5M));
            Assert.That(memoryPagedOutSummary.Warning.Total, Is.EqualTo(0));
            Assert.That(memoryPagedOutSummary.Warning.Percentage, Is.EqualTo(0));
            Assert.That(memoryPagedOutSummary.Inconclusive.Total, Is.EqualTo(0));
            Assert.That(memoryPagedOutSummary.Inconclusive.Percentage, Is.EqualTo(0));
            Assert.That(summary.Any(x => x.Id == typeof(RedeliveredMessagesProbe).GetIdentifier()), Is.True);

            var redeliveredMessagesSummary = summary.SingleOrDefault(x => x.Id == typeof(RedeliveredMessagesProbe).GetIdentifier());

            Assert.That(redeliveredMessagesSummary, Is.Not.Null);
            Assert.That(redeliveredMessagesSummary.Healthy.Total, Is.EqualTo(0));
            Assert.That(redeliveredMessagesSummary.Healthy.Percentage, Is.EqualTo(0));
            Assert.That(redeliveredMessagesSummary.Unhealthy.Total, Is.EqualTo(8));
            Assert.That(redeliveredMessagesSummary.Unhealthy.Percentage, Is.EqualTo(100));
            Assert.That(redeliveredMessagesSummary.Warning.Total, Is.EqualTo(0));
            Assert.That(redeliveredMessagesSummary.Warning.Percentage, Is.EqualTo(0));
            Assert.That(redeliveredMessagesSummary.Inconclusive.Total, Is.EqualTo(0));
            Assert.That(redeliveredMessagesSummary.Inconclusive.Percentage, Is.EqualTo(0));
            Assert.That(summary.Any(x => x.Id == typeof(QueueNoFlowProbe).GetIdentifier()), Is.True);

            var noFlowQueueSummary = summary.SingleOrDefault(x => x.Id == typeof(QueueNoFlowProbe).GetIdentifier());

            Assert.That(noFlowQueueSummary, Is.Not.Null);
            Assert.That(noFlowQueueSummary.Healthy.Total, Is.EqualTo(3));
            Assert.That(noFlowQueueSummary.Healthy.Percentage, Is.EqualTo(37.5M));
            Assert.That(noFlowQueueSummary.Unhealthy.Total, Is.EqualTo(5));
            Assert.That(noFlowQueueSummary.Unhealthy.Percentage, Is.EqualTo(62.5M));
            Assert.That(noFlowQueueSummary.Warning.Total, Is.EqualTo(0));
            Assert.That(noFlowQueueSummary.Warning.Percentage, Is.EqualTo(0));
            Assert.That(noFlowQueueSummary.Inconclusive.Total, Is.EqualTo(0));
            Assert.That(noFlowQueueSummary.Inconclusive.Percentage, Is.EqualTo(0));
            Assert.That(summary.Any(x => x.Id == typeof(QueueGrowthProbe).GetIdentifier()), Is.True);

            var queueGrowthSummary = summary.SingleOrDefault(x => x.Id == typeof(QueueGrowthProbe).GetIdentifier());

            Assert.That(queueGrowthSummary, Is.Not.Null);
            Assert.That(queueGrowthSummary.Healthy.Total, Is.EqualTo(8));
            Assert.That(queueGrowthSummary.Healthy.Percentage, Is.EqualTo(100));
            Assert.That(queueGrowthSummary.Unhealthy.Total, Is.EqualTo(0));
            Assert.That(queueGrowthSummary.Unhealthy.Percentage, Is.EqualTo(0));
            Assert.That(queueGrowthSummary.Warning.Total, Is.EqualTo(0));
            Assert.That(queueGrowthSummary.Warning.Percentage, Is.EqualTo(0));
            Assert.That(queueGrowthSummary.Inconclusive.Total, Is.EqualTo(0));
            Assert.That(queueGrowthSummary.Inconclusive.Percentage, Is.EqualTo(0));
            Assert.That(summary.Any(x => x.Id == typeof(QueueLowFlowProbe).GetIdentifier()), Is.True);

            var lowFlowQueueSummary = summary.SingleOrDefault(x => x.Id == typeof(QueueLowFlowProbe).GetIdentifier());

            Assert.That(lowFlowQueueSummary, Is.Not.Null);
            Assert.That(lowFlowQueueSummary.Healthy.Total, Is.EqualTo(1));
            Assert.That(lowFlowQueueSummary.Healthy.Percentage, Is.EqualTo(12.5M));
            Assert.That(lowFlowQueueSummary.Unhealthy.Total, Is.EqualTo(7));
            Assert.That(lowFlowQueueSummary.Unhealthy.Percentage, Is.EqualTo(87.5M));
            Assert.That(lowFlowQueueSummary.Warning.Total, Is.EqualTo(0));
            Assert.That(lowFlowQueueSummary.Warning.Percentage, Is.EqualTo(0));
            Assert.That(lowFlowQueueSummary.Inconclusive.Total, Is.EqualTo(0));
            Assert.That(lowFlowQueueSummary.Inconclusive.Percentage, Is.EqualTo(0));
            Assert.That(summary.Any(x => x.Id == typeof(QueueHighFlowProbe).GetIdentifier()), Is.True);

            var highFlowQueueSummary = summary.SingleOrDefault(x => x.Id == typeof(QueueHighFlowProbe).GetIdentifier());

            Assert.That(highFlowQueueSummary, Is.Not.Null);
            Assert.That(highFlowQueueSummary.Healthy.Total, Is.EqualTo(7));;
            Assert.That(highFlowQueueSummary.Healthy.Percentage, Is.EqualTo(87.5M));
            Assert.That(highFlowQueueSummary.Unhealthy.Total, Is.EqualTo(1));
            Assert.That(highFlowQueueSummary.Unhealthy.Percentage, Is.EqualTo(12.5M));
            Assert.That(highFlowQueueSummary.Warning.Total, Is.EqualTo(0));
            Assert.That(highFlowQueueSummary.Warning.Percentage, Is.EqualTo(0));
            Assert.That(highFlowQueueSummary.Inconclusive.Total, Is.EqualTo(0));
            Assert.That(highFlowQueueSummary.Inconclusive.Percentage, Is.EqualTo(0));
            Assert.That(summary.Any(x => x.Id == typeof(ConsumerUtilizationProbe).GetIdentifier()), Is.True);

            var consumerUtilizationSummary = summary.SingleOrDefault(x => x.Id == typeof(ConsumerUtilizationProbe).GetIdentifier());

            Assert.That(consumerUtilizationSummary, Is.Not.Null);
            Assert.That(consumerUtilizationSummary.Healthy.Total, Is.EqualTo(0));
            Assert.That(consumerUtilizationSummary.Healthy.Percentage, Is.EqualTo(0));
            Assert.That(consumerUtilizationSummary.Unhealthy.Total, Is.EqualTo(8));
            Assert.That(consumerUtilizationSummary.Unhealthy.Percentage, Is.EqualTo(100));
            Assert.That(consumerUtilizationSummary.Warning.Total, Is.EqualTo(0));
            Assert.That(consumerUtilizationSummary.Warning.Percentage, Is.EqualTo(0));
            Assert.That(consumerUtilizationSummary.Inconclusive.Total, Is.EqualTo(0));
            Assert.That(consumerUtilizationSummary.Inconclusive.Percentage, Is.EqualTo(0));
        });
    }
        
    IEnumerable<QueueSnapshot> GetQueues()
    {
        yield return new ()
        {
            Identifier = "Queue0",
            Node = "Node0",
            Messages = new ()
            {
                Incoming = new (){Total = 17},
                Acknowledged = new (){Total = 34},
                Redelivered = new (){Total = 51}
            },
            Memory = new () {PagedOut = new () {Total = 17}}
        };
            
        yield return new ()
        {
            Identifier = "Queue1",
            Node = "Node0",
            Messages = new ()
            {
                Incoming = new (){Total = 0},
                Acknowledged = new (){Total = 0},
                Redelivered = new (){Total = 0}
            },
            Memory = new () {PagedOut = new () {Total = 0}}
        };
            
        yield return new ()
        {
            Identifier = "Queue2",
            Node = "Node0",
            Messages = new ()
            {
                Incoming = new (){Total = 20},
                Acknowledged = new (){Total = 40},
                Redelivered = new (){Total = 60}
            },
            Memory = new () {PagedOut = new () {Total = 20}}
        };
            
        yield return new ()
        {
            Identifier = "Queue3",
            Node = "Node0",
            Messages = new ()
            {
                Incoming = new (){Total = 0},
                Acknowledged = new (){Total = 0},
                Redelivered = new (){Total = 0}
            },
            Memory = new () {PagedOut = new () {Total = 0}}
        };
            
        yield return new ()
        {
            Identifier = "Queue4",
            Node = "Node0",
            Messages = new ()
            {
                Incoming = new (){Total = 200},
                Acknowledged = new (){Total = 400},
                Redelivered = new (){Total = 600}
            },
            Memory = new () {PagedOut = new () {Total = 200}}
        };
            
        yield return new ()
        {
            Identifier = "Queue5",
            Node = "Node0",
            Messages = new ()
            {
                Incoming = new (){Total = 0},
                Acknowledged = new (){Total = 0},
                Redelivered = new (){Total = 0}
            },
            Memory = new () {PagedOut = new () {Total = 0}}
        };
            
        yield return new ()
        {
            Identifier = "Queue6",
            Node = "Node0",
            Messages = new ()
            {
                Incoming = new (){Total = 0},
                Acknowledged = new (){Total = 0},
                Redelivered = new (){Total = 0}
            },
            Memory = new () {PagedOut = new () {Total = 0}}
        };
            
        yield return new ()
        {
            Identifier = "Queue7",
            Node = "Node0",
            Messages = new ()
            {
                Incoming = new (){Total = 0},
                Acknowledged = new (){Total = 0},
                Redelivered = new (){Total = 0}
            },
            Memory = new () {PagedOut = new () {Total = 0}}
        };
    }
}