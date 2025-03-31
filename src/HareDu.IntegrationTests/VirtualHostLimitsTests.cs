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
public class VirtualHostLimitsTests
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
    public async Task Verify_can_get_all_limits()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .GetAllLimits()
            .ScreenDump();
    }

    [Test]
    public void Verify_can_get_limits_of_specified_vhost()
    {
        var result = _services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .GetAllLimits()
            .Where(x => x.VirtualHost == "HareDu");

        foreach (var item in result)
        {
            Console.WriteLine("Name: {0}", item.VirtualHost);

            if (item.Limits.TryGetValue("max-connections", out ulong maxConnections))
                Console.WriteLine("max-connections: {0}", maxConnections);

            if (item.Limits.TryGetValue("max-queues", out ulong maxQueues))
                Console.WriteLine("max-queues: {0}", maxQueues);

            Console.WriteLine("****************************************************");
            Console.WriteLine();
        }
    }

    [Test]
    public async Task Verify_can_define_limits()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .DefineLimit("HareDu5", x =>
            {
                x.SetMaxQueueLimit(100);
                x.SetMaxConnectionLimit(1000);
            });

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_delete_limits()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .DeleteLimit("HareDu3", VirtualHostLimit.MaxConnections);

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
}