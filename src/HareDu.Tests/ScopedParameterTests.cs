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
    public class ScopedParameterTests :
        HareDuTesting
    {
        [Test]
        public async Task Should_be_able_to_get_all_scoped_parameters()
        {
            var services = GetContainerBuilder("TestData/ScopedParameterInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .GetAll()
                .ConfigureAwait(false);

            result.HasFaulted.ShouldBeFalse();
            result.HasData.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Count.ShouldBe(3);
            result.Data[0].ShouldNotBeNull();
            result.Data[0].Value.Count.ShouldBe(2);
            result.Data[0].Value["max-connections"].Cast<long>().ShouldBe(10);
            result.Data[0].Value["max-queues"].ToString().ShouldBe("value");
        }
        
        [Test]
        public async Task Verify_can_create_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Create<long>(x =>
                {
                    x.Parameter("fake_parameter", 89);
                    x.Targeting(t =>
                    {
                        t.Component("fake_component");
                        t.VirtualHost("HareDu");
                    });
                })
                .ConfigureAwait(false);

            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();
            
            ScopedParameterDefinition<long> definition = result.DebugInfo.Request.ToObject<ScopedParameterDefinition<long>>(Deserializer.Options);
            
            definition.Component.ShouldBe("fake_component");
            definition.ParameterName.ShouldBe("fake_parameter");
            definition.ParameterValue.ShouldBe(89);
            definition.VirtualHost.ShouldBe("HareDu");
        }
        
        [Test]
        public async Task Verify_can_create_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Create<string>(x =>
                {
                    x.Parameter("fake_parameter", "value");
                    x.Targeting(t =>
                    {
                        t.Component("fake_component");
                        t.VirtualHost("HareDu");
                    });
                })
                .ConfigureAwait(false);

            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();
            
            ScopedParameterDefinition<string> definition = result.DebugInfo.Request.ToObject<ScopedParameterDefinition<string>>(Deserializer.Options);
            
            definition.Component.ShouldBe("fake_component");
            definition.ParameterName.ShouldBe("fake_parameter");
            definition.ParameterValue.ShouldBe("value");
            definition.VirtualHost.ShouldBe("HareDu");
        }
        
        [Test]
        public async Task Verify_cannot_create_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Create<string>(x =>
                {
                    x.Parameter(string.Empty, "value");
                    x.Targeting(t =>
                    {
                        t.Component("fake_component");
                        t.VirtualHost("HareDu");
                    });
                })
                .ConfigureAwait(false);

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            ScopedParameterDefinition<string> definition = result.DebugInfo.Request.ToObject<ScopedParameterDefinition<string>>(Deserializer.Options);
            
            definition.Component.ShouldBe("fake_component");
            definition.ParameterName.ShouldBeNullOrEmpty();
            definition.ParameterValue.ShouldBe("value");
            definition.VirtualHost.ShouldBe("HareDu");
        }
        
        [Test]
        public async Task Verify_cannot_create_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Create<string>(x =>
                {
                    x.Targeting(t =>
                    {
                        t.Component("fake_component");
                        t.VirtualHost("HareDu");
                    });
                })
                .ConfigureAwait(false);

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            ScopedParameterDefinition<string> definition = result.DebugInfo.Request.ToObject<ScopedParameterDefinition<string>>(Deserializer.Options);
            
            definition.Component.ShouldBe("fake_component");
            definition.ParameterName.ShouldBeNullOrEmpty();
            definition.ParameterValue.ShouldBeNullOrEmpty();
            definition.VirtualHost.ShouldBe("HareDu");
        }
        
        [Test]
        public async Task Verify_cannot_create_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Create<string>(x =>
                {
                    x.Parameter("fake_parameter", "value");
                    x.Targeting(t =>
                    {
                        t.Component(string.Empty);
                        t.VirtualHost("HareDu");
                    });
                })
                .ConfigureAwait(false);

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            ScopedParameterDefinition<string> definition = result.DebugInfo.Request.ToObject<ScopedParameterDefinition<string>>(Deserializer.Options);
            
            definition.Component.ShouldBeNullOrEmpty();
            definition.ParameterName.ShouldBe("fake_parameter");
            definition.ParameterValue.ShouldBe("value");
            definition.VirtualHost.ShouldBe("HareDu");
        }
        
        [Test]
        public async Task Verify_cannot_create_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Create<string>(x =>
                {
                    x.Parameter("fake_parameter", "value");
                    x.Targeting(t =>
                    {
                        t.VirtualHost("HareDu");
                    });
                })
                .ConfigureAwait(false);

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            ScopedParameterDefinition<string> definition = result.DebugInfo.Request.ToObject<ScopedParameterDefinition<string>>(Deserializer.Options);
            
            definition.Component.ShouldBeNullOrEmpty();
            definition.ParameterName.ShouldBe("fake_parameter");
            definition.ParameterValue.ShouldBe("value");
            definition.VirtualHost.ShouldBe("HareDu");
        }
        
        [Test]
        public async Task Verify_cannot_create_5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Create<string>(x =>
                {
                    x.Parameter("fake_parameter", "value");
                    x.Targeting(t =>
                    {
                        t.Component("fake_component");
                        t.VirtualHost(string.Empty);
                    });
                })
                .ConfigureAwait(false);

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            ScopedParameterDefinition<string> definition = result.DebugInfo.Request.ToObject<ScopedParameterDefinition<string>>(Deserializer.Options);
            
            definition.Component.ShouldBe("fake_component");
            definition.ParameterName.ShouldBe("fake_parameter");
            definition.ParameterValue.ShouldBe("value");
            definition.VirtualHost.ShouldBeNullOrEmpty();
        }
        
        [Test]
        public async Task Verify_cannot_create_6()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Create<string>(x =>
                {
                    x.Parameter("fake_parameter", "value");
                    x.Targeting(t =>
                    {
                        t.Component("fake_component");
                    });
                })
                .ConfigureAwait(false);

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            ScopedParameterDefinition<string> definition = result.DebugInfo.Request.ToObject<ScopedParameterDefinition<string>>(Deserializer.Options);
            
            definition.Component.ShouldBe("fake_component");
            definition.ParameterName.ShouldBe("fake_parameter");
            definition.ParameterValue.ShouldBe("value");
            definition.VirtualHost.ShouldBeNullOrEmpty();
        }
        
        [Test]
        public async Task Verify_cannot_create_7()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Create<string>(x =>
                {
                    x.Parameter("fake_parameter", "value");
                    x.Targeting(t =>
                    {
                        t.Component("fake_component");
                        t.VirtualHost(string.Empty);
                    });
                })
                .ConfigureAwait(false);

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            ScopedParameterDefinition<string> definition = result.DebugInfo.Request.ToObject<ScopedParameterDefinition<string>>(Deserializer.Options);
            
            definition.Component.ShouldBe("fake_component");
            definition.ParameterName.ShouldBe("fake_parameter");
            definition.ParameterValue.ShouldBe("value");
            definition.VirtualHost.ShouldBeNullOrEmpty();
        }
        
        [Test]
        public async Task Verify_cannot_create_8()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Create<string>(x =>
                {
                    x.Parameter(string.Empty, "value");
                    x.Targeting(t =>
                    {
                        t.Component(string.Empty);
                        t.VirtualHost("HareDu");
                    });
                })
                .ConfigureAwait(false);

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
            result.DebugInfo.ShouldNotBeNull();
            
            ScopedParameterDefinition<string> definition = result.DebugInfo.Request.ToObject<ScopedParameterDefinition<string>>(Deserializer.Options);
            
            definition.Component.ShouldBeNullOrEmpty();
            definition.ParameterName.ShouldBeNullOrEmpty();
            definition.ParameterValue.ShouldBe("value");
            definition.VirtualHost.ShouldBe("HareDu");
        }
        
        [Test]
        public async Task Verify_cannot_create_9()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Create<string>(x =>
                {
                    x.Targeting(t =>
                    {
                        t.VirtualHost("HareDu");
                    });
                })
                .ConfigureAwait(false);

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
            result.DebugInfo.ShouldNotBeNull();
            
            ScopedParameterDefinition<string> definition = result.DebugInfo.Request.ToObject<ScopedParameterDefinition<string>>(Deserializer.Options);
            
            definition.Component.ShouldBeNullOrEmpty();
            definition.ParameterName.ShouldBeNullOrEmpty();
            definition.ParameterValue.ShouldBeNullOrEmpty();
            definition.VirtualHost.ShouldBe("HareDu");
        }
        
        [Test]
        public async Task Verify_cannot_create_10()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Create<string>(x =>
                {
                    x.Parameter("fake_parameter", "value");
                    x.Targeting(t =>
                    {
                    });
                })
                .ConfigureAwait(false);

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
            result.DebugInfo.ShouldNotBeNull();
            
            ScopedParameterDefinition<string> definition = result.DebugInfo.Request.ToObject<ScopedParameterDefinition<string>>(Deserializer.Options);
            
            definition.Component.ShouldBeNullOrEmpty();
            definition.ParameterName.ShouldBe("fake_parameter");
            definition.ParameterValue.ShouldBe("value");
            definition.VirtualHost.ShouldBeNullOrEmpty();
        }
        
        [Test]
        public async Task Verify_cannot_create_11()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Create<string>(x =>
                {
                    x.Parameter("fake_parameter", "value");
                })
                .ConfigureAwait(false);

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
            result.DebugInfo.ShouldNotBeNull();
            
            ScopedParameterDefinition<string> definition = result.DebugInfo.Request.ToObject<ScopedParameterDefinition<string>>(Deserializer.Options);
            
            definition.Component.ShouldBeNullOrEmpty();
            definition.ParameterName.ShouldBe("fake_parameter");
            definition.ParameterValue.ShouldBe("value");
            definition.VirtualHost.ShouldBeNullOrEmpty();
        }
        
        [Test]
        public async Task Verify_cannot_create_12()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Create<string>(x =>
                {
                })
                .ConfigureAwait(false);

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(3);
            result.DebugInfo.ShouldNotBeNull();
            
            ScopedParameterDefinition<string> definition = result.DebugInfo.Request.ToObject<ScopedParameterDefinition<string>>(Deserializer.Options);
            
            definition.Component.ShouldBeNullOrEmpty();
            definition.ParameterName.ShouldBeNullOrEmpty();
            definition.ParameterValue.ShouldBeNullOrEmpty();
            definition.VirtualHost.ShouldBeNullOrEmpty();
        }

        [Test]
        public async Task Verify_can_delete()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(x =>
                {
                    x.Parameter("fake_parameter");
                    x.Targeting(t =>
                    {
                        t.Component("fake_component");
                        t.VirtualHost("HareDu");
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public async Task Verify_cannot_delete_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(x =>
                {
                    x.Parameter(string.Empty);
                    x.Targeting(t =>
                    {
                        t.Component("fake_component");
                        t.VirtualHost("HareDu");
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(x =>
                {
                    x.Targeting(t =>
                    {
                        t.Component("fake_component");
                        t.VirtualHost("HareDu");
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(x =>
                {
                    x.Parameter("fake_parameter");
                    x.Targeting(t =>
                    {
                        t.Component(string.Empty);
                        t.VirtualHost("HareDu");
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(x =>
                {
                    x.Parameter("fake_parameter");
                    x.Targeting(t =>
                    {
                        t.VirtualHost("HareDu");
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(x =>
                {
                    x.Parameter("fake_parameter");
                    x.Targeting(t =>
                    {
                        t.Component("fake_component");
                        t.VirtualHost(string.Empty);
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_6()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(x =>
                {
                    x.Parameter("fake_parameter");
                    x.Targeting(t =>
                    {
                        t.Component("fake_component");
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_7()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(x =>
                {
                    x.Parameter(string.Empty);
                    x.Targeting(t =>
                    {
                        t.Component(string.Empty);
                        t.VirtualHost("HareDu");
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_cannot_delete_8()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(x =>
                {
                    x.Targeting(t =>
                    {
                        t.VirtualHost("HareDu");
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_cannot_delete_9()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(x =>
                {
                    x.Parameter(string.Empty);
                    x.Targeting(t =>
                    {
                        t.Component("fake_component");
                        t.VirtualHost(string.Empty);
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_cannot_delete_10()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(x =>
                {
                    x.Targeting(t =>
                    {
                        t.Component("fake_component");
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_cannot_delete_11()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(x =>
                {
                    x.Parameter(string.Empty);
                    x.Targeting(t =>
                    {
                        t.Component(string.Empty);
                        t.VirtualHost(string.Empty);
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(3);
        }

        [Test]
        public async Task Verify_cannot_delete_12()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(x =>
                {
                    x.Targeting(t =>
                    {
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(3);
        }

        [Test]
        public async Task Verify_cannot_delete_13()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(x =>
                {
                    x.Parameter(string.Empty);
                    x.Targeting(t =>
                    {
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(3);
        }

        [Test]
        public async Task Verify_cannot_delete_14()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(x =>
                {
                    x.Parameter(string.Empty);
                })
                .GetResult();
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(3);
        }

        [Test]
        public async Task Verify_cannot_delete_15()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(x =>
                {
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(3);
        }
    }
}