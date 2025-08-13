namespace HareDu.Snapshotting.IntegrationTests;

using System;
using System.Threading.Tasks;
using Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using Model;
using NUnit.Framework;
using Observers;
using Serialization;

[TestFixture]
public class ClusterSnapshotTests
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
    public async Task Test()
    {
        var lens = _services.GetService<ISnapshotFactory>()
            .Lens<ClusterSnapshot>()
            .RegisterObserver(new DefaultClusterSnapshotConsoleLogger());
        var result = await lens.TakeSnapshot(x => x.UsingCredentials("guest", "guest"));

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
//            var snapshot = resource.Snapshots[0].Select(x => x.Data);
//            Console.WriteLine($"Cluster: {snapshot.ClusterName}");
    }
}