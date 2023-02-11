namespace HareDu.IntegrationTests;

using System;
using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
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
                x.Broker(b =>
                {
                    b.ConnectTo("http://localhost:15672");
                    b.UsingCredentials("guest", "guest");
                });
            })
            .BuildServiceProvider();
    }

    [Test]
    public async Task Should_be_able_to_get_all_nodes()
    {
        var result = await _services.GetService<IBrokerApiFactory>()
            .API<Node>()
            .GetAll()
            .ScreenDump();
    }

    [Test]
    public async Task Should_be_able_to_get_all_memory_usage()
    {
        var result = await _services.GetService<IBrokerApiFactory>()
            .API<Node>()
            .GetMemoryUsage("rabbit@localhost")
            .ScreenDump();
    }

    [Test]
    public async Task Verify_can_check_if_named_node_healthy()
    {
        var result = await _services.GetService<IBrokerApiFactory>()
            .API<Node>()
            .GetHealth("rabbit@localhost")
            .ScreenDump();
    }

    [Test]
    public async Task Verify_can_check_if_node_healthy()
    {
        var result = await _services.GetService<IBrokerApiFactory>()
            .API<Node>()
            .GetHealth();
            
        Console.WriteLine((string) result.DebugInfo.URL);
    }
}