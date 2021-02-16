namespace HareDu.IntegrationTests
{
    using System;
    using System.Threading.Tasks;
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
                .AddHareDu()
                .BuildServiceProvider();
        }

        [Test]
        public async Task Verify_can_create_dynamic_shovel()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Shovel>()
                .Create(x =>
                {
                    x.Shovel("test-shovel2");
                    x.Configure(c =>
                    {
                        c.Source(s =>
                        {
                            s.Protocol(Protocol.Amqp091);
                            s.Uri("amqp://user1@localhost");
                            s.Queue("queue1");
                        });
                        c.Destination(s =>
                        {
                            s.Protocol(Protocol.Amqp091);
                            s.Uri("amqp://user1@localhost");
                            s.Queue("queue2");
                        });
                    });
                    x.Targeting(t =>
                    {
                        t.VirtualHost("TestHareDu");
                    });
                })
                .ConfigureAwait(false);

            Console.WriteLine(result.ToJsonString());
        }

        [Test]
        public async Task Verify_can_delete_shovel()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Shovel>()
                .Delete(x =>
                {
                    x.Shovel("test-shovel2");
                    x.Targeting(t =>
                    {
                        t.VirtualHost("TestHareDu");
                    });
                })
                .ConfigureAwait(false);

            Console.WriteLine(result.ToJsonString());
        }
        
        [Test]
        public async Task Verify_can_get_all_shovels()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Shovel>()
                .GetAll()
                .ScreenDump();
            
            Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString());
        }
    }
}