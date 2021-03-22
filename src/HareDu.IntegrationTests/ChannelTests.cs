namespace HareDu.IntegrationTests
{
    using System;
    using System.Threading.Tasks;
    using Core.Extensions;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using MicrosoftIntegration;
    using NUnit.Framework;
    using Serialization.Converters;

    [TestFixture]
    public class ChannelTests
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
                .Object<Channel>()
                .GetAll()
                .ScreenDump();
            
            // Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString(Deserializer.Options));
        }
        
        [Test]
        public async Task Should_be_able_to_get_all_channels()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Channel>()
                .GetAll()
                .ScreenDump();
        }
    }
}