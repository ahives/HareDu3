namespace HareDu.Tests;

using System.Threading.Tasks;
using Core.Configuration;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;

[TestFixture]
public class ScopedParameterTests :
    HareDuTesting
{
    [Test]
    public async Task Verify_able_to_get_all_scoped_parameters1()
    {
        var result = await GetContainerBuilder("TestData/ScopedParameterInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .GetAll();
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(3, result.Data.Count);
            Assert.IsNotNull(result.Data[0]);
            Assert.AreEqual(2, result.Data[0].Value.Count);
            Assert.AreEqual("10", result.Data[0].Value["max-connections"].ToString());
            Assert.AreEqual("value", result.Data[0].Value["max-queues"].ToString());
        });
    }
        
    [Test]
    public async Task Verify_able_to_get_all_scoped_parameters2()
    {
        var result = await GetContainerBuilder("TestData/ScopedParameterInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetAllScopedParameters(x => x.UsingCredentials("guest", "guest"));
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(3, result.Data.Count);
            Assert.IsNotNull(result.Data[0]);
            Assert.AreEqual(2, result.Data[0].Value.Count);
            Assert.AreEqual("10", result.Data[0].Value["max-connections"].ToString());
            Assert.AreEqual("value", result.Data[0].Value["max-queues"].ToString());
        });
    }
        
    [Test]
    public async Task Verify_can_create1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create<long>("fake_parameter", 89, "fake_component", "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            
            ScopedParameterRequest<long> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<long>>();
            
            Assert.AreEqual("fake_component", request.Component);
            Assert.AreEqual("fake_parameter", request.ParameterName);
            Assert.AreEqual(89, request.ParameterValue);
            Assert.AreEqual("HareDu", request.VirtualHost);
        });
    }
        
    [Test]
    public async Task Verify_can_create2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateScopeParameter<long>(x => x.UsingCredentials("guest", "guest"), "fake_parameter", 89, "fake_component", "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            
            ScopedParameterRequest<long> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<long>>();
            
            Assert.AreEqual("fake_component", request.Component);
            Assert.AreEqual("fake_parameter", request.ParameterName);
            Assert.AreEqual(89, request.ParameterValue);
            Assert.AreEqual("HareDu", request.VirtualHost);
        });
    }
        
    [Test]
    public async Task Verify_can_create3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create("fake_parameter", "value", "fake_component", "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            
            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();
            
            Assert.AreEqual("fake_component", request.Component);
            Assert.AreEqual("fake_parameter", request.ParameterName);
            Assert.AreEqual("value", request.ParameterValue);
            Assert.AreEqual("HareDu", request.VirtualHost);
        });
    }
        
    [Test]
    public async Task Verify_can_create4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateScopeParameter(x => x.UsingCredentials("guest", "guest"), "fake_parameter", "value", "fake_component", "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            
            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();
            
            Assert.AreEqual("fake_component", request.Component);
            Assert.AreEqual("fake_parameter", request.ParameterName);
            Assert.AreEqual("value", request.ParameterValue);
            Assert.AreEqual("HareDu", request.VirtualHost);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create(string.Empty, "value", "fake_component", "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            
            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();
            
            Assert.AreEqual("fake_component", request.Component);
            Assert.That(request.ParameterName, Is.Empty.Or.Null);
            Assert.AreEqual("value", request.ParameterValue);
            Assert.AreEqual("HareDu", request.VirtualHost);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateScopeParameter(x => x.UsingCredentials("guest", "guest"), string.Empty, "value", "fake_component", "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            
            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();
            
            Assert.AreEqual("fake_component", request.Component);
            Assert.That(request.ParameterName, Is.Empty.Or.Null);
            Assert.AreEqual("value", request.ParameterValue);
            Assert.AreEqual("HareDu", request.VirtualHost);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create(string.Empty, string.Empty, "fake_component", "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            
            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();
            
            Assert.AreEqual("fake_component", request.Component);
            Assert.That(request.ParameterName, Is.Empty.Or.Null);
            Assert.That(request.ParameterValue, Is.Empty.Or.Null);
            Assert.AreEqual("HareDu", request.VirtualHost);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateScopeParameter(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty, "fake_component", "HareDu");

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);

            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();

            Assert.AreEqual("fake_component", request.Component);
            Assert.That(request.ParameterName, Is.Empty.Or.Null);
            Assert.That(request.ParameterValue, Is.Empty.Or.Null);
            Assert.AreEqual("HareDu", request.VirtualHost);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create("fake_parameter", "value", string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            
            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();
            
            Assert.That(request.Component, Is.Empty.Or.Null);
            Assert.AreEqual("fake_parameter", request.ParameterName);
            Assert.AreEqual("value", request.ParameterValue);
            Assert.AreEqual("HareDu", request.VirtualHost);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateScopeParameter(x => x.UsingCredentials("guest", "guest"), "fake_parameter", "value", string.Empty, "HareDu");

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);

            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();

            Assert.That(request.Component, Is.Empty.Or.Null);
            Assert.AreEqual("fake_parameter", request.ParameterName);
            Assert.AreEqual("value", request.ParameterValue);
            Assert.AreEqual("HareDu", request.VirtualHost);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create7()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create("fake_parameter", "value", string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            
            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();
            
            Assert.That(request.Component, Is.Empty.Or.Null);
            Assert.AreEqual("fake_parameter", request.ParameterName);
            Assert.AreEqual("value", request.ParameterValue);
            Assert.AreEqual("HareDu", request.VirtualHost);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create8()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateScopeParameter(x => x.UsingCredentials("guest", "guest"), "fake_parameter", "value", string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            
            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();
            
            Assert.That(request.Component, Is.Empty.Or.Null);
            Assert.AreEqual("fake_parameter", request.ParameterName);
            Assert.AreEqual("value", request.ParameterValue);
            Assert.AreEqual("HareDu", request.VirtualHost);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create9()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create("fake_parameter", "value", "fake_component", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            
            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();
            
            Assert.AreEqual("fake_component", request.Component);
            Assert.AreEqual("fake_parameter", request.ParameterName);
            Assert.AreEqual("value", request.ParameterValue);
            Assert.That(request.VirtualHost, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create10()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateScopeParameter(x => x.UsingCredentials("guest", "guest"), "fake_parameter", "value", "fake_component", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            
            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();
            
            Assert.AreEqual("fake_component", request.Component);
            Assert.AreEqual("fake_parameter", request.ParameterName);
            Assert.AreEqual("value", request.ParameterValue);
            Assert.That(request.VirtualHost, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create11()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create("fake_parameter", "value", "fake_component", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            
            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();
            
            Assert.AreEqual("fake_component", request.Component);
            Assert.AreEqual("fake_parameter", request.ParameterName);
            Assert.AreEqual("value", request.ParameterValue);
            Assert.That(request.VirtualHost, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create12()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateScopeParameter(x => x.UsingCredentials("guest", "guest"), "fake_parameter", "value", "fake_component", string.Empty);

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);

            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();

            Assert.AreEqual("fake_component", request.Component);
            Assert.AreEqual("fake_parameter", request.ParameterName);
            Assert.AreEqual("value", request.ParameterValue);
            Assert.That(request.VirtualHost, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create13()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create("fake_parameter", "value", "fake_component", string.Empty);

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);

            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();

            Assert.AreEqual("fake_component", request.Component);
            Assert.AreEqual("fake_parameter", request.ParameterName);
            Assert.AreEqual("value", request.ParameterValue);
            Assert.That(request.VirtualHost, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create14()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateScopeParameter(x => x.UsingCredentials("guest", "guest"), "fake_parameter", "value", "fake_component", string.Empty);

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);

            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();

            Assert.AreEqual("fake_component", request.Component);
            Assert.AreEqual("fake_parameter", request.ParameterName);
            Assert.AreEqual("value", request.ParameterValue);
            Assert.That(request.VirtualHost, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create15()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create(string.Empty, "value", string.Empty, "HareDu");

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();

            Assert.That(request.Component, Is.Empty.Or.Null);
            Assert.That(request.ParameterName, Is.Empty.Or.Null);
            Assert.AreEqual("value", request.ParameterValue);
            Assert.AreEqual("HareDu", request.VirtualHost);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create16()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateScopeParameter(x => x.UsingCredentials("guest", "guest"), string.Empty, "value", string.Empty, "HareDu");

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();

            Assert.That(request.Component, Is.Empty.Or.Null);
            Assert.That(request.ParameterName, Is.Empty.Or.Null);
            Assert.AreEqual("value", request.ParameterValue);
            Assert.AreEqual("HareDu", request.VirtualHost);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create17()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create(string.Empty, string.Empty, string.Empty,"HareDu");

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();

            Assert.That(request.Component, Is.Empty.Or.Null);
            Assert.That(request.ParameterName, Is.Empty.Or.Null);
            Assert.That(request.ParameterValue, Is.Empty.Or.Null);
            Assert.AreEqual("HareDu", request.VirtualHost);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create18()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateScopeParameter(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty, string.Empty,"HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();
            
            Assert.That(request.Component, Is.Empty.Or.Null);
            Assert.That(request.ParameterName, Is.Empty.Or.Null);
            Assert.That(request.ParameterValue, Is.Empty.Or.Null);
            Assert.AreEqual("HareDu", request.VirtualHost);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create19()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create("fake_parameter", "value", string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();
            
            Assert.That(request.Component, Is.Empty.Or.Null);
            Assert.That(request.VirtualHost, Is.Empty.Or.Null);
            Assert.AreEqual("fake_parameter", request.ParameterName);
            Assert.AreEqual("value", request.ParameterValue);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create20()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateScopeParameter(x => x.UsingCredentials("guest", "guest"), "fake_parameter", "value", string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();
            
            Assert.That(request.Component, Is.Empty.Or.Null);
            Assert.That(request.VirtualHost, Is.Empty.Or.Null);
            Assert.AreEqual("fake_parameter", request.ParameterName);
            Assert.AreEqual("value", request.ParameterValue);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create21()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create(string.Empty, string.Empty, string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(3, result.DebugInfo.Errors.Count);

            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();
            
            Assert.That(request.Component, Is.Empty.Or.Null);
            Assert.That(request.VirtualHost, Is.Empty.Or.Null);
            Assert.That(request.ParameterName, Is.Empty.Or.Null);
            Assert.That(request.ParameterValue, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create22()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateScopeParameter(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty, string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(3, result.DebugInfo.Errors.Count);

            ScopedParameterRequest<string> request = result.DebugInfo.Request.ToObject<ScopedParameterRequest<string>>();
            
            Assert.That(request.Component, Is.Empty.Or.Null);
            Assert.That(request.VirtualHost, Is.Empty.Or.Null);
            Assert.That(request.ParameterName, Is.Empty.Or.Null);
            Assert.That(request.ParameterValue, Is.Empty.Or.Null);
        });
    }

    [Test]
    public async Task Verify_can_delete1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Delete("fake_parameter", "fake_component", "HareDu");
            
        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_can_delete2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteScopedParameter(x => x.UsingCredentials("guest", "guest"), "fake_parameter", "fake_component", "HareDu");
            
        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_cannot_delete3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Delete(string.Empty, "fake_component", "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteScopedParameter(x => x.UsingCredentials("guest", "guest"), string.Empty, "fake_component", "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Delete("fake_parameter", string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteScopedParameter(x => x.UsingCredentials("guest", "guest"), "fake_parameter", string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete7()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Delete("fake_parameter", "fake_component", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete8()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteScopedParameter(x => x.UsingCredentials("guest", "guest"), "fake_parameter", "fake_component", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete9()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Delete(string.Empty, string.Empty, "HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete10()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteScopedParameter(x => x.UsingCredentials("guest", "guest"), string.Empty,string.Empty,"HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete11()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Delete(string.Empty, "fake_component", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete12()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteScopedParameter(x => x.UsingCredentials("guest", "guest"), string.Empty,"fake_component", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete13()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Delete(string.Empty, string.Empty,string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(3, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete14()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteScopedParameter(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty,string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(3, result.DebugInfo.Errors.Count);
        });
    }
}