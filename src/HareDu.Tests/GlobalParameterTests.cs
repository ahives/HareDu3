namespace HareDu.Tests;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Core.Serialization;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;
using Serialization;

[TestFixture]
public class GlobalParameterTests :
    HareDuTesting
{
    readonly IHareDuDeserializer _deserializer;

    public GlobalParameterTests()
    {
        _deserializer = new BrokerDeserializer();
    }

    [Test]
    public async Task Should_be_able_to_get_all_global_parameters1()
    {
        var result = await GetContainerBuilder("TestData/GlobalParameterInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<GlobalParameter>(x => x.UsingCredentials("guest", "guest"))
            .GetAll();

        Console.WriteLine(_deserializer.ToJsonString(result));
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
        var result = await GetContainerBuilder("TestData/GlobalParameterInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetAllGlobalParameters(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data.Count, Is.EqualTo(5));
            Assert.That(result.Data[3].Name, Is.EqualTo("fake_param1"));

            var value = _deserializer.ToObject<IDictionary<string, object>>(result.Data[3].Value.ToString());

            Assert.That(value.Count, Is.EqualTo(2));
            Assert.That(value["arg1"].ToString(), Is.EqualTo("value1"));
            Assert.That(value["arg2"].ToString(), Is.EqualTo("value2"));
        });
    }
        
    [Test]
    public async Task Verify_can_create_parameter1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<GlobalParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create("fake_param",x =>
            {
                x.Add("fake_arg", "fake_value");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            GlobalParameterRequest request = _deserializer.ToObject<GlobalParameterRequest>(result.DebugInfo.Request);

            Assert.That(request.Name, Is.EqualTo("fake_param"));
            Assert.That(_deserializer.ToObject<IDictionary<string, string>>(request.Value.ToString())["fake_arg"], Is.EqualTo("fake_value"));
        });
    }
        
    [Test]
    public async Task Verify_can_create_parameter2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateGlobalParameter(x => x.UsingCredentials("guest", "guest"),
                "fake_param", x =>
                {
                    x.Add("fake_arg", "fake_value");
                });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            GlobalParameterRequest request = _deserializer.ToObject<GlobalParameterRequest>(result.DebugInfo.Request);

            Assert.That(request.Name, Is.EqualTo("fake_param"));
            Assert.That(_deserializer.ToObject<IDictionary<string, string>>(request.Value.ToString())["fake_arg"], Is.EqualTo("fake_value"));
        });
    }
        
    [Test]
    public async Task Verify_can_create_parameter_2()
    {
        var result = await GetContainerBuilder("TestData/GlobalParameterInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<GlobalParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create("fake_param",x =>
            {
                x.Add("arg1", "value1");
                x.Add("arg2", 5);
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            GlobalParameterRequest request = _deserializer.ToObject<GlobalParameterRequest>(result.DebugInfo.Request);

            Assert.That(request.Name, Is.EqualTo("fake_param"));
            Assert.That(_deserializer.ToObject<IDictionary<string, string>>(request.Value
                .ToString())["arg1"].ToString(), Is.EqualTo("value1"));
            Assert.That(_deserializer.ToObject<IDictionary<string, string>>(request.Value
                .ToString())["arg2"].ToString(), Is.EqualTo("5"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_parameter_1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<GlobalParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create(string.Empty, x =>
            {
                x.Add("fake_arg", "fake_value");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));

            GlobalParameterRequest request = _deserializer.ToObject<GlobalParameterRequest>(result.DebugInfo.Request);

            Assert.That(request.Name, Is.EqualTo(string.Empty));
            Assert.That(_deserializer.ToObject<IDictionary<string, string>>(request.Value.ToString())["fake_arg"], Is.EqualTo("fake_value"));
        });
    }
        
    [Test]
    public async Task Verify_cannot_create_parameter_3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<GlobalParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create("fake_param", x =>
            {
                x.Add(string.Empty, "");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(0));

            GlobalParameterRequest request = _deserializer.ToObject<GlobalParameterRequest>(result.DebugInfo.Request);

            Assert.That(request.Name, Is.EqualTo("fake_param"));
            Assert.That(request.Value.Cast<string>(), Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create_parameter_4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<GlobalParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create(string.Empty, x =>
            {
                x.Add(string.Empty, "");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));

            GlobalParameterRequest request = _deserializer.ToObject<GlobalParameterRequest>(result.DebugInfo.Request);

            Assert.That(request.Name, Is.Empty.Or.Null);
            Assert.That(request.Value.Cast<string>(), Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create_parameter_5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<GlobalParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create(string.Empty, x =>
            {
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));

            GlobalParameterRequest request = _deserializer.ToObject<GlobalParameterRequest>(result.DebugInfo.Request);

            Assert.That(request.Name, Is.Empty.Or.Null);
            Assert.That(request.Value, Is.Null);
        });
    }
        
    [Test]
    public async Task Verify_can_delete_parameter1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<GlobalParameter>(x => x.UsingCredentials("guest", "guest"))
            .Delete("fake_param");

        Assert.That(result.HasFaulted, Is.False);
    }
        
    [Test]
    public async Task Verify_can_delete_parameter2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteGlobalParameter(x => x.UsingCredentials("guest", "guest"), "fake_param");

        Assert.That(result.HasFaulted, Is.False);
    }
        
    [Test]
    public async Task Verify_cannot_delete_parameter3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<GlobalParameter>(x => x.UsingCredentials("guest", "guest"))
            .Delete(string.Empty);

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }
        
    [Test]
    public async Task Verify_cannot_delete_parameter4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteGlobalParameter(x => x.UsingCredentials("guest", "guest"), string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }
}