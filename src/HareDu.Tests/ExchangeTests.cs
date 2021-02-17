namespace HareDu.Tests
{
    using System.Threading.Tasks;
    using Core.Extensions;
    using Core.Serialization;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using Model;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class ExchangeTests :
        HareDuTesting
    {
        [Test]
        public async Task Should_be_able_to_get_all_exchanges1()
        {
            var services = GetContainerBuilder("TestData/ExchangeInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .GetAll();
            
            result.HasFaulted.ShouldBeFalse();
            result.HasData.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Count.ShouldBe(9);
            result.Data[1].Durable.ShouldBeTrue();
            result.Data[1].Internal.ShouldBeFalse();
            result.Data[1].AutoDelete.ShouldBeFalse();
            result.Data[1].Name.ShouldBe("E2");
            result.Data[1].RoutingType.ShouldBe("direct");
            result.Data[1].VirtualHost.ShouldBe("HareDu");
            result.Data[1].Arguments.ShouldNotBeNull();
            result.Data[1].Arguments.Count.ShouldBe(1);
            result.Data[1].Arguments["alternate-exchange"].ToString().ShouldBe("exchange");
        }

        [Test]
        public async Task Should_be_able_to_get_all_exchanges2()
        {
            var services = GetContainerBuilder("TestData/ExchangeInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .GetAllExchanges();
            
            result.HasFaulted.ShouldBeFalse();
            result.HasData.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Count.ShouldBe(9);
            result.Data[1].Durable.ShouldBeTrue();
            result.Data[1].Internal.ShouldBeFalse();
            result.Data[1].AutoDelete.ShouldBeFalse();
            result.Data[1].Name.ShouldBe("E2");
            result.Data[1].RoutingType.ShouldBe("direct");
            result.Data[1].VirtualHost.ShouldBe("HareDu");
            result.Data[1].Arguments.ShouldNotBeNull();
            result.Data[1].Arguments.Count.ShouldBe(1);
            result.Data[1].Arguments["alternate-exchange"].ToString().ShouldBe("exchange");
        }

        [Test]
        public async Task Verify_can_create_exchange1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Create("fake_exchange", "HareDu", x =>
                {
                    x.IsDurable();
                    x.IsForInternalUse();
                    x.HasRoutingType(ExchangeRoutingType.Fanout);
                    x.HasArguments(arg =>
                    {
                        arg.Set("fake_arg", "8238b");
                    });
                });
            
            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();
            
            ExchangeDefinition definition = result.DebugInfo.Request.ToObject<ExchangeDefinition>(Deserializer.Options);

            result.DebugInfo.URL.ShouldBe("api/exchanges/HareDu/fake_exchange");
            definition.RoutingType.ShouldBe("fanout");
            definition.Durable.ShouldBeTrue();
            definition.Internal.ShouldBeTrue();
            definition.AutoDelete.ShouldBeFalse();
            definition.Arguments.Count.ShouldBe(1);
            definition.Arguments["fake_arg"].ToString().ShouldBe("8238b");
        }

        [Test]
        public async Task Verify_can_create_exchange2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .CreateExchange("fake_exchange", "HareDu", x =>
                {
                    x.IsDurable();
                    x.IsForInternalUse();
                    x.HasRoutingType(ExchangeRoutingType.Fanout);
                    x.HasArguments(arg =>
                    {
                        arg.Set("fake_arg", "8238b");
                    });
                });
            
            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();
            
            ExchangeDefinition definition = result.DebugInfo.Request.ToObject<ExchangeDefinition>(Deserializer.Options);

            result.DebugInfo.URL.ShouldBe("api/exchanges/HareDu/fake_exchange");
            definition.RoutingType.ShouldBe("fanout");
            definition.Durable.ShouldBeTrue();
            definition.Internal.ShouldBeTrue();
            definition.AutoDelete.ShouldBeFalse();
            definition.Arguments.Count.ShouldBe(1);
            definition.Arguments["fake_arg"].ToString().ShouldBe("8238b");
        }

        [Test]
        public async Task Verify_cannot_create_exchange_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Create(string.Empty, "HareDu", x =>
                {
                    x.IsDurable();
                    x.IsForInternalUse();
                    x.HasRoutingType(ExchangeRoutingType.Fanout);
                    x.HasArguments(arg =>
                    {
                        arg.Set("fake_arg", "8238b");
                    });
                });
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            result.DebugInfo.URL.ShouldBe("api/exchanges/HareDu/");
        }

        [Test]
        public async Task Verify_cannot_create_exchange_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Create(string.Empty, "HareDu", x =>
                {
                    x.IsDurable();
                    x.IsForInternalUse();
                    x.HasRoutingType(ExchangeRoutingType.Fanout);
                    x.HasArguments(arg =>
                    {
                        arg.Set("fake_arg", "8238b");
                    });
                });
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            result.DebugInfo.URL.ShouldBe("api/exchanges/HareDu/");
        }

        [Test]
        public async Task Verify_cannot_create_exchange_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Create("fake_exchange", string.Empty, x =>
                {
                    x.IsDurable();
                    x.IsForInternalUse();
                    x.HasRoutingType(ExchangeRoutingType.Fanout);
                    x.HasArguments(arg =>
                    {
                        arg.Set("fake_arg", "8238b");
                    });
                });
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            result.DebugInfo.URL.ShouldBe("api/exchanges//fake_exchange");
        }

        [Test]
        public async Task Verify_can_delete_exchange1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Delete("E3", "HareDu", x =>
                {
                    x.When(condition => condition.Unused());
                });
            
            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public async Task Verify_can_delete_exchange2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .DeleteExchange("E3", "HareDu", x =>
                {
                    x.When(condition => condition.Unused());
                });
            
            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public async Task Verify_cannot_delete_exchange_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Delete(string.Empty, "HareDu", x =>
                {
                    x.When(condition => condition.Unused());
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_exchange_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Delete("E3", string.Empty, x =>
                {
                    x.When(condition => condition.Unused());
                });
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_exchange_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Delete("E3", string.Empty, x =>
                {
                    x.When(condition => condition.Unused());
                });
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_exchange_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Delete(string.Empty, string.Empty, x =>
                {
                    x.When(condition => condition.Unused());
                });
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }
    }
}