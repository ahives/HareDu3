namespace HareDu.Snapshotting.Tests;

using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Extensions;
using Fakes;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;

[TestFixture]
public class ClusterSnapshotTests
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
            .Lens<ClusterSnapshot>()
            .TakeSnapshot(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0], Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.Memory, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.OS, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.OS?.FileDescriptors, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.OS?.SocketDescriptors, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.Disk, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.Disk?.Capacity, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.Disk?.IO?.Writes, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.Disk?.IO, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.Disk?.IO?.Writes?.Bytes, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.Disk?.IO?.Reads, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.Disk?.IO?.Reads?.Bytes, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.Runtime, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.Runtime.Processes, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.Memory?.AlarmInEffect, Is.True);
            Assert.That(result.Snapshot.Nodes[0]?.Disk?.AlarmInEffect, Is.True);
            Assert.That(result.Snapshot.Nodes[0]?.IsRunning, Is.True);
            Assert.That(result.Snapshot.BrokerVersion, Is.EqualTo("3.7.18"));
            Assert.That(result.Snapshot.ClusterName, Is.EqualTo("fake_cluster"));
            Assert.That(result.Snapshot.Nodes[0].Memory?.Limit, Is.EqualTo(723746434));
            Assert.That(result.Snapshot.Nodes[0].OS?.ProcessId, Is.EqualTo("OS123"));
            Assert.That(result.Snapshot.Nodes[0].OS?.FileDescriptors?.Used, Is.EqualTo(9203797));
            Assert.That(result.Snapshot.Nodes[0].OS?.SocketDescriptors?.Used, Is.EqualTo(8298347));
            Assert.That(result.Snapshot.Nodes[0].Disk?.Capacity?.Available, Is.EqualTo(7265368234));
            Assert.That(result.Snapshot.Nodes[0].Disk?.Limit, Is.EqualTo(8928739432));
            Assert.That(result.Snapshot.Nodes[0].AvailableCoresDetected, Is.EqualTo(8));
            Assert.That(result.Snapshot.Nodes[0].Disk?.IO?.Writes?.Total, Is.EqualTo(36478608776));
            Assert.That(result.Snapshot.Nodes[0].Disk?.IO?.Writes?.Bytes?.Total, Is.EqualTo(728364283));
            Assert.That(result.Snapshot.Nodes[0].Disk?.IO?.Reads?.Total, Is.EqualTo(892793874982));
            Assert.That(result.Snapshot.Nodes[0].Disk?.IO?.Reads?.Bytes?.Total, Is.EqualTo(78738764));
            Assert.That(result.Snapshot.Nodes[0].Runtime.Processes.Used, Is.EqualTo(9199849));
            Assert.That(result.Snapshot.Nodes[0]?.NetworkPartitions, Is.EquivalentTo(new List<string>{"partition1", "partition2", "partition3", "partition4"}));
        });
    }

    [Test]
    public async Task Verify_can_return_snapshot2()
    {
        var result = await _services.GetService<ISnapshotFactory>()
            .TakeClusterSnapshot(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0], Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.Memory, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.OS, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.OS?.FileDescriptors, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.OS?.SocketDescriptors, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.Disk, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.Disk?.Capacity, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.Disk?.IO?.Writes, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.Disk?.IO, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.Disk?.IO?.Writes?.Bytes, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.Disk?.IO?.Reads, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.Disk?.IO?.Reads?.Bytes, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.Runtime, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.Runtime.Processes, Is.Not.Null);
            Assert.That(result.Snapshot.Nodes[0]?.Memory?.AlarmInEffect, Is.True);
            Assert.That(result.Snapshot.Nodes[0]?.Disk?.AlarmInEffect, Is.True);
            Assert.That(result.Snapshot.Nodes[0]?.IsRunning, Is.True);
            Assert.That(result.Snapshot.BrokerVersion, Is.EqualTo("3.7.18"));
            Assert.That(result.Snapshot.ClusterName, Is.EqualTo("fake_cluster"));
            Assert.That(result.Snapshot.Nodes[0].Memory?.Limit, Is.EqualTo(723746434));
            Assert.That(result.Snapshot.Nodes[0].OS?.ProcessId, Is.EqualTo("OS123"));
            Assert.That(result.Snapshot.Nodes[0].OS?.FileDescriptors?.Used, Is.EqualTo(9203797));
            Assert.That(result.Snapshot.Nodes[0].OS?.SocketDescriptors?.Used, Is.EqualTo(8298347));
            Assert.That(result.Snapshot.Nodes[0].Disk?.Capacity?.Available, Is.EqualTo(7265368234));
            Assert.That(result.Snapshot.Nodes[0].Disk?.Limit, Is.EqualTo(8928739432));
            Assert.That(result.Snapshot.Nodes[0].AvailableCoresDetected, Is.EqualTo(8));
            Assert.That(result.Snapshot.Nodes[0].Disk?.IO?.Writes?.Total, Is.EqualTo(36478608776));
            Assert.That(result.Snapshot.Nodes[0].Disk?.IO?.Writes?.Bytes?.Total, Is.EqualTo(728364283));
            Assert.That(result.Snapshot.Nodes[0].Disk?.IO?.Reads?.Total, Is.EqualTo(892793874982));
            Assert.That(result.Snapshot.Nodes[0].Disk?.IO?.Reads?.Bytes?.Total, Is.EqualTo(78738764));
            Assert.That(result.Snapshot.Nodes[0].Runtime.Processes.Used, Is.EqualTo(9199849));
            Assert.That(result.Snapshot.Nodes[0]?.NetworkPartitions, Is.EquivalentTo(new List<string>{"partition1", "partition2", "partition3", "partition4"}));
        });
    }
}