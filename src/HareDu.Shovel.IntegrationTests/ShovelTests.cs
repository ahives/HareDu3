namespace HareDu.Shovel.IntegrationTests;

using Core;
using Core.Serialization;
using DependencyInjection;
using Extensions;
using HareDu.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
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
    readonly IHareDuDeserializer _deserializer;

    public ShovelTests()
    {
        _deserializer = new ShovelDeserializer();
    }

    [OneTimeSetUp]
    public void Init()
    {
        _services = new ServiceCollection()
            .AddHareDu()
            .AddHareDuShovel()
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
        var result = await _services.GetService<IHareDuFactory>()
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
        var result = await _services.GetService<IHareDuFactory>()
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

        Console.WriteLine(_deserializer.ToJsonString(result));
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel2()
    {
        Result result = await _services.GetService<IHareDuFactory>()
            .CreateShovel(x => x.UsingCredentials("guest", "guest"),"test-shovel2", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.Source("queue1", ShovelProtocol.Amqp091, c =>
                {
                    c.DeleteAfter(ShovelDeleteMode.QueueLength);
                });
                x.Destination("queue2");
            });

        Console.WriteLine(_deserializer.ToJsonString(result));
    }

    [Test]
    public async Task Verify_can_create_dynamic_shovel3()
    {
        Result result = await _services.GetService<IHareDuFactory>()
            .CreateShovel(x => x.UsingCredentials("guest", "guest"),"test-shovel6", "TestHareDu", x =>
            {
                x.Uri("amqp://user1@localhost");
                x.Source("queue1", ShovelProtocol.Amqp091, c =>
                {
                    c.DeleteAfter(5);
                });
                x.Destination("queue2");
            });

        Console.WriteLine(_deserializer.ToJsonString(result));
    }

    [Test]
    public async Task Verify_can_delete_shovel1()
    {
        var result = await _services.GetService<IHareDuFactory>()
            .API<Shovel>(x => x.UsingCredentials("guest", "guest"))
            .Delete("test-shovel2","TestHareDu");

        Console.WriteLine(_deserializer.ToJsonString(result));
    }

    [Test]
    public async Task Verify_can_delete_shovel2()
    {
        var result = await _services.GetService<IHareDuFactory>()
            .DeleteShovel(x => x.UsingCredentials("guest", "guest"), "test-shovel2","TestHareDu");

        Console.WriteLine(_deserializer.ToJsonString(result));
    }

    [Test]
    public async Task Verify_can_delete_all_shovels()
    {
        var result = await _services.GetService<IHareDuFactory>()
            .DeleteAllShovels(x => x.UsingCredentials("guest", "guest"),"TestHareDu");

        Console.WriteLine(_deserializer.ToJsonString(result));
    }

    [Test]
    public async Task Verify_can_get_all_shovels1()
    {
        var result = await _services.GetService<IHareDuFactory>()
            .API<Shovel>(x => x.UsingCredentials("guest", "guest"))
            .GetAll(_vhost)
            .ScreenDump();

        Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(_deserializer.ToJsonString(result));
    }
        
    [Test]
    public async Task Verify_can_get_all_shovels2()
    {
        var result = await _services.GetService<IHareDuFactory>()
            .GetAllShovels(x => x.UsingCredentials("guest", "guest"), _vhost)
            .ScreenDump();

        Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(_deserializer.ToJsonString(result));
    }

    [Test]
    public async Task Verify_can_get_all_shovels3()
    {
        var result = await _services.GetService<IHareDuFactory>()
            .API<Shovel>(x => x.UsingCredentials("guest", "guest"))
            .GetAll()
            .ScreenDump();

        Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(_deserializer.ToJsonString(result));
    }

    [Test]
    public async Task Verify_can_get_all_shovels4()
    {
        var result = await _services.GetService<IHareDuFactory>()
            .GetAllShovels(x => x.UsingCredentials("guest", "guest"))
            .ScreenDump();

        Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(_deserializer.ToJsonString(result));
    }

    [Test]
    public async Task Verify_can_get_shovels1()
    {
        var result = await _services.GetService<IHareDuFactory>()
            .API<Shovel>(x => x.UsingCredentials("guest", "guest"))
            .Get(_shovelName, _vhost)
            .ScreenDump();

        Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(_deserializer.ToJsonString(result));
    }
}