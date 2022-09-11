namespace HareDu.IntegrationTests;

using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using NUnit.Framework;

[TestFixture]
public class BrokerSystemTests
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

    [Test, Explicit]
    public async Task Test()
    {
        var result = await _services.GetService<IBrokerObjectFactory>()
            .Object<BrokerSystem>()
            .GetSystemOverview()
            .ScreenDump();
    }
}