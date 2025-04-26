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
    public async Task Should_be_able_to_get_all_vhosts()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .GetAll()
            .ScreenDump();
    }

    [Test]
    public async Task Verify_GetAll_HasResult_works()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .GetAll();

        // Assert.IsTrue(result.HasData);
    }

    [Test]
    public void Verify_filtered_GetAll_works()
    {
        var result = _services.GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
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
    public async Task Verify_can_create_vhost()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .Create("HareDu1",x =>
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
    public async Task Verify_can_delete_vhost()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<VirtualHost>(z => z.UsingCredentials("guest", "guest"))
            .Delete("HareDu7");

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_start_vhost()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .Startup("", "");
            
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public void Verify()
    {
        var result = _services.GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .DefineLimit("QueueTestVirtulHost", x =>
            {
                // x.SetMaxQueueLimit(100);
                x.SetMaxConnectionLimit(1000);
            });
        
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
}