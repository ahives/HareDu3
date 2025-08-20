namespace HareDu.IntegrationTests;

using System;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Core.Serialization;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using Model;
using NUnit.Framework;
using Serialization;

[TestFixture]
public class OperatorPolicyTests
{
    ServiceProvider _services;
    readonly IHareDuDeserializer _deserializer;

    public OperatorPolicyTests()
    {
        _deserializer = new BrokerDeserializer();
    }

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
    public async Task Should_be_able_to_get_all_policies()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<OperatorPolicy>(x => x.UsingCredentials("guest", "guest"))
            .GetAll()
            .ScreenDump();
    }

    [Test]
    public async Task Verify_can_create_operator_policy()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<OperatorPolicy>(x => x.UsingCredentials("guest", "guest"))
            .Create("test7", "TestHareDu", x =>
            {
                x.Pattern(".*");
                x.Priority(0);
                x.ApplyTo(OperatorPolicyAppliedTo.Queues);
                x.Definition(arg =>
                {
                    arg.SetMaxInMemoryBytes(9803129);
                    arg.SetMaxInMemoryLength(283);
                    arg.SetDeliveryLimit(5);
                    arg.SetExpiry(5000);
                    arg.SetMessageMaxSize(189173219);
                });
            });
            
        Console.WriteLine(result.ToJsonString(_deserializer.Options));
    }

    [Test]
    public async Task Test()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<OperatorPolicy>(x => x.UsingCredentials("guest", "guest"))
            .Delete("test6", "TestHareDu");
            
        Console.WriteLine(result.ToJsonString(_deserializer.Options));
    }
}