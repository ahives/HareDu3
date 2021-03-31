namespace HareDu.Snapshotting.Tests
{
    using System.Linq;
    using System.Threading.Tasks;
    using Extensions;
    using Fakes;
    using HareDu.Model;
    using Microsoft.Extensions.DependencyInjection;
    using Model;
    using NUnit.Framework;

    [TestFixture]
    public class ConnectionSnapshotTests
    {
        ServiceProvider _services;

        [OneTimeSetUp]
        public void Init()
        {
            _services = new ServiceCollection()
                .AddSingleton<IBrokerObjectFactory, FakeBrokerObjectFactory>()
                .AddSingleton<ISnapshotFactory, SnapshotFactory>()
                .BuildServiceProvider();
        }

        [Test]
        public async Task Verify_can_return_snapshot1()
        {
            var result = await _services.GetService<ISnapshotFactory>()
                .Lens<BrokerConnectivitySnapshot>()
                .TakeSnapshot();
            
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Snapshot);
                Assert.IsNotNull(result.Snapshot.Connections);
                Assert.IsNotNull(result.Snapshot.Connections[0]?.NetworkTraffic);
                Assert.IsNotNull(result.Snapshot.Connections[0]?.NetworkTraffic?.Received);
                Assert.IsNotNull(result.Snapshot.Connections[0]?.NetworkTraffic?.Sent);
                Assert.IsNotNull(result.Snapshot.Connections[0]?.Channels);
                Assert.AreEqual("3.7.18", result.Snapshot.BrokerVersion);
                Assert.AreEqual("fake_cluster", result.Snapshot.ClusterName);
                Assert.AreEqual("Connection 1", result.Snapshot.Connections[0]?.Identifier);
                Assert.AreEqual(BrokerConnectionState.Blocked, result.Snapshot.Connections[0]?.State);
                Assert.AreEqual(982738, result.Snapshot.Connections[0]?.OpenChannelsLimit);
                Assert.AreEqual("TestVirtualHost", result.Snapshot.Connections[0]?.VirtualHost);
                Assert.AreEqual("Node 1", result.Snapshot.Connections[0]?.NodeIdentifier);
                Assert.AreEqual(68721979894793, result.Snapshot.Connections[0]?.NetworkTraffic?.Received?.Total);
                Assert.AreEqual(871998847, result.Snapshot.Connections[0]?.NetworkTraffic?.Sent?.Total);
                Assert.AreEqual(627378937423, result.Snapshot.Connections[0]?.NetworkTraffic?.MaxFrameSize);
                Assert.IsTrue(result.Snapshot.Connections[0]?.Channels.Any());
                Assert.AreEqual("Channel 1", result.Snapshot.Connections[0]?.Channels[0]?.Identifier);
                Assert.AreEqual(90, result.Snapshot.Connections[0]?.Channels[0]?.Consumers);
                Assert.AreEqual(78, result.Snapshot.Connections[0]?.Channels[0]?.PrefetchCount);
                Assert.AreEqual(7882003, result.Snapshot.Connections[0]?.Channels[0]?.UnacknowledgedMessages);
                Assert.AreEqual(98237843, result.Snapshot.Connections[0]?.Channels[0]?.UncommittedAcknowledgements);
                Assert.AreEqual(82930, result.Snapshot.Connections[0]?.Channels[0]?.UnconfirmedMessages);
                Assert.AreEqual(383902, result.Snapshot.Connections[0]?.Channels[0]?.UncommittedMessages);
            });
        }

        [Test]
        public async Task Verify_can_return_snapshot2()
        {
            var result = await _services.GetService<ISnapshotFactory>()
                .TakeConnectivitySnapshot();
            
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Snapshot);
                Assert.IsNotNull(result.Snapshot.Connections);
                Assert.IsNotNull(result.Snapshot.Connections[0]?.NetworkTraffic);
                Assert.IsNotNull(result.Snapshot.Connections[0]?.NetworkTraffic?.Received);
                Assert.IsNotNull(result.Snapshot.Connections[0]?.NetworkTraffic?.Sent);
                Assert.IsNotNull(result.Snapshot.Connections[0]?.Channels);
                Assert.AreEqual("3.7.18", result.Snapshot.BrokerVersion);
                Assert.AreEqual("fake_cluster", result.Snapshot.ClusterName);
                Assert.AreEqual("Connection 1", result.Snapshot.Connections[0]?.Identifier);
                Assert.AreEqual(BrokerConnectionState.Blocked, result.Snapshot.Connections[0]?.State);
                Assert.AreEqual(982738, result.Snapshot.Connections[0]?.OpenChannelsLimit);
                Assert.AreEqual("TestVirtualHost", result.Snapshot.Connections[0]?.VirtualHost);
                Assert.AreEqual("Node 1", result.Snapshot.Connections[0]?.NodeIdentifier);
                Assert.AreEqual(68721979894793, result.Snapshot.Connections[0]?.NetworkTraffic?.Received?.Total);
                Assert.AreEqual(871998847, result.Snapshot.Connections[0]?.NetworkTraffic?.Sent?.Total);
                Assert.AreEqual(627378937423, result.Snapshot.Connections[0]?.NetworkTraffic?.MaxFrameSize);
                Assert.IsTrue(result.Snapshot.Connections[0]?.Channels.Any());
                Assert.AreEqual("Channel 1", result.Snapshot.Connections[0]?.Channels[0]?.Identifier);
                Assert.AreEqual(90, result.Snapshot.Connections[0]?.Channels[0]?.Consumers);
                Assert.AreEqual(78, result.Snapshot.Connections[0]?.Channels[0]?.PrefetchCount);
                Assert.AreEqual(7882003, result.Snapshot.Connections[0]?.Channels[0]?.UnacknowledgedMessages);
                Assert.AreEqual(98237843, result.Snapshot.Connections[0]?.Channels[0]?.UncommittedAcknowledgements);
                Assert.AreEqual(82930, result.Snapshot.Connections[0]?.Channels[0]?.UnconfirmedMessages);
                Assert.AreEqual(383902, result.Snapshot.Connections[0]?.Channels[0]?.UncommittedMessages);
            });
        }
    }
}