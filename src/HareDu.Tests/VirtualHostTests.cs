namespace HareDu.Tests;

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
            Assert.IsTrue(result.HasData);
            Assert.IsFalse(result.HasFaulted);
            Assert.AreEqual(3, result.Data.Count);
            Assert.AreEqual("TestVirtualHost", result.Data[2].Name);
            Assert.AreEqual(0, result.Data[2].TotalMessages);
            Assert.IsNotNull(result.Data[2].MessagesDetails);
            Assert.AreEqual(0.0M, result.Data[2].MessagesDetails?.Value);
            Assert.AreEqual(0, result.Data[2].ReadyMessages);
            Assert.IsNotNull(result.Data[2].ReadyMessagesDetails);
            Assert.AreEqual(0.0M, result.Data[2].ReadyMessagesDetails?.Value);
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
            Assert.IsTrue(result.HasData);
            Assert.IsFalse(result.HasFaulted);
            Assert.AreEqual(3, result.Data.Count);
            Assert.AreEqual("TestVirtualHost", result.Data[2].Name);
            Assert.AreEqual(0, result.Data[2].TotalMessages);
            Assert.IsNotNull(result.Data[2].MessagesDetails);
            Assert.AreEqual(0.0M, result.Data[2].MessagesDetails?.Value);
            Assert.AreEqual(0, result.Data[2].ReadyMessages);
            Assert.IsNotNull(result.Data[2].ReadyMessagesDetails);
            Assert.AreEqual(0.0M, result.Data[2].ReadyMessagesDetails?.Value);
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
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
                
            VirtualHostRequest request = result.DebugInfo.Request.ToObject<VirtualHostRequest>();

            Assert.IsTrue(request.Tracing);
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
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
                
            VirtualHostRequest request = result.DebugInfo.Request.ToObject<VirtualHostRequest>();

            Assert.IsTrue(request.Tracing);
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

        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_can_delete2()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteVirtualHost(x => x.UsingCredentials("guest", "guest"), "HareDu7");

        Assert.IsFalse(result.HasFaulted);
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
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
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

        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_can_start_vhost2()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .StartupVirtualHost(x => x.UsingCredentials("guest", "guest"), "FakeVirtualHost", "FakeNode");

        Assert.IsFalse(result.HasFaulted);
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
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
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
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
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
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }
}