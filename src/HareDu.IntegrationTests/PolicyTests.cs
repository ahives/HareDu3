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
    public class PolicyTests
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
        public async Task Should_be_able_to_get_all_policies()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Policy>()
                .GetAll()
                .ScreenDump();
        }
        
        [Test]
        public async Task Verify_can_create_policy()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Policy>()
                .Create("policy1", "^amq.", "TestHareDu", x =>
                {
                    x.SetHighAvailabilityMode(HighAvailabilityModes.Exactly);
                    x.SetHighAvailabilityParams(5);
                    x.SetExpiry(1000);
                }, PolicyAppliedTo.Queues, 0);
            
//            Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString(Deserializer.Options));
         }

        [Test]
        public async Task Verify_cannot_create_policy()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Policy>()
                .Create("P4", "^amq.", "HareDu", x =>
                {
                    x.SetHighAvailabilityMode(HighAvailabilityModes.All);
                    x.SetFederationUpstreamSet("all");
                    x.SetExpiry(1000);
                }, PolicyAppliedTo.All, 0);
            
            // Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString(Deserializer.Options));
        }

        [Test]
        public async Task Verify_can_delete_policy()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Policy>()
                .Delete("P4", "HareDu");
            
            // Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString(Deserializer.Options));
        }
    }
}