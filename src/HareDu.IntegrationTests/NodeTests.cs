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
        var result = await _services.GetService<IBrokerFactory>()
            .API<Node>()
            .GetAll()
            .ScreenDump();
    }

    [Test]
    public async Task Should_be_able_to_get_all_memory_usage()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Node>()
            .GetMemoryUsage("rabbit@localhost")
            .ScreenDump();
    }
}