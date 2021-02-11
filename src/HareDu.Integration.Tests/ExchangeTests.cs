namespace HareDu.Integration.Tests
{
    using System;
    using System.Threading.Tasks;
    using Core.Configuration;
    using Core.Extensions;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using MicrosoftIntegration;
    using NUnit.Framework;

    [TestFixture]
    public class ExchangeTests
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
        public async Task Should_be_able_to_get_all_exchanges()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .GetAll()
                .ScreenDump();

            // result.HasFaulted.ShouldBeFalse();
            Console.WriteLine(result.ToJsonString());
        }

        [Test]
        public async Task Should_be_able_to_get_all_exchanges_2()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .GetAll();

            foreach (var exchange in result.Select(x => x.Data))
            {
                Console.WriteLine("Name: {0}", exchange.Name);
                Console.WriteLine("VirtualHost: {0}", exchange.VirtualHost);
                Console.WriteLine("AutoDelete: {0}", exchange.AutoDelete);
                Console.WriteLine("Internal: {0}", exchange.Internal);
                Console.WriteLine("Durable: {0}", exchange.Durable);
                Console.WriteLine("RoutingType: {0}", exchange.RoutingType);
                Console.WriteLine("****************************************************");
                Console.WriteLine();
            }
            
            // result.HasFaulted.ShouldBeFalse();
            Console.WriteLine(result.ToJsonString());
        }

        [Test]
        public async Task Should_be_able_to_get_all_exchanges_3()
        {
            var provider = new HareDuConfigProvider();
            var config = provider.Configure(x => { });
            var factory = new BrokerObjectFactory(config);
            
            var result = await factory
                .Object<Exchange>()
                .GetAll()
                .ScreenDump();
            
            // result.HasFaulted.ShouldBeFalse();
            Console.WriteLine(result.ToJsonString());
        }

        [Test]
        public async Task Verify_can_filter_exchanges()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .GetAll();

            result
                .Where(x => x.Name == "amq.fanout")
                .ScreenDump();
            
            // Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString());
        }

        [Test]
        public async Task Verify_can_create_exchange()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Create(x =>
                {
                    x.Exchange("A2");
                    x.Configure(c =>
                    {
                        c.IsDurable();
                        c.IsForInternalUse();
                        c.HasRoutingType(ExchangeRoutingType.Fanout);
                        c.HasArguments(arg =>
                        {
                            arg.Set("arg1", "blah");
                        });
                    });
                    x.Targeting(t => t.VirtualHost("HareDu"));
                });
            
            // Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString());
        }

        [Test]
        public async Task Verify_can_delete_exchange()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Delete(x =>
                {
                    x.Exchange("E3");
                    x.Targeting(t => t.VirtualHost("HareDu"));
                    x.When(c => c.Unused());
                });
            
//            Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString());
        }
    }
}