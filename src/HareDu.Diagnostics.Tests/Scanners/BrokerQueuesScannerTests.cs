namespace HareDu.Diagnostics.Tests.Scanners
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Configuration;
    using Core.Extensions;
    using Diagnostics.Probes;
    using Diagnostics.Scanners;
    using KnowledgeBase;
    using NUnit.Framework;
    using Shouldly;
    using Snapshotting.Model;

    [TestFixture]
    public class BrokerQueuesScannerTests
    {
        IReadOnlyList<DiagnosticProbe> _probes;

        [OneTimeSetUp]
        public void Init()
        {
            var knowledgeBaseProvider = new KnowledgeBaseProvider();
            HareDuConfig config = new()
            {
                Diagnostics = new()
                {
                    Probes = new()
                    {
                        HighConnectionClosureRateThreshold = 100,
                        HighConnectionCreationRateThreshold = 100,
                        QueueHighFlowThreshold = 100,
                        QueueLowFlowThreshold = 20,
                        MessageRedeliveryThresholdCoefficient = 0.50M,
                        SocketUsageThresholdCoefficient = 0.60M,
                        RuntimeProcessUsageThresholdCoefficient = 0.65M,
                        FileDescriptorUsageThresholdCoefficient = 0.65M,
                        ConsumerUtilizationThreshold = 0.50M
                    }
                }
            };
            
            _probes = new List<DiagnosticProbe>
            {
                new QueueGrowthProbe(knowledgeBaseProvider),
                new MessagePagingProbe(knowledgeBaseProvider),
                new RedeliveredMessagesProbe(config.Diagnostics, knowledgeBaseProvider),
                new ConsumerUtilizationProbe(config.Diagnostics, knowledgeBaseProvider),
                new UnroutableMessageProbe(knowledgeBaseProvider),
                new QueueLowFlowProbe(config.Diagnostics, knowledgeBaseProvider),
                new QueueNoFlowProbe(knowledgeBaseProvider),
                new QueueHighFlowProbe(config.Diagnostics, knowledgeBaseProvider)
            };
        }

        [Test]
        public void Verify_analyzers_fired()
        {
            BrokerQueuesSnapshot snapshot = new ()
            {
                Churn = new() {NotRouted = new() {Total = 1}},
                Queues = new List<QueueSnapshot>
                {
                    new()
                    {
                        Identifier = "FakeQueue1",
                        VirtualHost = "FakeVirtualHost",
                        Node = "FakeNode",
                        Consumers = 2,
                        ConsumerUtilization = 1.57M,
                        IdleSince = new DateTimeOffset(2019, 8, 20, 8, 0, 55, TimeSpan.Zero),
                        Memory = new()
                        {
                            RAM = new() {Target = 83, Total = 33, Unacknowledged = 62, Ready = 92},
                            PagedOut = new() {Total = 3}
                        },
                        Messages = new()
                        {
                            Incoming = new() {Total = 768578, Rate = 3845.5M},
                            Unacknowledged = new() {Total = 8293, Rate = 774.5M},
                            Ready = new() {Total = 8381, Rate = 3433.5M},
                            Gets = new() {Total = 934, Rate = 500.5M},
                            GetsWithoutAck = new() {Total = 0, Rate = 0},
                            Delivered = new() {Total = 7339, Rate = 948.5M},
                            DeliveredWithoutAck = new() {Total = 34, Rate = 5.5M},
                            DeliveredGets = new() {Total = 0, Rate = 0},
                            Redelivered = new() {Total = 768578, Rate = 3845.5M},
                            Acknowledged = new() {Total = 9238, Rate = 8934.5M},
                            Aggregate = new() {Total = 823847, Rate = 9847.5M}
                        }
                    }
                }
            };

            var result = new BrokerQueuesScanner(_probes)
                .Scan(snapshot);

            result.Count.ShouldBe(8);
            result.Count(x => x.Id == typeof(QueueGrowthProbe).GetIdentifier()).ShouldBe(1);
            result.Count(x => x.Id == typeof(MessagePagingProbe).GetIdentifier()).ShouldBe(1);
            result.Count(x => x.Id == typeof(RedeliveredMessagesProbe).GetIdentifier()).ShouldBe(1);
            result.Count(x => x.Id == typeof(ConsumerUtilizationProbe).GetIdentifier()).ShouldBe(1);
            result.Count(x => x.Id == typeof(UnroutableMessageProbe).GetIdentifier()).ShouldBe(1);
            result.Count(x => x.Id == typeof(QueueLowFlowProbe).GetIdentifier()).ShouldBe(1);
            result.Count(x => x.Id == typeof(QueueNoFlowProbe).GetIdentifier()).ShouldBe(1);
            result.Count(x => x.Id == typeof(QueueHighFlowProbe).GetIdentifier()).ShouldBe(1);
        }

        [Test]
        public void Verify_empty_result_returned_when_snapshot_null()
        {
            BrokerQueuesSnapshot snapshot = null;
            
            var result = new BrokerQueuesScanner(_probes)
                .Scan(snapshot);

            result.ShouldBeEmpty();
        }
    }
}