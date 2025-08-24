namespace HareDu.Shovel.Tests;

using Core;
using Core.Extensions;
using Core.Serialization;
using Extensions;
using HareDu.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Model;
using Serialization;

[TestFixture]
public class ShovelTests :
    HareDuTesting
{
    readonly IHareDuDeserializer _deserializer;

    public ShovelTests()
    {
        _deserializer = new ShovelDeserializer();
    }

    [Test]
    public async Task Verify_able_to_get_all_dynamic_shovels1()
    {
        var result = await GetContainerBuilder("TestData/ShovelInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Shovel>(x => x.UsingCredentials("guest", "guest"))
            .GetAll("test_vhost");

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(1));
            Assert.That(result.Data[0], Is.Not.Null);
            Assert.That(result.Data[0].Name, Is.EqualTo("test-shovel"));
            Assert.That(result.Data[0].VirtualHost, Is.EqualTo("TestHareDu"));
        });
    }

    [Test]
    public async Task Verify_able_to_get_all_dynamic_shovels2()
    {
        var result = await GetContainerBuilder("TestData/ShovelInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetAllShovels(x => x.UsingCredentials("guest", "guest"), "test_vhost");

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(1));
            Assert.That(result.Data[0], Is.Not.Null);
            Assert.That(result.Data[0].Name, Is.EqualTo("test-shovel"));
            Assert.That(result.Data[0].VirtualHost, Is.EqualTo("TestHareDu"));
        });
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Shovel>(x => x.UsingCredentials("guest", "guest"))
            .Create("test-shovel1", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.AcknowledgementMode(ShovelAckMode.OnPublish);
                x.Source("queue1", ShovelProtocol.Amqp091, c =>
                {
                    c.DeleteAfter(ShovelDeleteMode.QueueLength);
                });
                x.Destination("queue2");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            Console.WriteLine(result.DebugInfo.Request);

            ShovelRequest request = _deserializer.ToObject<ShovelRequest>(result.DebugInfo.Request);

            Assert.That(request.Value.AcknowledgeMode, Is.EqualTo(ShovelAckMode.OnPublish));
            Assert.That(request.Value.SourcePrefetchCount, Is.EqualTo(1000));
            Assert.That(request.Value.SourceProtocol, Is.EqualTo(ShovelProtocol.Amqp091));
            Assert.That(request.Value.SourceQueue, Is.EqualTo("queue1"));
            Assert.That(request.Value.SourceUri, Is.EqualTo("amqp://user1@localhost"));
            Assert.That(request.Value.SourceDeleteAfter.ToString(), Is.EqualTo(ShovelDeleteMode.QueueLength.Convert()));
            Assert.That(request.Value.DestinationQueue, Is.EqualTo("queue2"));
            Assert.That(request.Value.DestinationUri, Is.EqualTo("amqp://user1@localhost"));

            Console.WriteLine(_deserializer.ToJsonString(result));
        });
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateShovel(x => x.UsingCredentials("guest", "guest"), "test-shovel1", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.Source("queue1", ShovelProtocol.Amqp091, c =>
                {
                    c.DeleteAfter(ShovelDeleteMode.QueueLength);
                });
                x.Destination("queue2");
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            ShovelRequest request = _deserializer.ToObject<ShovelRequest>(result.DebugInfo.Request);

            Assert.That(request.Value, Is.Not.Null);
            Assert.That(request.Value.AcknowledgeMode, Is.EqualTo(ShovelAckMode.OnPublish));
            Assert.That(request.Value.SourcePrefetchCount, Is.EqualTo(1000));
            Assert.That(request.Value.SourceProtocol, Is.EqualTo(ShovelProtocol.Amqp091));
            Assert.That(request.Value.SourceQueue, Is.EqualTo("queue1"));
            Assert.That(request.Value.SourceUri, Is.EqualTo("amqp://user1@localhost"));
            Assert.That(request.Value.SourceDeleteAfter.ToString(), Is.EqualTo(ShovelDeleteMode.QueueLength.Convert()));
            Assert.That(request.Value.DestinationQueue, Is.EqualTo("queue2"));
            Assert.That(request.Value.DestinationUri, Is.EqualTo("amqp://user1@localhost"));

            Console.WriteLine(_deserializer.ToJsonString(result));
        });
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Shovel>(x => x.UsingCredentials("guest", "guest"))
            .Create("test-shovel1", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.Source("queue1", ShovelProtocol.Amqp091, c =>
                {
                    c.Exchange("exchange1", null);
                    c.DeleteAfter(ShovelDeleteMode.QueueLength);
                });
                x.Destination("queue2", c =>
                {
                    c.Exchange("exchange2", null);
                });
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.True);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            ShovelRequest request = _deserializer.ToObject<ShovelRequest>(result.DebugInfo.Request);

            Assert.That(request.Value, Is.Not.Null);
            Assert.That(request.Value.AcknowledgeMode, Is.EqualTo(ShovelAckMode.OnPublish));
            Assert.That(request.Value.SourcePrefetchCount, Is.EqualTo(1000));
            Assert.That(request.Value.SourceProtocol, Is.EqualTo(ShovelProtocol.Amqp091));
            Assert.That(request.Value.SourceQueue, Is.EqualTo("queue1"));
            Assert.That(request.Value.SourceUri, Is.EqualTo("amqp://user1@localhost"));
            Assert.That(request.Value.SourceDeleteAfter.ToString(), Is.EqualTo(ShovelDeleteMode.QueueLength.Convert()));
            Assert.That(request.Value.DestinationQueue, Is.EqualTo("queue2"));
            Assert.That(request.Value.DestinationUri, Is.EqualTo("exchange1"));
            Assert.That(request.Value.SourceExchange, Is.EqualTo("exchange2"));
            Assert.That(request.Value.DestinationExchange, Is.EqualTo("amqp://user1@localhost"));
        });
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateShovel(x => x.UsingCredentials("guest", "guest"), "test-shovel1", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.Source("queue1", ShovelProtocol.Amqp091, c =>
                {
                    c.Exchange("exchange1", null);
                    c.DeleteAfter(ShovelDeleteMode.QueueLength);
                });
                x.Destination("queue2", c =>
                {
                    c.Exchange("exchange2", null);
                });
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.True);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            ShovelRequest request = _deserializer.ToObject<ShovelRequest>(result.DebugInfo.Request);

            Assert.That(request.Value, Is.Not.Null);
            Assert.That(request.Value.AcknowledgeMode, Is.EqualTo(ShovelAckMode.OnPublish));
            Assert.That(request.Value.SourcePrefetchCount, Is.EqualTo(1000));
            Assert.That(request.Value.SourceProtocol, Is.EqualTo(ShovelProtocol.Amqp091));
            Assert.That(request.Value.SourceQueue, Is.EqualTo("queue1"));
            Assert.That(request.Value.SourceUri, Is.EqualTo("amqp://user1@localhost"));
            Assert.That(request.Value.SourceDeleteAfter.ToString(), Is.EqualTo(ShovelDeleteMode.QueueLength.Convert()));
            Assert.That(request.Value.DestinationQueue, Is.EqualTo("queue2"));
            Assert.That(request.Value.DestinationUri, Is.EqualTo("exchange1"));
            Assert.That(request.Value.SourceExchange, Is.EqualTo("exchange2"));
            Assert.That(request.Value.DestinationExchange, Is.EqualTo("amqp://user1@localhost"));
        });
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Shovel>(x => x.UsingCredentials("guest", "guest"))
            .Create("test-shovel1", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.Source(string.Empty, ShovelProtocol.Amqp091, c =>
                {
                    c.Exchange(string.Empty, null);
                    c.DeleteAfter(ShovelDeleteMode.QueueLength);
                });
                x.Destination("queue2", c =>
                {
                    c.Exchange("exchange2", null);
                });
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.True);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            ShovelRequest request = _deserializer.ToObject<ShovelRequest>(result.DebugInfo.Request);

            Assert.That(request.Value, Is.Not.Null);
            Assert.That(request.Value.AcknowledgeMode, Is.EqualTo(ShovelAckMode.OnPublish));
            Assert.That(request.Value.SourcePrefetchCount, Is.EqualTo(1000));
            Assert.That(request.Value.SourceProtocol, Is.EqualTo(ShovelProtocol.Amqp091));
            Assert.That(request.Value.SourceQueue, Is.EqualTo("queue1"));
            Assert.That(request.Value.SourceUri, Is.EqualTo("amqp://user1@localhost"));
            Assert.That(request.Value.SourceDeleteAfter.ToString(), Is.EqualTo(ShovelDeleteMode.QueueLength.Convert()));
            Assert.That(request.Value.DestinationQueue, Is.EqualTo("queue2"));
            Assert.That(request.Value.DestinationUri, Is.EqualTo("exchange1"));
            Assert.That(request.Value.SourceExchange, Is.EqualTo("exchange2"));
            Assert.That(request.Value.DestinationExchange, Is.EqualTo("amqp://user1@localhost"));
        });
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateShovel(x => x.UsingCredentials("guest", "guest"), "test-shovel1", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.Source(string.Empty, ShovelProtocol.Amqp091, c =>
                {
                    c.Exchange(string.Empty, null);
                    c.DeleteAfter(ShovelDeleteMode.QueueLength);
                });
                x.Destination("queue2", c =>
                {
                    c.Exchange("exchange2", null);
                });
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.True);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            ShovelRequest request = _deserializer.ToObject<ShovelRequest>(result.DebugInfo.Request);

            Assert.That(request.Value, Is.Not.Null);
            Assert.That(request.Value.AcknowledgeMode, Is.EqualTo(ShovelAckMode.OnPublish));
            Assert.That(request.Value.SourcePrefetchCount, Is.EqualTo(1000));
            Assert.That(request.Value.SourceProtocol, Is.EqualTo(ShovelProtocol.Amqp091));
            Assert.That(request.Value.SourceQueue, Is.EqualTo("queue1"));
            Assert.That(request.Value.SourceUri, Is.EqualTo("amqp://user1@localhost"));
            Assert.That(request.Value.SourceDeleteAfter.ToString(), Is.EqualTo(ShovelDeleteMode.QueueLength.Convert()));
            Assert.That(request.Value.DestinationQueue, Is.EqualTo("queue2"));
            Assert.That(request.Value.DestinationUri, Is.EqualTo("exchange1"));
            Assert.That(request.Value.SourceExchange, Is.EqualTo("exchange2"));
            Assert.That(request.Value.DestinationExchange, Is.EqualTo("amqp://user1@localhost"));
        });
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel7()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Shovel>(x => x.UsingCredentials("guest", "guest"))
            .Create("test-shovel1", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.Source("queue2", ShovelProtocol.Amqp091, c =>
                {
                    c.Exchange("exchange2", null);
                    c.DeleteAfter(ShovelDeleteMode.QueueLength);
                });
                x.Destination(string.Empty, c =>
                {
                    c.Exchange(string.Empty, null);
                });
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.True);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            ShovelRequest request = _deserializer.ToObject<ShovelRequest>(result.DebugInfo.Request);

            Assert.That(request.Value, Is.Not.Null);
            Assert.That(request.Value.AcknowledgeMode, Is.EqualTo(ShovelAckMode.OnPublish));
            Assert.That(request.Value.SourcePrefetchCount, Is.EqualTo(1000));
            Assert.That(request.Value.SourceProtocol, Is.EqualTo(ShovelProtocol.Amqp091));
            Assert.That(request.Value.SourceQueue, Is.EqualTo("queue1"));
            Assert.That(request.Value.SourceUri, Is.EqualTo("amqp://user1@localhost"));
            Assert.That(request.Value.SourceDeleteAfter.ToString(), Is.EqualTo(ShovelDeleteMode.QueueLength.Convert()));
            Assert.That(request.Value.DestinationQueue, Is.Empty.Or.Null);
            Assert.That(request.Value.DestinationExchange, Is.Empty.Or.Null);
            Assert.That(request.Value.DestinationUri, Is.EqualTo("exchange1"));
            Assert.That(request.Value.SourceExchange, Is.EqualTo("exchange2"));
        });
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel8()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateShovel(x => x.UsingCredentials("guest", "guest"), "test-shovel1", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.Source("queue2", ShovelProtocol.Amqp091, c =>
                {
                    c.Exchange("exchange2", null);
                    c.DeleteAfter(ShovelDeleteMode.QueueLength);
                });
                x.Destination(string.Empty, c =>
                {
                    c.Exchange(string.Empty, null);
                });
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.True);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            ShovelRequest request = _deserializer.ToObject<ShovelRequest>(result.DebugInfo.Request);

            Assert.That(request.Value, Is.Not.Null);
            Assert.That(request.Value.AcknowledgeMode, Is.EqualTo(ShovelAckMode.OnPublish));
            Assert.That(request.Value.SourcePrefetchCount, Is.EqualTo(1000));
            Assert.That(request.Value.SourceProtocol, Is.EqualTo(ShovelProtocol.Amqp091));
            Assert.That(request.Value.SourceQueue, Is.EqualTo("queue1"));
            Assert.That(request.Value.SourceUri, Is.EqualTo("amqp://user1@localhost"));
            Assert.That(request.Value.SourceDeleteAfter.ToString(), Is.EqualTo(ShovelDeleteMode.QueueLength.Convert()));
            Assert.That(request.Value.DestinationQueue, Is.EqualTo("queue2"));
            Assert.That(request.Value.DestinationUri, Is.EqualTo("amqp://user1@localhost"));
            Assert.That(request.Value.SourceExchange, Is.EqualTo("exchange2"));
            Assert.That(request.Value.DestinationExchange, Is.EqualTo("amqp://user1@localhost"));
        });
    }

    [Test]
    public async Task Verify_can_delete_shovel1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Shovel>(x => x.UsingCredentials("guest", "guest"))
            .Delete("test-shovel2","TestHareDu");
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted,  Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
        });
    }

    [Test]
    public async Task Verify_can_delete_shovel2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteShovel(x => x.UsingCredentials("guest", "guest"), "test-shovel2","TestHareDu");
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted,  Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
        });
    }
}