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
public class TopicPermissionsTest
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
    public async Task Verify_can_get_all_topic_permissions()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<TopicPermissions>()
            .GetAll()
            .ScreenDump();

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
        
    [Test]
    public void Verify_can_filter_topic_permissions()
    {
        var result = _services.GetService<IBrokerFactory>()
            .API<TopicPermissions>()
            .GetAll()
            .Where(x => x.VirtualHost == "HareDu");
            
        foreach (var permission in result)
        {
            Console.WriteLine("VirtualHost: {0}", permission.VirtualHost);
            Console.WriteLine("Exchange: {0}", permission.Exchange);
            Console.WriteLine("Read: {0}", permission.Read);
            Console.WriteLine("Write: {0}", permission.Write);
            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }
    }

    [Test]
    public async Task Verify_can_create_user_permissions()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<TopicPermissions>()
            .Create("guest", "HareDu", x =>
            {
                x.Exchange("E4");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_delete_user_permissions()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<TopicPermissions>()
            .Delete("guest", "HareDu7");
            
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
}