namespace HareDu.Snapshotting.IntegrationTests;

using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using Model;
using NUnit.Framework;
using Observers;

[TestFixture]
public class ConnectionSnapshotTests
{
    ServiceProvider _services;

    [OneTimeSetUp]
    public void Init()
    {
        _services = new ServiceCollection()
            .AddHareDu()
            .BuildServiceProvider();
    }

    [Test]
    public async Task Test1()
    {
        var result = _services.GetService<ISnapshotFactory>()
            .Lens<BrokerConnectivitySnapshot>()
            .RegisterObserver(new DefaultConnectivitySnapshotConsoleLogger())
            .TakeSnapshot(x => x.UsingCredentials("guest", "guest"));
    }

    [Test]
    public async Task Test2()
    {
        var result = _services.GetService<ISnapshotFactory>()
            .Lens<BrokerConnectivitySnapshot>()
            .TakeSnapshot(x => x.UsingCredentials("guest", "guest"));
    }
}