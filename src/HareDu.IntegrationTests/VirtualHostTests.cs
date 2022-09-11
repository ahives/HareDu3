namespace HareDu.IntegrationTests;

using System;
using System.Threading.Tasks;
using Core.Extensions;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using NUnit.Framework;
using Serialization;

[TestFixture]
public class VirtualHostTests
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
    public async Task Should_be_able_to_get_all_vhosts()
    {
        var result = await _services.GetService<IBrokerObjectFactory>()
            .Object<VirtualHost>()
            .GetAll()
            .ScreenDump();
    }

    [Test]
    public async Task Verify_GetAll_HasResult_works()
    {
        var result = await _services.GetService<IBrokerObjectFactory>()
            .Object<VirtualHost>()
            .GetAll();

        // Assert.IsTrue(result.HasData);
    }

    [Test]
    public void Verify_filtered_GetAll_works()
    {
        var result = _services.GetService<IBrokerObjectFactory>()
            .Object<VirtualHost>()
            .GetAll()
            .Where(x => x.Name == "HareDu");

        foreach (var vhost in result)
        {
            Console.WriteLine("Name: {0}", vhost.Name);
            Console.WriteLine("Tracing: {0}", vhost.Tracing);
            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }
    }

    [Test]
    public async Task Verify_Create_works()
    {
        var result = await _services.GetService<IBrokerObjectFactory>()
            .Object<VirtualHost>()
            .Create("HareDu9",x =>
            {
                x.WithTracingEnabled();
                x.Description("My test vhost.");
                x.Tags(t =>
                {
                    t.Add("accounts");
                    t.Add("production");
                });
            });

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_Delete_works()
    {
        var result = await _services.GetService<IBrokerObjectFactory>()
            .Object<VirtualHost>()
            .Delete("HareDu7");

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_start_vhost()
    {
        var result = await _services.GetService<IBrokerObjectFactory>()
            .Object<VirtualHost>()
            .Startup("", "");
            
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
}