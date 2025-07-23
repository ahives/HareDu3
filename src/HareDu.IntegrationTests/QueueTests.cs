namespace HareDu.IntegrationTests;

using System;
using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using NUnit.Framework;
using Serialization;

[TestFixture]
public class QueueTests
{
    ServiceProvider _services;
    string _vhost = "QueueTestVirtulHost1";
    string _node = "rabbit@b91edc210b0d";
        
    [OneTimeSetUp]
    public void Init()
    {
        _services = new ServiceCollection()
            .AddHareDu()
            .BuildServiceProvider();
    }

    [SetUp]
    public async Task SetUp()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .Create(_vhost);
    }

    [TearDown]
    public async Task TearDown()
    {
        // var result = await _services.GetService<IBrokerFactory>()
        //     .API<VirtualHost>()
        //     .Delete(_vhost);
    }

    [Test]
    public async Task Verify_can_create_queue()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Create("TestQueue1", _vhost, _node, x =>
            {
                x.IsDurable();
                // x.HasArguments(arg =>
                // {
                //     arg.SetQueueExpiration(1000);
                //     arg.SetAlternateExchange("your_alternate_exchange_name");
                //     arg.SetDeadLetterExchange("your_deadletter_exchange_name");
                //     arg.SetPerQueuedMessageExpiration(1000);
                //     arg.SetDeadLetterExchangeRoutingKey("your_routing_key");
                // });
            });
            
        Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_issue_2_fix()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Create("Issue2", "/", _node, x =>
            {
                x.IsDurable();
            });
            
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
        Assert.That(result.HasFaulted, Is.False);
    }

    [Test]
    public async Task Verify_can_create_queue_without_configurator()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Create("TestQueue2", _vhost,_node);
            
        Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_cannot_create_queue()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Create("TestQueue31", "HareDu",null, x =>
            {
                x.IsDurable();
                x.HasArguments(arg =>
                {
                    arg.SetQueueExpiration(1000);
                    arg.SetAlternateExchange("your_alternate_exchange_name");
                    arg.SetDeadLetterExchange("your_deadletter_exchange_name");
                    arg.SetPerQueuedMessageExpiration(1000);
                    arg.SetDeadLetterExchangeRoutingKey("your_routing_key");
                });
            });
            
        Assert.That(result.HasFaulted, Is.True);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Should_be_able_to_get_all_queues()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .GetAll()
            .ScreenDump();

        Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Should_be_able_to_get_details_all_queues()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .GetDetails()
            .ScreenDump();

        Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_get_all_json()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .GetAll();
            
        Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_delete_queue()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Delete("TestQueue10", "HareDu",x =>
            {
                x.WhenHasNoConsumers();
            });

        // Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_sync_queue()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Sync("order-state", "TestOrders");
            
        // Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_sync_queue1()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Empty("", "HareDu");
            
        Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_empty_queue2()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .EmptyQueue(x => x.UsingCredentials("guest", "guest"), "", "HareDu");
            
        Assert.That(result.HasFaulted, Is.False);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_create_exchange_binding_without_arguments1()
    {
        string vhost = "vhost";
        string queue = "queue";
        string exchange = "exchange";
        var result1 = await _services
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .Create(vhost);
        var result2 = await _services
            .GetService<IBrokerFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Create(queue, vhost, "rabbit@6089ab1a7b81", x =>
            {
                x.IsDurable();
            });
        var result3 = await _services
            .GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Create(exchange, vhost);
        var result = await _services
            .GetService<IBrokerFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .BindToQueue(vhost, exchange, x =>
            {
                x.Destination(queue);
            });

        // Assert.Multiple(() =>
        // {
        //     Assert.IsFalse(result.HasFaulted);
        //     Assert.IsNotNull(result.DebugInfo);
        //     Assert.IsNotNull(result.DebugInfo.Request);
        //
        //     BindingRequest request = result.DebugInfo.Request.ToObject<BindingRequest>();
        //         
        //     Assert.That(request.BindingKey, Is.Empty.Or.Null);
        //     Assert.IsNull(request.Arguments);
        // });
    }
}