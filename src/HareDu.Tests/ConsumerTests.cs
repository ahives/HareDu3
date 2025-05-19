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
        var result = await GetContainerBuilder("TestData/ConsumerInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Consumer>(x => x.UsingCredentials("guest", "guest"))
            .GetAll();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data[0].ChannelDetails, Is.Not.Null);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(2));
            Assert.That(result.Data[0].ChannelDetails?.Name, Is.EqualTo("127.0.0.0:61113 -> 127.0.0.0:5672 (1)"));
            Assert.That(result.Data[0].ChannelDetails?.Node, Is.EqualTo("rabbit@localhost"));
            Assert.That(result.Data[0].ChannelDetails?.Number, Is.EqualTo(1));
            Assert.That(result.Data[0].ChannelDetails?.ConnectionName, Is.EqualTo("127.0.0.0:61113 -> 127.0.0.0:5672"));
            Assert.That(result.Data[0].ChannelDetails?.PeerPort, Is.EqualTo(99883));
            Assert.That(result.Data[0].ChannelDetails?.User, Is.EqualTo("guest"));
            Assert.That(result.Data[0].QueueConsumerDetails, Is.Not.Null);
            Assert.That(result.Data[0].QueueConsumerDetails?.Name, Is.EqualTo("fake_queue"));
            Assert.That(result.Data[0].QueueConsumerDetails?.VirtualHost, Is.EqualTo("TestVirtualHost1"));
            Assert.That(result.Data[0].ConsumerTag, Is.EqualTo("amq.ctag-fOtZo9ajuHDYQQ5hzrgasA"));
            Assert.That(result.Data[0].PreFetchCount, Is.EqualTo(0));
            Assert.That(result.Data[0].Exclusive, Is.False);
            Assert.That(result.Data[0].AcknowledgementRequired, Is.True);
        });
    }

    [Test]
    public async Task Verify_can_get_all_consumers_in_vhost()
    {
        var result = await GetContainerBuilder("TestData/ConsumerInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Consumer>(x => x.UsingCredentials("guest", "guest"))
            .GetByVirtualHost("TestVirtualHost1");

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data[0].ChannelDetails, Is.Not.Null);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(2));
            Assert.That(result.Data[0].ChannelDetails?.Name, Is.EqualTo("127.0.0.0:61113 -> 127.0.0.0:5672 (1)"));
            Assert.That(result.Data[0].ChannelDetails?.Node, Is.EqualTo("rabbit@localhost"));
            Assert.That(result.Data[0].ChannelDetails?.Number, Is.EqualTo(1));
            Assert.That(result.Data[0].ChannelDetails?.ConnectionName, Is.EqualTo("127.0.0.0:61113 -> 127.0.0.0:5672"));
            Assert.That(result.Data[0].ChannelDetails?.PeerHost, Is.EqualTo("127.0.0.0"));
            Assert.That(result.Data[0].ChannelDetails?.User, Is.EqualTo("guest"));
            Assert.That(result.Data[0].QueueConsumerDetails, Is.Not.Null);
            Assert.That(result.Data[0].QueueConsumerDetails?.Name, Is.EqualTo("fake_queue"));
            Assert.That(result.Data[0].QueueConsumerDetails?.VirtualHost, Is.EqualTo("TestVirtualHost1"));
            Assert.That(result.Data[0].ConsumerTag, Is.EqualTo("amq.ctag-fOtZo9ajuHDYQQ5hzrgasA"));
            Assert.That(result.Data[0].PreFetchCount, Is.EqualTo(0));
            Assert.That(result.Data[0].Exclusive, Is.False);
            Assert.That(result.Data[0].AcknowledgementRequired, Is.True);
        });
    }

    [Test]
    public async Task Verify_cannot_get_consumers_in_vhost_with_missing_name()
    {
        var result = await GetContainerBuilder("TestData/ConsumerInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Consumer>(x => x.UsingCredentials("guest", "guest"))
            .GetByVirtualHost(string.Empty);

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_can_get_all_consumers2()
    {
        var result = await GetContainerBuilder("TestData/ConsumerInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetAllConsumers(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data[0].ChannelDetails, Is.Not.Null);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(2));
            Assert.That(result.Data[0].ChannelDetails?.Name, Is.EqualTo("127.0.0.0:61113 -> 127.0.0.0:5672 (1)"));
            Assert.That(result.Data[0].ChannelDetails?.Node, Is.EqualTo("rabbit@localhost"));
            Assert.That(result.Data[0].ChannelDetails?.Number, Is.EqualTo(1));
            Assert.That(result.Data[0].ChannelDetails?.ConnectionName, Is.EqualTo("127.0.0.0:61113 -> 127.0.0.0:5672"));
            Assert.That(result.Data[0].ChannelDetails?.PeerHost, Is.EqualTo("127.0.0.0"));
            Assert.That(result.Data[0].ChannelDetails?.User, Is.EqualTo("guest"));
            Assert.That(result.Data[0].QueueConsumerDetails, Is.Not.Null);
            Assert.That(result.Data[0].QueueConsumerDetails?.Name, Is.EqualTo("fake_queue"));
            Assert.That(result.Data[0].QueueConsumerDetails?.VirtualHost, Is.EqualTo("TestVirtualHost1"));
            Assert.That(result.Data[0].ConsumerTag, Is.EqualTo("amq.ctag-fOtZo9ajuHDYQQ5hzrgasA"));
            Assert.That(result.Data[0].PreFetchCount, Is.EqualTo(0));
            Assert.That(result.Data[0].Exclusive, Is.False);
            Assert.That(result.Data[0].AcknowledgementRequired, Is.True);
        });
    }
}