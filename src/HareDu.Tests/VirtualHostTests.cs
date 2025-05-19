namespace HareDu.Tests;

using System.Linq;
using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;

[TestFixture]
public class VirtualHostTests :
    HareDuTesting
{
    [Test]
    public async Task Should_be_able_to_get_all_vhosts1()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .GetAll();
        
        Assert.Multiple(() =>
        {
            Assert.That(result.HasData, Is.True);
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.Data.Count, Is.EqualTo(3));

            var data1 = result.Data.SingleOrDefault(x => x.Name == "/");
            var data2 = result.Data.SingleOrDefault(x => x.Name == "QueueTestVirtualHost1");
            var data3 = result.Data.SingleOrDefault(x => x.Name == "QueueTestVirtualHost2");

            Assert.That(data1, Is.Not.Null);
            Assert.That(data2, Is.Not.Null);
            Assert.That(data3, Is.Not.Null);
            Assert.That(data1?.Name, Is.EqualTo("/"));;
            Assert.That(data2?.Name, Is.EqualTo("QueueTestVirtualHost1"));
            Assert.That(data3?.Name, Is.EqualTo("QueueTestVirtualHost2"));;
            Assert.That(data3.TotalMessages, Is.EqualTo(10));;
            Assert.That(data3.MessagesDetails, Is.Not.Null);
            Assert.That(data3.MessagesDetails?.Value, Is.EqualTo(1.0M));;
            Assert.That(data3.ReadyMessages, Is.EqualTo(7));;
            Assert.That(data3.ReadyMessagesDetails, Is.Not.Null);
            Assert.That(data3.ReadyMessagesDetails?.Value, Is.EqualTo(1.0M));
            Assert.That(data3.UnacknowledgedMessages, Is.EqualTo(2));
            Assert.That(data3.UnacknowledgedMessagesDetails, Is.Not.Null);
            Assert.That(data3.UnacknowledgedMessagesDetails?.Value, Is.EqualTo(2.0M));
        });
    }

    [Test]
    public async Task Should_be_able_to_get_vhosts1()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostInfo2.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .Get("QueueTestVirtualHost1");
        
        Assert.Multiple(() =>
        {
            Assert.That(result.HasData, Is.True);
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Name, Is.EqualTo("HareDu1"));
        });
    }

    [Test]
    public async Task Should_be_able_to_get_all_vhosts2()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetAllVirtualHosts(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasData, Is.True);
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.Data.Count, Is.EqualTo(3));

            var data1 = result.Data.SingleOrDefault(x => x.Name == "/");
            var data2 = result.Data.SingleOrDefault(x => x.Name == "QueueTestVirtualHost1");
            var data3 = result.Data.SingleOrDefault(x => x.Name == "QueueTestVirtualHost2");

            Assert.That(data1, Is.Not.Null);
            Assert.That(data2, Is.Not.Null);
            Assert.That(data3, Is.Not.Null);
            Assert.That(data1?.Name, Is.EqualTo("/"));;
            Assert.That(data2?.Name, Is.EqualTo("QueueTestVirtualHost1"));
            Assert.That(data3?.Name, Is.EqualTo("QueueTestVirtualHost2"));;
            Assert.That(data3.TotalMessages, Is.EqualTo(10));;
            Assert.That(data3.MessagesDetails, Is.Not.Null);
            Assert.That(data3.MessagesDetails?.Value, Is.EqualTo(1.0M));;
            Assert.That(data3.ReadyMessages, Is.EqualTo(7));;
            Assert.That(data3.ReadyMessagesDetails, Is.Not.Null);
            Assert.That(data3.ReadyMessagesDetails?.Value, Is.EqualTo(1.0M));
            Assert.That(data3.UnacknowledgedMessages, Is.EqualTo(2));
            Assert.That(data3.UnacknowledgedMessagesDetails, Is.Not.Null);
            Assert.That(data3.UnacknowledgedMessagesDetails?.Value, Is.EqualTo(2.0M));
        });
    }

    [Test]
    public async Task Verify_Create_works1()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .Create("HareDu7",x =>
            {
                x.WithTracingEnabled();
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            VirtualHostRequest request = result.DebugInfo.Request.ToObject<VirtualHostRequest>();

            Assert.That(request.Tracing, Is.True);
        });
    }

    [Test]
    public async Task Verify_Create_works2()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateVirtualHost(x => x.UsingCredentials("guest", "guest"), "HareDu7",x =>
            {
                x.WithTracingEnabled();
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            VirtualHostRequest request = result.DebugInfo.Request.ToObject<VirtualHostRequest>();

            Assert.That(request.Tracing, Is.True);
        });
    }

    [Test]
    public async Task Verify_can_delete1()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .Delete("HareDu7");

        Assert.That(result.HasFaulted, Is.False);
    }

    [Test]
    public async Task Verify_can_delete2()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteVirtualHost(x => x.UsingCredentials("guest", "guest"), "HareDu7");

        Assert.That(result.HasFaulted, Is.False);
    }

    [Test]
    public async Task Verify_cannot_delete_1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .Delete(string.Empty);

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_can_start_vhost1()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .Startup("FakeVirtualHost", "FakeNode");

        Assert.That(result.HasFaulted, Is.False);
    }

    [Test]
    public async Task Verify_can_start_vhost2()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .StartupVirtualHost(x => x.UsingCredentials("guest", "guest"), "FakeVirtualHost", "FakeNode");

        Assert.That(result.HasFaulted, Is.False);
    }

    [Test]
    public async Task Verify_cannot_start_vhost_1()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .Startup(string.Empty, "FakeNode");

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_start_vhost_2()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .Startup("FakeVirtualHost", string.Empty);

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_start_vhost_3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .Startup(string.Empty, string.Empty);

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_can_delete_user_permissions1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .DeletePermissions("haredu_user", "HareDu5");

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task Verify_can_delete_user_permissions2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteVirtualHostUserPermissions(x => x.UsingCredentials("guest", "guest"), "haredu_user", "HareDu5");

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_user_permissions1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .DeletePermissions(string.Empty, "HareDu5");
            
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
            .DeleteVirtualHostUserPermissions(x => x.UsingCredentials("guest", "guest"), string.Empty, "HareDu5");
            
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
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .DeletePermissions("haredu_user", string.Empty);
            
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
            .DeleteVirtualHostUserPermissions(x => x.UsingCredentials("guest", "guest"), "haredu_user", string.Empty);
            
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
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .DeletePermissions(string.Empty, string.Empty);
            
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
            .DeleteVirtualHostUserPermissions(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_can_create_user_permissions1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .ApplyPermissions("haredu_user", "HareDu5", x =>
            {
                x.UsingConfigurePattern(".*");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(0));
            Assert.That(result.DebugInfo, Is.Not.Null);

            UserPermissionsRequest request = result.DebugInfo.Request.ToObject<UserPermissionsRequest>();
            
            Assert.That(request.Configure, Is.EqualTo(".*"));
            Assert.That(request.Write, Is.EqualTo(".*"));
            Assert.That(request.Read, Is.EqualTo(".*"));
        });
    }

    [Test]
    public async Task Verify_can_create_user_permissions2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .ApplyVirtualHostUserPermissions(x => x.UsingCredentials("guest", "guest"), "haredu_user", "HareDu5", x =>
            {
                x.UsingConfigurePattern(".*");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(0));
            Assert.That(result.DebugInfo, Is.Not.Null);

            UserPermissionsRequest request = result.DebugInfo.Request.ToObject<UserPermissionsRequest>();
            
            Assert.That(request.Configure, Is.EqualTo(".*"));
            Assert.That(request.Write, Is.EqualTo(".*"));
            Assert.That(request.Read, Is.EqualTo(".*"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_user_permissions1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .ApplyPermissions(string.Empty, "HareDu5", x =>
            {
                x.UsingConfigurePattern(".*");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
            Assert.That(result.DebugInfo, Is.Not.Null);

            UserPermissionsRequest request = result.DebugInfo.Request.ToObject<UserPermissionsRequest>();
            
            Assert.That(request.Configure, Is.EqualTo(".*"));
            Assert.That(request.Write, Is.EqualTo(".*"));
            Assert.That(request.Read, Is.EqualTo(".*"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_user_permissions2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .ApplyVirtualHostUserPermissions(x => x.UsingCredentials("guest", "guest"), string.Empty, "HareDu5", x =>
            {
                x.UsingConfigurePattern(".*");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
            Assert.That(result.DebugInfo, Is.Not.Null);

            UserPermissionsRequest request = result.DebugInfo.Request.ToObject<UserPermissionsRequest>();
            
            Assert.That(request.Configure, Is.EqualTo(".*"));
            Assert.That(request.Write, Is.EqualTo(".*"));
            Assert.That(request.Read, Is.EqualTo(".*"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_user_permissions3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .ApplyPermissions("haredu_user", string.Empty, x =>
            {
                x.UsingConfigurePattern(".*");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
            Assert.That(result.DebugInfo, Is.Not.Null);

            UserPermissionsRequest request = result.DebugInfo.Request.ToObject<UserPermissionsRequest>();
            
            Assert.That(request.Configure, Is.EqualTo(".*"));
            Assert.That(request.Write, Is.EqualTo(".*"));
            Assert.That(request.Read, Is.EqualTo(".*"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_user_permissions4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .ApplyVirtualHostUserPermissions(x => x.UsingCredentials("guest", "guest"), "haredu_user", string.Empty, x =>
            {
                x.UsingConfigurePattern(".*");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
            Assert.That(result.DebugInfo, Is.Not.Null);

            UserPermissionsRequest request = result.DebugInfo.Request.ToObject<UserPermissionsRequest>();
            
            Assert.That(request.Configure, Is.EqualTo(".*"));
            Assert.That(request.Write, Is.EqualTo(".*"));
            Assert.That(request.Read, Is.EqualTo(".*"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_user_permissions5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .ApplyPermissions(string.Empty, string.Empty, x =>
            {
                x.UsingConfigurePattern(".*");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
            Assert.That(result.DebugInfo, Is.Not.Null);

            UserPermissionsRequest request = result.DebugInfo.Request.ToObject<UserPermissionsRequest>();

            Assert.That(request.Configure, Is.EqualTo(".*"));
            Assert.That(request.Write, Is.EqualTo(".*"));
            Assert.That(request.Read, Is.EqualTo(".*"));
        });
    }

    [Test]
    public async Task Verify_cannot_create_user_permissions6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .ApplyVirtualHostUserPermissions(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty, x =>
            {
                x.UsingConfigurePattern(".*");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
            Assert.That(result.DebugInfo, Is.Not.Null);

            UserPermissionsRequest request = result.DebugInfo.Request.ToObject<UserPermissionsRequest>();

            Assert.That(request.Configure, Is.EqualTo(".*"));
            Assert.That(request.Write, Is.EqualTo(".*"));
            Assert.That(request.Read, Is.EqualTo(".*"));
        });
    }

    [Test]
    public async Task Verify_can_get_user_permissions()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetVirtualHostUserPermissions(x => x.UsingCredentials("guest", "guest"), "guest", "HareDu1");

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(0));
            Assert.That(result.DebugInfo, Is.Not.Null);
        });
    }
}