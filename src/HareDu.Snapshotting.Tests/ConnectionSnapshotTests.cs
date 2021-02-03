namespace HareDu.Snapshotting.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Configuration;
    using HareDu.Model;
    using HareDu.Registration;
    using Microsoft.Extensions.DependencyInjection;
    using Model;
    using NUnit.Framework;
    using Registration;
    using Shouldly;

    [TestFixture]
    public class ConnectionSnapshotTests
    {
        ServiceProvider _services;

        [OneTimeSetUp]
        public void Init()
        {
            _services = new ServiceCollection()
                .AddSingleton<IBrokerObjectFactory>(x => new FakeBrokerObjectFactory())
                .AddSingleton<ISnapshotFactory, SnapshotFactory>()
                .BuildServiceProvider();
        }

        [Test]
        public async Task Test()
        {
            var lens = _services.GetService<ISnapshotFactory>()
                .Lens<BrokerConnectivitySnapshot>();
            var result = lens.TakeSnapshot();
            
            result.ShouldNotBeNull();
            result.Snapshot.ShouldNotBeNull();
            result.Snapshot.BrokerVersion.ShouldBe("3.7.18");
            result.Snapshot.ClusterName.ShouldBe("fake_cluster");
            result.Snapshot.Connections.ShouldNotBeNull();
            result.Snapshot.Connections[0]?.Identifier.ShouldBe("Connection 1");
            result.Snapshot.Connections[0]?.State.ShouldBe(ConnectionState.Blocked);
            result.Snapshot.Connections[0]?.OpenChannelsLimit.ShouldBe<ulong>(982738);
            result.Snapshot.Connections[0]?.VirtualHost.ShouldBe("TestVirtualHost");
            result.Snapshot.Connections[0]?.NodeIdentifier.ShouldBe("Node 1");
            result.Snapshot.Connections[0]?.NetworkTraffic.ShouldNotBeNull();
            result.Snapshot.Connections[0]?.NetworkTraffic?.Received.ShouldNotBeNull();
            result.Snapshot.Connections[0]?.NetworkTraffic?.Received?.Total.ShouldBe<ulong>(68721979894793);
            result.Snapshot.Connections[0]?.NetworkTraffic?.Sent.ShouldNotBeNull();
            result.Snapshot.Connections[0]?.NetworkTraffic?.Sent?.Total.ShouldBe<ulong>(871998847);
            result.Snapshot.Connections[0]?.NetworkTraffic?.MaxFrameSize.ShouldBe<ulong>(627378937423);
            result.Snapshot.Connections[0]?.Channels.ShouldNotBeNull();
            result.Snapshot.Connections[0]?.Channels.Any().ShouldBeTrue();
            result.Snapshot.Connections[0]?.Channels[0]?.Identifier.ShouldBe("Channel 1");
            result.Snapshot.Connections[0]?.Channels[0]?.Consumers.ShouldBe<ulong>(90);
            result.Snapshot.Connections[0]?.Channels[0]?.PrefetchCount.ShouldBe<uint>(78);
            result.Snapshot.Connections[0]?.Channels[0]?.UnacknowledgedMessages.ShouldBe<ulong>(7882003);
            result.Snapshot.Connections[0]?.Channels[0]?.UncommittedAcknowledgements.ShouldBe<ulong>(98237843);
            result.Snapshot.Connections[0]?.Channels[0]?.UnconfirmedMessages.ShouldBe<ulong>(82930);
            result.Snapshot.Connections[0]?.Channels[0]?.UncommittedMessages.ShouldBe<ulong>(383902);
        }
    }
    
    public class FakeBrokerObjectFactory :
        IBrokerObjectFactory,
        HareDuTestingFake
    {
        public HareDuConfig Config { get; }

        public T Object<T>()
            where T : BrokerObject
        {
            if (typeof(T) == typeof(SystemOverview))
            {
                SystemOverview obj = new SystemOverviewObject();

                return (T) obj;
            }

            if (typeof(T) == typeof(Node))
            {
                Node obj = new FakeNodeObject();

                return (T) obj;
            }

            if (typeof(T) == typeof(Connection))
            {
                Connection obj = new FakeConnectionObject();

                return (T) obj;
            }

            if (typeof(T) == typeof(Channel))
            {
                Channel obj = new FakeChannelObject();

                return (T) obj;
            }

            if (typeof(T) == typeof(Queue))
            {
                Queue obj = new FakeQueueObject();

                return (T) obj;
            }

            return default;
        }

        public bool IsRegistered(string key) => throw new System.NotImplementedException();
        
        public IReadOnlyDictionary<string, object> GetObjects() => throw new System.NotImplementedException();

        public void CancelPendingRequest() => throw new System.NotImplementedException();

        public bool TryRegisterAll() => throw new System.NotImplementedException();
    }

    public class SystemOverviewObject :
        SystemOverview,
        HareDuTestingFake
    {
        public async Task<Result<SystemOverviewInfo>> Get(CancellationToken cancellationToken = default)
        {
            var data = new SystemOverviewInfo()
            {
                RabbitMqVersion = "3.7.18",
                ErlangVersion = "22.1",
                MessageStats = new ()
                {
                    TotalMessagesAcknowledged = 7287736,
                    TotalMessageDeliveryGets = 78263767,
                    TotalMessagesPublished = 1234,
                    TotalMessagesConfirmed = 83,
                    TotalUnroutableMessages = 737,
                    TotalDiskReads = 83,
                    TotalMessageGets = 723,
                    TotalMessageGetsWithoutAck = 373,
                    TotalMessagesDelivered = 7234,
                    TotalMessagesRedelivered = 7237,
                    TotalMessageDeliveredWithoutAck = 8723,
                    MessagesPublishedDetails = new Rate{Value = 7},
                    UnroutableMessagesDetails = new Rate{Value = 48},
                    MessageGetDetails = new Rate{Value = 324},
                    MessageGetsWithoutAckDetails = new Rate{Value = 84},
                    MessageDeliveryDetails = new Rate{Value = 84},
                    MessagesDeliveredWithoutAckDetails = new Rate{Value = 56},
                    MessagesRedeliveredDetails = new Rate{Value = 89},
                    MessagesAcknowledgedDetails = new Rate{Value = 723},
                    MessageDeliveryGetDetails = new Rate{Value = 738},
                    MessagesConfirmedDetails = new Rate{Value = 7293}
                },
                ClusterName = "fake_cluster",
                QueueStats = new ()
                {
                    TotalMessagesReadyForDelivery = 82937489379,
                    MessagesReadyForDeliveryDetails = new Rate{Value = 34.4M},
                    TotalUnacknowledgedDeliveredMessages = 892387397238,
                    UnacknowledgedDeliveredMessagesDetails = new Rate{Value = 73.3M},
                    TotalMessages = 9230748297,
                    MessageDetails = new Rate{Value = 80.3M}
                }
            };
            
            return new SuccessfulResult<SystemOverviewInfo>{Data = data, DebugInfo = null};
        }
    }

    public class FakeNodeObject :
        Node,
        HareDuTestingFake
    {
        public async Task<ResultList<NodeInfo>> GetAll(CancellationToken cancellationToken = default)
        {
            var node1 = new NodeInfo
            {
                Partitions = GetPartitions().ToList(),
                MemoryAlarm = true,
                MemoryLimit = 723746434,
                OperatingSystemProcessId = "OS123",
                TotalFileDescriptors = 8923747343434,
                TotalSocketsAvailable = 8186263662,
                FreeDiskLimit = 8928739432,
                FreeDiskAlarm = true,
                TotalProcesses = 7234364,
                AvailableCoresDetected = 8,
                IsRunning = true,
                MemoryUsed = 7826871,
                FileDescriptorUsed = 9203797,
                SocketsUsed = 8298347,
                ProcessesUsed = 9199849,
                FreeDiskSpace = 7265368234,
                TotalOpenFileHandleAttempts = 356446478,
                TotalIOWrites = 36478608776,
                TotalIOReads = 892793874982,
                TotalMessageStoreReads = 9097887,
                TotalMessageStoreWrites = 776788733,
                TotalIOBytesRead = 78738764,
                TotalIOBytesWritten = 728364283
            };
            var node2 = new NodeInfo
            {
                Partitions = GetPartitions().ToList(),
                MemoryAlarm = true,
                MemoryLimit = 723746434,
                OperatingSystemProcessId = "OS123",
                TotalFileDescriptors = 8923747343434,
                TotalSocketsAvailable = 8186263662,
                FreeDiskLimit = 8928739432,
                FreeDiskAlarm = true,
                TotalProcesses = 7234364,
                AvailableCoresDetected = 8,
                IsRunning = true,
                MemoryUsed = 7826871,
                FileDescriptorUsed = 9203797,
                SocketsUsed = 8298347,
                ProcessesUsed = 9199849,
                FreeDiskSpace = 7265368234,
                TotalOpenFileHandleAttempts = 356446478,
                TotalIOWrites = 36478608776,
                TotalIOReads = 892793874982,
                TotalMessageStoreReads = 9097887,
                TotalMessageStoreWrites = 776788733,
                TotalIOBytesRead = 78738764,
                TotalIOBytesWritten = 728364283
            };

            return new SuccessfulResultList<NodeInfo>{Data = new List<NodeInfo> {node1, node2}, DebugInfo = null};
        }

        public async Task<Result<NodeHealthInfo>> GetHealth(string node = null, CancellationToken cancellationToken = default) => throw new System.NotImplementedException();

        public async Task<Result<NodeMemoryUsageInfo>> GetMemoryUsage(string node, CancellationToken cancellationToken = default) => throw new System.NotImplementedException();

        IEnumerable<string> GetPartitions()
        {
            yield return "partition1";
            yield return "partition2";
            yield return "partition3";
            yield return "partition4";
        }
    }

    public class FakeConnectionObject :
        Connection,
        HareDuTestingFake
    {
        public async Task<ResultList<ConnectionInfo>> GetAll(CancellationToken cancellationToken = default)
        {
            var connection1 = new ConnectionInfo
            {
                TotalReductions = 897274932,
                OpenChannelsLimit = 982738,
                MaxFrameSizeInBytes = 627378937423,
                VirtualHost = "TestVirtualHost",
                Name = "Connection 1",
                Node = "Node 1",
                Channels = 7687264882,
                SendPending = 686219897,
                PacketsSent = 871998847,
                PacketBytesSent = 83008482374,
                PacketsReceived = 68721979894793,
                State = "blocked"
            };
            var connection2 = new ConnectionInfo
            {
                TotalReductions = 897274932,
                OpenChannelsLimit = 982738,
                MaxFrameSizeInBytes = 627378937423,
                VirtualHost = "TestVirtualHost",
                Name = "Connection 2",
                Node = "Node 1",
                Channels = 7687264882,
                SendPending = 686219897,
                PacketsSent = 871998847,
                PacketBytesSent = 83008482374,
                PacketsReceived = 68721979894793,
                State = "blocked"
            };
            var connection3 = new ConnectionInfo
            {
                TotalReductions = 897274932,
                OpenChannelsLimit = 982738,
                MaxFrameSizeInBytes = 627378937423,
                VirtualHost = "TestVirtualHost",
                Name = "Connection 3",
                Node = "Node 1",
                Channels = 7687264882,
                SendPending = 686219897,
                PacketsSent = 871998847,
                PacketBytesSent = 83008482374,
                PacketsReceived = 68721979894793,
                State = "blocked"
            };

            return new SuccessfulResultList<ConnectionInfo>{Data = new List<ConnectionInfo> {connection1, connection2, connection3}, DebugInfo = null};
        }
    }

    public class FakeChannelObject :
        Channel,
        HareDuTestingFake
    {
        public async Task<ResultList<ChannelInfo>> GetAll(CancellationToken cancellationToken = default)
        {
            var channel = new ChannelInfo
            {
                TotalReductions = 872634826473,
                VirtualHost = "TestVirtualHost",
                Node = "Node 1",
                FrameMax = 728349837,
                Name = "Channel 1",
                TotalChannels = 87,
                SentPending = 89,
                PrefetchCount = 78,
                UncommittedAcknowledgements = 98237843,
                UncommittedMessages = 383902,
                UnconfirmedMessages = 82930,
                UnacknowledgedMessages = 7882003,
                TotalConsumers = 90,
                ConnectionDetails = new ()
                {
                    Name = "Connection 1"
                }
            };

            return new SuccessfulResultList<ChannelInfo>{Data = new List<ChannelInfo> {channel}, DebugInfo = null};
        }
    }

    public class FakeQueueObject :
        Queue,
        HareDuTestingFake
    {
        public async Task<ResultList<QueueInfo>> GetAll(CancellationToken cancellationToken = default)
        {
            var channel = new QueueInfo
            {
                TotalMessages = 7823668,
                UnacknowledgedMessages = 7273020,
                ReadyMessages = 9293093,
                TotalReductions = 992039,
                Name = "Queue 1",
                MessageBytesPagedOut = 239939803,
                TotalMessagesPagedOut = 90290398,
                MessageBytesInRAM = 992390933,
                TotalBytesOfMessagesDeliveredButUnacknowledged = 82830892,
                TotalBytesOfMessagesReadyForDelivery = 892839823,
                TotalBytesOfAllMessages = 82938938723,
                UnacknowledgedMessagesInRAM = 82938982323,
                MessagesReadyForDeliveryInRAM = 8892388929,
                MessagesInRAM = 9883892938,
                Consumers = 773709938,
                ConsumerUtilization = 0.50M,
                Memory = 92990390,
                MessageStats = new ()
                {
                    TotalMessagesPublished = 763928923,
                    TotalMessageGets = 82938820903,
                    TotalMessageGetsWithoutAck = 23997979383,
                    TotalMessagesDelivered = 238847970,
                    TotalMessageDeliveredWithoutAck = 48898693232,
                    TotalMessageDeliveryGets = 50092830929,
                    TotalMessagesRedelivered = 488983002934,
                    TotalMessagesAcknowledged = 92303949398
                }
            };

            return new SuccessfulResultList<QueueInfo>{Data = new List<QueueInfo> {channel}, DebugInfo = null};
        }

        public async Task<Result> Create(Action<QueueCreateAction> action, CancellationToken cancellationToken = default) => throw new NotImplementedException();

        public async Task<Result> Delete(Action<QueueDeleteAction> action, CancellationToken cancellationToken = default) => throw new NotImplementedException();

        public async Task<Result> Empty(Action<QueueEmptyAction> action, CancellationToken cancellationToken = default) => throw new NotImplementedException();

        public async Task<ResultList<PeekedMessageInfo>> Peek(Action<QueuePeekAction> action, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    }

    public interface HareDuTestingFake
    {
    }
}