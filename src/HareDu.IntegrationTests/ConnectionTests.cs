namespace HareDu.IntegrationTests;

using System;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using NUnit.Framework;
using Serialization;

[TestFixture]
public class ConnectionTests
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

    [Test, Explicit]
    public async Task Should_be_able_to_get_all_connections()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Connection>(x => x.UsingCredentials("guest", "guest"))
            .GetAll()
            .ScreenDump();
    }

    [Test]
    public async Task Should_be_able_to__filter_by_name()
    {
        string connectionName;
        var result = await _services.GetService<IBrokerFactory>()
            .API<Connection>(x => x.UsingCredentials("guest", "guest"))
            .GetByName("127.0.0.1:56601 -> 127.0.0.1:5672");
    }

    [Test]
    public async Task Test()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .DeleteConnection(x => x.UsingCredentials("guest", "guest"), "127.0.0.1:56601 -> 127.0.0.1:5672");
            
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
}