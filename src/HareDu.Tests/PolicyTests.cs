namespace HareDu.Tests;

using System.Threading.Tasks;
using Core;
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
            Assert.That(result.Data[0].Name, Is.EqualTo("test2"));
            Assert.That(result.Data[0].VirtualHost, Is.EqualTo("vhost1"));
            Assert.That(result.Data[0].Pattern, Is.EqualTo("."));
            Assert.That(result.Data[0].AppliedTo, Is.EqualTo(PolicyAppliedTo.Queues));
            Assert.That(result.Data[0].Definition, Is.Not.Null);
            Assert.That(result.Data[0].Definition.DeliveryLimit, Is.EqualTo(3));
            Assert.That(result.Data[0].Definition.ConsumerTimeout, Is.EqualTo(65));
            Assert.That(result.Data[0].Definition.DeadLetterExchangeName, Is.EqualTo("test"));
            Assert.That(result.Data[0].Definition.DeadLetterRoutingKey, Is.EqualTo(".*"));
            Assert.That(result.Data[0].Definition.DeadLetterQueueStrategy, Is.EqualTo(DeadLetterQueueStrategy.AtLeastOnce));
            Assert.That(result.Data[0].Definition.AutoExpire, Is.EqualTo(1));
            Assert.That(result.Data[0].Definition.MaxLength, Is.EqualTo(1));
            Assert.That(result.Data[0].Definition.MaxLengthBytes, Is.EqualTo(3));
            Assert.That(result.Data[0].Definition.MessageTimeToLive, Is.EqualTo(32));
            Assert.That(result.Data[0].Definition.OverflowBehavior, Is.EqualTo(QueueOverflowBehavior.DropHead));
            Assert.That(result.Data[0].Definition.QueueLeaderLocator, Is.EqualTo(QueueLeaderLocator.Balanced));
            Assert.That(result.Data[0].Priority, Is.EqualTo(1));
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
            Assert.That(result.Data[0].Name, Is.EqualTo("test2"));
            Assert.That(result.Data[0].VirtualHost, Is.EqualTo("vhost1"));
            Assert.That(result.Data[0].Pattern, Is.EqualTo("."));
            Assert.That(result.Data[0].AppliedTo, Is.EqualTo(PolicyAppliedTo.Queues));
            Assert.That(result.Data[0].Definition, Is.Not.Null);
            Assert.That(result.Data[0].Definition.DeliveryLimit, Is.EqualTo(3));
            Assert.That(result.Data[0].Definition.ConsumerTimeout, Is.EqualTo(65));
            Assert.That(result.Data[0].Definition.DeadLetterExchangeName, Is.EqualTo("test"));
            Assert.That(result.Data[0].Definition.DeadLetterRoutingKey, Is.EqualTo(".*"));
            Assert.That(result.Data[0].Definition.DeadLetterQueueStrategy, Is.EqualTo(DeadLetterQueueStrategy.AtLeastOnce));
            Assert.That(result.Data[0].Definition.AutoExpire, Is.EqualTo(1));
            Assert.That(result.Data[0].Definition.MaxLength, Is.EqualTo(1));
            Assert.That(result.Data[0].Definition.MaxLengthBytes, Is.EqualTo(3));
            Assert.That(result.Data[0].Definition.MessageTimeToLive, Is.EqualTo(32));
            Assert.That(result.Data[0].Definition.OverflowBehavior, Is.EqualTo(QueueOverflowBehavior.DropHead));
            Assert.That(result.Data[0].Definition.QueueLeaderLocator, Is.EqualTo(QueueLeaderLocator.Balanced));
            Assert.That(result.Data[0].Priority, Is.EqualTo(1));
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
                x.ApplyTo(PolicyAppliedTo.QueuesAndExchanges);
                x.Priority(0);
                x.Definition(arg =>
                {
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
            Assert.That(request.Definition.AutoExpire, Is.EqualTo(1000));
            Assert.That(request.ApplyTo, Is.EqualTo(PolicyAppliedTo.QueuesAndExchanges));
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
                x.ApplyTo(PolicyAppliedTo.QueuesAndExchanges);
                x.Priority(0);
                x.Definition(arg =>
                {
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
            Assert.That(request.Definition.AutoExpire, Is.EqualTo(1000));
            Assert.That(request.ApplyTo, Is.EqualTo(PolicyAppliedTo.QueuesAndExchanges));
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
                x.ApplyTo(PolicyAppliedTo.QueuesAndExchanges);
                x.Priority(0);
                x.Definition(arg =>
                {
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
            Assert.That(request.Definition.AutoExpire, Is.EqualTo(1000));
            Assert.That(request.ApplyTo, Is.EqualTo(PolicyAppliedTo.QueuesAndExchanges));
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
                x.ApplyTo(PolicyAppliedTo.QueuesAndExchanges);
                x.Priority(0);
                x.Definition(arg =>
                {
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
            Assert.That(request.Definition.AutoExpire, Is.EqualTo(1000));
            Assert.That(request.ApplyTo, Is.EqualTo(PolicyAppliedTo.QueuesAndExchanges));
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
                x.ApplyTo(PolicyAppliedTo.QueuesAndExchanges);
                x.Priority(0);
                x.Definition(arg =>
                {
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
            Assert.That(request.Definition.AutoExpire, Is.EqualTo(1000));
            Assert.That(request.ApplyTo, Is.EqualTo(PolicyAppliedTo.QueuesAndExchanges));
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
                x.ApplyTo(PolicyAppliedTo.QueuesAndExchanges);
                x.Priority(0);
                x.Definition(arg =>
                {
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
            Assert.That(request.Definition.AutoExpire, Is.EqualTo(1000));
            Assert.That(request.ApplyTo, Is.EqualTo(PolicyAppliedTo.QueuesAndExchanges));
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
                x.ApplyTo(PolicyAppliedTo.QueuesAndExchanges);
                x.Priority(0);
                x.Definition(arg =>
                {
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
            Assert.That(request.Definition.AutoExpire, Is.EqualTo(1000));
            Assert.That(request.ApplyTo, Is.EqualTo(PolicyAppliedTo.QueuesAndExchanges));
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
                x.ApplyTo(PolicyAppliedTo.QueuesAndExchanges);
                x.Priority(0);
                x.Definition(arg =>
                {
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
            Assert.That(request.Definition.AutoExpire, Is.EqualTo(1000));
            Assert.That(request.ApplyTo, Is.EqualTo(PolicyAppliedTo.QueuesAndExchanges));
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