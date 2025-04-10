namespace HareDu.Tests;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;
using Serialization;

[TestFixture]
public class GlobalParameterTests :
    HareDuTesting
{
    [Test]
    public async Task Should_be_able_to_get_all_global_parameters1()
    {
        var services = GetContainerBuilder("TestData/GlobalParameterInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<GlobalParameter>()
            .GetAll();

        Console.WriteLine(result.ToJsonString(Deserializer.Options));
        // Assert.Multiple(() =>
        // {
        //     Assert.IsFalse(result.HasFaulted);
        //     Assert.IsTrue(result.HasData);
        //     Assert.AreEqual(5, result.Data.Count);
        //     Assert.AreEqual("fake_param1", result.Data[3].Name);
        //     
        //     var value = result.Data[3].Value.ToString().ToObject<IDictionary<string, object>>();
        //     
        //     Assert.AreEqual(2, value.Count);
        //     Assert.AreEqual("value1", value["arg1"].ToString());
        //     Assert.AreEqual("value2", value["arg2"].ToString());
        // });
    }
        
    [Test]
    public async Task Should_be_able_to_get_all_global_parameters2()
    {
        var services = GetContainerBuilder("TestData/GlobalParameterInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .GetAllGlobalParameters();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.AreEqual(5, result.Data.Count);
            Assert.AreEqual("fake_param1", result.Data[3].Name);
            
            var value = result.Data[3].Value.ToString().ToObject<IDictionary<string, object>>();
            
            Assert.AreEqual(2, value.Count);
            Assert.AreEqual("value1", value["arg1"].ToString());
            Assert.AreEqual("value2", value["arg2"].ToString());
        });
    }
        
    [Test]
    public async Task Verify_can_create_parameter1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<GlobalParameter>()
            .Create("fake_param",x =>
            {
                x.Add("fake_value", "");
            });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);

            GlobalParameterRequest request = result.DebugInfo.Request.ToObject<GlobalParameterRequest>();
            
            Assert.AreEqual("fake_param", request.Name);
            Assert.AreEqual("fake_value", request.Value.ToString());
        });
    }
        
    [Test]
    public async Task Verify_can_create_parameter2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .CreateGlobalParameter("fake_param",x =>
            {
                x.Add("fake_value", "");
            });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);

            GlobalParameterRequest request = result.DebugInfo.Request.ToObject<GlobalParameterRequest>();
            
            Assert.AreEqual("fake_param", request.Name);
            Assert.AreEqual("fake_value", request.Value.ToString());
        });
    }
        
    [Test]
    public async Task Verify_can_create_parameter_2()
    {
        var services = GetContainerBuilder("TestData/ExchangeInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<GlobalParameter>()
            .Create("fake_param",x =>
            {
                x.Add("arg1", "value1");
                x.Add("arg2", 5);
            });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);

            GlobalParameterRequest request = result.DebugInfo.Request.ToObject<GlobalParameterRequest>();
            
            Assert.AreEqual("fake_param", request.Name);
            Assert.AreEqual("value1", request.Value
                .ToString()
                .ToObject<IDictionary<string, object>>(Deserializer.Options)["arg1"].ToString());
            Assert.AreEqual("5", request.Value
                .ToString()
                .ToObject<IDictionary<string, object>>(Deserializer.Options)["arg2"].ToString());
        });
    }
        
    [Test]
    public async Task Verify_cannot_create_parameter_1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<GlobalParameter>()
            .Create(string.Empty, x =>
            {
                x.Add("fake_value", "test");
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);

            GlobalParameterRequest request = result.DebugInfo.Request.ToObject<GlobalParameterRequest>();
            
            Assert.AreEqual(string.Empty, request.Name);
            Assert.AreEqual("fake_value", request.Value.ToString());
        });
    }
        
    [Test]
    public async Task Verify_cannot_create_parameter_3()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<GlobalParameter>()
            .Create("fake_param", x =>
            {
                x.Add(string.Empty, "");
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);

            GlobalParameterRequest request = result.DebugInfo.Request.ToObject<GlobalParameterRequest>();
            
            Assert.AreEqual("fake_param", request.Name);
            Assert.That(request.Value.Cast<string>(), Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create_parameter_4()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<GlobalParameter>()
            .Create(string.Empty, x =>
            {
                x.Add(string.Empty, "");
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            GlobalParameterRequest request = result.DebugInfo.Request.ToObject<GlobalParameterRequest>();
            
            Assert.That(request.Name, Is.Empty.Or.Null);
            Assert.That(request.Value.Cast<string>(), Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create_parameter_5()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<GlobalParameter>()
            .Create(string.Empty, x =>
            {
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            GlobalParameterRequest request = result.DebugInfo.Request.ToObject<GlobalParameterRequest>();
            
            Assert.That(request.Name, Is.Empty.Or.Null);
            Assert.IsNull(request.Value);
        });
    }
        
    [Test]
    public async Task Verify_can_delete_parameter1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<GlobalParameter>()
            .Delete("fake_param");
            
        Assert.IsFalse(result.HasFaulted);
    }
        
    [Test]
    public async Task Verify_can_delete_parameter2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .DeleteGlobalParameter("fake_param");
            
        Assert.IsFalse(result.HasFaulted);
    }
        
    [Test]
    public async Task Verify_cannot_delete_parameter3()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<GlobalParameter>()
            .Delete(string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }
        
    [Test]
    public async Task Verify_cannot_delete_parameter4()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .DeleteGlobalParameter(string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }
}