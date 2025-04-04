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
        var services = GetContainerBuilder("TestData/ShovelInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<Shovel>()
            .GetAll();
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(1, result.Data.Count);
            Assert.IsNotNull(result.Data[0]);
            Assert.AreEqual("test-shovel", result.Data[0].Name);
            Assert.AreEqual("TestHareDu", result.Data[0].VirtualHost);
            Assert.AreEqual("rabbit@localhost", result.Data[0].Node);
            Assert.AreEqual(ShovelType.Dynamic, result.Data[0].Type);
            Assert.AreEqual(ShovelState.Starting, result.Data[0].State);
        });
    }

    [Test]
    public async Task Verify_able_to_get_all_dynamic_shovels2()
    {
        var services = GetContainerBuilder("TestData/ShovelInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .GetAllShovels();
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(1, result.Data.Count);
            Assert.IsNotNull(result.Data[0]);
            Assert.AreEqual("test-shovel", result.Data[0].Name);
            Assert.AreEqual("TestHareDu", result.Data[0].VirtualHost);
            Assert.AreEqual("rabbit@localhost", result.Data[0].Node);
            Assert.AreEqual(ShovelType.Dynamic, result.Data[0].Type);
            Assert.AreEqual(ShovelState.Starting, result.Data[0].State);
        });
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<Shovel>()
            .Create("test-shovel1", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.AcknowledgementMode(AckMode.OnPublish);
                x.Source("queue1", c =>
                {
                    c.DeleteAfter(DeleteShovelMode.QueueLength);
                });
                x.Destination("queue2");
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
                
            Console.WriteLine(result.DebugInfo.Request);
                
            ShovelRequest request = result.DebugInfo.Request.ToObject<ShovelRequest>(Deserializer.Options);
                
            Assert.IsNotNull(request.Value);
            Assert.AreEqual(AckMode.OnPublish, request.Value.AcknowledgeMode);
            Assert.AreEqual(1000, request.Value.SourcePrefetchCount);
            Assert.AreEqual(ShovelProtocolType.Amqp091, request.Value.SourceProtocol);
            Assert.AreEqual("queue1", request.Value.SourceQueue);
            Assert.AreEqual("amqp://user1@localhost", request.Value.SourceUri);
            Assert.AreEqual(DeleteShovelMode.QueueLength.Convert(), request.Value.SourceDeleteAfter.ToString());
            Assert.AreEqual("queue2", request.Value.DestinationQueue);
            Assert.AreEqual("amqp://user1@localhost", request.Value.DestinationUri);
                
            Console.WriteLine(request.ToJsonString(Deserializer.Options));
        });
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .CreateShovel("test-shovel1", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.Source("queue1", c =>
                {
                    c.DeleteAfter(DeleteShovelMode.QueueLength);
                });
                x.Destination("queue2");
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);

            ShovelRequest request = result.DebugInfo.Request.ToObject<ShovelRequest>(Deserializer.Options);
                
            Assert.IsNotNull(request.Value);
            Assert.AreEqual(1000, request.Value.SourcePrefetchCount);
            Assert.AreEqual(ShovelProtocolType.Amqp091, request.Value.SourceProtocol);
            Assert.AreEqual("queue1", request.Value.SourceQueue);
            Assert.AreEqual("amqp://user1@localhost", request.Value.SourceUri);
            Assert.AreEqual(DeleteShovelMode.QueueLength.Convert(), request.Value.SourceDeleteAfter.ToString());
            Assert.AreEqual("queue2", request.Value.DestinationQueue);
            Assert.AreEqual("amqp://user1@localhost", request.Value.DestinationUri);
        });
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel3()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<Shovel>()
            .Create("test-shovel1", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.Source("queue1", c =>
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
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            ShovelRequest request = result.DebugInfo.Request.ToObject<ShovelRequest>(Deserializer.Options);
                
            Assert.IsNotNull(request.Value);
            Assert.AreEqual(1000, request.Value.SourcePrefetchCount);
            Assert.AreEqual(ShovelProtocolType.Amqp091, request.Value.SourceProtocol);
            Assert.AreEqual("queue1", request.Value.SourceQueue);
            Assert.AreEqual("amqp://user1@localhost", request.Value.SourceUri);
            Assert.AreEqual(DeleteShovelMode.QueueLength.Convert(), request.Value.SourceDeleteAfter.ToString());
            Assert.AreEqual("queue2", request.Value.DestinationQueue);
            Assert.AreEqual("amqp://user1@localhost", request.Value.DestinationUri);
            Assert.AreEqual("exchange1", request.Value.SourceExchange);
            Assert.AreEqual("exchange2", request.Value.DestinationExchange);
        });
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel4()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .CreateShovel("test-shovel1", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.Source("queue1", c =>
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
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            ShovelRequest request = result.DebugInfo.Request.ToObject<ShovelRequest>(Deserializer.Options);
                
            Assert.IsNotNull(request.Value);
            Assert.AreEqual(1000, request.Value.SourcePrefetchCount);
            Assert.AreEqual(ShovelProtocolType.Amqp091, request.Value.SourceProtocol);
            Assert.AreEqual("queue1", request.Value.SourceQueue);
            Assert.AreEqual("amqp://user1@localhost", request.Value.SourceUri);
            Assert.AreEqual(DeleteShovelMode.QueueLength.Convert(), request.Value.SourceDeleteAfter.ToString());
            Assert.AreEqual("queue2", request.Value.DestinationQueue);
            Assert.AreEqual("amqp://user1@localhost", request.Value.DestinationUri);
            Assert.AreEqual("exchange1", request.Value.SourceExchange);
            Assert.AreEqual("exchange2", request.Value.DestinationExchange);
        });
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel5()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<Shovel>()
            .Create("test-shovel1", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.Source(string.Empty, c =>
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
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            ShovelRequest request = result.DebugInfo.Request.ToObject<ShovelRequest>(Deserializer.Options);
                
            Assert.IsNotNull(request.Value);
            Assert.AreEqual(1000, request.Value.SourcePrefetchCount);
            Assert.AreEqual(ShovelProtocolType.Amqp091, request.Value.SourceProtocol);
            Assert.That(request.Value.SourceQueue, Is.Empty.Or.Null);
            Assert.AreEqual("amqp://user1@localhost", request.Value.SourceUri);
            Assert.AreEqual(DeleteShovelMode.QueueLength.Convert(), request.Value.SourceDeleteAfter.ToString());
            Assert.AreEqual("queue2", request.Value.DestinationQueue);
            Assert.AreEqual("amqp://user1@localhost", request.Value.DestinationUri);
            Assert.That(request.Value.SourceExchange, Is.Empty.Or.Null);
            Assert.AreEqual("exchange2", request.Value.DestinationExchange);
        });
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel6()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .CreateShovel("test-shovel1", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.Source(string.Empty, c =>
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
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            ShovelRequest request = result.DebugInfo.Request.ToObject<ShovelRequest>(Deserializer.Options);
                
            Assert.IsNotNull(request.Value);
            Assert.AreEqual(1000, request.Value.SourcePrefetchCount);
            Assert.AreEqual(ShovelProtocolType.Amqp091, request.Value.SourceProtocol);
            Assert.That(request.Value.SourceQueue, Is.Empty.Or.Null);
            Assert.AreEqual("amqp://user1@localhost", request.Value.SourceUri);
            Assert.AreEqual(DeleteShovelMode.QueueLength.Convert(), request.Value.SourceDeleteAfter.ToString());
            Assert.AreEqual("queue2", request.Value.DestinationQueue);
            Assert.AreEqual("amqp://user1@localhost", request.Value.DestinationUri);
            Assert.That(request.Value.SourceExchange, Is.Empty.Or.Null);
            Assert.AreEqual("exchange2", request.Value.DestinationExchange);
        });
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel7()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<Shovel>()
            .Create("test-shovel1", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.Source("queue2", c =>
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
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            ShovelRequest request = result.DebugInfo.Request.ToObject<ShovelRequest>(Deserializer.Options);
                
            Assert.IsNotNull(request.Value);
            Assert.AreEqual(1000, request.Value.SourcePrefetchCount);
            Assert.AreEqual(ShovelProtocolType.Amqp091, request.Value.SourceProtocol);
            Assert.AreEqual("queue2", request.Value.SourceQueue);
            Assert.AreEqual("amqp://user1@localhost", request.Value.SourceUri);
            Assert.AreEqual(DeleteShovelMode.QueueLength.Convert(), request.Value.SourceDeleteAfter.ToString());
            Assert.AreEqual("amqp://user1@localhost", request.Value.DestinationUri);
            Assert.AreEqual("exchange2", request.Value.SourceExchange);
            Assert.That(request.Value.DestinationQueue, Is.Empty.Or.Null);
            Assert.That(request.Value.DestinationExchange, Is.Empty.Or.Null);
        });
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel8()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .CreateShovel("test-shovel1", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.Source("queue2", c =>
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
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            ShovelRequest request = result.DebugInfo.Request.ToObject<ShovelRequest>(Deserializer.Options);
                
            Assert.IsNotNull(request.Value);
            Assert.AreEqual(1000, request.Value.SourcePrefetchCount);
            Assert.AreEqual(ShovelProtocolType.Amqp091, request.Value.SourceProtocol);
            Assert.AreEqual("queue2", request.Value.SourceQueue);
            Assert.AreEqual("amqp://user1@localhost", request.Value.SourceUri);
            Assert.AreEqual(DeleteShovelMode.QueueLength.Convert(), request.Value.SourceDeleteAfter.ToString());
            Assert.AreEqual("amqp://user1@localhost", request.Value.DestinationUri);
            Assert.AreEqual("exchange2", request.Value.SourceExchange);
            Assert.That(request.Value.DestinationQueue, Is.Empty.Or.Null);
            Assert.That(request.Value.DestinationExchange, Is.Empty.Or.Null);
        });
    }

    [Test]
    public async Task Verify_can_delete_shovel1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<Shovel>()
            .Delete("test-shovel2","TestHareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
        });
    }

    [Test]
    public async Task Verify_can_delete_shovel2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .DeleteShovel("test-shovel2","TestHareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
        });
    }
}