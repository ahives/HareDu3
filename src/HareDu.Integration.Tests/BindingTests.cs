namespace HareDu.Integration.Tests
{
    using System;
    using System.Threading.Tasks;
    using Core.Configuration;
    using Core.Extensions;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;

    [TestFixture]
    public class BindingTests
    {
        ServiceProvider _services;
        
        [OneTimeSetUp]
        public void Init()
        {
            _services = new ServiceCollection()
                .AddSingleton<IBrokerObjectFactory>(x => new BrokerObjectFactory(new HareDuConfig()
                {
                    Broker = new ()
                    {
                        Url = "http://localhost:15672",
                        Credentials = new (){Username = "guest", Password = "guest"}
                    }
                }))
                .BuildServiceProvider();
        }

        [Test]
        public async Task Should_be_able_to_get_all_bindings()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .GetAll()
                .ScreenDump();
            
            Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString());
        }

        [Test]
        public async Task Verify_can_add_arguments()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create(x =>
                {
                    x.Binding(b =>
                    {
                        b.Source("A1");
                        b.Destination("Queue2");
                        b.Type(BindingType.Queue);
                    });
                    x.Configure(c =>
                    {
                        c.HasRoutingKey("*.");
                        c.HasArguments(arg =>
                        {
                            arg.Set("arg1", "value1");
                        });
                    });
                    x.Targeting(t => t.VirtualHost("HareDu"));
                });
            
//            Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString());
        }

        [Test]
        public async Task Verify_can_delete_binding()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete(x =>
                {
                    x.Binding(b =>
                    {
                        b.Name("%2A.");
                        b.Source("E2");
                        b.Destination("Q4");
                        b.Type(BindingType.Queue);
                    });
                    x.Targeting(t => t.VirtualHost("HareDu"));
                });
            
//            Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString());
        }
    }
}