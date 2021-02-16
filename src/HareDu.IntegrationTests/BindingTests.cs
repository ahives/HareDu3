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
    public class BindingTests
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
                    x.Configure(c =>
                    {
                        c.Source("A1");
                        c.Destination("Queue2");
                        c.Type(BindingType.Queue);
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
                    x.Configure(b =>
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