namespace HareDu.Snapshotting.Tests
{
    using System.Linq;
    using System.Threading.Tasks;
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
                .AddSingleton<IBrokerObjectFactory, FakeBrokerObjectFactory>()
                .AddSingleton<ISnapshotFactory, SnapshotFactory>()
                .BuildServiceProvider();
        }
        
        [Test]
        public async Task Verify_can_return_snapshot1()
        {
            var result = await _services.GetService<ISnapshotFactory>()
                .Lens<BrokerQueuesSnapshot>()
                .TakeSnapshot();

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result.Snapshot.Queues);
                Assert.IsNotNull(result.Snapshot.Queues[0].Memory);
                Assert.IsNotNull(result.Snapshot.Queues[0].Memory.PagedOut);
                Assert.IsNotNull(result.Snapshot.Queues[0].Messages.Acknowledged);
                Assert.IsNotNull(result.Snapshot.Queues[0].Messages.Aggregate);
                Assert.IsNotNull(result.Snapshot.Queues[0].Messages.Delivered);
                Assert.IsNotNull(result.Snapshot.Queues[0].Messages.Gets);
                Assert.IsNotNull(result.Snapshot.Queues[0].Messages.Incoming);
                Assert.IsNotNull(result.Snapshot.Queues[0].Messages.Ready);
                Assert.IsNotNull(result.Snapshot.Queues[0].Messages.Redelivered);
                Assert.IsNotNull(result.Snapshot.Queues[0].Messages.Unacknowledged);
                Assert.IsNotNull(result.Snapshot.Queues[0].Messages.DeliveredGets);
                Assert.IsNotNull(result.Snapshot.Queues[0].Messages.DeliveredWithoutAck);
                Assert.IsNotNull(result.Snapshot.Queues[0].Messages.GetsWithoutAck);
                Assert.IsNotNull(result.Snapshot.Churn);
                Assert.IsNotNull(result.Snapshot.Churn.Broker);
                Assert.IsNotNull(result.Snapshot.Churn.Delivered);
                Assert.IsNotNull(result.Snapshot.Churn.Gets);
                Assert.IsNotNull(result.Snapshot.Churn.Incoming);
                Assert.IsNotNull(result.Snapshot.Churn.Ready);
                Assert.IsNotNull(result.Snapshot.Churn.Redelivered);
                Assert.IsNotNull(result.Snapshot.Churn.Unacknowledged);
                Assert.IsNotNull(result.Snapshot.Churn.DeliveredGets);
                Assert.IsNotNull(result.Snapshot.Churn.NotRouted);
                Assert.IsNotNull(result.Snapshot.Churn.DeliveredWithoutAck);
                Assert.IsNotNull(result.Snapshot.Churn.GetsWithoutAck);
                Assert.IsTrue(result.Snapshot.Queues.Any());
                Assert.AreEqual("fake_cluster", result.Snapshot.ClusterName);
                Assert.AreEqual(773709938, result.Snapshot.Queues[0].Consumers);
                Assert.AreEqual(92990390, result.Snapshot.Queues[0].Memory.Total);
                Assert.AreEqual(90290398, result.Snapshot.Queues[0].Memory.PagedOut.Total);
                Assert.AreEqual(239939803, result.Snapshot.Queues[0].Memory.PagedOut.Bytes);
                Assert.AreEqual(92303949398, result.Snapshot.Queues[0].Messages.Acknowledged.Total);
                Assert.AreEqual(7823668, result.Snapshot.Queues[0].Messages.Aggregate.Total);
                Assert.AreEqual(238847970, result.Snapshot.Queues[0].Messages.Delivered.Total);
                Assert.AreEqual(82938820903, result.Snapshot.Queues[0].Messages.Gets.Total);
                Assert.AreEqual(763928923, result.Snapshot.Queues[0].Messages.Incoming.Total);
                Assert.AreEqual(9293093, result.Snapshot.Queues[0].Messages.Ready.Total);
                Assert.AreEqual(488983002934, result.Snapshot.Queues[0].Messages.Redelivered.Total);
                Assert.AreEqual(7273020, result.Snapshot.Queues[0].Messages.Unacknowledged.Total);
                Assert.AreEqual(50092830929, result.Snapshot.Queues[0].Messages.DeliveredGets.Total);
                Assert.AreEqual(48898693232, result.Snapshot.Queues[0].Messages.DeliveredWithoutAck.Total);
                Assert.AreEqual(23997979383, result.Snapshot.Queues[0].Messages.GetsWithoutAck.Total);
                Assert.AreEqual(7287736, result.Snapshot.Churn.Acknowledged.Total);
                Assert.AreEqual(9230748297, result.Snapshot.Churn.Broker.Total);
                Assert.AreEqual(80.3M, result.Snapshot.Churn.Broker.Rate);
                Assert.AreEqual(7234, result.Snapshot.Churn.Delivered.Total);
                Assert.AreEqual(84, result.Snapshot.Churn.Delivered.Rate);
                Assert.AreEqual(723, result.Snapshot.Churn.Gets.Total);
                Assert.AreEqual(324, result.Snapshot.Churn.Gets.Rate);
                Assert.AreEqual(1234, result.Snapshot.Churn.Incoming.Total);
                Assert.AreEqual(7, result.Snapshot.Churn.Incoming.Rate);
                Assert.AreEqual(82937489379, result.Snapshot.Churn.Ready.Total);
                Assert.AreEqual(34.4M, result.Snapshot.Churn.Ready.Rate);
                Assert.AreEqual(7237, result.Snapshot.Churn.Redelivered.Total);
                Assert.AreEqual(89, result.Snapshot.Churn.Redelivered.Rate);
                Assert.AreEqual(892387397238, result.Snapshot.Churn.Unacknowledged.Total);
                Assert.AreEqual(73.3M, result.Snapshot.Churn.Unacknowledged.Rate);
                Assert.AreEqual(78263767, result.Snapshot.Churn.DeliveredGets.Total);
                Assert.AreEqual(738, result.Snapshot.Churn.DeliveredGets.Rate);
                Assert.AreEqual(737, result.Snapshot.Churn.NotRouted.Total);
                Assert.AreEqual(48, result.Snapshot.Churn.NotRouted.Rate);
                Assert.AreEqual(8723, result.Snapshot.Churn.DeliveredWithoutAck.Total);
                Assert.AreEqual(56, result.Snapshot.Churn.DeliveredWithoutAck.Rate);
                Assert.AreEqual(373, result.Snapshot.Churn.GetsWithoutAck.Total);
                Assert.AreEqual(84, result.Snapshot.Churn.GetsWithoutAck.Rate);
            });
        }
        
        [Test]
        public async Task Verify_can_return_snapshot2()
        {
            var result = await _services.GetService<ISnapshotFactory>()
                .TakeQueueSnapshot();

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result.Snapshot.Queues);
                Assert.IsNotNull(result.Snapshot.Queues[0].Memory);
                Assert.IsNotNull(result.Snapshot.Queues[0].Memory.PagedOut);
                Assert.IsNotNull(result.Snapshot.Queues[0].Messages.Acknowledged);
                Assert.IsNotNull(result.Snapshot.Queues[0].Messages.Aggregate);
                Assert.IsNotNull(result.Snapshot.Queues[0].Messages.Delivered);
                Assert.IsNotNull(result.Snapshot.Queues[0].Messages.Gets);
                Assert.IsNotNull(result.Snapshot.Queues[0].Messages.Incoming);
                Assert.IsNotNull(result.Snapshot.Queues[0].Messages.Ready);
                Assert.IsNotNull(result.Snapshot.Queues[0].Messages.Redelivered);
                Assert.IsNotNull(result.Snapshot.Queues[0].Messages.Unacknowledged);
                Assert.IsNotNull(result.Snapshot.Queues[0].Messages.DeliveredGets);
                Assert.IsNotNull(result.Snapshot.Queues[0].Messages.DeliveredWithoutAck);
                Assert.IsNotNull(result.Snapshot.Queues[0].Messages.GetsWithoutAck);
                Assert.IsNotNull(result.Snapshot.Churn);
                Assert.IsNotNull(result.Snapshot.Churn.Broker);
                Assert.IsNotNull(result.Snapshot.Churn.Delivered);
                Assert.IsNotNull(result.Snapshot.Churn.Gets);
                Assert.IsNotNull(result.Snapshot.Churn.Incoming);
                Assert.IsNotNull(result.Snapshot.Churn.Ready);
                Assert.IsNotNull(result.Snapshot.Churn.Redelivered);
                Assert.IsNotNull(result.Snapshot.Churn.Unacknowledged);
                Assert.IsNotNull(result.Snapshot.Churn.DeliveredGets);
                Assert.IsNotNull(result.Snapshot.Churn.NotRouted);
                Assert.IsNotNull(result.Snapshot.Churn.DeliveredWithoutAck);
                Assert.IsNotNull(result.Snapshot.Churn.GetsWithoutAck);
                Assert.IsTrue(result.Snapshot.Queues.Any());
                Assert.AreEqual("fake_cluster", result.Snapshot.ClusterName);
                Assert.AreEqual(773709938, result.Snapshot.Queues[0].Consumers);
                Assert.AreEqual(92990390, result.Snapshot.Queues[0].Memory.Total);
                Assert.AreEqual(90290398, result.Snapshot.Queues[0].Memory.PagedOut.Total);
                Assert.AreEqual(239939803, result.Snapshot.Queues[0].Memory.PagedOut.Bytes);
                Assert.AreEqual(92303949398, result.Snapshot.Queues[0].Messages.Acknowledged.Total);
                Assert.AreEqual(7823668, result.Snapshot.Queues[0].Messages.Aggregate.Total);
                Assert.AreEqual(238847970, result.Snapshot.Queues[0].Messages.Delivered.Total);
                Assert.AreEqual(82938820903, result.Snapshot.Queues[0].Messages.Gets.Total);
                Assert.AreEqual(763928923, result.Snapshot.Queues[0].Messages.Incoming.Total);
                Assert.AreEqual(9293093, result.Snapshot.Queues[0].Messages.Ready.Total);
                Assert.AreEqual(488983002934, result.Snapshot.Queues[0].Messages.Redelivered.Total);
                Assert.AreEqual(7273020, result.Snapshot.Queues[0].Messages.Unacknowledged.Total);
                Assert.AreEqual(50092830929, result.Snapshot.Queues[0].Messages.DeliveredGets.Total);
                Assert.AreEqual(48898693232, result.Snapshot.Queues[0].Messages.DeliveredWithoutAck.Total);
                Assert.AreEqual(23997979383, result.Snapshot.Queues[0].Messages.GetsWithoutAck.Total);
                Assert.AreEqual(7287736, result.Snapshot.Churn.Acknowledged.Total);
                Assert.AreEqual(9230748297, result.Snapshot.Churn.Broker.Total);
                Assert.AreEqual(80.3M, result.Snapshot.Churn.Broker.Rate);
                Assert.AreEqual(7234, result.Snapshot.Churn.Delivered.Total);
                Assert.AreEqual(84, result.Snapshot.Churn.Delivered.Rate);
                Assert.AreEqual(723, result.Snapshot.Churn.Gets.Total);
                Assert.AreEqual(324, result.Snapshot.Churn.Gets.Rate);
                Assert.AreEqual(1234, result.Snapshot.Churn.Incoming.Total);
                Assert.AreEqual(7, result.Snapshot.Churn.Incoming.Rate);
                Assert.AreEqual(82937489379, result.Snapshot.Churn.Ready.Total);
                Assert.AreEqual(34.4M, result.Snapshot.Churn.Ready.Rate);
                Assert.AreEqual(7237, result.Snapshot.Churn.Redelivered.Total);
                Assert.AreEqual(89, result.Snapshot.Churn.Redelivered.Rate);
                Assert.AreEqual(892387397238, result.Snapshot.Churn.Unacknowledged.Total);
                Assert.AreEqual(73.3M, result.Snapshot.Churn.Unacknowledged.Rate);
                Assert.AreEqual(78263767, result.Snapshot.Churn.DeliveredGets.Total);
                Assert.AreEqual(738, result.Snapshot.Churn.DeliveredGets.Rate);
                Assert.AreEqual(737, result.Snapshot.Churn.NotRouted.Total);
                Assert.AreEqual(48, result.Snapshot.Churn.NotRouted.Rate);
                Assert.AreEqual(8723, result.Snapshot.Churn.DeliveredWithoutAck.Total);
                Assert.AreEqual(56, result.Snapshot.Churn.DeliveredWithoutAck.Rate);
                Assert.AreEqual(373, result.Snapshot.Churn.GetsWithoutAck.Total);
                Assert.AreEqual(84, result.Snapshot.Churn.GetsWithoutAck.Rate);
            });
        }
    }
}