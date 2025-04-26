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
    public async Task Verify_can_get_all_topic_permissions()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<TopicPermissions>(x => x.UsingCredentials("guest", "guest"))
            .GetAll()
            .ScreenDump();

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
        
    [Test]
    public void Verify_can_filter_topic_permissions()
    {
        var result = _services.GetService<IBrokerFactory>()
            .API<TopicPermissions>(x => x.UsingCredentials("guest", "guest"))
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
            .API<TopicPermissions>(x => x.UsingCredentials("guest", "guest"))
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
            .API<TopicPermissions>(x => x.UsingCredentials("guest", "guest"))
            .Delete("guest", "HareDu7");
            
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
}