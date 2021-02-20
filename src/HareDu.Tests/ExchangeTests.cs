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

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.HasFaulted);
                Assert.IsTrue(result.HasData);
                Assert.IsNotNull(result.Data);
                Assert.AreEqual(9, result.Data.Count);
                Assert.IsTrue(result.Data[1].Durable);
                Assert.IsFalse(result.Data[1].Internal);
                Assert.IsFalse(result.Data[1].AutoDelete);
                Assert.AreEqual("E2", result.Data[1].Name);
                Assert.AreEqual("direct", result.Data[1].RoutingType);
                Assert.AreEqual("HareDu", result.Data[1].VirtualHost);
                Assert.IsNotNull(result.Data[1].Arguments);
                Assert.AreEqual(1, result.Data[1].Arguments.Count);
                Assert.AreEqual(result.Data[1].Arguments["alternate-exchange"].ToString(), "exchange");
            });
        }

        [Test]
        public async Task Should_be_able_to_get_all_exchanges2()
        {
            var services = GetContainerBuilder("TestData/ExchangeInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .GetAllExchanges();
            
            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.HasFaulted);
                Assert.IsTrue(result.HasData);
                Assert.IsNotNull(result.Data);
                Assert.AreEqual(9, result.Data.Count);
                Assert.IsTrue(result.Data[1].Durable);
                Assert.IsFalse(result.Data[1].Internal);
                Assert.IsFalse(result.Data[1].AutoDelete);
                Assert.AreEqual("E2", result.Data[1].Name);
                Assert.AreEqual("direct", result.Data[1].RoutingType);
                Assert.AreEqual("HareDu", result.Data[1].VirtualHost);
                Assert.IsNotNull(result.Data[1].Arguments);
                Assert.AreEqual(1, result.Data[1].Arguments.Count);
                Assert.AreEqual(result.Data[1].Arguments["alternate-exchange"].ToString(), "exchange");
            });
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
                        arg.Add("fake_arg", "8238b");
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
                        arg.Add("fake_arg", "8238b");
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
                        arg.Add("fake_arg", "8238b");
                    });
                });
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
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
                        arg.Add("fake_arg", "8238b");
                    });
                });
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
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
                        arg.Add("fake_arg", "8238b");
                    });
                });
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
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
            result.DebugInfo.Errors.Count.ShouldBe(1);
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
            result.DebugInfo.Errors.Count.ShouldBe(1);
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
            result.DebugInfo.Errors.Count.ShouldBe(1);
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
            result.DebugInfo.Errors.Count.ShouldBe(2);
        }
    }
}