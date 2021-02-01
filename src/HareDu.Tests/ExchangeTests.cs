namespace HareDu.Tests
{
    using Core.Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using Model;
    using NUnit.Framework;
    using Registration;
    using Shouldly;

    [TestFixture]
    public class ExchangeTests :
        HareDuTesting
    {
        [Test]
        public void Should_be_able_to_get_all_exchanges()
        {
            var services = GetContainerBuilder("TestData/ExchangeInfo.json").BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .GetAll()
                .GetResult();
            
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
            result.Data[1].Arguments["alternate-exchange"].ShouldBe("exchange");
        }

        [Test]
        public void Verify_can_create_exchange()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Create(x =>
                {
                    x.Exchange("fake_exchange");
                    x.Configure(c =>
                    {
                        c.IsDurable();
                        c.IsForInternalUse();
                        c.HasRoutingType(ExchangeRoutingType.Fanout);
                        c.HasArguments(arg =>
                        {
                            arg.Set("fake_arg", "8238b");
                        });
                    });
                    x.Targeting(t => t.VirtualHost("HareDu"));
                })
                .GetResult();
            
            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();
            
            ExchangeDefinition definition = result.DebugInfo.Request.ToObject<ExchangeDefinition>();

            result.DebugInfo.URL.ShouldBe("api/exchanges/HareDu/fake_exchange");
            definition.RoutingType.ShouldBe("fanout");
            definition.Durable.ShouldBeTrue();
            definition.Internal.ShouldBeTrue();
            definition.AutoDelete.ShouldBeFalse();
            definition.Arguments.Count.ShouldBe(1);
            definition.Arguments["fake_arg"].ShouldBe("8238b");
        }

        [Test]
        public void Verify_cannot_create_exchange_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Create(x =>
                {
                    x.Exchange(string.Empty);
                    x.Configure(c =>
                    {
                        c.IsDurable();
                        c.IsForInternalUse();
                        c.HasRoutingType(ExchangeRoutingType.Fanout);
                        c.HasArguments(arg =>
                        {
                            arg.Set("fake_arg", "8238b");
                        });
                    });
                    x.Targeting(t => t.VirtualHost("HareDu"));
                })
                .GetResult();
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            result.DebugInfo.URL.ShouldBe("api/exchanges/HareDu/");
        }

        [Test]
        public void Verify_cannot_create_exchange_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Create(x =>
                {
                    x.Configure(c =>
                    {
                        c.IsDurable();
                        c.IsForInternalUse();
                        c.HasRoutingType(ExchangeRoutingType.Fanout);
                        c.HasArguments(arg =>
                        {
                            arg.Set("fake_arg", "8238b");
                        });
                    });
                    x.Targeting(t => t.VirtualHost("HareDu"));
                })
                .GetResult();
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            result.DebugInfo.URL.ShouldBe("api/exchanges/HareDu/");
        }

        [Test]
        public void Verify_cannot_create_exchange_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Create(x =>
                {
                    x.Exchange("fake_exchange");
                    x.Configure(c =>
                    {
                        c.IsDurable();
                        c.IsForInternalUse();
                        c.HasRoutingType(ExchangeRoutingType.Fanout);
                        c.HasArguments(arg =>
                        {
                            arg.Set("fake_arg", "8238b");
                        });
                    });
                    x.Targeting(t => t.VirtualHost(string.Empty));
                })
                .GetResult();
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            result.DebugInfo.URL.ShouldBe("api/exchanges//fake_exchange");
        }

        [Test]
        public void Verify_cannot_create_exchange_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Create(x =>
                {
                })
                .GetResult();
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(3);
            result.DebugInfo.ShouldNotBeNull();
            result.DebugInfo.URL.ShouldBe("api/exchanges//");
        }

        [Test]
        public void Verify_can_delete_exchange()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Delete(x =>
                {
                    x.Exchange("E3");
                    x.Targeting(t => t.VirtualHost("HareDu"));
                    x.When(c => c.Unused());
                })
                .GetResult();
            
            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public void Verify_cannot_delete_exchange_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Delete(x =>
                {
                    x.Exchange(string.Empty);
                    x.Targeting(t => t.VirtualHost("HareDu"));
                    x.When(c => c.Unused());
                })
                .GetResult();
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public void Verify_cannot_delete_exchange_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Delete(x =>
                {
                    x.Exchange("E3");
                    x.Targeting(t => t.VirtualHost(string.Empty));
                    x.When(c => c.Unused());
                })
                .GetResult();
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public void Verify_cannot_delete_exchange_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Delete(x =>
                {
                    x.Exchange("E3");
                    x.Targeting(t => {});
                    x.When(c => c.Unused());
                })
                .GetResult();
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public void Verify_cannot_delete_exchange_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Delete(x =>
                {
                    x.Exchange(string.Empty);
                    x.Targeting(t => t.VirtualHost(string.Empty));
                    x.When(c => c.Unused());
                })
                .GetResult();
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public void Verify_cannot_delete_exchange_5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Delete(x =>
                {
                    x.Exchange(string.Empty);
                    x.Targeting(t => {});
                    x.When(c => c.Unused());
                })
                .GetResult();
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public void Verify_cannot_delete_exchange_6()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Delete(x =>
                {
                    x.When(c => c.Unused());
                })
                .GetResult();
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }
    }
}