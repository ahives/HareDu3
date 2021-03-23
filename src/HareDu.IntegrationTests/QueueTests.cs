namespace HareDu.IntegrationTests
{
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
        public async Task Verify_can_create_queue()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
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
            
            Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString(Deserializer.Options));
        }

        [Test]
        public async Task Should_be_able_to_get_all_queues()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .GetAll()
                .ScreenDump();

            Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString(Deserializer.Options));
        }

        [Test]
        public async Task Verify_can_get_all_json()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .GetAll();
            
            Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString(Deserializer.Options));
        }

        [Test]
        public async Task Verify_can_delete_queue()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
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
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Sync("order-state", "TestOrders", QueueSyncAction.Sync);
            
            // Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString(Deserializer.Options));
        }

        [Test]
        public async Task Verify_can_sync_queue1()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Empty("", "HareDu");
            
            Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString(Deserializer.Options));
        }

        [Test]
        public async Task Verify_can_empty_queue2()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .EmptyQueue("", "HareDu");
            
            Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString(Deserializer.Options));
        }
    }
}