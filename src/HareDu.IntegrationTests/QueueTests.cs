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
    string _vhost = "QueueTestVirtulHost";
    string _node = "rabbit@192a30bbb161";
        
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

    [SetUp]
    public async Task SetUp()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
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
            .API<Queue>()
            .Create("TestQueue1", _vhost, _node, x =>
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
            
        Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_create_queue_without_configurator()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Queue>()
            .Create("TestQueue2", _vhost,_node);
            
        Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_cannot_create_queue()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Queue>()
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
            
        Assert.IsTrue(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Should_be_able_to_get_all_queues()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Queue>()
            .GetAll()
            .ScreenDump();

        Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Should_be_able_to_get_details_all_queues()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Queue>()
            .GetDetails()
            .ScreenDump();

        Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_get_all_json()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Queue>()
            .GetAll();
            
        Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_delete_queue()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Queue>()
            .Delete("TestQueue10", "HareDu",x =>
            {
                x.WhenHasNoConsumers();
            });

//            Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_sync_queue()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Queue>()
            .Sync("order-state", "TestOrders", QueueSyncAction.Sync);
            
        // Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_sync_queue1()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<Queue>()
            .Empty("", "HareDu");
            
        Assert.IsFalse(result.HasFaulted);
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Verify_can_empty_queue2()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .EmptyQueue("", "HareDu");
            
        Assert.IsFalse(result.HasFaulted);
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
            .API<VirtualHost>()
            .Create(vhost);
        var result2 = await _services
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Create(queue, vhost, "rabbit@6089ab1a7b81", x =>
            {
                x.IsDurable();
            });
        var result3 = await _services
            .GetService<IBrokerFactory>()
            .API<Exchange>()
            .Create(exchange, vhost);
        var result = await _services
            .GetService<IBrokerFactory>()
            .API<Queue>()
            .Bind(vhost, exchange, x =>
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