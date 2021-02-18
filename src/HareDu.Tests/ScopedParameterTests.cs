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
        public async Task Should_be_able_to_get_all_scoped_parameters1()
        {
            var services = GetContainerBuilder("TestData/ScopedParameterInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .GetAll();

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
        public async Task Should_be_able_to_get_all_scoped_parameters2()
        {
            var services = GetContainerBuilder("TestData/ScopedParameterInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .GetAllScopedParameters();

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
        public async Task Verify_can_create1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Create<long>("fake_parameter", 89, "fake_component", "HareDu");

            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();
            
            ScopedParameterDefinition<long> definition = result.DebugInfo.Request.ToObject<ScopedParameterDefinition<long>>(Deserializer.Options);
            
            definition.Component.ShouldBe("fake_component");
            definition.ParameterName.ShouldBe("fake_parameter");
            definition.ParameterValue.ShouldBe(89);
            definition.VirtualHost.ShouldBe("HareDu");
        }
        
        [Test]
        public async Task Verify_can_create2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .CreateScopeParameter<long>("fake_parameter", 89, "fake_component", "HareDu");

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
                .Create("fake_parameter", "value", "fake_component", "HareDu");

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
                .Create(string.Empty, "value", "fake_component", "HareDu");

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
                .Create(string.Empty, string.Empty, "fake_component", "HareDu");

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
                .Create("fake_parameter", "value", string.Empty, "HareDu");

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
                .Create("fake_parameter", "value", string.Empty, "HareDu");

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
                .Create("fake_parameter", "value", "fake_component", string.Empty);

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
                .Create("fake_parameter", "value", "fake_component", string.Empty);

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
                .Create("fake_parameter", "value", "fake_component", string.Empty);

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
                .Create(string.Empty, "value", string.Empty, "HareDu");

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
                .Create(string.Empty, string.Empty, string.Empty,"HareDu");

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
                .Create("fake_parameter", "value", string.Empty, string.Empty);

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
                .Create(string.Empty, string.Empty, string.Empty, string.Empty);

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
        public async Task Verify_can_delete1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete("fake_parameter", "fake_component", "HareDu");
            
            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public async Task Verify_can_delete2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .DeleteScopedParameter("fake_parameter", "fake_component", "HareDu");
            
            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public async Task Verify_cannot_delete_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(string.Empty, "fake_component", "HareDu");
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(string.Empty, "fake_component", "HareDu");
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete("fake_parameter", string.Empty, "HareDu");
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete("fake_parameter", string.Empty, "HareDu");
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete("fake_parameter", "fake_component", string.Empty);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_6()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete("fake_parameter", "fake_component", string.Empty);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_7()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(string.Empty, string.Empty, "HareDu");
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_cannot_delete_8()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(string.Empty,string.Empty,"HareDu");
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_cannot_delete_9()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(string.Empty, "fake_component", string.Empty);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_cannot_delete_10()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(string.Empty,"fake_component", string.Empty);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_cannot_delete_11()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Delete(string.Empty, string.Empty,string.Empty);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(3);
        }
    }
}