namespace HareDu.IntegrationTests
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using MicrosoftIntegration;
    using NUnit.Framework;

    [TestFixture]
    public class ShovelTests
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
        public async Task Verify_can_create_dynamic_shovel1()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Shovel>()
                .Create("test-shovel1", "TestHareDu", x =>
                {
                    x.Source("queue1", "amqp://user1@localhost", c =>
                    {
                        c.DeleteAfter(DeleteShovelAfterMode.QueueLength);
                    });
                    x.Destination("queue2", "amqp://user1@localhost");
                });

            Console.WriteLine(result.ToJsonString());
        }

        [Test]
        public async Task Verify_can_create_dynamic_shovel2()
        {
            Result result = await _services.GetService<IBrokerObjectFactory>()
                .CreateShovel("test-shovel2", "TestHareDu", x =>
                {
                    x.Source("queue1", "amqp://user1@localhost", c =>
                    {
                        c.DeleteAfter(DeleteShovelAfterMode.QueueLength);
                    });
                    x.Destination("queue2", "amqp://user1@localhost");
                });

            Console.WriteLine(result.ToJsonString());
        }

        [Test]
        public async Task Verify_can_create_dynamic_shovel3()
        {
            Result result = await _services.GetService<IBrokerObjectFactory>()
                .CreateShovel("test-shovel5", "TestHareDu", x =>
                {
                    x.Source("queue1", "amqp://user1@localhost", c =>
                    {
                        c.DeleteAfter(5);
                    });
                    x.Destination("queue2", "amqp://user1@localhost");
                });

            Console.WriteLine(result.ToJsonString());
        }

        [Test]
        public async Task Verify_can_delete_shovel1()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Shovel>()
                .Delete("test-shovel2","TestHareDu");

            Console.WriteLine(result.ToJsonString());
        }

        [Test]
        public async Task Verify_can_delete_shovel2()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .DeleteShovel("test-shovel2","TestHareDu");

            Console.WriteLine(result.ToJsonString());
        }
        
        [Test]
        public async Task Verify_can_get_all_shovels1()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Shovel>()
                .GetAll()
                .ScreenDump();
            
            Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString());
        }
        
        [Test]
        public async Task Verify_can_get_all_shovels2()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .GetAllShovels()
                .ScreenDump();
            
            Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString());
        }
    }
}