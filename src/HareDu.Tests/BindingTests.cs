namespace HareDu.Tests
{
    using System.Threading.Tasks;
    using Core.Extensions;
    using Core.Serialization;
    using Microsoft.Extensions.DependencyInjection;
    using Model;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class BindingTests :
        HareDuTesting
    {
        [Test]
        public async Task Should_be_able_to_get_all_bindings()
        {
            var services = GetContainerBuilder("TestData/BindingInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .GetAll();

            result.HasData.ShouldBeTrue();
            result.Data.Count.ShouldBe(12);
            result.HasFaulted.ShouldBeFalse();
            result.Data.ShouldNotBeNull();
        }

        [Test]
        public async Task Verify_can_add_arguments()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create("E2", "Q1", BindingType.Exchange, "HareDu", x =>
                {
                    x.HasRoutingKey("*.");
                    x.HasArguments(arg => { arg.Add("arg1", "value1"); });
                });

            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();

            BindingDefinition definition = result.DebugInfo.Request.ToObject<BindingDefinition>(Deserializer.Options);

            definition.RoutingKey.ShouldBe("*.");
            definition.Arguments["arg1"].ToString().ShouldBe("value1");
        }

        [Test]
        public async Task Verify_cannot_add_arguments_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create(string.Empty, "Q1", BindingType.Exchange, "HareDu", x =>
                {
                    x.HasRoutingKey("*.");
                    x.HasArguments(arg => { arg.Add("arg1", "value1"); });
                });

            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_add_arguments_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create("E2", string.Empty, BindingType.Exchange, "HareDu", x =>
                {
                    x.HasRoutingKey("*.");
                    x.HasArguments(arg => { arg.Add("arg1", "value1"); });
                });

            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_add_arguments_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create(string.Empty, string.Empty, BindingType.Exchange, "HareDu", x =>
                {
                    x.HasRoutingKey("*.");
                    x.HasArguments(arg => { arg.Add("arg1", "value1"); });
                });

            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_cannot_add_arguments_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create("E2", "Q1", BindingType.Exchange, string.Empty, x =>
                {
                    x.HasRoutingKey("*.");
                    x.HasArguments(arg => { arg.Add("arg1", "value1"); });
                });

            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_add_arguments_5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create(string.Empty, string.Empty, BindingType.Exchange, string.Empty, x =>
                {
                    x.HasRoutingKey("*.");
                    x.HasArguments(arg => { arg.Add("arg1", "value1"); });
                });

            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(3);
        }

        [Test]
        public async Task Verify_cannot_add_arguments_6()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create("Q1", string.Empty, BindingType.Exchange, "HareDu", x =>
                {
                    x.HasRoutingKey("*.");
                    x.HasArguments(arg => { arg.Add("arg1", "value1"); });
                });

            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_add_arguments_7()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create("E2", string.Empty, BindingType.Exchange, "HareDu", x =>
                {
                    x.HasRoutingKey("*.");
                    x.HasArguments(arg => { arg.Add("arg1", "value1"); });
                });

            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_add_arguments_8()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create(string.Empty, string.Empty, BindingType.Exchange, "HareDu", x =>
                {
                    x.HasRoutingKey("*.");
                    x.HasArguments(arg => { arg.Add("arg1", "value1"); });
                });

            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_cannot_add_arguments_9()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create("E2", "Q1", BindingType.Exchange, "", x =>
                {
                    x.HasRoutingKey("*.");
                    x.HasArguments(arg => { arg.Add("arg1", "value1"); });
                    // x.Configure(c =>
                    // {
                    //     c.Source();
                    //     c.Destination();
                    //     c.Type(BindingType.Exchange);
                    //     c.HasRoutingKey("*.");
                    //     c.HasArguments(arg => { arg.Set("arg1", "value1"); });
                    // });
                });

            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_add_arguments_10()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create("", "", BindingType.Exchange, "", x =>
                {
                    x.HasRoutingKey("*.");
                    x.HasArguments(arg => { arg.Add("arg1", "value1"); });
                    // x.Configure(c =>
                    // {
                    //     c.Type(BindingType.Exchange);
                    //     c.HasRoutingKey("*.");
                    //     c.HasArguments(arg => { arg.Set("arg1", "value1"); });
                    // });
                });

            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(3);
        }

        // [Test]
        // public async Task Verify_can_delete_binding()
        // {
        //     var services = GetContainerBuilder().BuildServiceProvider();
        //     var result = await services.GetService<IBrokerObjectFactory>()
        //         .Object<Binding>()
        //         .Delete("E2", "Q4", string.Empty, "HareDu", BindingType.Queue);
        //         .Delete(x =>
        //         {
        //             x.Configure(b =>
        //             {
        //                 b.Name("Binding1");
        //                 b.Source("E2");
        //                 b.Destination("Q4");
        //                 b.Type(BindingType.Queue);
        //             });
        //             x.Targeting(t => t.VirtualHost("HareDu"));
        //         })
        //         .ConfigureAwait(false);
        //     
        //     result.HasFaulted.ShouldBeFalse();
        //     result.DebugInfo.ShouldNotBeNull();
        //     result.DebugInfo.URL.ShouldBe("api/bindings/HareDu/e/E2/q/Q4/Binding1");
        // }

        [Test]
        public async Task Verify_cannot_delete_binding_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete("E2", "Q4", string.Empty, "HareDu", BindingType.Queue);
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_binding_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete("E2", string.Empty, string.Empty, "HareDu", BindingType.Queue);
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_binding_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete(string.Empty, string.Empty, string.Empty, "HareDu", BindingType.Queue);
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_cannot_delete_binding_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            // var result = await services.GetService<IBrokerObjectFactory>()
            //     .Object<Binding>()
            //     .Delete(x =>
            //     {
            //         x.Configure(b =>
            //         {
            //             b.Name(string.Empty);
            //             b.Source("E2");
            //             b.Destination(string.Empty);
            //             b.Type(BindingType.Queue);
            //         });
            //         x.Targeting(t => t.VirtualHost(string.Empty));
            //     })
            //     .ConfigureAwait(false);
            //
            // result.HasFaulted.ShouldBeTrue();
            // result.Errors.Count.ShouldBe(3);
        }

        [Test]
        public async Task Verify_cannot_delete_binding_5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            // var result = await services.GetService<IBrokerObjectFactory>()
            //     .Object<Binding>()
            //     .Delete(x =>
            //     {
            //         x.Configure(b =>
            //         {
            //             b.Name(string.Empty);
            //             b.Source("E2");
            //             b.Destination("Q4");
            //             b.Type(BindingType.Queue);
            //         });
            //         x.Targeting(t => t.VirtualHost(string.Empty));
            //     })
            //     .ConfigureAwait(false);
            //
            // result.HasFaulted.ShouldBeTrue();
            // result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_cannot_delete_binding_6()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            // var result = await services.GetService<IBrokerObjectFactory>()
            //     .Object<Binding>()
            //     .Delete(x =>
            //     {
            //         x.Configure(b =>
            //         {
            //             b.Name("Binding1");
            //             b.Source("E2");
            //             b.Destination(string.Empty);
            //             b.Type(BindingType.Queue);
            //         });
            //         x.Targeting(t => t.VirtualHost(string.Empty));
            //     })
            //     .ConfigureAwait(false);
            //
            // result.HasFaulted.ShouldBeTrue();
            // result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_cannot_delete_binding_7()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            // var result = await services.GetService<IBrokerObjectFactory>()
            //     .Object<Binding>()
            //     .Delete(x =>
            //     {
            //         x.Configure(b =>
            //         {
            //             b.Source("E2");
            //             b.Destination("Q4");
            //             b.Type(BindingType.Queue);
            //         });
            //         x.Targeting(t => t.VirtualHost("HareDu"));
            //     })
            //     .ConfigureAwait(false);
            //
            // result.HasFaulted.ShouldBeTrue();
            // result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_binding_8()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            // var result = await services.GetService<IBrokerObjectFactory>()
            //     .Object<Binding>()
            //     .Delete(x => { x.Targeting(t => t.VirtualHost("HareDu")); })
            //     .ConfigureAwait(false);
            //
            // result.HasFaulted.ShouldBeTrue();
            // result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_cannot_delete_binding_9()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            // var result = await services.GetService<IBrokerObjectFactory>()
            //     .Object<Binding>()
            //     .Delete(x => { })
            //     .ConfigureAwait(false);
            //
            // result.HasFaulted.ShouldBeTrue();
            // result.Errors.Count.ShouldBe(3);
        }
    }
}