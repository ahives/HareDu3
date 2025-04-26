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
                    b.WithBehavior(behavior =>
                    {
                        behavior.LimitRequests(5, 5);
                    });
                });
                x.Diagnostics(d =>
                {
                    d.Probes(p =>
                    {
                        p.SetConsumerUtilizationThreshold(1);
                        p.SetFileDescriptorUsageThresholdCoefficient(1);
                        p.SetHighConnectionClosureRateThreshold(1);
                        p.SetFileDescriptorUsageThresholdCoefficient(1);
                        p.SetHighConnectionClosureRateThreshold(1);
                        p.SetMessageRedeliveryThresholdCoefficient(1);
                        p.SetHighConnectionCreationRateThreshold(1);
                        p.SetQueueHighFlowThreshold(1);
                        p.SetQueueLowFlowThreshold(1);
                        p.SetRuntimeProcessUsageThresholdCoefficient(1);
                        p.SetSocketUsageThresholdCoefficient(1);
                    });
                });
            })
            .BuildServiceProvider();
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel1()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Shovel>(x => x.UsingCredentials("guest", "guest"))
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
            .CreateShovel(x => x.UsingCredentials("guest", "guest"),"test-shovel2", "TestHareDu", x =>
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
            .CreateShovel(x => x.UsingCredentials("guest", "guest"),"test-shovel6", "TestHareDu", x =>
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
            .API<Shovel>(x => x.UsingCredentials("guest", "guest"))
            .Delete("test-shovel2","TestHareDu");

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_delete_shovel2()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .DeleteShovel(x => x.UsingCredentials("guest", "guest"), "test-shovel2","TestHareDu");

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_delete_all_shovels()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .DeleteAllShovels(x => x.UsingCredentials("guest", "guest"),"TestHareDu");

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
        
    [Test]
    public async Task Verify_can_get_all_shovels1()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Shovel>(x => x.UsingCredentials("guest", "guest"))
            .GetAll()
            .ScreenDump();
            
        Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
        
    [Test]
    public async Task Verify_can_get_all_shovels2()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .GetAllShovels(x => x.UsingCredentials("guest", "guest"))
            .ScreenDump();
            
        Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
}