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
        public async Task Should_be_able_to_get_all_bindings1()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .GetAll()
                .ScreenDump();
            
            Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString());
        }

        [Test]
        public async Task Should_be_able_to_get_all_bindings2()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .GetAllBindings()
                .ScreenDump();
            
            Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString());
        }

        [Test]
        public async Task Verify_can_add_arguments()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create("queue1", "queue2", BindingType.Queue, "TestHareDu", x =>
                {
                    x.HasRoutingKey("*.");
                    x.HasArguments(arg =>
                    {
                        arg.Set("arg1", "value1");
                    });
                });
            
//            Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString());
        }

        [Test]
        public async Task Verify_can_delete_binding()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete("E2", "Q4", "%2A.","HareDu", x =>
                {
                    x.Configure(b =>
                    {
                        b.Type(BindingType.Queue);
                    });
                    // x.Targeting(t => t.VirtualHost());
                });
            
//            Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString());
        }
    }
}