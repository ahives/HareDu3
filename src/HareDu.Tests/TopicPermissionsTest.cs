namespace HareDu.Tests;

using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;

[TestFixture]
public class TopicPermissionsTest :
    HareDuTesting
{
    [Test]
    public async Task Verify_can_get_all_topic_permissions1()
    {
        var services = GetContainerBuilder("TestData/TopicPermissionsInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<TopicPermissions>()
            .GetAll();
            
        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_can_get_all_topic_permissions2()
    {
        var services = GetContainerBuilder("TestData/TopicPermissionsInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .GetAllTopicPermissions();
            
        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_can_create_user_permissions1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<TopicPermissions>()
            .Create("guest", "HareDu", x =>
            {
                x.Exchange("E4");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.AreEqual(0, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_can_create_user_permissions2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .CreateTopicPermission("guest", "HareDu", x =>
            {
                x.Exchange("E4");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.AreEqual(0, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<TopicPermissions>()
            .Create(string.Empty, "HareDu", x =>
            {
                x.Exchange("E4");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();
                
            Assert.AreEqual("E4", request.Exchange);
            Assert.AreEqual(".*", request.Read);
            Assert.AreEqual(".*", request.Write);
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .CreateTopicPermission(string.Empty, "HareDu", x =>
            {
                x.Exchange("E4");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();
                
            Assert.AreEqual("E4", request.Exchange);
            Assert.AreEqual(".*", request.Read);
            Assert.AreEqual(".*", request.Write);
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions3()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<TopicPermissions>()
            .Create("guest", string.Empty, x =>
            {
                x.Exchange("E4");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();
                
            Assert.AreEqual("E4", request.Exchange);
            Assert.AreEqual(".*", request.Read);
            Assert.AreEqual(".*", request.Write);
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions4()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .CreateTopicPermission("guest", string.Empty, x =>
            {
                x.Exchange("E4");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();
                
            Assert.AreEqual("E4", request.Exchange);
            Assert.AreEqual(".*", request.Read);
            Assert.AreEqual(".*", request.Write);
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions5()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<TopicPermissions>()
            .Create(string.Empty, string.Empty, x =>
            {
                x.Exchange(string.Empty);
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(3, result.DebugInfo.Errors.Count);

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();
                
            Assert.That(request.Exchange, Is.Empty.Or.Null);
            Assert.AreEqual(".*", request.Read);
            Assert.AreEqual(".*", request.Write);
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions6()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .CreateTopicPermission(string.Empty, string.Empty, x =>
            {
                x.Exchange(string.Empty);
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(3, result.DebugInfo.Errors.Count);

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();
                
            Assert.That(request.Exchange, Is.Empty.Or.Null);
            Assert.AreEqual(".*", request.Read);
            Assert.AreEqual(".*", request.Write);
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions7()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<TopicPermissions>()
            .Create(string.Empty, string.Empty, x =>
            {
                x.Exchange(string.Empty);
                x.UsingReadPattern(string.Empty);
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(4, result.DebugInfo.Errors.Count);

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();
                
            Assert.That(request.Exchange, Is.Empty.Or.Null);
            Assert.That(request.Read, Is.Empty.Or.Null);
            Assert.AreEqual(".*", request.Write);
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions8()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .CreateTopicPermission(string.Empty, string.Empty, x =>
            {
                x.Exchange(string.Empty);
                x.UsingReadPattern(string.Empty);
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(4, result.DebugInfo.Errors.Count);

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();
                
            Assert.That(request.Exchange, Is.Empty.Or.Null);
            Assert.That(request.Read, Is.Empty.Or.Null);
            Assert.AreEqual(".*", request.Write);
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions9()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<TopicPermissions>()
            .Create(string.Empty, string.Empty, x =>
            {
                x.Exchange(string.Empty);
                x.UsingReadPattern(".*");
                x.UsingWritePattern(string.Empty);
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(4, result.DebugInfo.Errors.Count);

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();
                
            Assert.That(request.Exchange, Is.Empty.Or.Null);
            Assert.That(request.Write, Is.Empty.Or.Null);
            Assert.AreEqual(".*", request.Read);
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions10()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .CreateTopicPermission(string.Empty, string.Empty, x =>
            {
                x.Exchange(string.Empty);
                x.UsingReadPattern(".*");
                x.UsingWritePattern(string.Empty);
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(4, result.DebugInfo.Errors.Count);

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();
                
            Assert.That(request.Exchange, Is.Empty.Or.Null);
            Assert.That(request.Write, Is.Empty.Or.Null);
            Assert.AreEqual(".*", request.Read);
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions11()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<TopicPermissions>()
            .Create(string.Empty, string.Empty, x =>
            {
                x.Exchange(string.Empty);
                x.UsingReadPattern(string.Empty);
                x.UsingWritePattern(string.Empty);
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(5, result.DebugInfo.Errors.Count);

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();
                
            Assert.That(request.Exchange, Is.Empty.Or.Null);
            Assert.That(request.Read, Is.Empty.Or.Null);
            Assert.That(request.Write, Is.Empty.Or.Null);
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions12()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .CreateTopicPermission(string.Empty, string.Empty, x =>
            {
                x.Exchange(string.Empty);
                x.UsingReadPattern(string.Empty);
                x.UsingWritePattern(string.Empty);
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(5, result.DebugInfo.Errors.Count);

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();
                
            Assert.That(request.Exchange, Is.Empty.Or.Null);
            Assert.That(request.Read, Is.Empty.Or.Null);
            Assert.That(request.Write, Is.Empty.Or.Null);
        });
    }

    [Test]
    public async Task Verify_can_delete_user_permissions2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .DeleteTopicPermission("guest", "HareDu7");
            
        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_cannot_delete_user_permissions1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<TopicPermissions>()
            .Delete(string.Empty, "HareDu7");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_user_permissions2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .DeleteTopicPermission(string.Empty, "HareDu7");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_user_permissions3()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<TopicPermissions>()
            .Delete("guest", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_user_permissions4()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .DeleteTopicPermission("guest", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_user_permissions5()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<TopicPermissions>()
            .Delete(string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_user_permissions6()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .DeleteTopicPermission(string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }
}