namespace HareDu.Tests;

using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Core.Serialization;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;
using Serialization;

[TestFixture]
public class PolicyTests :
    HareDuTesting
{
    readonly IHareDuDeserializer _deserializer;

    public PolicyTests()
    {
        _deserializer = new BrokerDeserializer();
    }

    [Test]
    public async Task Should_be_able_to_get_all_policies1()
    {
        var result = await GetContainerBuilder("TestData/PolicyInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Policy>(x => x.UsingCredentials("guest", "guest"))
            .GetAll();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasData, Is.True);
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(2));
            Assert.That(result.Data[0].Name, Is.EqualTo("P1"));
            Assert.That(result.Data[0].VirtualHost, Is.EqualTo("HareDu"));
            Assert.That(result.Data[0].Pattern, Is.EqualTo("!@#@"));
            Assert.That(result.Data[0].AppliedTo, Is.EqualTo("all"));
            Assert.That(result.Data[0].Definition, Is.Not.Null);
            Assert.That(result.Data[0].Definition["ha-mode"], Is.EqualTo("exactly"));
            Assert.That(result.Data[0].Definition["ha-sync-mode"], Is.EqualTo("automatic"));
            Assert.That(result.Data[0].Definition["ha-params"], Is.EqualTo("2"));
            Assert.That(result.Data[0].Priority, Is.EqualTo(0));
        });
    }
        
    [Test]
    public async Task Should_be_able_to_get_all_policies2()
    {
        var result = await GetContainerBuilder("TestData/PolicyInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetAllPolicies(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasData, Is.True);
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(2));
            Assert.That(result.Data[0].Name, Is.EqualTo("P1"));
            Assert.That(result.Data[0].VirtualHost, Is.EqualTo("HareDu"));
            Assert.That(result.Data[0].Pattern, Is.EqualTo("!@#@"));
            Assert.That(result.Data[0].AppliedTo, Is.EqualTo("all"));
            Assert.That(result.Data[0].Definition, Is.Not.Null);
            Assert.That(result.Data[0].Definition["ha-mode"], Is.EqualTo("exactly"));
            Assert.That(result.Data[0].Definition["ha-sync-mode"], Is.EqualTo("automatic"));
            Assert.That(result.Data[0].Definition["ha-params"], Is.EqualTo("2"));
            Assert.That(result.Data[0].Priority, Is.EqualTo(0));
        });
    }
        
    [Test]
    public async Task Verify_can_create_policy1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Policy>(x => x.UsingCredentials("guest", "guest"))
            .Create("P5", "HareDu", x =>
            {
                x.Pattern("^amq.");
                x.ApplyTo(PolicyAppliedTo.All);
                x.Priority(0);
                x.Definition(arg =>
                {
                    arg.SetHighAvailabilityMode(HighAvailabilityMode.All);
                    arg.SetExpiry(1000);
                });
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            PolicyRequest request = _deserializer.ToObject<PolicyRequest>(result.DebugInfo.Request);

            Assert.That(request.Pattern, Is.EqualTo("^amq."));
            Assert.That(request.Priority, Is.EqualTo(0));
            Assert.That(request.Arguments["ha-mode"], Is.EqualTo("all"));
            Assert.That(request.Arguments["expires"], Is.EqualTo("1000"));
            Assert.That(request.ApplyTo, Is.EqualTo(PolicyAppliedTo.All));
        });
    }
        
    [Test]
    public async Task Verify_can_create_policy2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreatePolicy(x => x.UsingCredentials("guest", "guest"), "P5", "HareDu", x =>
            {
                x.Pattern("^amq.");
                x.ApplyTo(PolicyAppliedTo.All);
                x.Priority(0);
                x.Definition(arg =>
                {
                    arg.SetHighAvailabilityMode(HighAvailabilityMode.All);
                    arg.SetExpiry(1000);
                });
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            PolicyRequest request = _deserializer.ToObject<PolicyRequest>(result.DebugInfo.Request);

            Assert.That(request.Pattern, Is.EqualTo("^amq."));
            Assert.That(request.Priority, Is.EqualTo(0));
            Assert.That(request.Arguments["ha-mode"], Is.EqualTo("all"));
            Assert.That(request.Arguments["expires"], Is.EqualTo("1000"));
            Assert.That(request.ApplyTo, Is.EqualTo(PolicyAppliedTo.All));
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Policy>(x => x.UsingCredentials("guest", "guest"))
            .Create(string.Empty, string.Empty, x =>
            {
                x.Pattern("^amq.");
                x.ApplyTo(PolicyAppliedTo.All);
                x.Priority(0);
                x.Definition(arg =>
                {
                    arg.SetHighAvailabilityMode(HighAvailabilityMode.All);
                    arg.SetExpiry(1000);
                    arg.SetFederationUpstreamSet("all");
                });
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            PolicyRequest request = _deserializer.ToObject<PolicyRequest>(result.DebugInfo.Request);

            Assert.That(request.Pattern, Is.EqualTo("^amq."));
            Assert.That(request.Priority, Is.EqualTo(0));
            Assert.That(request.Arguments["ha-mode"], Is.EqualTo("all"));
            Assert.That(request.Arguments["expires"], Is.EqualTo("1000"));
            Assert.That(request.ApplyTo, Is.EqualTo(PolicyAppliedTo.All));
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreatePolicy(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty, x =>
            {
                x.Pattern("^amq.");
                x.ApplyTo(PolicyAppliedTo.All);
                x.Priority(0);
                x.Definition(arg =>
                {
                    arg.SetHighAvailabilityMode(HighAvailabilityMode.All);
                    arg.SetExpiry(1000);
                });
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            PolicyRequest request = _deserializer.ToObject<PolicyRequest>(result.DebugInfo.Request);

            Assert.That(request.Pattern, Is.EqualTo("^amq."));
            Assert.That(request.Priority, Is.EqualTo(0));
            Assert.That(request.Arguments["ha-mode"], Is.EqualTo("all"));
            Assert.That(request.Arguments["expires"], Is.EqualTo("1000"));
            Assert.That(request.ApplyTo, Is.EqualTo(PolicyAppliedTo.All));
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Policy>(x => x.UsingCredentials("guest", "guest"))
            .Create(string.Empty, string.Empty, x =>
            {
                x.Pattern("^amq.");
                x.ApplyTo(PolicyAppliedTo.All);
                x.Priority(0);
                x.Definition(arg =>
                {
                    arg.SetHighAvailabilityMode(HighAvailabilityMode.All);
                    arg.SetExpiry(1000);
                });
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            PolicyRequest request = _deserializer.ToObject<PolicyRequest>(result.DebugInfo.Request);

            Assert.That(request.Pattern, Is.EqualTo("^amq."));
            Assert.That(request.Priority, Is.EqualTo(0));
            Assert.That(request.Arguments["ha-mode"], Is.EqualTo("all"));
            Assert.That(request.Arguments["expires"], Is.EqualTo("1000"));
            Assert.That(request.ApplyTo, Is.EqualTo(PolicyAppliedTo.All));
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreatePolicy(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty, x =>
            {
                x.Pattern("^amq.");
                x.ApplyTo(PolicyAppliedTo.All);
                x.Priority(0);
                x.Definition(arg =>
                {
                    arg.SetHighAvailabilityMode(HighAvailabilityMode.All);
                    arg.SetExpiry(1000);
                });
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            PolicyRequest request = _deserializer.ToObject<PolicyRequest>(result.DebugInfo.Request);

            Assert.That(request.Pattern, Is.EqualTo("^amq."));
            Assert.That(request.Priority, Is.EqualTo(0));
            Assert.That(request.Arguments["ha-mode"], Is.EqualTo("all"));
            Assert.That(request.Arguments["expires"], Is.EqualTo("1000"));
            Assert.That(request.ApplyTo, Is.EqualTo(PolicyAppliedTo.All));
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Policy>(x => x.UsingCredentials("guest", "guest"))
            .Create(string.Empty, string.Empty, x =>
            {
                x.Pattern("^amq.");
                x.ApplyTo(PolicyAppliedTo.All);
                x.Priority(0);
                x.Definition(arg =>
                {
                    arg.SetHighAvailabilityMode(HighAvailabilityMode.All);
                    arg.SetFederationUpstreamSet("all");
                    arg.SetExpiry(1000);
                });
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            PolicyRequest request = _deserializer.ToObject<PolicyRequest>(result.DebugInfo.Request);

            Assert.That(request.Pattern, Is.EqualTo("^amq."));
            Assert.That(request.Priority, Is.EqualTo(0));
            Assert.That(request.Arguments["ha-mode"], Is.EqualTo("all"));
            Assert.That(request.Arguments["expires"], Is.EqualTo("1000"));
            Assert.That(request.ApplyTo, Is.EqualTo(PolicyAppliedTo.All));
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreatePolicy(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty, x =>
            {
                x.Pattern("^amq.");
                x.ApplyTo(PolicyAppliedTo.All);
                x.Priority(0);
                x.Definition(arg =>
                {
                    arg.SetHighAvailabilityMode(HighAvailabilityMode.All);
                    arg.SetFederationUpstreamSet("all");
                    arg.SetExpiry(1000);
                });
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            PolicyRequest request = _deserializer.ToObject<PolicyRequest>(result.DebugInfo.Request);

            Assert.That(request.Pattern, Is.EqualTo("^amq."));
            Assert.That(request.Priority, Is.EqualTo(0));
            Assert.That(request.Arguments["ha-mode"], Is.EqualTo("all"));
            Assert.That(request.Arguments["expires"], Is.EqualTo("1000"));
            Assert.That(request.ApplyTo, Is.EqualTo(PolicyAppliedTo.All));
        });
    }

    [Test]
    public async Task Verify_can_delete_policy1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Policy>(x => x.UsingCredentials("guest", "guest"))
            .Delete("P4", "HareDu");

        Assert.That(result.HasFaulted, Is.False);
    }

    [Test]
    public async Task Verify_can_delete_policy2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeletePolicy(x => x.UsingCredentials("guest", "guest"), "P4", "HareDu");
            
        Assert.That(result.HasFaulted, Is.False);
    }

    [Test]
    public async Task Verify_cannot_delete_policy1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Policy>(x => x.UsingCredentials("guest", "guest"))
            .Delete(string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_policy2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeletePolicy(x => x.UsingCredentials("guest", "guest"), string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_policy3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Policy>(x => x.UsingCredentials("guest", "guest"))
            .Delete("P4", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_policy4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeletePolicy(x => x.UsingCredentials("guest", "guest"), "P4", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_policy5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Policy>(x => x.UsingCredentials("guest", "guest"))
            .Delete(string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_policy6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeletePolicy(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_policy7()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Policy>(x => x.UsingCredentials("guest", "guest"))
            .Delete(string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_policy8()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeletePolicy(x => x.UsingCredentials("guest", "guest"), string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_policy9()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Policy>(x => x.UsingCredentials("guest", "guest"))
            .Delete("P4", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_policy10()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeletePolicy(x => x.UsingCredentials("guest", "guest"), "P4", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }
}