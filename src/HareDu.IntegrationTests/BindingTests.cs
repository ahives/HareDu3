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
        var result = await _services.GetService<IBrokerObjectFactory>()
            .Object<Binding>()
            .GetAll()
            .ScreenDump();
            
        Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Should_be_able_to_get_all_bindings2()
    {
        var result = await _services.GetService<IBrokerObjectFactory>()
            .GetAllBindings()
            .ScreenDump();
            
        Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Test1()
    {
        var result = await _services.GetService<IBrokerObjectFactory>()
            .CreateExchangeBinding("HareDuExchange1", "HareDuExchange2", "TestHareDu");
            
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Test2()
    {
        var result = await _services.GetService<IBrokerObjectFactory>()
            .DeleteExchangeBinding("HareDuExchange1", "HareDuExchange2", "~", "TestHareDu");
            
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_add_arguments()
    {
        var result = await _services.GetService<IBrokerObjectFactory>()
            .Object<Binding>()
            .Create("queue1", "queue2", BindingType.Queue, "TestHareDu", "*.", x =>
            {
                x.Add("arg1", "value1");
            });
            
//            Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_delete_binding()
    {
        var result = await _services.GetService<IBrokerObjectFactory>()
            .Object<Binding>()
            .Delete("E2", "Q4", "%2A.","HareDu", BindingType.Queue);
            
//            Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
}