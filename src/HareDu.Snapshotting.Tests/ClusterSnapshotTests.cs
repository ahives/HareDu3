namespace HareDu.Snapshotting.Tests;

using System.Collections.Generic;
using System.Threading.Tasks;
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
            .AddSingleton<IBrokerFactory, FakeBrokerFactory>()
            .AddSingleton<ISnapshotFactory, SnapshotFactory>()
            .BuildServiceProvider();
    }

    [Test]
    public async Task Verify_can_return_snapshot1()
    {
        var result = await _services.GetService<ISnapshotFactory>()
            .Lens<ClusterSnapshot>()
            .TakeSnapshot();

        Assert.Multiple(() =>
        {
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Snapshot.Nodes[0]);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.Memory);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.OS);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.OS?.FileDescriptors);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.OS?.SocketDescriptors);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.Disk);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.Disk?.Capacity);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.Disk?.IO?.Writes);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.Disk?.IO);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.Disk?.IO?.Writes?.Bytes);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.Disk?.IO?.Reads);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.Disk?.IO?.Reads?.Bytes);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.Runtime);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.Runtime.Processes);
            Assert.IsTrue(result.Snapshot.Nodes[0]?.Memory?.AlarmInEffect);
            Assert.IsTrue(result.Snapshot.Nodes[0]?.Disk?.AlarmInEffect);
            Assert.IsTrue(result.Snapshot.Nodes[0]?.IsRunning);
            Assert.AreEqual("3.7.18", result.Snapshot.BrokerVersion);
            Assert.AreEqual("fake_cluster", result.Snapshot.ClusterName);
            Assert.AreEqual(723746434, result.Snapshot.Nodes[0].Memory?.Limit);
            Assert.AreEqual("OS123", result.Snapshot.Nodes[0].OS?.ProcessId);
            Assert.AreEqual(9203797, result.Snapshot.Nodes[0].OS?.FileDescriptors?.Used);
            Assert.AreEqual(8298347, result.Snapshot.Nodes[0].OS?.SocketDescriptors?.Used);
            Assert.AreEqual(7265368234, result.Snapshot.Nodes[0].Disk?.Capacity?.Available);
            Assert.AreEqual(8928739432, result.Snapshot.Nodes[0].Disk?.Limit);
            Assert.AreEqual(8, result.Snapshot.Nodes[0].AvailableCoresDetected);
            Assert.AreEqual(36478608776, result.Snapshot.Nodes[0].Disk?.IO?.Writes?.Total);
            Assert.AreEqual(728364283, result.Snapshot.Nodes[0].Disk?.IO?.Writes?.Bytes?.Total);
            Assert.AreEqual(892793874982, result.Snapshot.Nodes[0].Disk?.IO?.Reads?.Total);
            Assert.AreEqual(78738764, result.Snapshot.Nodes[0].Disk?.IO?.Reads?.Bytes?.Total);
            Assert.AreEqual(9199849, result.Snapshot.Nodes[0].Runtime.Processes.Used);
            Assert.AreEqual(new List<string>{"partition1", "partition2", "partition3", "partition4"}, result.Snapshot.Nodes[0]?.NetworkPartitions);
        });
    }

    [Test]
    public async Task Verify_can_return_snapshot2()
    {
        var result = await _services.GetService<ISnapshotFactory>()
            .TakeClusterSnapshot();

        Assert.Multiple(() =>
        {
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Snapshot.Nodes[0]);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.Memory);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.OS);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.OS?.FileDescriptors);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.OS?.SocketDescriptors);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.Disk);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.Disk?.Capacity);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.Disk?.IO?.Writes);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.Disk?.IO);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.Disk?.IO?.Writes?.Bytes);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.Disk?.IO?.Reads);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.Disk?.IO?.Reads?.Bytes);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.Runtime);
            Assert.IsNotNull(result.Snapshot.Nodes[0]?.Runtime.Processes);
            Assert.IsTrue(result.Snapshot.Nodes[0]?.Memory?.AlarmInEffect);
            Assert.IsTrue(result.Snapshot.Nodes[0]?.Disk?.AlarmInEffect);
            Assert.IsTrue(result.Snapshot.Nodes[0]?.IsRunning);
            Assert.AreEqual("3.7.18", result.Snapshot.BrokerVersion);
            Assert.AreEqual("fake_cluster", result.Snapshot.ClusterName);
            Assert.AreEqual(723746434, result.Snapshot.Nodes[0].Memory?.Limit);
            Assert.AreEqual("OS123", result.Snapshot.Nodes[0].OS?.ProcessId);
            Assert.AreEqual(9203797, result.Snapshot.Nodes[0].OS?.FileDescriptors?.Used);
            Assert.AreEqual(8298347, result.Snapshot.Nodes[0].OS?.SocketDescriptors?.Used);
            Assert.AreEqual(7265368234, result.Snapshot.Nodes[0].Disk?.Capacity?.Available);
            Assert.AreEqual(8928739432, result.Snapshot.Nodes[0].Disk?.Limit);
            Assert.AreEqual(8, result.Snapshot.Nodes[0].AvailableCoresDetected);
            Assert.AreEqual(36478608776, result.Snapshot.Nodes[0].Disk?.IO?.Writes?.Total);
            Assert.AreEqual(728364283, result.Snapshot.Nodes[0].Disk?.IO?.Writes?.Bytes?.Total);
            Assert.AreEqual(892793874982, result.Snapshot.Nodes[0].Disk?.IO?.Reads?.Total);
            Assert.AreEqual(78738764, result.Snapshot.Nodes[0].Disk?.IO?.Reads?.Bytes?.Total);
            Assert.AreEqual(9199849, result.Snapshot.Nodes[0].Runtime.Processes.Used);
            Assert.AreEqual(new List<string>{"partition1", "partition2", "partition3", "partition4"}, result.Snapshot.Nodes[0]?.NetworkPartitions);
        });
    }
}