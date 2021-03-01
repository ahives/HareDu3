namespace HareDu.Snapshotting.IntegrationTests
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using MicrosoftIntegration;
    using Model;
    using NUnit.Framework;
    using Observers;

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
            var result = lens.TakeSnapshot();

//            var snapshot = resource.Snapshots[0].Select(x => x.Data);
//            Console.WriteLine($"Cluster: {snapshot.ClusterName}");
        }
    }
}