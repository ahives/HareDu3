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
    public class OperatorPolicyTests
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
        public async Task Should_be_able_to_get_all_policies()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<OperatorPolicy>()
                .GetAll()
                .ScreenDump();
        }

        [Test]
        public async Task Verify_can_create_operator_policy()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<OperatorPolicy>()
                .Create("test7", ".*", "TestHareDu", x =>
                {
                    x.SetMaxInMemoryBytes(9803129);
                    x.SetMaxInMemoryLength(283);
                    x.SetDeliveryLimit(5);
                    x.SetExpiry(5000);
                    x.SetMessageMaxSize(189173219);
                }, OperatorPolicyAppliedTo.Queues, 0);
            
            Console.WriteLine(result.ToJsonString(Deserializer.Options));
        }

        [Test]
        public async Task Test()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<OperatorPolicy>()
                .Delete("test6", "TestHareDu");
            
            Console.WriteLine(result.ToJsonString(Deserializer.Options));
        }
    }
}