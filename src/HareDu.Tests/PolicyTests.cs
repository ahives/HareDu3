namespace HareDu.Tests;

using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;
using Serialization;

[TestFixture]
public class PolicyTests :
    HareDuTesting
{
    [Test]
    public async Task Should_be_able_to_get_all_policies1()
    {
        var services = GetContainerBuilder("TestData/PolicyInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Policy>()
            .GetAll();

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasData);
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(2, result.Data.Count);
            Assert.AreEqual("P1", result.Data[0].Name);
            Assert.AreEqual("HareDu", result.Data[0].VirtualHost);
            Assert.AreEqual("!@#@", result.Data[0].Pattern);
            Assert.AreEqual("all", result.Data[0].AppliedTo);
            Assert.IsNotNull(result.Data[0].Definition);
            Assert.AreEqual("exactly", result.Data[0].Definition["ha-mode"]);
            Assert.AreEqual("automatic", result.Data[0].Definition["ha-sync-mode"]);
            Assert.AreEqual("2", result.Data[0].Definition["ha-params"]);
            Assert.AreEqual(0, result.Data[0].Priority);
        });
    }
        
    [Test]
    public async Task Should_be_able_to_get_all_policies2()
    {
        var services = GetContainerBuilder("TestData/PolicyInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .GetAllPolicies();

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasData);
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(2, result.Data.Count);
            Assert.AreEqual("P1", result.Data[0].Name);
            Assert.AreEqual("HareDu", result.Data[0].VirtualHost);
            Assert.AreEqual("!@#@", result.Data[0].Pattern);
            Assert.AreEqual("all", result.Data[0].AppliedTo);
            Assert.IsNotNull(result.Data[0].Definition);
            Assert.AreEqual("exactly", result.Data[0].Definition["ha-mode"]);
            Assert.AreEqual("automatic", result.Data[0].Definition["ha-sync-mode"]);
            Assert.AreEqual("2", result.Data[0].Definition["ha-params"]);
            Assert.AreEqual(0, result.Data[0].Priority);
        });
    }
        
    [Test]
    public async Task Verify_can_create_policy1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Policy>()
            .Create("P5", "HareDu", x =>
            {
                x.Pattern("^amq.");
                x.ApplyTo(PolicyAppliedTo.All);
                x.Priority(0);
                x.Definition(arg =>
                {
                    arg.SetHighAvailabilityMode(HighAvailabilityModes.All);
                    arg.SetExpiry(1000);
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);

            PolicyRequest request = result.DebugInfo.Request.ToObject<PolicyRequest>(Deserializer.Options);

            Assert.AreEqual("^amq.", request.Pattern);
            Assert.AreEqual(0, request.Priority);
            Assert.AreEqual("all", request.Arguments["ha-mode"]);
            Assert.AreEqual("1000", request.Arguments["expires"]);
            Assert.AreEqual(PolicyAppliedTo.All, request.ApplyTo);
        });
    }
        
    [Test]
    public async Task Verify_can_create_policy2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .CreatePolicy("P5", "HareDu", x =>
            {
                x.Pattern("^amq.");
                x.ApplyTo(PolicyAppliedTo.All);
                x.Priority(0);
                x.Definition(arg =>
                {
                    arg.SetHighAvailabilityMode(HighAvailabilityModes.All);
                    arg.SetExpiry(1000);
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);

            PolicyRequest request = result.DebugInfo.Request.ToObject<PolicyRequest>(Deserializer.Options);

            Assert.AreEqual("^amq.", request.Pattern);
            Assert.AreEqual(0, request.Priority);
            Assert.AreEqual("all", request.Arguments["ha-mode"]);
            Assert.AreEqual("1000", request.Arguments["expires"]);
            Assert.AreEqual(PolicyAppliedTo.All, request.ApplyTo);
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Policy>()
            .Create(string.Empty, string.Empty, x =>
            {
                x.Pattern("^amq.");
                x.ApplyTo(PolicyAppliedTo.All);
                x.Priority(0);
                x.Definition(arg =>
                {
                    arg.SetHighAvailabilityMode(HighAvailabilityModes.All);
                    arg.SetExpiry(1000);
                    arg.SetFederationUpstreamSet("all");
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            PolicyRequest request = result.DebugInfo.Request.ToObject<PolicyRequest>(Deserializer.Options);

            Assert.AreEqual("^amq.", request.Pattern);
            Assert.AreEqual(0, request.Priority);
            Assert.AreEqual("all", request.Arguments["ha-mode"]);
            Assert.AreEqual("1000", request.Arguments["expires"]);
            Assert.AreEqual(PolicyAppliedTo.All, request.ApplyTo);
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .CreatePolicy(string.Empty, string.Empty, x =>
            {
                x.Pattern("^amq.");
                x.ApplyTo(PolicyAppliedTo.All);
                x.Priority(0);
                x.Definition(arg =>
                {
                    arg.SetHighAvailabilityMode(HighAvailabilityModes.All);
                    arg.SetExpiry(1000);
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            PolicyRequest request = result.DebugInfo.Request.ToObject<PolicyRequest>(Deserializer.Options);

            Assert.AreEqual("^amq.", request.Pattern);
            Assert.AreEqual(0, request.Priority);
            Assert.AreEqual("all", request.Arguments["ha-mode"]);
            Assert.AreEqual("1000", request.Arguments["expires"]);
            Assert.AreEqual(PolicyAppliedTo.All, request.ApplyTo);
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy3()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Policy>()
            .Create(string.Empty, string.Empty, x =>
            {
                x.Pattern("^amq.");
                x.ApplyTo(PolicyAppliedTo.All);
                x.Priority(0);
                x.Definition(arg =>
                {
                    arg.SetHighAvailabilityMode(HighAvailabilityModes.All);
                    arg.SetExpiry(1000);
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            PolicyRequest request = result.DebugInfo.Request.ToObject<PolicyRequest>(Deserializer.Options);

            Assert.AreEqual("^amq.", request.Pattern);
            Assert.AreEqual(0, request.Priority);
            Assert.AreEqual("all", request.Arguments["ha-mode"]);
            Assert.AreEqual("1000", request.Arguments["expires"]);
            Assert.AreEqual(PolicyAppliedTo.All, request.ApplyTo);
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy4()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .CreatePolicy(string.Empty, string.Empty, x =>
            {
                x.Pattern("^amq.");
                x.ApplyTo(PolicyAppliedTo.All);
                x.Priority(0);
                x.Definition(arg =>
                {
                    arg.SetHighAvailabilityMode(HighAvailabilityModes.All);
                    arg.SetExpiry(1000);
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            PolicyRequest request = result.DebugInfo.Request.ToObject<PolicyRequest>(Deserializer.Options);

            Assert.AreEqual("^amq.", request.Pattern);
            Assert.AreEqual(0, request.Priority);
            Assert.AreEqual("all", request.Arguments["ha-mode"]);
            Assert.AreEqual("1000", request.Arguments["expires"]);
            Assert.AreEqual(PolicyAppliedTo.All, request.ApplyTo);
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy5()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Policy>()
            .Create(string.Empty, string.Empty, x =>
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

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            PolicyRequest request = result.DebugInfo.Request.ToObject<PolicyRequest>(Deserializer.Options);

            Assert.AreEqual("^amq.", request.Pattern);
            Assert.AreEqual(0, request.Priority);
            Assert.AreEqual("all", request.Arguments["ha-mode"]);
            Assert.AreEqual("1000", request.Arguments["expires"]);
            Assert.AreEqual(PolicyAppliedTo.All, request.ApplyTo);
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy6()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .CreatePolicy(string.Empty, string.Empty, x =>
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

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            PolicyRequest request = result.DebugInfo.Request.ToObject<PolicyRequest>(Deserializer.Options);

            Assert.AreEqual("^amq.", request.Pattern);
            Assert.AreEqual(0, request.Priority);
            Assert.AreEqual("all", request.Arguments["ha-mode"]);
            Assert.AreEqual("1000", request.Arguments["expires"]);
            Assert.AreEqual(PolicyAppliedTo.All, request.ApplyTo);
        });
    }

    [Test]
    public async Task Verify_can_delete_policy1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Policy>()
            .Delete("P4", "HareDu");
            
        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_can_delete_policy2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DeletePolicy("P4", "HareDu");
            
        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_cannot_delete_policy1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Policy>()
            .Delete(string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_policy2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DeletePolicy(string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_policy3()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Policy>()
            .Delete("P4", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_policy4()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DeletePolicy("P4", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_policy5()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Policy>()
            .Delete(string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_policy6()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DeletePolicy(string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_policy7()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Policy>()
            .Delete(string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_policy8()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DeletePolicy(string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_policy9()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Policy>()
            .Delete("P4", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_policy10()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DeletePolicy("P4", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }
}