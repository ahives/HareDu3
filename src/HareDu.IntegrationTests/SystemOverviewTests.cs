namespace HareDu.IntegrationTests
{
    using System.Threading.Tasks;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using MicrosoftIntegration;
    using NUnit.Framework;

    [TestFixture]
    public class SystemOverviewTests
    {
        ServiceProvider _services;

        [OneTimeSetUp]
        public void Init()
        {
            _services = new ServiceCollection()
                .AddHareDu()
                .BuildServiceProvider();
        }

        [Test, Explicit]
        public async Task Test()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<SystemOverview>()
                .Get()
                .ScreenDump();
        }
    }
}