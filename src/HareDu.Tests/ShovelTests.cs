namespace HareDu.Tests;

using System;
using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;
using Serialization;

[TestFixture]
public class ShovelTests :
    HareDuTesting
{
    [Test]
    public async Task Verify_able_to_get_all_dynamic_shovels1()
    {
        var result = await GetContainerBuilder("TestData/ShovelInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Shovel>(x => x.UsingCredentials("guest", "guest"))
            .GetAll();
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(1));
            Assert.That(result.Data[0], Is.Not.Null);
            Assert.That(result.Data[0].Name, Is.EqualTo("test-shovel"));
            Assert.That(result.Data[0].VirtualHost, Is.EqualTo("TestHareDu"));
            Assert.That(result.Data[0].Node, Is.EqualTo("rabbit@localhost"));
            Assert.That(result.Data[0].Type, Is.EqualTo(ShovelType.Dynamic));
            Assert.That(result.Data[0].State, Is.EqualTo(ShovelState.Starting));
        });
    }

    [Test]
    public async Task Verify_able_to_get_all_dynamic_shovels2()
    {
        var result = await GetContainerBuilder("TestData/ShovelInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetAllShovels(x => x.UsingCredentials("guest", "guest"));
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(1));
            Assert.That(result.Data[0], Is.Not.Null);
            Assert.That(result.Data[0].Name, Is.EqualTo("test-shovel"));
            Assert.That(result.Data[0].VirtualHost, Is.EqualTo("TestHareDu"));
            Assert.That(result.Data[0].Node, Is.EqualTo("rabbit@localhost"));
            Assert.That(result.Data[0].Type, Is.EqualTo(ShovelType.Dynamic));
            Assert.That(result.Data[0].State, Is.EqualTo(ShovelState.Starting));
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
                x.AcknowledgementMode(AckMode.OnPublish);
                x.Source("queue1", ShovelProtocol.Amqp091, c =>
                {
                    c.DeleteAfter(DeleteShovelMode.QueueLength);
                });
                x.Destination("queue2");
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            Console.WriteLine(result.DebugInfo.Request);

            ShovelRequest request = result.DebugInfo.Request.ToObject<ShovelRequest>(Deserializer.Options);

            Assert.That(request.Value.AcknowledgeMode, Is.EqualTo(AckMode.OnPublish));
            Assert.That(request.Value.SourcePrefetchCount, Is.EqualTo(1000));
            Assert.That(request.Value.SourceProtocol, Is.EqualTo(ShovelProtocol.Amqp091));
            Assert.That(request.Value.SourceQueue, Is.EqualTo("queue1"));
            Assert.That(request.Value.SourceUri, Is.EqualTo("amqp://user1@localhost"));
            Assert.That(request.Value.SourceDeleteAfter.ToString(), Is.EqualTo(DeleteShovelMode.QueueLength.Convert()));
            Assert.That(request.Value.DestinationQueue, Is.EqualTo("queue2"));
            Assert.That(request.Value.DestinationUri, Is.EqualTo("amqp://user1@localhost"));

            Console.WriteLine(request.ToJsonString(Deserializer.Options));
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
                    c.DeleteAfter(DeleteShovelMode.QueueLength);
                });
                x.Destination("queue2");
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            ShovelRequest request = result.DebugInfo.Request.ToObject<ShovelRequest>(Deserializer.Options);

            Assert.That(request.Value, Is.Not.Null);
            Assert.That(request.Value.AcknowledgeMode, Is.EqualTo(AckMode.OnPublish));
            Assert.That(request.Value.SourcePrefetchCount, Is.EqualTo(1000));
            Assert.That(request.Value.SourceProtocol, Is.EqualTo(ShovelProtocol.Amqp091));
            Assert.That(request.Value.SourceQueue, Is.EqualTo("queue1"));
            Assert.That(request.Value.SourceUri, Is.EqualTo("amqp://user1@localhost"));
            Assert.That(request.Value.SourceDeleteAfter.ToString(), Is.EqualTo(DeleteShovelMode.QueueLength.Convert()));
            Assert.That(request.Value.DestinationQueue, Is.EqualTo("queue2"));
            Assert.That(request.Value.DestinationUri, Is.EqualTo("amqp://user1@localhost"));

            Console.WriteLine(request.ToJsonString(Deserializer.Options));
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
                    c.DeleteAfter(DeleteShovelMode.QueueLength);
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

            ShovelRequest request = result.DebugInfo.Request.ToObject<ShovelRequest>(Deserializer.Options);

            Assert.That(request.Value, Is.Not.Null);
            Assert.That(request.Value.AcknowledgeMode, Is.EqualTo(AckMode.OnPublish));
            Assert.That(request.Value.SourcePrefetchCount, Is.EqualTo(1000));
            Assert.That(request.Value.SourceProtocol, Is.EqualTo(ShovelProtocol.Amqp091));
            Assert.That(request.Value.SourceQueue, Is.EqualTo("queue1"));
            Assert.That(request.Value.SourceUri, Is.EqualTo("amqp://user1@localhost"));
            Assert.That(request.Value.SourceDeleteAfter.ToString(), Is.EqualTo(DeleteShovelMode.QueueLength.Convert()));
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
                    c.DeleteAfter(DeleteShovelMode.QueueLength);
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

            ShovelRequest request = result.DebugInfo.Request.ToObject<ShovelRequest>(Deserializer.Options);

            Assert.That(request.Value, Is.Not.Null);
            Assert.That(request.Value.AcknowledgeMode, Is.EqualTo(AckMode.OnPublish));
            Assert.That(request.Value.SourcePrefetchCount, Is.EqualTo(1000));
            Assert.That(request.Value.SourceProtocol, Is.EqualTo(ShovelProtocol.Amqp091));
            Assert.That(request.Value.SourceQueue, Is.EqualTo("queue1"));
            Assert.That(request.Value.SourceUri, Is.EqualTo("amqp://user1@localhost"));
            Assert.That(request.Value.SourceDeleteAfter.ToString(), Is.EqualTo(DeleteShovelMode.QueueLength.Convert()));
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
                    c.DeleteAfter(DeleteShovelMode.QueueLength);
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

            ShovelRequest request = result.DebugInfo.Request.ToObject<ShovelRequest>(Deserializer.Options);

            Assert.That(request.Value, Is.Not.Null);
            Assert.That(request.Value.AcknowledgeMode, Is.EqualTo(AckMode.OnPublish));
            Assert.That(request.Value.SourcePrefetchCount, Is.EqualTo(1000));
            Assert.That(request.Value.SourceProtocol, Is.EqualTo(ShovelProtocol.Amqp091));
            Assert.That(request.Value.SourceQueue, Is.EqualTo("queue1"));
            Assert.That(request.Value.SourceUri, Is.EqualTo("amqp://user1@localhost"));
            Assert.That(request.Value.SourceDeleteAfter.ToString(), Is.EqualTo(DeleteShovelMode.QueueLength.Convert()));
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
                    c.DeleteAfter(DeleteShovelMode.QueueLength);
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

            ShovelRequest request = result.DebugInfo.Request.ToObject<ShovelRequest>(Deserializer.Options);

            Assert.That(request.Value, Is.Not.Null);
            Assert.That(request.Value.AcknowledgeMode, Is.EqualTo(AckMode.OnPublish));
            Assert.That(request.Value.SourcePrefetchCount, Is.EqualTo(1000));
            Assert.That(request.Value.SourceProtocol, Is.EqualTo(ShovelProtocol.Amqp091));
            Assert.That(request.Value.SourceQueue, Is.EqualTo("queue1"));
            Assert.That(request.Value.SourceUri, Is.EqualTo("amqp://user1@localhost"));
            Assert.That(request.Value.SourceDeleteAfter.ToString(), Is.EqualTo(DeleteShovelMode.QueueLength.Convert()));
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
                    c.DeleteAfter(DeleteShovelMode.QueueLength);
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

            ShovelRequest request = result.DebugInfo.Request.ToObject<ShovelRequest>(Deserializer.Options);

            Assert.That(request.Value, Is.Not.Null);
            Assert.That(request.Value.AcknowledgeMode, Is.EqualTo(AckMode.OnPublish));
            Assert.That(request.Value.SourcePrefetchCount, Is.EqualTo(1000));
            Assert.That(request.Value.SourceProtocol, Is.EqualTo(ShovelProtocol.Amqp091));
            Assert.That(request.Value.SourceQueue, Is.EqualTo("queue1"));
            Assert.That(request.Value.SourceUri, Is.EqualTo("amqp://user1@localhost"));
            Assert.That(request.Value.SourceDeleteAfter.ToString(), Is.EqualTo(DeleteShovelMode.QueueLength.Convert()));
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
                    c.DeleteAfter(DeleteShovelMode.QueueLength);
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

            ShovelRequest request = result.DebugInfo.Request.ToObject<ShovelRequest>(Deserializer.Options);

            Assert.That(request.Value, Is.Not.Null);
            Assert.That(request.Value.AcknowledgeMode, Is.EqualTo(AckMode.OnPublish));
            Assert.That(request.Value.SourcePrefetchCount, Is.EqualTo(1000));
            Assert.That(request.Value.SourceProtocol, Is.EqualTo(ShovelProtocol.Amqp091));
            Assert.That(request.Value.SourceQueue, Is.EqualTo("queue1"));
            Assert.That(request.Value.SourceUri, Is.EqualTo("amqp://user1@localhost"));
            Assert.That(request.Value.SourceDeleteAfter.ToString(), Is.EqualTo(DeleteShovelMode.QueueLength.Convert()));
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