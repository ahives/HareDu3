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
            })
            .BuildServiceProvider();
    }

    [Test]
    public async Task Verify_can_get_all_users()
    {
        var result = await _services.GetService<IBrokerApiFactory>()
            .API<User>()
            .GetAll()
            .ScreenDump();
    }
        
    [Test]
    public async Task Verify_can_get_all_users_without_permissions()
    {
        var result = await _services.GetService<IBrokerApiFactory>()
            .API<User>()
            .GetAllWithoutPermissions()
            .ScreenDump();
    }
        
    [Test]
    public async Task Verify_can_create()
    {
        var result = await _services.GetService<IBrokerApiFactory>()
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
        var result = await _services.GetService<IBrokerApiFactory>()
            .API<User>()
            .Delete("");
    }

    [Test]
    public async Task Verify_can_bulk_delete()
    {
        var result = await _services.GetService<IBrokerApiFactory>()
            .DeleteUsers(new List<string> {"username1", "username2", "username3"});
            
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
}