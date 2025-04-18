namespace HareDu.IntegrationTests;

using System;
using System.Threading.Tasks;
using Core;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using NUnit.Framework;
using Serialization;

[TestFixture]
public class ShovelTests
{
    ServiceProvider _services;
        
    [OneTimeSetUp]
    public void Init()
    {
        _services = new ServiceCollection()
            .AddHareDu(x =>
            {
                x.Broker(b =>
                {
                    b.ConnectTo("http://localhost:15672");
                    b.UsingCredentials("guest", "guest");
                });
            })
            .BuildServiceProvider();
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel1()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Shovel>()
            .Create("test-shovel3", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.Source("queue1", c =>
                {
                    c.DeleteAfter(DeleteShovelMode.QueueLength);
                });
                x.Destination("queue2");
            });

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel2()
    {
        Result result = await _services.GetService<IBrokerFactory>()
            .CreateShovel("test-shovel2", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.Source("queue1", c =>
                {
                    c.DeleteAfter(DeleteShovelMode.QueueLength);
                });
                x.Destination("queue2");
            });

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel3()
    {
        Result result = await _services.GetService<IBrokerFactory>()
            .CreateShovel("test-shovel6", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.Source("queue1", c =>
                {
                    c.DeleteAfter(5);
                });
                x.Destination("queue2");
            });

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_delete_shovel1()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Shovel>()
            .Delete("test-shovel2","TestHareDu");

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_delete_shovel2()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .DeleteShovel("test-shovel2","TestHareDu");

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_delete_all_shovels()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .DeleteAllShovels("TestHareDu");

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
        
    [Test]
    public async Task Verify_can_get_all_shovels1()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Shovel>()
            .GetAll()
            .ScreenDump();
            
        Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
        
    [Test]
    public async Task Verify_can_get_all_shovels2()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .GetAllShovels()
            .ScreenDump();
            
        Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
}