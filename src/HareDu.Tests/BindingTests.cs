namespace HareDu.Tests
{
    using Core.Extensions;
    using Core.Serialization;
    using Microsoft.Extensions.DependencyInjection;
    using Model;
    using NUnit.Framework;
    using Registration;
    using Shouldly;

    [TestFixture]
    public class BindingTests :
        HareDuTesting
    {
        [Test]
        public void Should_be_able_to_get_all_bindings()
        {
            var services = GetContainerBuilder("TestData/BindingInfo.json").BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .GetAll()
                .GetResult();

            result.HasData.ShouldBeTrue();
            result.Data.Count.ShouldBe(12);
            result.HasFaulted.ShouldBeFalse();
            result.Data.ShouldNotBeNull();
        }

        [Test]
        public void Verify_can_add_arguments()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create(x =>
                {
                    x.Binding(b =>
                    {
                        b.Source("E2");
                        b.Destination("Q1");
                        b.Type(BindingType.Exchange);
                    });
                    x.Configure(c =>
                    {
                        c.HasRoutingKey("*.");
                        c.HasArguments(arg => { arg.Set("arg1", "value1"); });
                    });
                    x.Targeting(t => t.VirtualHost("HareDu"));
                })
                .GetResult();

            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();

            BindingDefinition definition = result.DebugInfo.Request.ToObject<BindingDefinition>(Deserializer.Options);

            definition.RoutingKey.ShouldBe("*.");
            definition.Arguments["arg1"].ShouldBe("value1");
        }

        [Test]
        public void Verify_cannot_add_arguments_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create(x =>
                {
                    x.Binding(b =>
                    {
                        b.Source(string.Empty);
                        b.Destination("Q1");
                        b.Type(BindingType.Exchange);
                    });
                    x.Configure(c =>
                    {
                        c.HasRoutingKey("*.");
                        c.HasArguments(arg => { arg.Set("arg1", "value1"); });
                    });
                    x.Targeting(t => t.VirtualHost("HareDu"));
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public void Verify_cannot_add_arguments_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create(x =>
                {
                    x.Binding(b =>
                    {
                        b.Source("E2");
                        b.Destination(string.Empty);
                        b.Type(BindingType.Exchange);
                    });
                    x.Configure(c =>
                    {
                        c.HasRoutingKey("*.");
                        c.HasArguments(arg => { arg.Set("arg1", "value1"); });
                    });
                    x.Targeting(t => t.VirtualHost("HareDu"));
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public void Verify_cannot_add_arguments_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create(x =>
                {
                    x.Binding(b =>
                    {
                        b.Source(string.Empty);
                        b.Destination(string.Empty);
                        b.Type(BindingType.Exchange);
                    });
                    x.Configure(c =>
                    {
                        c.HasRoutingKey("*.");
                        c.HasArguments(arg => { arg.Set("arg1", "value1"); });
                    });
                    x.Targeting(t => t.VirtualHost("HareDu"));
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public void Verify_cannot_add_arguments_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create(x =>
                {
                    x.Binding(b =>
                    {
                        b.Source("E2");
                        b.Destination("Q1");
                        b.Type(BindingType.Exchange);
                    });
                    x.Configure(c =>
                    {
                        c.HasRoutingKey("*.");
                        c.HasArguments(arg => { arg.Set("arg1", "value1"); });
                    });
                    x.Targeting(t => t.VirtualHost(string.Empty));
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public void Verify_cannot_add_arguments_5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create(x =>
                {
                    x.Binding(b =>
                    {
                        b.Source(string.Empty);
                        b.Destination(string.Empty);
                        b.Type(BindingType.Exchange);
                    });
                    x.Configure(c =>
                    {
                        c.HasRoutingKey("*.");
                        c.HasArguments(arg => { arg.Set("arg1", "value1"); });
                    });
                    x.Targeting(t => t.VirtualHost(string.Empty));
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(3);
        }

        [Test]
        public void Verify_cannot_add_arguments_6()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create(x =>
                {
                    x.Binding(b =>
                    {
                        b.Destination("Q1");
                        b.Type(BindingType.Exchange);
                    });
                    x.Configure(c =>
                    {
                        c.HasRoutingKey("*.");
                        c.HasArguments(arg => { arg.Set("arg1", "value1"); });
                    });
                    x.Targeting(t => t.VirtualHost("HareDu"));
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public void Verify_cannot_add_arguments_7()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create(x =>
                {
                    x.Binding(b =>
                    {
                        b.Source("E2");
                        b.Type(BindingType.Exchange);
                    });
                    x.Configure(c =>
                    {
                        c.HasRoutingKey("*.");
                        c.HasArguments(arg => { arg.Set("arg1", "value1"); });
                    });
                    x.Targeting(t => t.VirtualHost("HareDu"));
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public void Verify_cannot_add_arguments_8()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create(x =>
                {
                    x.Binding(b => { b.Type(BindingType.Exchange); });
                    x.Configure(c =>
                    {
                        c.HasRoutingKey("*.");
                        c.HasArguments(arg => { arg.Set("arg1", "value1"); });
                    });
                    x.Targeting(t => t.VirtualHost("HareDu"));
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public void Verify_cannot_add_arguments_9()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create(x =>
                {
                    x.Binding(b =>
                    {
                        b.Source("E2");
                        b.Destination("Q1");
                        b.Type(BindingType.Exchange);
                    });
                    x.Configure(c =>
                    {
                        c.HasRoutingKey("*.");
                        c.HasArguments(arg => { arg.Set("arg1", "value1"); });
                    });
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public void Verify_cannot_add_arguments_10()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create(x =>
                {
                    x.Binding(b => { b.Type(BindingType.Exchange); });
                    x.Configure(c =>
                    {
                        c.HasRoutingKey("*.");
                        c.HasArguments(arg => { arg.Set("arg1", "value1"); });
                    });
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(3);
        }

        [Test]
        public void Verify_can_delete_binding()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete(x =>
                {
                    x.Binding(b =>
                    {
                        b.Name("Binding1");
                        b.Source("E2");
                        b.Destination("Q4");
                        b.Type(BindingType.Queue);
                    });
                    x.Targeting(t => t.VirtualHost("HareDu"));
                })
                .GetResult();

            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();
            result.DebugInfo.URL.ShouldBe("api/bindings/HareDu/e/E2/q/Q4/Binding1");
        }

        [Test]
        public void Verify_cannot_delete_binding_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete(x =>
                {
                    x.Binding(b =>
                    {
                        b.Name(string.Empty);
                        b.Source("E2");
                        b.Destination("Q4");
                        b.Type(BindingType.Queue);
                    });
                    x.Targeting(t => t.VirtualHost("HareDu"));
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public void Verify_cannot_delete_binding_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete(x =>
                {
                    x.Binding(b =>
                    {
                        b.Name("Binding1");
                        b.Source("E2");
                        b.Destination(string.Empty);
                        b.Type(BindingType.Queue);
                    });
                    x.Targeting(t => t.VirtualHost("HareDu"));
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public void Verify_cannot_delete_binding_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete(x =>
                {
                    x.Binding(b =>
                    {
                        b.Name(string.Empty);
                        b.Source("E2");
                        b.Destination(string.Empty);
                        b.Type(BindingType.Queue);
                    });
                    x.Targeting(t => t.VirtualHost("HareDu"));
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public void Verify_cannot_delete_binding_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete(x =>
                {
                    x.Binding(b =>
                    {
                        b.Name(string.Empty);
                        b.Source("E2");
                        b.Destination(string.Empty);
                        b.Type(BindingType.Queue);
                    });
                    x.Targeting(t => t.VirtualHost(string.Empty));
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(3);
        }

        [Test]
        public void Verify_cannot_delete_binding_5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete(x =>
                {
                    x.Binding(b =>
                    {
                        b.Name(string.Empty);
                        b.Source("E2");
                        b.Destination("Q4");
                        b.Type(BindingType.Queue);
                    });
                    x.Targeting(t => t.VirtualHost(string.Empty));
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public void Verify_cannot_delete_binding_6()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete(x =>
                {
                    x.Binding(b =>
                    {
                        b.Name("Binding1");
                        b.Source("E2");
                        b.Destination(string.Empty);
                        b.Type(BindingType.Queue);
                    });
                    x.Targeting(t => t.VirtualHost(string.Empty));
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public void Verify_cannot_delete_binding_7()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete(x =>
                {
                    x.Binding(b =>
                    {
                        b.Source("E2");
                        b.Destination("Q4");
                        b.Type(BindingType.Queue);
                    });
                    x.Targeting(t => t.VirtualHost("HareDu"));
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public void Verify_cannot_delete_binding_8()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete(x => { x.Targeting(t => t.VirtualHost("HareDu")); })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public void Verify_cannot_delete_binding_9()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete(x => { })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(3);
        }
    }
}