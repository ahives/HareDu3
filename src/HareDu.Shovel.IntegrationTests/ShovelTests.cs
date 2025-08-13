namespace HareDu.Shovel.IntegrationTests;

using Core;
using Core.Extensions;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using Model;
using Serialization;
using Testing;

[TestFixture]
public class ShovelTests
{
    ServiceProvider _services;
    string _shovelName = "test-shovel1";
    string _vhost = "TestVirtualHost1";
    string _node = "rabbit@b91edc210b0d";

    [OneTimeSetUp]
    public void Init()
    {
        _services = new ServiceCollection()
            .AddHareDu()
            .BuildServiceProvider();
    }

    [OneTimeTearDown]
    public void FixtureTeardown()
    {
        _services.Dispose();
    }

    [SetUp]
    public async Task TestSetUp()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .Create(_vhost);
    }

    [TearDown]
    public async Task TestTearDown()
    {
        // var result = await _services.GetService<IBrokerFactory>()
        //     .API<VirtualHost>()
        //     .Delete(_vhost);
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel1()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Shovel>(x => x.UsingCredentials("guest", "guest"))
            .Create("test-shovel3", _vhost, x =>
            {
                x.Uri("amqp://user1@localhost");
                x.Source("queue1", ShovelProtocol.Amqp091, c =>
                {
                    c.DeleteAfter(ShovelDeleteMode.QueueLength);
                });
                x.Destination("queue2");
            });

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel2()
    {
        Result result = await _services.GetService<IBrokerFactory>()
            .CreateShovel(x => x.UsingCredentials("guest", "guest"),"test-shovel2", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.Source("queue1", ShovelProtocol.Amqp091, c =>
                {
                    c.DeleteAfter(ShovelDeleteMode.QueueLength);
                });
                x.Destination("queue2");
            });

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel3()
    {
        Result result = await _services.GetService<IBrokerFactory>()
            .CreateShovel(x => x.UsingCredentials("guest", "guest"),"test-shovel6", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.Source("queue1", ShovelProtocol.Amqp091, c =>
                {
                    c.DeleteAfter(5);
                });
                x.Destination("queue2");
            });

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_delete_shovel1()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Shovel>(x => x.UsingCredentials("guest", "guest"))
            .Delete("test-shovel2","TestHareDu");

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_delete_shovel2()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .DeleteShovel(x => x.UsingCredentials("guest", "guest"), "test-shovel2","TestHareDu");

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_delete_all_shovels()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .DeleteAllShovels(x => x.UsingCredentials("guest", "guest"),"TestHareDu");

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_get_all_shovels1()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Shovel>(x => x.UsingCredentials("guest", "guest"))
            .GetAll(_vhost)
            .ScreenDump();

        Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
        
    [Test]
    public async Task Verify_can_get_all_shovels2()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .GetAllShovels(x => x.UsingCredentials("guest", "guest"), _vhost)
            .ScreenDump();

        Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_get_all_shovels3()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Shovel>(x => x.UsingCredentials("guest", "guest"))
            .GetAll()
            .ScreenDump();

        Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_get_all_shovels4()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .GetAllShovels(x => x.UsingCredentials("guest", "guest"))
            .ScreenDump();

        Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_get_shovels1()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Shovel>(x => x.UsingCredentials("guest", "guest"))
            .Get(_shovelName, _vhost)
            .ScreenDump();

        Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
}