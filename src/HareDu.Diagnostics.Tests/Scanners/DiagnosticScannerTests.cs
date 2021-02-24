namespace HareDu.Diagnostics.Tests.Scanners
{
    using System;
    using System.Collections.Generic;
    using Core.Extensions;
    using Diagnostics.Scanners;
    using Fakes;
    using Microsoft.Extensions.DependencyInjection;
    using MicrosoftIntegration;
    using NUnit.Framework;
    using Snapshotting.Model;

    [TestFixture]
    public class DiagnosticScannerTests
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
        public void Verify_can_select_broker_connectivity_scanner()
        {
            BrokerConnectivitySnapshot snapshot = new ()
            {
                ConnectionsCreated = new () {Total = 100000, Rate = 102},
                ConnectionsClosed = new () {Total = 174000, Rate = 100},
                Connections = new List<ConnectionSnapshot>
                {
                    new ()
                    {
                        Identifier = "Connection1",
                        OpenChannelsLimit = 2,
                        Channels = new List<ChannelSnapshot>
                        {
                            new ()
                            {
                                PrefetchCount = 4,
                                UncommittedAcknowledgements = 2,
                                UncommittedMessages = 5,
                                UnconfirmedMessages = 8,
                                UnacknowledgedMessages = 5,
                                Consumers = 1,
                                Identifier = "Channel1"
                            }
                        },
                        State = ConnectionState.Blocked
                    }
                }
            };

            var result = _services.GetService<IScanner>()
                .Scan(snapshot);
            
            Assert.AreEqual(typeof(BrokerConnectivityScanner).GetIdentifier(), result.ScannerId);
        }

        [Test]
        public void Verify_can_select_cluster_scanner()
        {
            ClusterSnapshot snapshot = new ()
            {
                Nodes = new List<NodeSnapshot>
                {
                    new ()
                    {
                        NetworkPartitions = new List<string>
                        {
                            "node1@rabbitmq",
                            "node2@rabbitmq",
                            "node3@rabbitmq"
                        },
                        Runtime = new() {Processes = new() {Limit = 38, Used = 36, UsageRate = 5.3M}},
                        Disk = new() {AlarmInEffect = true, Capacity = new() {Available = 8, Rate = 5.5M}},
                        Memory = new() {Used = 273, Limit = 270, AlarmInEffect = true},
                        OS = new()
                        {
                            FileDescriptors = new() {Available = 100, Used = 90, UsageRate = 5.5M},
                            SocketDescriptors = new() {Available = 100, Used = 90, UsageRate = 5.5M}
                        },
                        AvailableCoresDetected = 1
                    }
                }
            };

            var result = _services.GetService<IScanner>()
                .Scan(snapshot);
            
            Assert.AreEqual(typeof(ClusterScanner).GetIdentifier(), result.ScannerId);
        }

        [Test]
        public void Verify_can_select_broker_queues_scanner()
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

            var result = _services.GetService<IScanner>()
                .Scan(snapshot);
            
            Assert.AreEqual(typeof(BrokerQueuesScanner).GetIdentifier(), result.ScannerId);
        }

        [Test]
        public void Verify_does_not_throw_when_scanner_not_found()
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
            
            IScannerFactory factory = new FakeScannerFactory();
            IScanner result = new Scanner(factory);

            var report = result.Scan(snapshot);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(typeof(NoOpScanner<EmptySnapshot>).GetIdentifier(), report.ScannerId);
                Assert.AreEqual(DiagnosticCache.EmptyScannerResult, report);
            });
        }
    }
}