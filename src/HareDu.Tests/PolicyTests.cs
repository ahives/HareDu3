namespace HareDu.Tests
{
    using System.Threading.Tasks;
    using Core.Extensions;
    using Core.Serialization;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using Model;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class PolicyTests :
        HareDuTesting
    {
        [Test]
        public async Task Should_be_able_to_get_all_policies1()
        {
            var services = GetContainerBuilder("TestData/PolicyInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Policy>()
                .GetAll();

            result.HasData.ShouldBeTrue();
            result.HasFaulted.ShouldBeFalse();
            result.Data.ShouldNotBeNull();
            result.Data.Count.ShouldBe(2);
            result.Data[0].Name.ShouldBe("P1");
            result.Data[0].VirtualHost.ShouldBe("HareDu");
            result.Data[0].Pattern.ShouldBe("!@#@");
            result.Data[0].AppliedTo.ShouldBe("all");
            result.Data[0].Definition.ShouldNotBeNull();
            result.Data[0].Definition["ha-mode"].ShouldBe("exactly");
            result.Data[0].Definition["ha-sync-mode"].ShouldBe("automatic");
            result.Data[0].Definition["ha-params"].ShouldBe("2");
            result.Data[0].Priority.ShouldBe(0);
        }
        
        [Test]
        public async Task Should_be_able_to_get_all_policies2()
        {
            var services = GetContainerBuilder("TestData/PolicyInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .GetAllPolicies();

            result.HasData.ShouldBeTrue();
            result.HasFaulted.ShouldBeFalse();
            result.Data.ShouldNotBeNull();
            result.Data.Count.ShouldBe(2);
            result.Data[0].Name.ShouldBe("P1");
            result.Data[0].VirtualHost.ShouldBe("HareDu");
            result.Data[0].Pattern.ShouldBe("!@#@");
            result.Data[0].AppliedTo.ShouldBe("all");
            result.Data[0].Definition.ShouldNotBeNull();
            result.Data[0].Definition["ha-mode"].ShouldBe("exactly");
            result.Data[0].Definition["ha-sync-mode"].ShouldBe("automatic");
            result.Data[0].Definition["ha-params"].ShouldBe("2");
            result.Data[0].Priority.ShouldBe(0);
        }
        
        [Test]
        public async Task Verify_can_create_policy1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
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
            
            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();

            PolicyDefinition definition = result.DebugInfo.Request.ToObject<PolicyDefinition>(Deserializer.Options);
            
            definition.Pattern.ShouldBe("^amq.");
            definition.Priority.ShouldBe(0);
            definition.Arguments["ha-mode"].ShouldBe("all");
            definition.Arguments["expires"].ShouldBe("1000");
            definition.ApplyTo.ShouldBe("all");
         }
        
        [Test]
        public async Task Verify_can_create_policy2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .CreatePolicy("P5", "HareDu", x =>
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
            
            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();

            PolicyDefinition definition = result.DebugInfo.Request.ToObject<PolicyDefinition>(Deserializer.Options);
            
            definition.Pattern.ShouldBe("^amq.");
            definition.Priority.ShouldBe(0);
            definition.Arguments["ha-mode"].ShouldBe("all");
            definition.Arguments["expires"].ShouldBe("1000");
            definition.ApplyTo.ShouldBe("all");
         }

        [Test]
        public async Task Verify_cannot_create_policy_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Policy>()
                .Create(string.Empty, string.Empty, x =>
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
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.ShouldNotBeNull();

            PolicyDefinition definition = result.DebugInfo.Request.ToObject<PolicyDefinition>(Deserializer.Options);
            
            definition.Pattern.ShouldBe("^amq.");
            definition.Priority.ShouldBe(0);
            definition.Arguments["ha-mode"].ShouldBe("all");
            definition.Arguments["expires"].ShouldBe("1000");
            definition.ApplyTo.ShouldBe("all");
            
            result.DebugInfo.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_cannot_create_policy_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Policy>()
                .Create(string.Empty, string.Empty, x =>
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
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.ShouldNotBeNull();

            PolicyDefinition definition = result.DebugInfo.Request.ToObject<PolicyDefinition>(Deserializer.Options);
            
            definition.Pattern.ShouldBe("^amq.");
            definition.Priority.ShouldBe(0);
            definition.Arguments["ha-mode"].ShouldBe("all");
            definition.Arguments["expires"].ShouldBe("1000");
            definition.ApplyTo.ShouldBe("all");
            
            result.DebugInfo.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_cannot_create_policy_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Policy>()
                .Create(string.Empty, string.Empty, x =>
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
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.ShouldNotBeNull();

            PolicyDefinition definition = result.DebugInfo.Request.ToObject<PolicyDefinition>(Deserializer.Options);
            
            definition.Pattern.ShouldBe("^amq.");
            definition.Priority.ShouldBe(0);
            definition.Arguments["ha-mode"].ShouldBe("all");
            definition.Arguments["expires"].ShouldBe("1000");
            definition.ApplyTo.ShouldBe("all");
            
            result.DebugInfo.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_can_delete_policy1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Policy>()
                .Delete("P4", "HareDu");
            
            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public async Task Verify_can_delete_policy2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .DeletePolicy("P4", "HareDu");
            
            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public async Task Verify_cannot_delete_policy_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Policy>()
                .Delete(string.Empty, "HareDu");
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_policy_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Policy>()
                .Delete("P4", string.Empty);
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_policy_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Policy>()
                .Delete(string.Empty, string.Empty);
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_cannot_delete_policy_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Policy>()
                .Delete(string.Empty, "HareDu");
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_policy_5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Policy>()
                .Delete("P4", string.Empty);
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
        }
    }
}