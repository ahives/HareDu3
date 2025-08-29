namespace HareDu.IntegrationTests;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Core.Serialization;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using Model;
using NUnit.Framework;
using Serialization;

[TestFixture]
public class UserTests
{
    ServiceProvider _services;
    readonly IHareDuDeserializer _deserializer;

    public UserTests()
    {
        _deserializer = new BrokerDeserializer();
    }

    [OneTimeSetUp]
    public void Init()
    {
        _services = new ServiceCollection()
            .AddHareDu(x =>
            {
                x.KnowledgeBase(k =>
                {
                    k.File("kb.json", "Articles");
                });
                x.Broker(b =>
                {
                    b.ConnectTo("http://localhost:15672");
                    b.WithBehavior(behavior =>
                    {
                        behavior.LimitRequests(5, 5);
                    });
                });
                x.Diagnostics(d =>
                {
                    d.Probes(p =>
                    {
                        p.SetConsumerUtilizationThreshold(1);
                        p.SetFileDescriptorUsageThresholdCoefficient(1);
                        p.SetHighConnectionClosureRateThreshold(1);
                        p.SetFileDescriptorUsageThresholdCoefficient(1);
                        p.SetHighConnectionClosureRateThreshold(1);
                        p.SetMessageRedeliveryThresholdCoefficient(1);
                        p.SetHighConnectionCreationRateThreshold(1);
                        p.SetQueueHighFlowThreshold(1);
                        p.SetQueueLowFlowThreshold(1);
                        p.SetRuntimeProcessUsageThresholdCoefficient(1);
                        p.SetSocketUsageThresholdCoefficient(1);
                    });
                });
            })
            .BuildServiceProvider();
    }

    [Test]
    public async Task Verify_can_get_all_users()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .GetAll()
            .ScreenDump();
    }

    [Test]
    public async Task Verify_can_get_all_users_without_permissions()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .GetAllWithoutPermissions()
            .ScreenDump();
    }

    [Test]
    public async Task Verify_can_create()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .Create("username3", "guest", configurator: x =>
            {
                x.WithTags(t =>
                {
                    t.AddTag(UserAccessTag.Monitoring);
                });
            });
            
        Console.WriteLine(_deserializer.ToJsonString(result));
    }

    [Test]
    public async Task Verify_can_delete()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .Delete("");
    }

    [Test]
    public async Task Verify_can_bulk_delete()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .DeleteUsers(x => x.UsingCredentials("guest", "guest"), new List<string> {"username1", "username2", "username3"});
            
        Console.WriteLine(_deserializer.ToJsonString(result));
    }

    [Test]
    public async Task Should_be_able_to_get_all_user_permissions()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .GetAllPermissions()
            .ScreenDump();
    }

    [Test]
    public async Task Verify_can_all_user_limits()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .GetAllUserLimits();

        Console.WriteLine(_deserializer.ToJsonString(result));
    }

    [Test]
    public async Task Verify_can_limit()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .GetLimitsByUser("test");

        Console.WriteLine(_deserializer.ToJsonString(result));
    }

    [Test]
    public async Task Verify_can_define_limit()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .DefineLimit("test", x =>
            {
                x.SetLimit(UserLimit.MaxChannels, 50);
            });

        Console.WriteLine(_deserializer.ToJsonString(result));
    }
}