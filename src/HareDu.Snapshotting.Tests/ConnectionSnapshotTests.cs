namespace HareDu.Snapshotting.Tests;

using System.Linq;
using System.Threading.Tasks;
using Core;
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
            .AddSingleton<IHareDuFactory, FakeHareDuFactory>()
            .AddSingleton<ISnapshotFactory, SnapshotFactory>()
            .BuildServiceProvider();
    }

    [Test]
    public async Task Verify_can_return_snapshot1()
    {
        var result = await _services.GetService<ISnapshotFactory>()
            .Lens<BrokerConnectivitySnapshot>()
            .TakeSnapshot(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Snapshot, Is.Not.Null);
            Assert.That(result.Snapshot.Connections, Is.Not.Null);
            Assert.That(result.Snapshot.Connections[0]?.NetworkTraffic, Is.Not.Null);
            Assert.That(result.Snapshot.Connections[0]?.NetworkTraffic?.Received, Is.Not.Null);
            Assert.That(result.Snapshot.Connections[0]?.NetworkTraffic?.Sent, Is.Not.Null);
            Assert.That(result.Snapshot.Connections[0]?.Channels, Is.Not.Null);
            Assert.That(result.Snapshot.BrokerVersion, Is.EqualTo("3.7.18"));
            Assert.That(result.Snapshot.ClusterName, Is.EqualTo("fake_cluster"));
            Assert.That(result.Snapshot.Connections[0]?.Identifier, Is.EqualTo("Connection 1"));
            Assert.That(result.Snapshot.Connections[0]?.State, Is.EqualTo(BrokerConnectionState.Blocked));
            Assert.That(result.Snapshot.Connections[0]?.OpenChannelsLimit, Is.EqualTo(982738));
            Assert.That(result.Snapshot.Connections[0]?.VirtualHost, Is.EqualTo("TestVirtualHost"));
            Assert.That(result.Snapshot.Connections[0]?.NodeIdentifier, Is.EqualTo("Node 1"));
            Assert.That(result.Snapshot.Connections[0]?.NetworkTraffic?.Received?.Total, Is.EqualTo(68721979894793));
            Assert.That(result.Snapshot.Connections[0]?.NetworkTraffic?.Sent?.Total, Is.EqualTo(871998847));
            Assert.That(result.Snapshot.Connections[0]?.NetworkTraffic?.MaxFrameSize, Is.EqualTo(627378937423));
            Assert.That(result.Snapshot.Connections[0]?.Channels.Any(), Is.True);
            Assert.That(result.Snapshot.Connections[0]?.Channels[0]?.Identifier, Is.EqualTo("Channel 1"));
            Assert.That(result.Snapshot.Connections[0]?.Channels[0]?.Consumers, Is.EqualTo(90));
            Assert.That(result.Snapshot.Connections[0]?.Channels[0]?.PrefetchCount, Is.EqualTo(78));
            Assert.That(result.Snapshot.Connections[0]?.Channels[0]?.UnacknowledgedMessages, Is.EqualTo(7882003));
            Assert.That(result.Snapshot.Connections[0]?.Channels[0]?.UncommittedAcknowledgements, Is.EqualTo(98237843));
            Assert.That(result.Snapshot.Connections[0]?.Channels[0]?.UnconfirmedMessages, Is.EqualTo(82930));
            Assert.That(result.Snapshot.Connections[0]?.Channels[0]?.UncommittedMessages, Is.EqualTo(383902));
        });
    }

    [Test]
    public async Task Verify_can_return_snapshot2()
    {
        var result = await _services.GetService<ISnapshotFactory>()
            .TakeConnectivitySnapshot(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Snapshot, Is.Not.Null);
            Assert.That(result.Snapshot.Connections, Is.Not.Null);
            Assert.That(result.Snapshot.Connections[0]?.NetworkTraffic, Is.Not.Null);
            Assert.That(result.Snapshot.Connections[0]?.NetworkTraffic?.Received, Is.Not.Null);
            Assert.That(result.Snapshot.Connections[0]?.NetworkTraffic?.Sent, Is.Not.Null);
            Assert.That(result.Snapshot.Connections[0]?.Channels, Is.Not.Null);
            Assert.That(result.Snapshot.BrokerVersion, Is.EqualTo("3.7.18"));
            Assert.That(result.Snapshot.ClusterName, Is.EqualTo("fake_cluster"));
            Assert.That(result.Snapshot.Connections[0]?.Identifier, Is.EqualTo("Connection 1"));
            Assert.That(result.Snapshot.Connections[0]?.State, Is.EqualTo(BrokerConnectionState.Blocked));
            Assert.That(result.Snapshot.Connections[0]?.OpenChannelsLimit, Is.EqualTo(982738));
            Assert.That(result.Snapshot.Connections[0]?.VirtualHost, Is.EqualTo("TestVirtualHost"));
            Assert.That(result.Snapshot.Connections[0]?.NodeIdentifier, Is.EqualTo("Node 1"));
            Assert.That(result.Snapshot.Connections[0]?.NetworkTraffic?.Received?.Total, Is.EqualTo(68721979894793));
            Assert.That(result.Snapshot.Connections[0]?.NetworkTraffic?.Sent?.Total, Is.EqualTo(871998847));
            Assert.That(result.Snapshot.Connections[0]?.NetworkTraffic?.MaxFrameSize, Is.EqualTo(627378937423));
            Assert.That(result.Snapshot.Connections[0]?.Channels.Any(), Is.True);
            Assert.That(result.Snapshot.Connections[0]?.Channels[0]?.Identifier, Is.EqualTo("Channel 1"));
            Assert.That(result.Snapshot.Connections[0]?.Channels[0]?.Consumers, Is.EqualTo(90));
            Assert.That(result.Snapshot.Connections[0]?.Channels[0]?.PrefetchCount, Is.EqualTo(78));
            Assert.That(result.Snapshot.Connections[0]?.Channels[0]?.UnacknowledgedMessages, Is.EqualTo(7882003));
            Assert.That(result.Snapshot.Connections[0]?.Channels[0]?.UncommittedAcknowledgements, Is.EqualTo(98237843));
            Assert.That(result.Snapshot.Connections[0]?.Channels[0]?.UnconfirmedMessages, Is.EqualTo(82930));
            Assert.That(result.Snapshot.Connections[0]?.Channels[0]?.UncommittedMessages, Is.EqualTo(383902));
        });
    }
}