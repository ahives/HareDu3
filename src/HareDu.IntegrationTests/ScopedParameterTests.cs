namespace HareDu.IntegrationTests;

using System;
using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using NUnit.Framework;

[TestFixture]
public class ScopedParameterTests
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
    public async Task Should_be_able_to_get_all_scoped_parameters()
    {
        var result = await _services.GetService<IBrokerObjectFactory>()
            .Object<ScopedParameter>()
            .GetAll()
            .ScreenDump();
    }
        
    [Test]
    public async Task Verify_can_create()
    {
        var result = await _services.GetService<IBrokerObjectFactory>()
            .Object<ScopedParameter>()
            .Create<string>("test", "me", "federation", "HareDu");
            
        Console.WriteLine("****************************************************");
        Console.WriteLine();
    }

    [Test]
    public async Task Verify_can_delete()
    {
        var result = await _services.GetService<IBrokerObjectFactory>()
            .Object<ScopedParameter>()
            .Delete("", "federation", "HareDu");
    }
}