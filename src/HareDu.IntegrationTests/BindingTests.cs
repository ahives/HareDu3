namespace HareDu.IntegrationTests;

using System;
using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using NUnit.Framework;
using Serialization;

[TestFixture]
public class BindingTests
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
    public async Task Should_be_able_to_get_all_bindings1()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Binding>()
            .GetAll()
            .ScreenDump();
            
        Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Should_be_able_to_get_all_bindings2()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .GetAllBindings()
            .ScreenDump();
            
        Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Test1()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .CreateBinding("TestHareDu", x =>
            {
                x.Source("HareDuExchange1");
                x.Destination("HareDuExchange2");
                x.BindingKey("*.");
                x.BindingType(BindingType.Exchange);
            });
            
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Test2()
    {
        var result = await _services.GetService<IBrokerFactory>()
                .DeleteBinding("TestHareDu", x =>
                {
                    x.Source("HareDuExchange1");
                    x.Destination("HareDuExchange2");
                    x.PropertiesKey("~");
                    x.BindingType(BindingType.Exchange);
                });
            
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_add_arguments()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Binding>()
            .Create("TestHareDu",
                x =>
                {
                    x.Source("queue1");
                    x.Destination("queue2");
                    x.BindingKey("*.");
                    x.BindingType(BindingType.Queue);
                    x.OptionalArguments(arg =>
                    {
                        arg.Add("arg1", "value1");
                    });
                });
            
//            Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_delete_binding()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Binding>()
            .Delete("HareDu", x =>
            {
                x.Source("E2");
                x.Destination("Q4");
                x.PropertiesKey("%2A.");
                x.BindingType(BindingType.Queue);
            });
            
//            Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
}