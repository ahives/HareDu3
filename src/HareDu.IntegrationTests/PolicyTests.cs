namespace HareDu.IntegrationTests;

using System;
using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using NUnit.Framework;
using Serialization;

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
        var result = await _services.GetService<IBrokerFactory>()
            .API<Policy>()
            .GetAll()
            .ScreenDump();
    }
        
    [Test]
    public async Task Verify_can_create_policy()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Policy>()
            .Create("policy1", "TestHareDu", x =>
            {
                x.Pattern("^amq.");
                x.ApplyTo(PolicyAppliedTo.Queues);
                x.Priority(0);
                x.Definition(arg =>
                {
                    arg.SetHighAvailabilityMode(HighAvailabilityModes.Exactly);
                    arg.SetHighAvailabilityParams(5);
                    arg.SetExpiry(1000);
                });
            });
            
//            Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_cannot_create_policy()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Policy>()
            .Create("P4", "HareDu", x =>
            {
                x.Pattern("^amq.");
                x.ApplyTo(PolicyAppliedTo.All);
                x.Priority(0);
                x.Definition(arg =>
                {
                    arg.SetHighAvailabilityMode(HighAvailabilityModes.All);
                    arg.SetFederationUpstreamSet("all");
                    arg.SetExpiry(1000);
                });
            });
            
        // Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_delete_policy()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Policy>()
            .Delete("P4", "HareDu");
            
        // Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
}