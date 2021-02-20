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
    public class GlobalParameterTests
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
        public async Task Should_be_able_to_get_all_global_parameters()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .GetAll()
                .ScreenDump();
        }
        
        [Test]
        public async Task Verify_can_create_parameter()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Create("fake_param2", x =>
                {
                    x.Value("fake_value");
                    // x.Arguments(arg =>
                    // {
                    //     arg.Set("arg1", "value1");
                    //     arg.Set("arg2", "value2");
                    // });
                });
             
            // Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString());
        }
        
        [Test]
        public async Task Verify_can_delete_parameter()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Delete("Fred");
            
            // Assert.IsFalse(result.HasFaulted);
            Console.WriteLine(result.ToJsonString());
        }
    }
}