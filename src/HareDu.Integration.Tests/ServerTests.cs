namespace HareDu.Integration.Tests
{
    using System.Threading.Tasks;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using MicrosoftIntegration;
    using NUnit.Framework;

    [TestFixture]
    public class ServerTests
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
        public async Task Should_be_able_to_get_all_definitions()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Server>()
                .Get()
                .ScreenDump();
        }
    }
}