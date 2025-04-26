namespace HareDu.IntegrationTests;

using System;
using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using NUnit.Framework;
using Serialization;

[TestFixture]
public class GlobalParameterTests
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
    public async Task Should_be_able_to_get_all_global_parameters()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<GlobalParameter>(x => x.UsingCredentials("guest", "guest"))
            .GetAll()
            .ScreenDump();
    }
        
    [Test]
    public async Task Verify_can_create_parameter()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<GlobalParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create("fake_param2", x =>
            {
                x.Add("arg1", "value1");
                x.Add("arg2", "value2");
            });
             
        // Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
        
    [Test]
    public async Task Verify_can_delete_parameter()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<GlobalParameter>(x => x.UsingCredentials("guest", "guest"))
            .Delete("Fred");
            
        // Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
}