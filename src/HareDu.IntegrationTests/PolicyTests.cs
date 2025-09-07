namespace HareDu.IntegrationTests;

using System;
using System.Threading.Tasks;
using Core;
using Core.Serialization;
using DependencyInjection;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;
using Serialization;

[TestFixture]
public class PolicyTests
{
    ServiceProvider _services;
    readonly IHareDuDeserializer _deserializer;

    public PolicyTests()
    {
        _deserializer = new BrokerDeserializer();
    }

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
    public async Task Should_be_able_to_get_all_policies()
    {
        var result = await _services.GetService<IHareDuFactory>()
            .API<Policy>(x => x.UsingCredentials("guest", "guest"))
            .GetAll()
            .ScreenDump();
    }
        
    [Test]
    public async Task Verify_can_create_policy()
    {
        var result = await _services.GetService<IHareDuFactory>()
            .API<Policy>(x => x.UsingCredentials("guest", "guest"))
            .Create("policy1", "TestHareDu", x =>
            {
                x.Pattern("^amq.");
                x.ApplyTo(PolicyAppliedTo.Queues);
                x.Priority(0);
                x.Definition(arg =>
                {
                    arg.SetExpiry(1000);
                });
            });
            
        // Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(_deserializer.ToJsonString(result));
    }

    [Test]
    public async Task Verify_cannot_create_policy()
    {
        var result = await _services.GetService<IHareDuFactory>()
            .API<Policy>(x => x.UsingCredentials("guest", "guest"))
            .Create("P4", "HareDu", x =>
            {
                x.Pattern("^amq.");
                x.ApplyTo(PolicyAppliedTo.QueuesAndExchanges);
                x.Priority(0);
                x.Definition(arg =>
                {
                    arg.SetFederationUpstreamSet("all");
                    arg.SetExpiry(1000);
                });
            });
            
        // Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(_deserializer.ToJsonString(result));
    }

    [Test]
    public async Task Verify_can_delete_policy()
    {
        var result = await _services.GetService<IHareDuFactory>()
            .API<Policy>(x => x.UsingCredentials("guest", "guest"))
            .Delete("P4", "HareDu");
            
        // Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(_deserializer.ToJsonString(result));
    }
}