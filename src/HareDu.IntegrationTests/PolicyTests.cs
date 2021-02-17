namespace HareDu.IntegrationTests
{
    using System;
    using System.Threading.Tasks;
    using Core.Extensions;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using MicrosoftIntegration;
    using NUnit.Framework;

    [TestFixture]
    public class PolicyTests
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
                .Create("P5", "HareDu", x =>
                {
                    x.UsingPattern("^amq.");
                    x.HasPriority(0);
                    x.HasArguments(d =>
                    {
                        d.SetHighAvailabilityMode(HighAvailabilityModes.All);
                        d.SetExpiry(1000);
                    });
                    x.ApplyTo(PolicyAppliedTo.All);
                });
            
//            Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString());
         }

        [Test]
        public async Task Verify_cannot_create_policy()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Policy>()
                .Create("P4", "HareDu", x =>
                {
                    x.UsingPattern("^amq.");
                    x.HasPriority(0);
                    x.HasArguments(d =>
                    {
                        d.SetHighAvailabilityMode(HighAvailabilityModes.All);
                        d.SetFederationUpstreamSet("all");
                        d.SetExpiry(1000);
                    });
                    x.ApplyTo(PolicyAppliedTo.All);
                });
            
            // Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString());
        }

        [Test]
        public async Task Verify_can_delete_policy()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Policy>()
                .Delete("P4", "HareDu");
            
            // Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString());
        }
    }
}