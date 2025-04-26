namespace HareDu.Tests;

using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

[TestFixture]
public class ConsumerTests :
    HareDuTesting
{
    [Test]
    public async Task Verify_can_get_all_consumers1()
    {
        var services = GetContainerBuilder("TestData/ConsumerInfo.json")
            .BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<Consumer>(x => x.UsingCredentials("guest", "guest"))
            .GetAll();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data[0].ChannelDetails);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(2, result.Data.Count);
            Assert.AreEqual("127.0.0.0:61113 -> 127.0.0.0:5672 (1)", result.Data[0].ChannelDetails?.Name);
            Assert.AreEqual("rabbit@localhost", result.Data[0].ChannelDetails?.Node);
            Assert.AreEqual(1, result.Data[0].ChannelDetails?.Number);
            Assert.AreEqual("127.0.0.0:61113 -> 127.0.0.0:5672", result.Data[0].ChannelDetails?.ConnectionName);
            Assert.AreEqual("127.0.0.0", result.Data[0].ChannelDetails?.PeerHost);
            Assert.AreEqual(99883, result.Data[0].ChannelDetails?.PeerPort);
            Assert.AreEqual("guest", result.Data[0].ChannelDetails?.User);
            Assert.IsNotNull(result.Data[0].QueueConsumerDetails);
            Assert.AreEqual("fake_queue", result.Data[0].QueueConsumerDetails?.Name);
            Assert.AreEqual("TestVirtualHost", result.Data[0].QueueConsumerDetails?.VirtualHost);
            Assert.AreEqual("amq.ctag-fOtZo9ajuHDYQQ5hzrgasA", result.Data[0].ConsumerTag);
            Assert.AreEqual(0, result.Data[0].PreFetchCount);
            Assert.IsFalse(result.Data[0].Exclusive);
            Assert.IsTrue(result.Data[0].AcknowledgementRequired);
        });
    }

    [Test]
    public async Task Verify_can_get_all_consumers_in_vhost()
    {
        var services = GetContainerBuilder("TestData/ConsumerInfo.json")
            .BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<Consumer>(x => x.UsingCredentials("guest", "guest"))
            .GetByVirtualHost("TestVirtualHost1");

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data[0].ChannelDetails);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(2, result.Data.Count);
            Assert.AreEqual("127.0.0.0:61113 -> 127.0.0.0:5672 (1)", result.Data[0].ChannelDetails?.Name);
            Assert.AreEqual("rabbit@localhost", result.Data[0].ChannelDetails?.Node);
            Assert.AreEqual(1, result.Data[0].ChannelDetails?.Number);
            Assert.AreEqual("127.0.0.0:61113 -> 127.0.0.0:5672", result.Data[0].ChannelDetails?.ConnectionName);
            Assert.AreEqual("127.0.0.0", result.Data[0].ChannelDetails?.PeerHost);
            Assert.AreEqual(99883, result.Data[0].ChannelDetails?.PeerPort);
            Assert.AreEqual("guest", result.Data[0].ChannelDetails?.User);
            Assert.IsNotNull(result.Data[0].QueueConsumerDetails);
            Assert.AreEqual("fake_queue", result.Data[0].QueueConsumerDetails?.Name);
            Assert.AreEqual("TestVirtualHost1", result.Data[0].QueueConsumerDetails?.VirtualHost);
            Assert.AreEqual("amq.ctag-fOtZo9ajuHDYQQ5hzrgasA", result.Data[0].ConsumerTag);
            Assert.AreEqual(0, result.Data[0].PreFetchCount);
            Assert.IsFalse(result.Data[0].Exclusive);
            Assert.IsTrue(result.Data[0].AcknowledgementRequired);
        });
    }

    [Test]
    public async Task Verify_cannot_get_consumers_in_vhost_with_missing_name()
    {
        var services = GetContainerBuilder("TestData/ConsumerInfo.json")
            .BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<Consumer>(x => x.UsingCredentials("guest", "guest"))
            .GetByVirtualHost(string.Empty);

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsFalse(result.HasData);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_can_get_all_consumers2()
    {
        var services = GetContainerBuilder("TestData/ConsumerInfo.json")
            .BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .GetAllConsumers(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data[0].ChannelDetails);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(2, result.Data.Count);
            Assert.AreEqual("127.0.0.0:61113 -> 127.0.0.0:5672 (1)", result.Data[0].ChannelDetails?.Name);
            Assert.AreEqual("rabbit@localhost", result.Data[0].ChannelDetails?.Node);
            Assert.AreEqual(1, result.Data[0].ChannelDetails?.Number);
            Assert.AreEqual("127.0.0.0:61113 -> 127.0.0.0:5672", result.Data[0].ChannelDetails?.ConnectionName);
            Assert.AreEqual("127.0.0.0", result.Data[0].ChannelDetails?.PeerHost);
            Assert.AreEqual(99883, result.Data[0].ChannelDetails?.PeerPort);
            Assert.AreEqual("guest", result.Data[0].ChannelDetails?.User);
            Assert.IsNotNull(result.Data[0].QueueConsumerDetails);
            Assert.AreEqual("fake_queue", result.Data[0].QueueConsumerDetails?.Name);
            Assert.AreEqual("TestVirtualHost", result.Data[0].QueueConsumerDetails?.VirtualHost);
            Assert.AreEqual("amq.ctag-fOtZo9ajuHDYQQ5hzrgasA", result.Data[0].ConsumerTag);
            Assert.AreEqual(0, result.Data[0].PreFetchCount);
            Assert.IsFalse(result.Data[0].Exclusive);
            Assert.IsTrue(result.Data[0].AcknowledgementRequired);
        });
    }
}