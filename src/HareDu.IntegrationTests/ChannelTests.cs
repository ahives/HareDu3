namespace HareDu.IntegrationTests;

using System;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Core.Serialization;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using NUnit.Framework;
using Serialization;

[TestFixture]
public class ChannelTests
{
    ServiceProvider _services;
    readonly IHareDuDeserializer _deserializer;

    public ChannelTests()
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

    [Test, Explicit]
    public async Task Test()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Channel>(x => x.UsingCredentials("guest", "guest"))
            .GetAll()
            .ScreenDump();
            
        // Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(result.ToJsonString(_deserializer.Options));
    }

    [Test]
    public async Task Should_be_able_to_get_all_channels()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Channel>(x => x.UsingCredentials("guest", "guest"))
            .GetAll()
            .ScreenDump();
    }
}