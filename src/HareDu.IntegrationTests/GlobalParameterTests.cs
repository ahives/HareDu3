namespace HareDu.IntegrationTests;

using System;
using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using NUnit.Framework;
using Serialization;

[TestFixture]
public class GlobalParameterTests
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
    public async Task Should_be_able_to_get_all_global_parameters()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<GlobalParameter>()
            .GetAll()
            .ScreenDump();
    }
        
    [Test]
    public async Task Verify_can_create_parameter()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<GlobalParameter>()
            .Create("fake_param2", x =>
            {
                x.Add("arg1", "value1");
                x.Add("arg2", "value2");
            });
             
        // Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
        
    [Test]
    public async Task Verify_can_delete_parameter()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<GlobalParameter>()
            .Delete("Fred");
            
        // Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
}