namespace HareDu.Tests;

using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;

[TestFixture]
public class BindingTests :
    HareDuTesting
{
    [Test]
    public async Task Verify_able_to_get_all_bindings1()
    {
        var services = GetContainerBuilder("TestData/BindingInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Binding>()
            .GetAll();

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasData);
            Assert.AreEqual(12, result.Data.Count);
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.Data);
        });
    }

    [Test]
    public async Task Verify_able_to_get_all_bindings2()
    {
        var services = GetContainerBuilder("TestData/BindingInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .GetAllBindings();

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasData);
            Assert.AreEqual(12, result.Data.Count);
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.Data);
        });
    }

    [Test]
    public async Task Verify_can_create_exchange_binding_without_arguments1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Binding>()
            .Create("HareDu", x =>
            {
                x.Source("E2");
                x.Destination("Q1");
                x.BindingType(BindingType.Exchange);
            });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.IsNotNull(result.DebugInfo.Request);

            BindingRequest request = result.DebugInfo.Request.ToObject<BindingRequest>();
                
            Assert.That(request.BindingKey, Is.Empty.Or.Null);
            Assert.IsNull(request.Arguments);
        });
    }

    [Test]
    public async Task Verify_can_create_exchange_binding_without_arguments2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
                .CreateBinding("HareDu", x =>
                {
                    x.Source("E2");
                    x.Destination("Q1");
                    x.BindingKey("*.");
                    x.BindingType(BindingType.Exchange);
                });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.IsNotNull(result.DebugInfo.Request);

            BindingRequest request = result.DebugInfo.Request.ToObject<BindingRequest>();
                
            Assert.That(request.BindingKey, Is.Empty.Or.Null);
            Assert.IsNull(request.Arguments);
        });
    }

    [Test]
    public async Task Verify_can_create_queue_binding_without_arguments1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Binding>()
            .Create("HareDu", x =>
            {
                x.Source("E2");
                x.Destination("Q1");
                x.BindingType(BindingType.Queue);
            });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.IsNotNull(result.DebugInfo.Request);

            BindingRequest request = result.DebugInfo.Request.ToObject<BindingRequest>();
                
            Assert.That(request.BindingKey, Is.Empty.Or.Null);
            Assert.IsNull(request.Arguments);
        });
    }

    [Test]
    public async Task Verify_can_create_queue_binding_without_arguments2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
                .CreateBinding("HareDu", x =>
                {
                    x.Source("E2");
                    x.Destination("Q1");
                    x.BindingType(BindingType.Exchange);
                });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.IsNotNull(result.DebugInfo.Request);

            BindingRequest request = result.DebugInfo.Request.ToObject<BindingRequest>();
                
            Assert.That(request.BindingKey, Is.Empty.Or.Null);
            Assert.IsNull(request.Arguments);
        });
    }

    [Test]
    public async Task Verify_can_create_exchange_binding_with_arguments1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Binding>()
            .Create("HareDu",
                x =>
                {
                    x.Source("E2");
                    x.Destination("Q1");
                    x.BindingKey("*.");
                    x.BindingType(BindingType.Exchange);
                    x.OptionalArguments(arg =>
                    {
                        arg.Add("arg1", "value1");
                    });
                });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.IsNotNull(result.DebugInfo.Request);
                
            BindingRequest request = result.DebugInfo.Request.ToObject<BindingRequest>();

            Assert.AreEqual("*.", request.BindingKey);
            Assert.AreEqual("value1", request.Arguments["arg1"].ToString());
        });
    }

    [Test]
    public async Task Verify_can_create_exchange_binding_with_arguments2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .CreateBinding("HareDu",
                x =>
                {
                    x.Source("E2");
                    x.Destination("Q1");
                    x.BindingKey("*.");
                    x.BindingType(BindingType.Exchange);
                    x.OptionalArguments(arg =>
                    {
                        arg.Add("arg1", "value1");
                    });
                });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.IsNotNull(result.DebugInfo.Request);
                
            BindingRequest request = result.DebugInfo.Request.ToObject<BindingRequest>();

            Assert.AreEqual("*.", request.BindingKey);
            Assert.AreEqual("value1", request.Arguments["arg1"].ToString());
        });
    }

    [Test]
    public async Task Verify_can_create_queue_binding_with_arguments1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Binding>()
            .Create("HareDu",
                x =>
                {
                    x.Source("E2");
                    x.Destination("Q1");
                    x.BindingKey("*.");
                    x.BindingType(BindingType.Queue);
                    x.OptionalArguments(arg =>
                    {
                        arg.Add("arg1", "value1");
                    });
                });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.IsNotNull(result.DebugInfo.Request);
                
            BindingRequest request = result.DebugInfo.Request.ToObject<BindingRequest>();

            Assert.AreEqual("*.", request.BindingKey);
            Assert.AreEqual("value1", request.Arguments["arg1"].ToString());
        });
    }

    [Test]
    public async Task Verify_can_create_queue_binding_with_arguments2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .CreateBinding("HareDu",
                x =>
                {
                    x.Source("E2");
                    x.Destination("Q1");
                    x.BindingKey("*.");
                    x.BindingType(BindingType.Queue);
                    x.OptionalArguments(arg =>
                    {
                        arg.Add("arg1", "value1");
                    });
                });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.IsNotNull(result.DebugInfo.Request);
                
            BindingRequest request = result.DebugInfo.Request.ToObject<BindingRequest>();

            Assert.AreEqual("*.", request.BindingKey);
            Assert.AreEqual("value1", request.Arguments["arg1"].ToString());
        });
    }

    [Test]
    public async Task Verify_cannot_create_exchange_binding_without_arguments1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Binding>()
            .Create("HareDu", x =>
            {
                x.Source(string.Empty);
                x.Destination("Q1");
                x.BindingType(BindingType.Exchange);
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_create_exchange_binding_without_arguments2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Binding>()
            .Create("HareDu", x =>
            {
                x.Source("E1");
                x.Destination(string.Empty);
                x.BindingType(BindingType.Exchange);
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_create_exchange_binding_without_arguments3()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .CreateBinding("HareDu", x =>
            {
                x.Source(string.Empty);
                x.Destination("Q1");
                x.BindingType(BindingType.Exchange);
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_create_exchange_binding_without_arguments4()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .CreateBinding("HareDu", x =>
            {
                x.Source("E1");
                x.Destination(string.Empty);
                x.BindingType(BindingType.Exchange);
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue_binding_without_arguments1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
                .CreateBinding("HareDu", x =>
                {
                    x.Source(string.Empty);
                    x.Destination("Q1");
                    x.BindingType(BindingType.Exchange);
                });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue_binding_without_arguments2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
                .CreateBinding("HareDu", x =>
                {
                    x.Source("E1");
                    x.Destination(string.Empty);
                    x.BindingType(BindingType.Exchange);
                });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue_binding_without_arguments3()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
                .CreateBinding("HareDu", x =>
                {
                    x.Source(string.Empty);
                    x.Destination(string.Empty);
                    x.BindingType(BindingType.Exchange);
                });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_create_queue_binding_without_arguments4()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .CreateBinding(string.Empty, x =>
            {
                x.Source("E1");
                x.Destination("Q1");
                x.BindingType(BindingType.Exchange);
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_can_delete_queue_binding1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Binding>()
            .Delete("HareDu", x =>
            {
                x.Source("E1");
                x.Destination("Q1");
                x.PropertiesKey(string.Empty);
                x.BindingType(BindingType.Queue);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.AreEqual(0, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_can_delete_queue_binding2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
                .CreateBinding("HareDu", x =>
                {
                    x.Source("E1");
                    x.Destination("Q1");
                    x.BindingType(BindingType.Exchange);
                });
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.AreEqual(0, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_can_delete_queue_binding3()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Binding>()
            .Delete("HareDu", x =>
            {
                x.Source("E1");
                x.Destination("Q1");
                x.PropertiesKey(string.Empty);
                x.BindingType(BindingType.Exchange);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.AreEqual(0, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_can_delete_queue_binding4()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .CreateBinding("HareDu", x =>
            {
                x.Source("E1");
                x.Destination("Q1");
                x.BindingKey("*.");
                x.BindingType(BindingType.Exchange);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.AreEqual(0, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Binding>()
            .Delete("HareDu", x =>
            {
                x.Source("E2");
                x.Destination(string.Empty);
                x.PropertiesKey(string.Empty);
                x.BindingType(BindingType.Queue);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DeleteBinding("HareDu", x =>
            {
                x.Source("E2");
                x.Destination(string.Empty);
                x.PropertiesKey(string.Empty);
                x.BindingType(BindingType.Queue);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding3()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Binding>()
            .Delete("HareDu", x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
                x.PropertiesKey(string.Empty);
                x.BindingType(BindingType.Queue);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding4()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DeleteBinding("HareDu", x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
                x.PropertiesKey(string.Empty);
                x.BindingType(BindingType.Queue);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding5()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Binding>()
            .Delete(string.Empty, x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
                x.PropertiesKey(string.Empty);
                x.BindingType(BindingType.Queue);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(3, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding6()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DeleteBinding(string.Empty, x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
                x.PropertiesKey(string.Empty);
                x.BindingType(BindingType.Queue);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(3, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Binding>()
            .Delete("HareDu", x =>
            {
                x.Source("E2");
                x.Destination(string.Empty);
                x.PropertiesKey(string.Empty);
                x.BindingType(BindingType.Exchange);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DeleteBinding("HareDu", x =>
            {
                x.Source("E2");
                x.Destination(string.Empty);
                x.PropertiesKey(string.Empty);
                x.BindingType(BindingType.Exchange);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding3()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Binding>()
            .Delete("HareDu", x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
                x.PropertiesKey(string.Empty);
                x.BindingType(BindingType.Exchange);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding4()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DeleteBinding("HareDu", x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
                x.PropertiesKey(string.Empty);
                x.BindingType(BindingType.Exchange);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding5()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<Binding>()
            .Delete(string.Empty, x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
                x.PropertiesKey(string.Empty);
                x.BindingType(BindingType.Exchange);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(3, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding6()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DeleteBinding(string.Empty, x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
                x.PropertiesKey(string.Empty);
                x.BindingType(BindingType.Exchange);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(3, result.DebugInfo.Errors.Count);
        });
    }
}