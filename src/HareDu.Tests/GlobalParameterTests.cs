namespace HareDu.Tests
{
    using System.Collections.Generic;
    using Core.Extensions;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using Model;
    using NUnit.Framework;
    using Registration;
    using Shouldly;

    [TestFixture]
    public class GlobalParameterTests :
        HareDuTesting
    {
        [Test]
        public void Should_be_able_to_get_all_global_parameters()
        {
            var services = GetContainerBuilder("TestData/GlobalParameterInfo.json").BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .GetAll()
                .GetResult();

            result.HasFaulted.ShouldBeFalse();
            result.HasData.ShouldBeTrue();
            result.Data.Count.ShouldBe(5);
            result.Data[3].Name.ShouldBe("fake_param1");
            
            var value = result.Data[3].Value.ToString().ToObject<IDictionary<string, object>>();
            
            value.Count.ShouldBe(2);
            value["arg1"].ShouldBe("value1");
            value["arg2"].ShouldBe("value2");
        }
        
        [Test]
        public void Verify_can_create_parameter_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Create(x =>
                {
                    x.Parameter("fake_param");
                    x.Value("fake_value");
                })
                .GetResult();
             
            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();
            
            GlobalParameterDefinition definition = result.DebugInfo.Request.ToObject<GlobalParameterDefinition>();
            
            definition.Name.ShouldBe("fake_param");
            definition.Value.ShouldBe("fake_value");
        }
        
        [Test]
        public void Verify_can_create_parameter_2()
        {
            var services = GetContainerBuilder("TestData/ExchangeInfo.json").BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
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
                .GetResult();
             
            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();
            
            GlobalParameterDefinition definition = result.DebugInfo.Request.ToObject<GlobalParameterDefinition>();
            
            definition.Name.ShouldBe("fake_param");
            definition.Value
                .ToString()
                .ToObject<IDictionary<string, object>>()["arg1"]
                .Cast<string>()
                .ShouldBe("value1");
            definition.Value
                .ToString()
                .ToObject<IDictionary<string, object>>()["arg2"]
                .ShouldBe(5);
        }
        
        [Test]
        public void Verify_cannot_create_parameter_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Create(x =>
                {
                    x.Parameter(string.Empty);
                    x.Value("fake_value");
                })
                .GetResult();
             
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            GlobalParameterDefinition definition = result.DebugInfo.Request.ToObject<GlobalParameterDefinition>();
            
            definition.Name.ShouldBeNullOrEmpty();
            definition.Value.ShouldBe("fake_value");
        }
        
        [Test]
        public void Verify_cannot_create_parameter_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Create(x =>
                {
                    x.Value("fake_value");
                })
                .GetResult();
             
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            GlobalParameterDefinition definition = result.DebugInfo.Request.ToObject<GlobalParameterDefinition>();
            
            definition.Name.ShouldBeNullOrEmpty();
            definition.Value.ShouldBe("fake_value");
        }
        
        [Test]
        public void Verify_cannot_create_parameter_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Create(x =>
                {
                    x.Parameter("fake_param");
                    x.Value(string.Empty);
                })
                .GetResult();
             
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();
            
            GlobalParameterDefinition definition = result.DebugInfo.Request.ToObject<GlobalParameterDefinition>();
            
            definition.Name.ShouldBe("fake_param");
            definition.Value.Cast<string>().ShouldBeNullOrEmpty();
        }
        
        [Test]
        public void Verify_cannot_create_parameter_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Create(x =>
                {
                    x.Parameter(string.Empty);
                    x.Value(string.Empty);
                })
                .GetResult();
             
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
            result.DebugInfo.ShouldNotBeNull();
            
            GlobalParameterDefinition definition = result.DebugInfo.Request.ToObject<GlobalParameterDefinition>();
            
            definition.Name.ShouldBeNullOrEmpty();
            definition.Value.Cast<string>().ShouldBeNullOrEmpty();
        }
        
        [Test]
        public void Verify_cannot_create_parameter_5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Create(x =>
                {
                    x.Parameter(string.Empty);
                })
                .GetResult();
             
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
            result.DebugInfo.ShouldNotBeNull();
            
            GlobalParameterDefinition definition = result.DebugInfo.Request.ToObject<GlobalParameterDefinition>();
            
            definition.Name.ShouldBeNullOrEmpty();
            definition.Value.ShouldBeNull();
        }
        
        [Test]
        public void Verify_cannot_create_parameter_6()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Create(x =>
                {
                    x.Value(string.Empty);
                })
                .GetResult();
             
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
            result.DebugInfo.ShouldNotBeNull();
            
            GlobalParameterDefinition definition = result.DebugInfo.Request.ToObject<GlobalParameterDefinition>();
            
            definition.Name.ShouldBeNullOrEmpty();
            definition.Value.Cast<string>().ShouldBeNullOrEmpty();
        }
        
        [Test]
        public void Verify_cannot_create_parameter_7()
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
            
            GlobalParameterDefinition definition = result.DebugInfo.Request.ToObject<GlobalParameterDefinition>();
            
            definition.Name.ShouldBeNullOrEmpty();
            definition.Value.ShouldBeNull();
        }
        
        [Test]
        public void Verify_can_delete_parameter()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Delete(x => x.Parameter("fake_param"))
                .GetResult();
            
            result.HasFaulted.ShouldBeFalse();
        }
        
        [Test]
        public void Verify_cannot_delete_parameter_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Delete(x => x.Parameter(string.Empty))
                .GetResult();
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }
        
        [Test]
        public void Verify_cannot_delete_parameter_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Delete(x => {})
                .GetResult();
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }
    }
}