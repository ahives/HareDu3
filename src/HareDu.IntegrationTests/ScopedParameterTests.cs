namespace HareDu.IntegrationTests;

using System;
using System.Threading.Tasks;
using Core;
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
    public async Task Should_be_able_to_get_all_scoped_parameters()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .GetAll()
            .ScreenDump();
    }
        
    [Test]
    public async Task Verify_can_create()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create<string>("test", "me", "federation", "HareDu");
            
        Console.WriteLine("****************************************************");
        Console.WriteLine();
    }

    [Test]
    public async Task Verify_can_delete()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Delete("", "federation", "HareDu");
    }
}