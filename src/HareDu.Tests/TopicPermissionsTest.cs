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
        var result = await GetContainerBuilder("TestData/TopicPermissionsInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<TopicPermissions>(x => x.UsingCredentials("guest", "guest"))
            .GetAll();
            
        Assert.That(result.HasFaulted, Is.False);
    }

    [Test]
    public async Task Verify_can_get_all_topic_permissions2()
    {
        var result = await GetContainerBuilder("TestData/TopicPermissionsInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetAllTopicPermissions(x => x.UsingCredentials("guest", "guest"));
            
        Assert.That(result.HasFaulted, Is.False);
    }

    [Test]
    public async Task Verify_can_create_user_permissions1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<TopicPermissions>(x => x.UsingCredentials("guest", "guest"))
            .Create("guest", "HareDu", x =>
            {
                x.Exchange("E4");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task Verify_can_create_user_permissions2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateTopicPermission(x => x.UsingCredentials("guest", "guest"), "guest", "HareDu", x =>
            {
                x.Exchange("E4");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<TopicPermissions>(x => x.UsingCredentials("guest", "guest"))
            .Create(string.Empty, "HareDu", x =>
            {
                x.Exchange("E4");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();
                
            Assert.That(request.Exchange, Is.EqualTo("E4"));
            Assert.That(request.Read, Is.EqualTo(".*"));
            Assert.That(request.Write, Is.EqualTo(".*"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateTopicPermission(x => x.UsingCredentials("guest", "guest"), string.Empty, "HareDu", x =>
            {
                x.Exchange("E4");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();

            Assert.That(request.Exchange, Is.EqualTo("E4"));
            Assert.That(request.Read, Is.EqualTo(".*"));
            Assert.That(request.Write, Is.EqualTo(".*"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<TopicPermissions>(x => x.UsingCredentials("guest", "guest"))
            .Create("guest", string.Empty, x =>
            {
                x.Exchange("E4");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();

            Assert.That(request.Exchange, Is.EqualTo("E4"));
            Assert.That(request.Read, Is.EqualTo(".*"));
            Assert.That(request.Write, Is.EqualTo(".*"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateTopicPermission(x => x.UsingCredentials("guest", "guest"), "guest", string.Empty, x =>
            {
                x.Exchange("E4");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();

            Assert.That(request.Exchange, Is.EqualTo("E4"));
            Assert.That(request.Read, Is.EqualTo(".*"));
            Assert.That(request.Write, Is.EqualTo(".*"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<TopicPermissions>(x => x.UsingCredentials("guest", "guest"))
            .Create(string.Empty, string.Empty, x =>
            {
                x.Exchange(string.Empty);
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(3));

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();
                
            Assert.That(request.Exchange, Is.Empty.Or.Null);
            Assert.That(request.Read, Is.EqualTo(".*"));
            Assert.That(request.Write, Is.EqualTo(".*"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateTopicPermission(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty, x =>
            {
                x.Exchange(string.Empty);
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(3));

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();
                
            Assert.That(request.Exchange, Is.Empty.Or.Null);
            Assert.That(request.Read, Is.EqualTo(".*"));
            Assert.That(request.Write, Is.EqualTo(".*"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions7()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<TopicPermissions>(x => x.UsingCredentials("guest", "guest"))
            .Create(string.Empty, string.Empty, x =>
            {
                x.Exchange(string.Empty);
                x.UsingReadPattern(string.Empty);
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(4));

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();
                
            Assert.That(request.Exchange, Is.Empty.Or.Null);
            Assert.That(request.Read, Is.Empty.Or.Null);
            Assert.That(request.Write, Is.EqualTo(".*"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions8()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateTopicPermission(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty, x =>
            {
                x.Exchange(string.Empty);
                x.UsingReadPattern(string.Empty);
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(4));

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();
                
            Assert.That(request.Exchange, Is.Empty.Or.Null);
            Assert.That(request.Read, Is.Empty.Or.Null);
            Assert.That(request.Write, Is.EqualTo(".*"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions9()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<TopicPermissions>(x => x.UsingCredentials("guest", "guest"))
            .Create(string.Empty, string.Empty, x =>
            {
                x.Exchange(string.Empty);
                x.UsingReadPattern(".*");
                x.UsingWritePattern(string.Empty);
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(4));

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();
                
            Assert.That(request.Exchange, Is.Empty.Or.Null);
            Assert.That(request.Write, Is.Empty.Or.Null);
            Assert.That(request.Read, Is.EqualTo(".*"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions10()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateTopicPermission(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty, x =>
            {
                x.Exchange(string.Empty);
                x.UsingReadPattern(".*");
                x.UsingWritePattern(string.Empty);
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(4));

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();
                
            Assert.That(request.Exchange, Is.Empty.Or.Null);
            Assert.That(request.Write, Is.Empty.Or.Null);
            Assert.That(request.Read, Is.EqualTo(".*"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions11()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<TopicPermissions>(x => x.UsingCredentials("guest", "guest"))
            .Create(string.Empty, string.Empty, x =>
            {
                x.Exchange(string.Empty);
                x.UsingReadPattern(string.Empty);
                x.UsingWritePattern(string.Empty);
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(5));

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();
                
            Assert.That(request.Exchange, Is.Empty.Or.Null);
            Assert.That(request.Read, Is.Empty.Or.Null);
            Assert.That(request.Write, Is.Empty.Or.Null);
        });
    }

    [Test]
    public async Task Verify_cannot_create_topic_permissions12()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateTopicPermission(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty, x =>
            {
                x.Exchange(string.Empty);
                x.UsingReadPattern(string.Empty);
                x.UsingWritePattern(string.Empty);
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(5));

            TopicPermissionsRequest request = result.DebugInfo.Request.ToObject<TopicPermissionsRequest>();
                
            Assert.That(request.Exchange, Is.Empty.Or.Null);
            Assert.That(request.Read, Is.Empty.Or.Null);
            Assert.That(request.Write, Is.Empty.Or.Null);
        });
    }

    [Test]
    public async Task Verify_can_delete_user_permissions2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteTopicPermission(x => x.UsingCredentials("guest", "guest"), "guest", "HareDu7");
            
        Assert.That(result.HasFaulted, Is.False);
    }

    [Test]
    public async Task Verify_cannot_delete_user_permissions1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<TopicPermissions>(x => x.UsingCredentials("guest", "guest"))
            .Delete(string.Empty, "HareDu7");
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_user_permissions2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteTopicPermission(x => x.UsingCredentials("guest", "guest"), string.Empty, "HareDu7");
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_user_permissions3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<TopicPermissions>(x => x.UsingCredentials("guest", "guest"))
            .Delete("guest", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_user_permissions4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteTopicPermission(x => x.UsingCredentials("guest", "guest"), "guest", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_user_permissions5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<TopicPermissions>(x => x.UsingCredentials("guest", "guest"))
            .Delete(string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_user_permissions6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteTopicPermission(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }
}