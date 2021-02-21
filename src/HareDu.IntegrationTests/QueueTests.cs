namespace HareDu.IntegrationTests
{
    using System;
    using System.Threading.Tasks;
    using Core.Extensions;
    using Core.Serialization;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using MicrosoftIntegration;
    using NUnit.Framework;

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
                    x.When(condition =>
                    {
                        // condition.HasNoConsumers();
                        // condition.IsEmpty();
                    });
                });

//            Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString(Deserializer.Options));
        }

        [Test]
        public async Task Verify_can_peek_messages()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
                .Get("Queue1", "HareDu",x =>
                {
                    x.Take(5);
                    x.AckMode(RequeueMode.AckRequeue);
                    x.TruncateIfAbove(5000);
                    x.Encoding(MessageEncoding.Auto);
                });
            
//            Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString(Deserializer.Options));
        }

        [Test]
        public async Task Verify_can_empty_queue1()
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

        [Test]
        public async Task Verify_can_peek_messages2()
        {
            // var result = await _services.GetService<IBrokerObjectFactory>()
            //     .GetMessagesFromQueue("order-state", "TestOrders");
            var result = await _services.GetService<IBrokerObjectFactory>()
                .GetMessagesFromQueue("order-state", "TestOrders", x =>
                {
                    x.Take(2);
                    x.AckMode(RequeueMode.AckRequeue);
                    x.Encoding(MessageEncoding.Auto);
                    x.TruncateIfAbove(5000);
                });
            
            Console.WriteLine(result.ToJsonString(Deserializer.Options));
        }
    }
}