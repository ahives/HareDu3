namespace HareDu.Diagnostics.Tests.Scanners
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Configuration;
    using Core.Extensions;
    using Diagnostics.Probes;
    using Diagnostics.Scanners;
    using Fakes;
    using KnowledgeBase;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MicrosoftIntegration;
    using NUnit.Framework;
    using Shouldly;
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
        public void Test()
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

            var newProbe = new NewProbe(null);
            var isRegistered = _services.GetService<IScannerFactory>()
                .RegisterProbe(newProbe);
            
            var result = _services.GetService<IScanner>()
                .Scan(snapshot);
            
            Console.WriteLine($"Id: {result.Id}");
            Console.WriteLine($"Scanner Id: {result.ScannerId}");
            
            for (int i = 0; i < result.Results.Count; i++)
            {
                Console.WriteLine($"Id: {result.Results[i].Id}");
                Console.WriteLine($"Name: {result.Results[i].Name}");
                Console.WriteLine($"Status: {result.Results[i].Status}");
                Console.WriteLine($"Component Id: {result.Results[i].ComponentId}");
                Console.WriteLine($"Component Type: {result.Results[i].ComponentType}");
                Console.WriteLine($"Parent Component Id: {result.Results[i].ParentComponentId}");
                Console.WriteLine();
            }

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
            
            result.ScannerId.ShouldBe(typeof(BrokerConnectivityScanner).GetIdentifier());
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
            
            result.ScannerId.ShouldBe(typeof(ClusterScanner).GetIdentifier());
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
            
            result.ScannerId.ShouldBe(typeof(BrokerQueuesScanner).GetIdentifier());
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
            
            report.ScannerId.ShouldBe(typeof(NoOpScanner<EmptySnapshot>).GetIdentifier());
            report.ShouldBe(DiagnosticCache.EmptyScannerResult);
        }
    }

    public class NewProbe :
        BaseDiagnosticProbe,
        DiagnosticProbe
    {
        public NewProbe(IKnowledgeBaseProvider kb) : base(kb)
        {
        }

        public string Id => GetType().GetIdentifier();
        public string Name => "New Probe";
        public string Description => "";
        public ComponentType ComponentType => ComponentType.Connection;
        public ProbeCategory Category => ProbeCategory.Connectivity;

        public ProbeResult Execute<T>(T snapshot)
        {
            ProbeResult result;
            BrokerConnectivitySnapshot data = snapshot as BrokerConnectivitySnapshot;
            
            if (data.Connections.Any(x => x.Identifier == "Connection1"))
            {
                result = new HealthyProbeResult
                {
                    ParentComponentId = null,
                    ComponentId = null,
                    Id = Id,
                    Name = Name,
                    ComponentType = ComponentType,
                    Data = null,
                    KB = null
                };
            }
            else
            {
                result = new UnhealthyProbeResult
                {
                    ParentComponentId = null,
                    ComponentId = null,
                    Id = Id,
                    Name = Name,
                    ComponentType = ComponentType,
                    Data = null,
                    KB = null
                };
            }

            NotifyObservers(result);

            return result;
        }
    }
}