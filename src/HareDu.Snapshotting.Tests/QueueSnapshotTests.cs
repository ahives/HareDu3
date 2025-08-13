namespace HareDu.Snapshotting.Tests;

using System.Linq;
using System.Threading.Tasks;
using Core;
using Extensions;
using Fakes;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;

[TestFixture]
public class QueueSnapshotTests
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
            .Lens<BrokerQueuesSnapshot>()
            .TakeSnapshot(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.Snapshot.Queues, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Memory, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Memory.PagedOut, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Messages.Acknowledged, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Messages.Aggregate, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Messages.Delivered, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Messages.Gets, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Messages.Incoming, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Messages.Ready, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Messages.Redelivered, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Messages.Unacknowledged, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Messages.DeliveredGets, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Messages.DeliveredWithoutAck, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Messages.GetsWithoutAck, Is.Not.Null);
            Assert.That(result.Snapshot.Churn, Is.Not.Null);
            Assert.That(result.Snapshot.Churn.Broker, Is.Not.Null);
            Assert.That(result.Snapshot.Churn.Delivered, Is.Not.Null);
            Assert.That(result.Snapshot.Churn.Gets, Is.Not.Null);
            Assert.That(result.Snapshot.Churn.Incoming, Is.Not.Null);
            Assert.That(result.Snapshot.Churn.Ready, Is.Not.Null);
            Assert.That(result.Snapshot.Churn.Redelivered, Is.Not.Null);
            Assert.That(result.Snapshot.Churn.Unacknowledged, Is.Not.Null);
            Assert.That(result.Snapshot.Churn.DeliveredGets, Is.Not.Null);
            Assert.That(result.Snapshot.Churn.NotRouted, Is.Not.Null);
            Assert.That(result.Snapshot.Churn.DeliveredWithoutAck, Is.Not.Null);
            Assert.That(result.Snapshot.Churn.GetsWithoutAck, Is.Not.Null);
            Assert.That(result.Snapshot.Queues.Any(), Is.True);
            Assert.That(result.Snapshot.ClusterName, Is.EqualTo("fake_cluster"));
            Assert.That(result.Snapshot.Queues[0].Consumers, Is.EqualTo(773709938));
            Assert.That(result.Snapshot.Queues[0].Memory.Total, Is.EqualTo(92990390));
            Assert.That(result.Snapshot.Queues[0].Memory.PagedOut.Total, Is.EqualTo(90290398));
            Assert.That(result.Snapshot.Queues[0].Memory.PagedOut.Bytes, Is.EqualTo(239939803));
            Assert.That(result.Snapshot.Queues[0].Messages.Acknowledged.Total, Is.EqualTo(92303949398));
            Assert.That(result.Snapshot.Queues[0].Messages.Aggregate.Total, Is.EqualTo(7823668));
            Assert.That(result.Snapshot.Queues[0].Messages.Delivered.Total, Is.EqualTo(238847970));
            Assert.That(result.Snapshot.Queues[0].Messages.Gets.Total, Is.EqualTo(82938820903));
            Assert.That(result.Snapshot.Queues[0].Messages.Incoming.Total, Is.EqualTo(763928923));
            Assert.That(result.Snapshot.Queues[0].Messages.Ready.Total, Is.EqualTo(9293093));
            Assert.That(result.Snapshot.Queues[0].Messages.Redelivered.Total, Is.EqualTo(488983002934));
            Assert.That(result.Snapshot.Queues[0].Messages.Unacknowledged.Total, Is.EqualTo(7273020));
            Assert.That(result.Snapshot.Queues[0].Messages.DeliveredGets.Total, Is.EqualTo(50092830929));
            Assert.That(result.Snapshot.Queues[0].Messages.DeliveredWithoutAck.Total, Is.EqualTo(48898693232));
            Assert.That(result.Snapshot.Queues[0].Messages.GetsWithoutAck.Total, Is.EqualTo(23997979383));
            Assert.That(result.Snapshot.Churn.Acknowledged.Total, Is.EqualTo(7287736));
            Assert.That(result.Snapshot.Churn.Broker.Total, Is.EqualTo(9230748297));
            Assert.That(result.Snapshot.Churn.Broker.Rate, Is.EqualTo(80.3M));
            Assert.That(result.Snapshot.Churn.Delivered.Total, Is.EqualTo(7234));
            Assert.That(result.Snapshot.Churn.Delivered.Rate, Is.EqualTo(84));
            Assert.That(result.Snapshot.Churn.Gets.Total, Is.EqualTo(723));
            Assert.That(result.Snapshot.Churn.Gets.Rate, Is.EqualTo(324));
            Assert.That(result.Snapshot.Churn.Incoming.Total, Is.EqualTo(1234));
            Assert.That(result.Snapshot.Churn.Incoming.Rate, Is.EqualTo(7));
            Assert.That(result.Snapshot.Churn.Ready.Total, Is.EqualTo(82937489379));
            Assert.That(result.Snapshot.Churn.Ready.Rate, Is.EqualTo(34.4M));
            Assert.That(result.Snapshot.Churn.Redelivered.Total, Is.EqualTo(7237));
            Assert.That(result.Snapshot.Churn.Redelivered.Rate, Is.EqualTo(89));
            Assert.That(result.Snapshot.Churn.Unacknowledged.Total, Is.EqualTo(892387397238));
            Assert.That(result.Snapshot.Churn.Unacknowledged.Rate, Is.EqualTo(73.3M));
            Assert.That(result.Snapshot.Churn.DeliveredGets.Total, Is.EqualTo(78263767));
            Assert.That(result.Snapshot.Churn.DeliveredGets.Rate, Is.EqualTo(738));
            Assert.That(result.Snapshot.Churn.NotRouted.Total, Is.EqualTo(737));
            Assert.That(result.Snapshot.Churn.NotRouted.Rate, Is.EqualTo(48));
            Assert.That(result.Snapshot.Churn.DeliveredWithoutAck.Total, Is.EqualTo(8723));
            Assert.That(result.Snapshot.Churn.DeliveredWithoutAck.Rate, Is.EqualTo(56));
            Assert.That(result.Snapshot.Churn.GetsWithoutAck.Total, Is.EqualTo(373));
            Assert.That(result.Snapshot.Churn.GetsWithoutAck.Rate, Is.EqualTo(84));
        });
    }
        
    [Test]
    public async Task Verify_can_return_snapshot2()
    {
        var result = await _services.GetService<ISnapshotFactory>()
            .TakeQueueSnapshot(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.Snapshot.Queues, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Memory, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Memory.PagedOut, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Messages.Acknowledged, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Messages.Aggregate, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Messages.Delivered, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Messages.Gets, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Messages.Incoming, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Messages.Ready, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Messages.Redelivered, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Messages.Unacknowledged, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Messages.DeliveredGets, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Messages.DeliveredWithoutAck, Is.Not.Null);
            Assert.That(result.Snapshot.Queues[0].Messages.GetsWithoutAck, Is.Not.Null);
            Assert.That(result.Snapshot.Churn, Is.Not.Null);
            Assert.That(result.Snapshot.Churn.Broker, Is.Not.Null);
            Assert.That(result.Snapshot.Churn.Delivered, Is.Not.Null);
            Assert.That(result.Snapshot.Churn.Gets, Is.Not.Null);
            Assert.That(result.Snapshot.Churn.Incoming, Is.Not.Null);
            Assert.That(result.Snapshot.Churn.Ready, Is.Not.Null);
            Assert.That(result.Snapshot.Churn.Redelivered, Is.Not.Null);
            Assert.That(result.Snapshot.Churn.Unacknowledged, Is.Not.Null);
            Assert.That(result.Snapshot.Churn.DeliveredGets, Is.Not.Null);
            Assert.That(result.Snapshot.Churn.NotRouted, Is.Not.Null);
            Assert.That(result.Snapshot.Churn.DeliveredWithoutAck, Is.Not.Null);
            Assert.That(result.Snapshot.Churn.GetsWithoutAck, Is.Not.Null);
            Assert.That(result.Snapshot.Queues.Any(), Is.True);
            Assert.That(result.Snapshot.ClusterName, Is.EqualTo("fake_cluster"));
            Assert.That(result.Snapshot.Queues[0].Consumers, Is.EqualTo(773709938));
            Assert.That(result.Snapshot.Queues[0].Memory.Total, Is.EqualTo(92990390));
            Assert.That(result.Snapshot.Queues[0].Memory.PagedOut.Total, Is.EqualTo(90290398));
            Assert.That(result.Snapshot.Queues[0].Memory.PagedOut.Bytes, Is.EqualTo(239939803));
            Assert.That(result.Snapshot.Queues[0].Messages.Acknowledged.Total, Is.EqualTo(92303949398));
            Assert.That(result.Snapshot.Queues[0].Messages.Aggregate.Total, Is.EqualTo(7823668));
            Assert.That(result.Snapshot.Queues[0].Messages.Delivered.Total, Is.EqualTo(238847970));
            Assert.That(result.Snapshot.Queues[0].Messages.Gets.Total, Is.EqualTo(82938820903));
            Assert.That(result.Snapshot.Queues[0].Messages.Incoming.Total, Is.EqualTo(763928923));
            Assert.That(result.Snapshot.Queues[0].Messages.Ready.Total, Is.EqualTo(9293093));
            Assert.That(result.Snapshot.Queues[0].Messages.Redelivered.Total, Is.EqualTo(488983002934));
            Assert.That(result.Snapshot.Queues[0].Messages.Unacknowledged.Total, Is.EqualTo(7273020));
            Assert.That(result.Snapshot.Queues[0].Messages.DeliveredGets.Total, Is.EqualTo(50092830929));
            Assert.That(result.Snapshot.Queues[0].Messages.DeliveredWithoutAck.Total, Is.EqualTo(48898693232));
            Assert.That(result.Snapshot.Queues[0].Messages.GetsWithoutAck.Total, Is.EqualTo(23997979383));
            Assert.That(result.Snapshot.Churn.Acknowledged.Total, Is.EqualTo(7287736));
            Assert.That(result.Snapshot.Churn.Broker.Total, Is.EqualTo(9230748297));
            Assert.That(result.Snapshot.Churn.Broker.Rate, Is.EqualTo(80.3M));
            Assert.That(result.Snapshot.Churn.Delivered.Total, Is.EqualTo(7234));
            Assert.That(result.Snapshot.Churn.Delivered.Rate, Is.EqualTo(84));
            Assert.That(result.Snapshot.Churn.Gets.Total, Is.EqualTo(723));
            Assert.That(result.Snapshot.Churn.Gets.Rate, Is.EqualTo(324));
            Assert.That(result.Snapshot.Churn.Incoming.Total, Is.EqualTo(1234));
            Assert.That(result.Snapshot.Churn.Incoming.Rate, Is.EqualTo(7));
            Assert.That(result.Snapshot.Churn.Ready.Total, Is.EqualTo(82937489379));
            Assert.That(result.Snapshot.Churn.Ready.Rate, Is.EqualTo(34.4M));
            Assert.That(result.Snapshot.Churn.Redelivered.Total, Is.EqualTo(7237));
            Assert.That(result.Snapshot.Churn.Redelivered.Rate, Is.EqualTo(89));
            Assert.That(result.Snapshot.Churn.Unacknowledged.Total, Is.EqualTo(892387397238));
            Assert.That(result.Snapshot.Churn.Unacknowledged.Rate, Is.EqualTo(73.3M));
            Assert.That(result.Snapshot.Churn.DeliveredGets.Total, Is.EqualTo(78263767));
            Assert.That(result.Snapshot.Churn.DeliveredGets.Rate, Is.EqualTo(738));
            Assert.That(result.Snapshot.Churn.NotRouted.Total, Is.EqualTo(737));
            Assert.That(result.Snapshot.Churn.NotRouted.Rate, Is.EqualTo(48));
            Assert.That(result.Snapshot.Churn.DeliveredWithoutAck.Total, Is.EqualTo(8723));
            Assert.That(result.Snapshot.Churn.DeliveredWithoutAck.Rate, Is.EqualTo(56));
            Assert.That(result.Snapshot.Churn.GetsWithoutAck.Total, Is.EqualTo(373));
            Assert.That(result.Snapshot.Churn.GetsWithoutAck.Rate, Is.EqualTo(84));
        });
    }
}