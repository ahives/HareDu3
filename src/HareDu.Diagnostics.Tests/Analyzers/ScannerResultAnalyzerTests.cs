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
            Assert.IsNotNull(summary);
            Assert.AreEqual(2, summary.Count);
            Assert.IsTrue(summary.Any(x => x.Id == "Queue"));

            var queueSummary = summary.SingleOrDefault(x => x.Id == "Queue");
                
            Assert.IsNotNull(queueSummary);
            Assert.AreEqual(24, queueSummary.Healthy.Total);
            Assert.AreEqual(42.86M, Decimal.Round(queueSummary.Healthy.Percentage, 2));
            Assert.AreEqual(32, queueSummary.Unhealthy.Total);
            Assert.AreEqual(57.14M, Decimal.Round(queueSummary.Unhealthy.Percentage, 2));
            Assert.AreEqual(0, queueSummary.Warning.Total);
            Assert.AreEqual(0, queueSummary.Warning.Percentage);
            Assert.AreEqual(0, queueSummary.Inconclusive.Total);
            Assert.AreEqual(0, queueSummary.Inconclusive.Percentage);
            Assert.IsTrue(summary.Any(x => x.Id == "Exchange"));

            var exchangeSummary = summary.SingleOrDefault(x => x.Id == "Exchange");
                
            Assert.IsNotNull(exchangeSummary);
            Assert.AreEqual(0, exchangeSummary.Healthy.Total);
            Assert.AreEqual(0, exchangeSummary.Healthy.Percentage);
            Assert.AreEqual(1, exchangeSummary.Unhealthy.Total);
            Assert.AreEqual(100, exchangeSummary.Unhealthy.Percentage);
            Assert.AreEqual(0, exchangeSummary.Warning.Total);
            Assert.AreEqual(0, exchangeSummary.Warning.Percentage);
            Assert.AreEqual(0, exchangeSummary.Inconclusive.Total);
            Assert.AreEqual(0, exchangeSummary.Inconclusive.Percentage);
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
            Assert.IsNotNull(summary);
            Assert.AreEqual(2, summary.Count);
            Assert.IsTrue(summary.Any(x => x.Id == "Cluster 1"));
            Assert.IsTrue(summary.Any(x => x.Id == "Node0"));

            var nodeSummary = summary.SingleOrDefault(x => x.Id == "Node0");
                
            Assert.IsNotNull(nodeSummary);
            Assert.AreEqual(24, nodeSummary.Healthy.Total);
            Assert.AreEqual(42.86M, Decimal.Round(nodeSummary.Healthy.Percentage, 2));
            Assert.AreEqual(32, nodeSummary.Unhealthy.Total);
            Assert.AreEqual(57.14M, Decimal.Round(nodeSummary.Unhealthy.Percentage, 2));
            Assert.AreEqual(0, nodeSummary.Warning.Total);
            Assert.AreEqual(0, nodeSummary.Warning.Percentage);
            Assert.AreEqual(0, nodeSummary.Inconclusive.Total);
            Assert.AreEqual(0, nodeSummary.Inconclusive.Percentage);

            var clusterSummary = summary.SingleOrDefault(x => x.Id == "Cluster 1");
                
            Assert.IsNotNull(clusterSummary);
            Assert.AreEqual(0, clusterSummary.Healthy.Total);
            Assert.AreEqual(0, clusterSummary.Healthy.Percentage);
            Assert.AreEqual(1, clusterSummary.Unhealthy.Total);
            Assert.AreEqual(100, clusterSummary.Unhealthy.Percentage);
            Assert.AreEqual(0, clusterSummary.Warning.Total);
            Assert.AreEqual(0, clusterSummary.Warning.Percentage);
            Assert.AreEqual(0, clusterSummary.Inconclusive.Total);
            Assert.AreEqual(0, clusterSummary.Inconclusive.Percentage);
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
            Assert.IsNotNull(summary);
            Assert.AreEqual(8, summary.Count);
            Assert.IsTrue(summary.Any(x => x.Id == typeof(UnroutableMessageProbe).GetIdentifier()));

            var unroutableSummary = summary.SingleOrDefault(x => x.Id == typeof(UnroutableMessageProbe).GetIdentifier());
                
            Assert.IsNotNull(unroutableSummary);
            Assert.AreEqual(0, unroutableSummary.Healthy.Total);
            Assert.AreEqual(0, unroutableSummary.Healthy.Percentage);
            Assert.AreEqual(1, unroutableSummary.Unhealthy.Total);
            Assert.AreEqual(100, unroutableSummary.Unhealthy.Percentage);
            Assert.AreEqual(0, unroutableSummary.Warning.Total);
            Assert.AreEqual(0, unroutableSummary.Warning.Percentage);
            Assert.AreEqual(0, unroutableSummary.Inconclusive.Total);
            Assert.AreEqual(0, unroutableSummary.Inconclusive.Percentage);
                
            Assert.IsTrue(summary.Any(x => x.Id == typeof(MessagePagingProbe).GetIdentifier()));
                
            var memoryPagedOutSummary = summary.SingleOrDefault(x => x.Id == typeof(MessagePagingProbe).GetIdentifier());
                
            Assert.IsNotNull(memoryPagedOutSummary);
            Assert.AreEqual(5, memoryPagedOutSummary.Healthy.Total);
            Assert.AreEqual(62.5M, memoryPagedOutSummary.Healthy.Percentage);
            Assert.AreEqual(3, memoryPagedOutSummary.Unhealthy.Total);
            Assert.AreEqual(37.5M, memoryPagedOutSummary.Unhealthy.Percentage);
            Assert.AreEqual(0, memoryPagedOutSummary.Warning.Total);
            Assert.AreEqual(0, memoryPagedOutSummary.Warning.Percentage);
            Assert.AreEqual(0, memoryPagedOutSummary.Inconclusive.Total);
            Assert.AreEqual(0, memoryPagedOutSummary.Inconclusive.Percentage);
                
            Assert.IsTrue(summary.Any(x => x.Id == typeof(RedeliveredMessagesProbe).GetIdentifier()));
                
            var redeliveredMessagesSummary = summary.SingleOrDefault(x => x.Id == typeof(RedeliveredMessagesProbe).GetIdentifier());

            Assert.IsNotNull(redeliveredMessagesSummary);
            Assert.AreEqual(0, redeliveredMessagesSummary.Healthy.Total);
            Assert.AreEqual(0, redeliveredMessagesSummary.Healthy.Percentage);
            Assert.AreEqual(8, redeliveredMessagesSummary.Unhealthy.Total);
            Assert.AreEqual(100, redeliveredMessagesSummary.Unhealthy.Percentage);
            Assert.AreEqual(0, redeliveredMessagesSummary.Warning.Total);
            Assert.AreEqual(0, redeliveredMessagesSummary.Warning.Percentage);
            Assert.AreEqual(0, redeliveredMessagesSummary.Inconclusive.Total);
            Assert.AreEqual(0, redeliveredMessagesSummary.Inconclusive.Percentage);

            Assert.IsTrue(summary.Any(x => x.Id == typeof(QueueNoFlowProbe).GetIdentifier()));
                
            var noFlowQueueSummary = summary.SingleOrDefault(x => x.Id == typeof(QueueNoFlowProbe).GetIdentifier());

            Assert.IsNotNull(noFlowQueueSummary);
            Assert.AreEqual(3, noFlowQueueSummary.Healthy.Total);
            Assert.AreEqual(37.5M, noFlowQueueSummary.Healthy.Percentage);
            Assert.AreEqual(5, noFlowQueueSummary.Unhealthy.Total);
            Assert.AreEqual(62.5M, noFlowQueueSummary.Unhealthy.Percentage);
            Assert.AreEqual(0, noFlowQueueSummary.Warning.Total);
            Assert.AreEqual(0, noFlowQueueSummary.Warning.Percentage);
            Assert.AreEqual(0, noFlowQueueSummary.Inconclusive.Total);
            Assert.AreEqual(0, noFlowQueueSummary.Inconclusive.Percentage);
                
            Assert.IsTrue(summary.Any(x => x.Id == typeof(QueueGrowthProbe).GetIdentifier()));

            var queueGrowthSummary = summary.SingleOrDefault(x => x.Id == typeof(QueueGrowthProbe).GetIdentifier());

            Assert.IsNotNull(queueGrowthSummary);
            Assert.AreEqual(8, queueGrowthSummary.Healthy.Total);
            Assert.AreEqual(100, queueGrowthSummary.Healthy.Percentage);
            Assert.AreEqual(0, queueGrowthSummary.Unhealthy.Total);
            Assert.AreEqual(0, queueGrowthSummary.Unhealthy.Percentage);
            Assert.AreEqual(0, queueGrowthSummary.Warning.Total);
            Assert.AreEqual(0, queueGrowthSummary.Warning.Percentage);
            Assert.AreEqual(0, queueGrowthSummary.Inconclusive.Total);
            Assert.AreEqual(0, queueGrowthSummary.Inconclusive.Percentage);

            Assert.IsTrue(summary.Any(x => x.Id == typeof(QueueLowFlowProbe).GetIdentifier()));
                
            var lowFlowQueueSummary = summary.SingleOrDefault(x => x.Id == typeof(QueueLowFlowProbe).GetIdentifier());

            Assert.IsNotNull(lowFlowQueueSummary);
            Assert.AreEqual(1, lowFlowQueueSummary.Healthy.Total);
            Assert.AreEqual(12.5M, lowFlowQueueSummary.Healthy.Percentage);
            Assert.AreEqual(7, lowFlowQueueSummary.Unhealthy.Total);
            Assert.AreEqual(87.5M, lowFlowQueueSummary.Unhealthy.Percentage);
            Assert.AreEqual(0, lowFlowQueueSummary.Warning.Total);
            Assert.AreEqual(0, lowFlowQueueSummary.Warning.Percentage);
            Assert.AreEqual(0, lowFlowQueueSummary.Inconclusive.Total);
            Assert.AreEqual(0, lowFlowQueueSummary.Inconclusive.Percentage);
                
            Assert.IsTrue(summary.Any(x => x.Id == typeof(QueueHighFlowProbe).GetIdentifier()));

            var highFlowQueueSummary = summary.SingleOrDefault(x => x.Id == typeof(QueueHighFlowProbe).GetIdentifier());

            Assert.IsNotNull(highFlowQueueSummary);
            Assert.AreEqual(7, highFlowQueueSummary.Healthy.Total);
            Assert.AreEqual(87.5M, highFlowQueueSummary.Healthy.Percentage);
            Assert.AreEqual(1, highFlowQueueSummary.Unhealthy.Total);
            Assert.AreEqual(12.5M, highFlowQueueSummary.Unhealthy.Percentage);
            Assert.AreEqual(0, highFlowQueueSummary.Warning.Total);
            Assert.AreEqual(0, highFlowQueueSummary.Warning.Percentage);
            Assert.AreEqual(0, highFlowQueueSummary.Inconclusive.Total);
            Assert.AreEqual(0, highFlowQueueSummary.Inconclusive.Percentage);
                
            Assert.IsTrue(summary.Any(x => x.Id == typeof(ConsumerUtilizationProbe).GetIdentifier()));

            var consumerUtilizationSummary = summary.SingleOrDefault(x => x.Id == typeof(ConsumerUtilizationProbe).GetIdentifier());

            Assert.IsNotNull(consumerUtilizationSummary);
            Assert.AreEqual(0, consumerUtilizationSummary.Healthy.Total);
            Assert.AreEqual(0, consumerUtilizationSummary.Healthy.Percentage);
            Assert.AreEqual(8, consumerUtilizationSummary.Unhealthy.Total);
            Assert.AreEqual(100, consumerUtilizationSummary.Unhealthy.Percentage);
            Assert.AreEqual(0, consumerUtilizationSummary.Warning.Total);
            Assert.AreEqual(0, consumerUtilizationSummary.Warning.Percentage);
            Assert.AreEqual(0, consumerUtilizationSummary.Inconclusive.Total);
            Assert.AreEqual(0, consumerUtilizationSummary.Inconclusive.Percentage);
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