namespace HareDu.IntegrationTests;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using NUnit.Framework;
using Serialization;

[TestFixture]
public class UserTests
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
            .API<User>()
            .GetAll()
            .ScreenDump();
    }
        
    [Test]
    public async Task Verify_can_get_all_users_without_permissions()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<User>()
            .GetAllWithoutPermissions()
            .ScreenDump();
    }
        
    [Test]
    public async Task Verify_can_create()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<User>()
            .Create("username3", "guest", configurator: x =>
            {
                x.WithTags(t =>
                {
                    t.Monitoring();
                });
            });
            
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_delete()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<User>()
            .Delete("");
    }

    [Test]
    public async Task Verify_can_bulk_delete()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .DeleteUsers(new List<string> {"username1", "username2", "username3"});
            
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Should_be_able_to_get_all_user_permissions()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<User>()
            .GetAllPermissions()
            .ScreenDump();
    }

    [Test]
    public async Task Verify_can_delete_user_permissions()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<User>()
            .DeletePermissions("", "HareDu5");
    }

    [Test]
    public async Task Verify_can_create_user_permissions()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<User>()
            .ApplyPermissions("", "HareDu5", x =>
            {
                x.UsingConfigurePattern("");
                x.UsingReadPattern("");
                x.UsingWritePattern("");
            });
    }

    [Test]
    public async Task Verify_can_all_user_limits()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<User>()
            .GetAllUserLimits();

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_limit()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<User>()
            .GetLimitsByUser("test");

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_define_limit()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<User>()
            .DefineLimit("test", x =>
            {
                x.SetLimit(UserLimit.MaxChannels, 50);
            });

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
}