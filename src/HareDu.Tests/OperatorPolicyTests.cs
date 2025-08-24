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
public class OperatorPolicyTests :
    HareDuTesting
{
    readonly IHareDuDeserializer _deserializer;

    public OperatorPolicyTests()
    {
        _deserializer = new BrokerDeserializer();
    }

    [Test]
    public async Task Should_be_able_to_get_all_policies1()
    {
        var result = await GetContainerBuilder("TestData/OperatorPolicyInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<OperatorPolicy>(x => x.UsingCredentials("guest", "guest"))
            .GetAll();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasData, Is.True);
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(1));
            Assert.That(result.Data[0].Name, Is.EqualTo("test"));
            Assert.That(result.Data[0].VirtualHost, Is.EqualTo("TestHareDu"));
            Assert.That(result.Data[0].Pattern, Is.EqualTo(".*"));
            Assert.That(result.Data[0].AppliedTo, Is.EqualTo(OperatorPolicyAppliedTo.Queues));
            Assert.That(result.Data[0].Definition, Is.Not.Null);
            Assert.That(result.Data[0].Definition["max-length"], Is.EqualTo(100));
            Assert.That(result.Data[0].Priority, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task Should_be_able_to_get_all_policies2()
    {
        var result = await GetContainerBuilder("TestData/OperatorPolicyInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetAllOperatorPolicies(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasData, Is.True);
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(1));
            Assert.That(result.Data[0].Name, Is.EqualTo("test"));
            Assert.That(result.Data[0].VirtualHost, Is.EqualTo("TestHareDu"));
            Assert.That(result.Data[0].Pattern, Is.EqualTo(".*"));
            Assert.That(result.Data[0].AppliedTo, Is.EqualTo(OperatorPolicyAppliedTo.Queues));
            Assert.That(result.Data[0].Definition, Is.Not.Null);
            Assert.That(result.Data[0].Definition["max-length"], Is.EqualTo(100));
            Assert.That(result.Data[0].Priority, Is.EqualTo(0));
        });
    }
        
    [Test]
    public async Task Verify_can_create_policy1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<OperatorPolicy>(x => x.UsingCredentials("guest", "guest"))
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            OperatorPolicyRequest request = _deserializer.ToObject<OperatorPolicyRequest>(result.DebugInfo.Request);

            Assert.That(request.Pattern, Is.EqualTo("^amq."));
            Assert.That(request.Priority, Is.EqualTo(0));
            Assert.That(request.Arguments["delivery-limit"], Is.EqualTo(5));
            Assert.That(request.Arguments["expires"], Is.EqualTo(1000));
            Assert.That(request.ApplyTo, Is.EqualTo(OperatorPolicyAppliedTo.Queues));
        });
    }
        
    [Test]
    public async Task Verify_can_create_policy2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateOperatorPolicy(x => x.UsingCredentials("guest", "guest"), "policy1", "HareDu", x =>
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            OperatorPolicyRequest request = _deserializer.ToObject<OperatorPolicyRequest>(result.DebugInfo.Request);

            Assert.That(request.Pattern, Is.EqualTo("^amq."));
            Assert.That(request.Priority, Is.EqualTo(0));
            Assert.That(request.Arguments["delivery-limit"], Is.EqualTo(5));
            Assert.That(request.Arguments["expires"], Is.EqualTo(1000));
            Assert.That(request.ApplyTo, Is.EqualTo(OperatorPolicyAppliedTo.Queues));
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<OperatorPolicy>(x => x.UsingCredentials("guest", "guest"))
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            OperatorPolicyRequest request = _deserializer.ToObject<OperatorPolicyRequest>(result.DebugInfo.Request);

            Assert.That(request.Pattern, Is.EqualTo("^amq."));
            Assert.That(request.Priority, Is.EqualTo(0));
            Assert.That(request.Arguments["delivery-limit"], Is.EqualTo(5));
            Assert.That(request.Arguments["expires"], Is.EqualTo(1000));
            Assert.That(request.ApplyTo, Is.EqualTo(OperatorPolicyAppliedTo.Queues));
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateOperatorPolicy(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty, x =>
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            OperatorPolicyRequest request = _deserializer.ToObject<OperatorPolicyRequest>(result.DebugInfo.Request);

            Assert.That(request.Pattern, Is.EqualTo("^amq."));
            Assert.That(request.Priority, Is.EqualTo(0));
            Assert.That(request.Arguments["delivery-limit"], Is.EqualTo(5));
            Assert.That(request.Arguments["expires"], Is.EqualTo(1000));
            Assert.That(request.ApplyTo, Is.EqualTo(OperatorPolicyAppliedTo.Queues));
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<OperatorPolicy>(x => x.UsingCredentials("guest", "guest"))
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            OperatorPolicyRequest request = _deserializer.ToObject<OperatorPolicyRequest>(result.DebugInfo.Request);

            Assert.That(request.Pattern, Is.EqualTo("^amq."));
            Assert.That(request.Priority, Is.EqualTo(0));
            Assert.That(request.Arguments["delivery-limit"], Is.EqualTo(5));
            Assert.That(request.Arguments["expires"], Is.EqualTo(1000));
            Assert.That(request.ApplyTo, Is.EqualTo(OperatorPolicyAppliedTo.Queues));
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateOperatorPolicy(x => x.UsingCredentials("guest", "guest"),
                string.Empty, string.Empty, x =>
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            OperatorPolicyRequest request = _deserializer.ToObject<OperatorPolicyRequest>(result.DebugInfo.Request);

            Assert.That(request.Pattern, Is.EqualTo(".*"));
            Assert.That(request.Priority, Is.EqualTo(0));
            Assert.That(request.Arguments["delivery-limit"], Is.EqualTo(5));
            Assert.That(request.Arguments["expires"], Is.EqualTo(1000));
            Assert.That(request.ApplyTo, Is.EqualTo(OperatorPolicyAppliedTo.Queues));
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<OperatorPolicy>(x => x.UsingCredentials("guest", "guest"))
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            OperatorPolicyRequest request = _deserializer.ToObject<OperatorPolicyRequest>(result.DebugInfo.Request);

            Assert.That(request.Pattern, Is.EqualTo("^amq."));
            Assert.That(request.Priority, Is.EqualTo(0));
            Assert.That(request.Arguments["delivery-limit"], Is.EqualTo(5));
            Assert.That(request.Arguments["expires"], Is.EqualTo(1000));
            Assert.That(request.ApplyTo, Is.EqualTo(OperatorPolicyAppliedTo.Queues));
        });
    }

    [Test]
    public async Task Verify_cannot_create_policy6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateOperatorPolicy(x => x.UsingCredentials("guest", "guest"),
                string.Empty, string.Empty, x =>
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            OperatorPolicyRequest request = _deserializer.ToObject<OperatorPolicyRequest>(result.DebugInfo.Request);

            Assert.That(request.Pattern, Is.EqualTo("^amq."));
            Assert.That(request.Priority, Is.EqualTo(0));
            Assert.That(request.Arguments["delivery-limit"], Is.EqualTo(5));
            Assert.That(request.Arguments["expires"], Is.EqualTo(1000));
            Assert.That(request.ApplyTo, Is.EqualTo(OperatorPolicyAppliedTo.Queues));
        });
    }

    [Test]
    public async Task Verify_can_delete_policy1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<OperatorPolicy>(x => x.UsingCredentials("guest", "guest"))
            .Delete("P4", "HareDu");
            
        Assert.That(result.HasFaulted, Is.False);
    }

    [Test]
    public async Task Verify_can_delete_policy2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteOperatorPolicy(x => x.UsingCredentials("guest", "guest"), "P4", "HareDu");
            
        Assert.That(result.HasFaulted, Is.False);
    }

    [Test]
    public async Task Verify_cannot_delete_policy1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<OperatorPolicy>(x => x.UsingCredentials("guest", "guest"))
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
            .DeleteOperatorPolicy(x => x.UsingCredentials("guest", "guest"), string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_policy3()
    {
        var services = GetContainerBuilder()
            .BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<OperatorPolicy>(x => x.UsingCredentials("guest", "guest"))
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
            .DeleteOperatorPolicy(x => x.UsingCredentials("guest", "guest"), "P4", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_policy5()
    {
        var services = GetContainerBuilder()
            .BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<OperatorPolicy>(x => x.UsingCredentials("guest", "guest"))
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
            .DeleteOperatorPolicy(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty);
            
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
            .API<OperatorPolicy>(x => x.UsingCredentials("guest", "guest"))
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
            .DeleteOperatorPolicy(x => x.UsingCredentials("guest", "guest"), string.Empty, "HareDu");
            
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
            .API<OperatorPolicy>(x => x.UsingCredentials("guest", "guest"))
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
            .DeleteOperatorPolicy(x => x.UsingCredentials("guest", "guest"), "P4", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }
}