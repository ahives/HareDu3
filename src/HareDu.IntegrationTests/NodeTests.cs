namespace HareDu.IntegrationTests;

using System.Threading.Tasks;
using Core;
using DependencyInjection;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

[TestFixture]
public class NodeTests
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
    public async Task Should_be_able_to_get_all_nodes()
    {
        var result = await _services.GetService<IHareDuFactory>()
            .API<Node>(x => x.UsingCredentials("guest", "guest"))
            .GetAll()
            .ScreenDump();
    }

    [Test]
    public async Task Should_be_able_to_get_all_memory_usage()
    {
        var result = await _services.GetService<IHareDuFactory>()
            .API<Node>(x => x.UsingCredentials("guest", "guest"))
            .GetMemoryUsage("rabbit@localhost")
            .ScreenDump();
    }
}