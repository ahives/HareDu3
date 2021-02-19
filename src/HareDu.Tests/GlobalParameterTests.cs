namespace HareDu.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Extensions;
    using Core.Serialization;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using Model;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class GlobalParameterTests :
        HareDuTesting
    {
        [Test]
        public async Task Should_be_able_to_get_all_global_parameters1()
        {
            var services = GetContainerBuilder("TestData/GlobalParameterInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .GetAll();

            result.HasFaulted.ShouldBeFalse();
            result.HasData.ShouldBeTrue();
            result.Data.Count.ShouldBe(5);
            result.Data[3].Name.ShouldBe("fake_param1");
            
            var value = result.Data[3].Value.ToString().ToObject<IDictionary<string, object>>(Deserializer.Options);
            
            value.Count.ShouldBe(2);
            value["arg1"].ToString().ShouldBe("value1");
            value["arg2"].ToString().ShouldBe("value2");
        }
        
        [Test]
        public async Task Should_be_able_to_get_all_global_parameters2()
        {
            var services = GetContainerBuilder("TestData/GlobalParameterInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .GetAllGlobalParameters();

            result.HasFaulted.ShouldBeFalse();
            result.HasData.ShouldBeTrue();
            result.Data.Count.ShouldBe(5);
            result.Data[3].Name.ShouldBe("fake_param1");
            
            var value = result.Data[3].Value.ToString().ToObject<IDictionary<string, object>>(Deserializer.Options);
            
            value.Count.ShouldBe(2);
            value["arg1"].ToString().ShouldBe("value1");
            value["arg2"].ToString().ShouldBe("value2");
        }
        
        [Test]
        public async Task Verify_can_create_parameter_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Create("fake_param",x =>
                {
                    x.Value("fake_value");
                });
             
            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();
            
            GlobalParameterDefinition definition = result.DebugInfo.Request.ToObject<GlobalParameterDefinition>(Deserializer.Options);
            
            definition.Name.ShouldBe("fake_param");
            definition.Value.ToString().ShouldBe("fake_value");
        }
        
        [Test]
        public async Task Verify_can_create_parameter2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .CreateGlobalParameter("fake_param",x =>
                {
                    x.Value("fake_value");
                });
             
            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();
            
            GlobalParameterDefinition definition = result.DebugInfo.Request.ToObject<GlobalParameterDefinition>(Deserializer.Options);
            
            definition.Name.ShouldBe("fake_param");
            definition.Value.ToString().ShouldBe("fake_value");
        }
        
        [Test]
        public async Task Verify_can_create_parameter_2()
        {
            var services = GetContainerBuilder("TestData/ExchangeInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Create("fake_param",x =>
                {
                    x.Value(arg =>
                    {
                        arg.Set("arg1", "value1");
                        arg.Set("arg2", 5);
                    });
                });
             
            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();
            
            GlobalParameterDefinition definition = result.DebugInfo.Request.ToObject<GlobalParameterDefinition>(Deserializer.Options);
            
            definition.Name.ShouldBe("fake_param");
            definition.Value
                .ToString()
                .ToObject<IDictionary<string, object>>(Deserializer.Options)["arg1"]
                .ToString()
                .ShouldBe("value1");
            definition.Value
                .ToString()
                .ToObject<IDictionary<string, object>>(Deserializer.Options)["arg2"]
                .Cast<int>()
                .ShouldBe(5);
        }
        
        [Test]
        public async Task Verify_cannot_create_parameter_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Create(string.Empty, x =>
                {
                    x.Value("fake_value");
                });
             
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            GlobalParameterDefinition definition = result.DebugInfo.Request.ToObject<GlobalParameterDefinition>(Deserializer.Options);
            
            definition.Name.ShouldBeNullOrEmpty();
            definition.Value.ToString().ShouldBe("fake_value");
        }
        
        [Test]
        public async Task Verify_cannot_create_parameter_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Create("fake_param", x =>
                {
                    x.Value(string.Empty);
                });
             
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            GlobalParameterDefinition definition = result.DebugInfo.Request.ToObject<GlobalParameterDefinition>(Deserializer.Options);
            
            definition.Name.ShouldBe("fake_param");
            definition.Value.Cast<string>().ShouldBeNullOrEmpty();
        }
        
        [Test]
        public async Task Verify_cannot_create_parameter_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Create(string.Empty, x =>
                {
                    x.Value(string.Empty);
                });
             
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(2);
            result.DebugInfo.ShouldNotBeNull();
            
            GlobalParameterDefinition definition = result.DebugInfo.Request.ToObject<GlobalParameterDefinition>(Deserializer.Options);
            
            definition.Name.ShouldBeNullOrEmpty();
            definition.Value.Cast<string>().ShouldBeNullOrEmpty();
        }
        
        [Test]
        public async Task Verify_cannot_create_parameter_5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Create(string.Empty, x =>
                {
                });
             
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(2);
            result.DebugInfo.ShouldNotBeNull();
            
            GlobalParameterDefinition definition = result.DebugInfo.Request.ToObject<GlobalParameterDefinition>(Deserializer.Options);
            
            definition.Name.ShouldBeNullOrEmpty();
            definition.Value.ShouldBeNull();
        }
        
        [Test]
        public async Task Verify_can_delete_parameter1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Delete("fake_param");
            
            result.HasFaulted.ShouldBeFalse();
        }
        
        [Test]
        public async Task Verify_can_delete_parameter2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .DeleteGlobalParameter("fake_param");
            
            result.HasFaulted.ShouldBeFalse();
        }
        
        [Test]
        public async Task Verify_cannot_delete_parameter_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Delete(string.Empty);
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
        }
    }
}