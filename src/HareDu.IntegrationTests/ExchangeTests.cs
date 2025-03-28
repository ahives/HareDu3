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
                    b.UsingCredentials("guest", "guest");
                });
            })
            .BuildServiceProvider();
    }

    [Test]
    public async Task Should_be_able_to_get_all_exchanges()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Exchange>()
            .GetAll()
            .ScreenDump();

        // result.HasFaulted.ShouldBeFalse();
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Should_be_able_to_get_all_exchanges_2()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Exchange>()
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
            .API<Exchange>()
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
            .API<Exchange>()
            .Create("HareDuExchange2", "TestHareDu", x =>
            {
                x.IsDurable();
                x.IsForInternalUse();
                x.HasRoutingType(ExchangeRoutingType.Fanout);
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
            .API<Exchange>()
            .Delete("E3", "HareDu");
            
//            Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
}