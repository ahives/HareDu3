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
        public async Task Should_be_able_to_get_all_global_parameters()
        {
            var services = GetContainerBuilder("TestData/GlobalParameterInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .GetAll()
                .ConfigureAwait(false);

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
                .Create(x =>
                {
                    x.Parameter("fake_param");
                    x.Value("fake_value");
                })
                .ConfigureAwait(false);
             
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
                .Create(x =>
                {
                    x.Parameter("fake_param");
                    x.Value(arg =>
                    {
                        arg.Set("arg1", "value1");
                        arg.Set("arg2", 5);
                    });
                })
                .ConfigureAwait(false);
             
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
                .Create(x =>
                {
                    x.Parameter(string.Empty);
                    x.Value("fake_value");
                })
                .ConfigureAwait(false);
             
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            GlobalParameterDefinition definition = result.DebugInfo.Request.ToObject<GlobalParameterDefinition>(Deserializer.Options);
            
            definition.Name.ShouldBeNullOrEmpty();
            definition.Value.ToString().ShouldBe("fake_value");
        }
        
        [Test]
        public async Task Verify_cannot_create_parameter_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Create(x =>
                {
                    x.Value("fake_value");
                })
                .ConfigureAwait(false);
             
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
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
                .Create(x =>
                {
                    x.Parameter("fake_param");
                    x.Value(string.Empty);
                })
                .ConfigureAwait(false);
             
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
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
                .Create(x =>
                {
                    x.Parameter(string.Empty);
                    x.Value(string.Empty);
                })
                .ConfigureAwait(false);
             
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
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
                .Create(x =>
                {
                    x.Parameter(string.Empty);
                })
                .ConfigureAwait(false);
             
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
            result.DebugInfo.ShouldNotBeNull();
            
            GlobalParameterDefinition definition = result.DebugInfo.Request.ToObject<GlobalParameterDefinition>(Deserializer.Options);
            
            definition.Name.ShouldBeNullOrEmpty();
            definition.Value.ShouldBeNull();
        }
        
        [Test]
        public async Task Verify_cannot_create_parameter_6()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Create(x =>
                {
                    x.Value(string.Empty);
                })
                .ConfigureAwait(false);
             
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
            result.DebugInfo.ShouldNotBeNull();
            
            GlobalParameterDefinition definition = result.DebugInfo.Request.ToObject<GlobalParameterDefinition>(Deserializer.Options);
            
            definition.Name.ShouldBeNullOrEmpty();
            definition.Value.Cast<string>().ShouldBeNullOrEmpty();
        }
        
        [Test]
        public async Task Verify_cannot_create_parameter_7()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Create(x =>
                {
                })
                .GetResult();
             
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
            result.DebugInfo.ShouldNotBeNull();
            
            GlobalParameterDefinition definition = result.DebugInfo.Request.ToObject<GlobalParameterDefinition>(Deserializer.Options);
            
            definition.Name.ShouldBeNullOrEmpty();
            definition.Value.ShouldBeNull();
        }
        
        [Test]
        public async Task Verify_can_delete_parameter()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Delete(x => x.Parameter("fake_param"))
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeFalse();
        }
        
        [Test]
        public async Task Verify_cannot_delete_parameter_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Delete(x => x.Parameter(string.Empty))
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }
        
        [Test]
        public async Task Verify_cannot_delete_parameter_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Delete(x => {})
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }
    }
}