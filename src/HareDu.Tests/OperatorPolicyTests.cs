namespace HareDu.Tests;

using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;
using Serialization;

[TestFixture]
public class OperatorPolicyTests :
    HareDuTesting
{
    [Test]
    public async Task Should_be_able_to_get_all_policies1()
    {
        var services = GetContainerBuilder("TestData/OperatorPolicyInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<OperatorPolicy>()
            .GetAll();

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasData);
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(1, result.Data.Count);
            Assert.AreEqual("test", result.Data[0].Name);
            Assert.AreEqual("TestHareDu", result.Data[0].VirtualHost);
            Assert.AreEqual(".*", result.Data[0].Pattern);
            Assert.AreEqual(OperatorPolicyAppliedTo.Queues, result.Data[0].AppliedTo);
            Assert.IsNotNull(result.Data[0].Definition);
            Assert.AreEqual(100, result.Data[0].Definition["max-length"]);
            Assert.AreEqual(0, result.Data[0].Priority);
        });
    }

    [Test]
    public async Task Should_be_able_to_get_all_policies2()
    {
        var services = GetContainerBuilder("TestData/OperatorPolicyInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .GetAllOperatorPolicies();

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasData);
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(1, result.Data.Count);
            Assert.AreEqual("test", result.Data[0].Name);
            Assert.AreEqual("TestHareDu", result.Data[0].VirtualHost);
            Assert.AreEqual(".*", result.Data[0].Pattern);
            Assert.AreEqual(OperatorPolicyAppliedTo.Queues, result.Data[0].AppliedTo);
            Assert.IsNotNull(result.Data[0].Definition);
            Assert.AreEqual(100, result.Data[0].Definition["max-length"]);
            Assert.AreEqual(0, result.Data[0].Priority);
        });
    }
        
    [Test]
    public async Task Verify_can_create_policy1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<OperatorPolicy>()
            .Create("policy1", "HareDu", x =>
            {
                x.Pattern("^amq.");
                x.Priority(0);
                x.ApplyTo(OperatorPolicyAppliedTo.Queues);
                x.Definition(arg =>
                {
                    arg.SetDeliveryLimit(5);
                    arg.SetExpiry(1000);
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);

            OperatorPolicyRequest request = result.DebugInfo.Request.ToObject<OperatorPolicyRequest>(Deserializer.Options);

            Assert.AreEqual("^amq.", request.Pattern);
            Assert.AreEqual(0, request.Priority);
            Assert.AreEqual(5, request.Arguments["delivery-limit"]);
            Assert.AreEqual(1000, request.Arguments["expires"]);
            Assert.AreEqual(OperatorPolicyAppliedTo.Queues, request.ApplyTo);
        });
    }
        
    [Test]
    public async Task Verify_can_create_policy2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .CreateOperatorPolicy("policy1", "HareDu", x =>
            {
                x.Pattern("^amq.");
                x.Priority(0);
                x.ApplyTo(OperatorPolicyAppliedTo.Queues);
                x.Definition(arg =>
                {
                    arg.SetDeliveryLimit(5);
                    arg.SetExpiry(1000);
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);

            OperatorPolicyRequest request = result.DebugInfo.Request.ToObject<OperatorPolicyRequest>(Deserializer.Options);

            Assert.AreEqual("^amq.", request.Pattern);
            Assert.AreEqual(0, request.Priority);
            Assert.AreEqual(5, request.Arguments["delivery-limit"]);
            Assert.AreEqual(1000, request.Arguments["expires"]);
            Assert.AreEqual(OperatorPolicyAppliedTo.Queues, request.ApplyTo);
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<OperatorPolicy>()
            .Create(string.Empty, string.Empty, x =>
            {
                x.Pattern("^amq.");
                x.Priority(0);
                x.ApplyTo(OperatorPolicyAppliedTo.Queues);
                x.Definition(arg =>
                {
                    arg.SetDeliveryLimit(5);
                    arg.SetExpiry(1000);
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            OperatorPolicyRequest request = result.DebugInfo.Request.ToObject<OperatorPolicyRequest>(Deserializer.Options);

            Assert.AreEqual("^amq.", request.Pattern);
            Assert.AreEqual(0, request.Priority);
            Assert.AreEqual(5, request.Arguments["delivery-limit"]);
            Assert.AreEqual(1000, request.Arguments["expires"]);
            Assert.AreEqual(OperatorPolicyAppliedTo.Queues, request.ApplyTo);
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .CreateOperatorPolicy(string.Empty, string.Empty, x =>
            {
                x.Pattern("^amq.");
                x.Priority(0);
                x.ApplyTo(OperatorPolicyAppliedTo.Queues);
                x.Definition(arg =>
                {
                    arg.SetDeliveryLimit(5);
                    arg.SetExpiry(1000);
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            OperatorPolicyRequest request = result.DebugInfo.Request.ToObject<OperatorPolicyRequest>(Deserializer.Options);

            Assert.AreEqual("^amq.", request.Pattern);
            Assert.AreEqual(0, request.Priority);
            Assert.AreEqual(5, request.Arguments["delivery-limit"]);
            Assert.AreEqual(1000, request.Arguments["expires"]);
            Assert.AreEqual(OperatorPolicyAppliedTo.Queues, request.ApplyTo);
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy3()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<OperatorPolicy>()
            .Create(string.Empty, string.Empty, x =>
            {
                x.Pattern("^amq.");
                x.Priority(0);
                x.ApplyTo(OperatorPolicyAppliedTo.Queues);
                x.Definition(arg =>
                {
                    arg.SetDeliveryLimit(5);
                    arg.SetExpiry(1000);
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            OperatorPolicyRequest request = result.DebugInfo.Request.ToObject<OperatorPolicyRequest>(Deserializer.Options);

            Assert.AreEqual("^amq.", request.Pattern);
            Assert.AreEqual(0, request.Priority);
            Assert.AreEqual(5, request.Arguments["delivery-limit"]);
            Assert.AreEqual(1000, request.Arguments["expires"]);
            Assert.AreEqual(OperatorPolicyAppliedTo.Queues, request.ApplyTo);
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy4()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .CreateOperatorPolicy(string.Empty, string.Empty, x =>
            {
                x.Pattern(".*");
                x.Priority(0);
                x.ApplyTo(OperatorPolicyAppliedTo.Queues);
                x.Definition(arg =>
                {
                    arg.SetDeliveryLimit(5);
                    arg.SetExpiry(1000);
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            OperatorPolicyRequest request = result.DebugInfo.Request.ToObject<OperatorPolicyRequest>(Deserializer.Options);

            Assert.AreEqual("^amq.", request.Pattern);
            Assert.AreEqual(0, request.Priority);
            Assert.AreEqual(5, request.Arguments["delivery-limit"]);
            Assert.AreEqual(1000, request.Arguments["expires"]);
            Assert.AreEqual(OperatorPolicyAppliedTo.Queues, request.ApplyTo);
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy5()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<OperatorPolicy>()
            .Create(string.Empty, string.Empty, x =>
            {
                x.Pattern("^amq.");
                x.Priority(0);
                x.ApplyTo(OperatorPolicyAppliedTo.Queues);
                x.Definition(arg =>
                {
                    arg.SetDeliveryLimit(5);
                    arg.SetExpiry(1000);
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            OperatorPolicyRequest request = result.DebugInfo.Request.ToObject<OperatorPolicyRequest>(Deserializer.Options);

            Assert.AreEqual("^amq.", request.Pattern);
            Assert.AreEqual(0, request.Priority);
            Assert.AreEqual(5, request.Arguments["delivery-limit"]);
            Assert.AreEqual(1000, request.Arguments["expires"]);
            Assert.AreEqual(OperatorPolicyAppliedTo.Queues, request.ApplyTo);
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy6()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .CreateOperatorPolicy(string.Empty, string.Empty, x =>
            {
                x.Pattern("^amq.");
                x.Priority(0);
                x.ApplyTo(OperatorPolicyAppliedTo.Queues);
                x.Definition(arg =>
                {
                    arg.SetDeliveryLimit(5);
                    arg.SetExpiry(1000);
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            OperatorPolicyRequest request = result.DebugInfo.Request.ToObject<OperatorPolicyRequest>(Deserializer.Options);

            Assert.AreEqual("^amq.", request.Pattern);
            Assert.AreEqual(0, request.Priority);
            Assert.AreEqual(5, request.Arguments["delivery-limit"]);
            Assert.AreEqual(1000, request.Arguments["expires"]);
            Assert.AreEqual(OperatorPolicyAppliedTo.Queues, request.ApplyTo);
        });
    }

    [Test]
    public async Task Verify_can_delete_policy1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<OperatorPolicy>()
            .Delete("P4", "HareDu");
            
        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_can_delete_policy2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DeleteOperatorPolicy("P4", "HareDu");
            
        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_cannot_delete_policy1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<OperatorPolicy>()
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
            .DeleteOperatorPolicy(string.Empty, "HareDu");
            
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
            .API<OperatorPolicy>()
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
            .DeleteOperatorPolicy("P4", string.Empty);
            
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
            .API<OperatorPolicy>()
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
            .DeleteOperatorPolicy(string.Empty, string.Empty);
            
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
            .API<OperatorPolicy>()
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
            .DeleteOperatorPolicy(string.Empty, "HareDu");
            
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
            .API<OperatorPolicy>()
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
            .DeleteOperatorPolicy("P4", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }
}