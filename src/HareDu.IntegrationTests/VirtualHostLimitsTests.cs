namespace HareDu.IntegrationTests;

using System;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using DependencyInjection;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;
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
                x.KnowledgeBase(k =>
                {
                    k.File("kb.json", "Articles");
                });
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
    public async Task Verify_can_get_all_limits()
    {
        var result = await _services.GetService<IHareDuFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .GetAllLimits()
            .ScreenDump();
    }

    [Test]
    public void Verify_can_get_limits_of_specified_vhost()
    {
        var result = _services.GetService<IHareDuFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
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
        var result = await _services.GetService<IHareDuFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .DefineLimit("HareDu5", x =>
            {
                x.SetMaxQueueLimit(100);
                x.SetMaxConnectionLimit(1000);
            });

        Console.WriteLine(BrokerDeserializer.Instance.ToJsonString(result));
    }

    [Test]
    public async Task Verify_can_delete_limits()
    {
        var result = await _services.GetService<IHareDuFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .DeleteLimit("HareDu3", VirtualHostLimit.MaxConnections);

        Console.WriteLine(BrokerDeserializer.Instance.ToJsonString(result));
    }
}