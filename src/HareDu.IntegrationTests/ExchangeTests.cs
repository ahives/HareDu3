namespace HareDu.IntegrationTests;

using System;
using System.Threading.Tasks;
using Core.Configuration;
using Core.Extensions;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using NUnit.Framework;
using Serialization;

[TestFixture]
public class ExchangeTests
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
    public async Task Should_be_able_to_get_all_exchanges()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .GetAll()
            .ScreenDump();

        // result.HasFaulted.ShouldBeFalse();
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Should_be_able_to_get_all_exchanges_2()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .GetAll();

        foreach (var exchange in result.Select(x => x.Data))
        {
            Console.WriteLine("Name: {0}", exchange.Name);
            Console.WriteLine("VirtualHost: {0}", exchange.VirtualHost);
            Console.WriteLine("AutoDelete: {0}", exchange.AutoDelete);
            Console.WriteLine("Internal: {0}", exchange.Internal);
            Console.WriteLine("Durable: {0}", exchange.Durable);
            Console.WriteLine("RoutingType: {0}", exchange.RoutingType);
            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }
            
        // result.HasFaulted.ShouldBeFalse();
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    // [Test]
    // public async Task Should_be_able_to_get_all_exchanges_3()
    // {
    //     var provider = new HareDuConfigProvider();
    //     var config = provider.Configure(x => { });
    //     var factory = new BrokerFactory(config);
    //         
    //     var result = await factory
    //         .API<Exchange>()
    //         .GetAll()
    //         .ScreenDump();
    //
    //     // result.HasFaulted.ShouldBeFalse();
    //     Console.WriteLine(result.ToJsonString(Deserializer.Options));
    // }

    [Test]
    public async Task Verify_can_filter_exchanges()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .GetAll();

        result
            .Where(x => x.Name == "amq.fanout")
            .ScreenDump();

        // Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_create_exchange()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Create("HareDuExchange2", "TestHareDu", x =>
            {
                x.IsDurable();
                x.IsForInternalUse();
                x.WithRoutingType(RoutingType.Fanout);
                // x.HasArguments(arg =>
                // {
                //     arg.Set("arg1", "blah");
                // });
            });
            
        // Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_delete_exchange()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Delete("E3", "HareDu");
            
//            Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public void Test()
    {
        string exchange = "test-exchange1";;
        string vhost = "test-vhost";
        var result0 = _services.GetService<IBrokerFactory>()
            .CreateVirtualHost(x => x.UsingCredentials("guest", "guest"), vhost, x =>
            {
                x.Tags(t =>
                {
                    t.Add("test");
                });
            });
        var result1 = _services.GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Create(exchange, vhost, x =>
            {
                x.WithRoutingType(RoutingType.Direct);
            });
        string node = "rabbit@6089ab1a7b81";
        string queue = "test-queue1";
        var result2 = _services.GetService<IBrokerFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Create(queue, vhost, node, x =>
            {
                x.IsDurable();
            });
        var result3 = _services.GetService<IBrokerFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .BindToQueue(vhost, exchange, x =>
            {
                x.Destination(queue);
                x.BindingKey("test");
            });
    }
}