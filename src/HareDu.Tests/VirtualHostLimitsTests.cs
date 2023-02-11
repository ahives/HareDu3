namespace HareDu.Tests;

using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;

[TestFixture]
public class VirtualHostLimitsTests :
    HareDuTesting
{
    [Test]
    public async Task Verify_can_get_all_limits1()
    {
        var services = GetContainerBuilder("TestData/VirtualHostLimitsInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .Object<VirtualHostLimits>()
            .GetAll();
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(3, result.Data.Count);
            Assert.AreEqual("HareDu1", result.Data[0].VirtualHostName);
            Assert.AreEqual(2, result.Data[0].Limits.Count);
            Assert.AreEqual(10, result.Data[0].Limits["max-connections"]);
            Assert.AreEqual(10, result.Data[0].Limits["max-queues"]);
        });
    }

    [Test]
    public async Task Verify_can_get_all_limits2()
    {
        var services = GetContainerBuilder("TestData/VirtualHostLimitsInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .GetAllVirtualHostLimits();
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(3, result.Data.Count);
            Assert.AreEqual("HareDu1", result.Data[0].VirtualHostName);
            Assert.AreEqual(2, result.Data[0].Limits.Count);
            Assert.AreEqual(10, result.Data[0].Limits["max-connections"]);
            Assert.AreEqual(10, result.Data[0].Limits["max-queues"]);
        });
    }

    [Test]
    public async Task Verify_can_define_limits1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .Object<VirtualHostLimits>()
            .Define("HareDu5", x =>
            {
                x.SetMaxQueueLimit(1);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.AreEqual(0, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);
        });
    }

    [Test]
    public async Task Verify_can_define_limits2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DefineVirtualHostLimits("HareDu5", x =>
            {
                x.SetMaxQueueLimit(1);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.AreEqual(0, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .Object<VirtualHostLimits>()
            .Define("HareDu5", x =>
            {
                x.SetMaxQueueLimit(0);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            VirtualHostLimitsRequest request = result.DebugInfo.Request.ToObject<VirtualHostLimitsRequest>();
                
            Assert.AreEqual(0, request.MaxQueueLimit);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DefineVirtualHostLimits("HareDu5", x =>
            {
                x.SetMaxQueueLimit(0);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            VirtualHostLimitsRequest request = result.DebugInfo.Request.ToObject<VirtualHostLimitsRequest>();
                
            Assert.AreEqual(0, request.MaxQueueLimit);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits3()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .Object<VirtualHostLimits>()
            .Define("HareDu5", x =>
            {
                x.SetMaxConnectionLimit(0);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            VirtualHostLimitsRequest request = result.DebugInfo.Request.ToObject<VirtualHostLimitsRequest>();
                
            Assert.AreEqual(0, request.MaxConnectionLimit);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits4()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DefineVirtualHostLimits("HareDu5", x =>
            {
                x.SetMaxConnectionLimit(0);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            VirtualHostLimitsRequest request = result.DebugInfo.Request.ToObject<VirtualHostLimitsRequest>();
                
            Assert.AreEqual(0, request.MaxConnectionLimit);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits5()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .Object<VirtualHostLimits>()
            .Define(string.Empty, x =>
            {
                x.SetMaxQueueLimit(100);
                x.SetMaxConnectionLimit(1000);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            VirtualHostLimitsRequest request = result.DebugInfo.Request.ToObject<VirtualHostLimitsRequest>();
                
            Assert.AreEqual(100, request.MaxQueueLimit);
            Assert.AreEqual(1000, request.MaxConnectionLimit);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits6()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DefineVirtualHostLimits(string.Empty, x =>
            {
                x.SetMaxQueueLimit(100);
                x.SetMaxConnectionLimit(1000);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            VirtualHostLimitsRequest request = result.DebugInfo.Request.ToObject<VirtualHostLimitsRequest>();
                
            Assert.AreEqual(100, request.MaxQueueLimit);
            Assert.AreEqual(1000, request.MaxConnectionLimit);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits7()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .Object<VirtualHostLimits>()
            .Define(string.Empty, x =>
            {
                x.SetMaxQueueLimit(0);
                x.SetMaxConnectionLimit(1000);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            VirtualHostLimitsRequest request = result.DebugInfo.Request.ToObject<VirtualHostLimitsRequest>();
                
            Assert.AreEqual(0, request.MaxQueueLimit);
            Assert.AreEqual(1000, request.MaxConnectionLimit);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits8()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DefineVirtualHostLimits(string.Empty, x =>
            {
                x.SetMaxQueueLimit(0);
                x.SetMaxConnectionLimit(1000);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            VirtualHostLimitsRequest request = result.DebugInfo.Request.ToObject<VirtualHostLimitsRequest>();
                
            Assert.AreEqual(0, request.MaxQueueLimit);
            Assert.AreEqual(1000, request.MaxConnectionLimit);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits9()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .Object<VirtualHostLimits>()
            .Define(string.Empty, x =>
            {
                x.SetMaxQueueLimit(100);
                x.SetMaxConnectionLimit(0);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            VirtualHostLimitsRequest request = result.DebugInfo.Request.ToObject<VirtualHostLimitsRequest>();
                
            Assert.AreEqual(100, request.MaxQueueLimit);
            Assert.AreEqual(0, request.MaxConnectionLimit);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits10()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DefineVirtualHostLimits(string.Empty, x =>
            {
                x.SetMaxQueueLimit(100);
                x.SetMaxConnectionLimit(0);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            VirtualHostLimitsRequest request = result.DebugInfo.Request.ToObject<VirtualHostLimitsRequest>();
                
            Assert.AreEqual(100, request.MaxQueueLimit);
            Assert.AreEqual(0, request.MaxConnectionLimit);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits11()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .Object<VirtualHostLimits>()
            .Define(string.Empty, x =>
            {
                x.SetMaxQueueLimit(0);
                x.SetMaxConnectionLimit(0);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(3, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            VirtualHostLimitsRequest request = result.DebugInfo.Request.ToObject<VirtualHostLimitsRequest>();
                
            Assert.AreEqual(0, request.MaxQueueLimit);
            Assert.AreEqual(0, request.MaxConnectionLimit);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits12()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DefineVirtualHostLimits(string.Empty, x =>
            {
                x.SetMaxQueueLimit(0);
                x.SetMaxConnectionLimit(0);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(3, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            VirtualHostLimitsRequest request = result.DebugInfo.Request.ToObject<VirtualHostLimitsRequest>();
                
            Assert.AreEqual(0, request.MaxQueueLimit);
            Assert.AreEqual(0, request.MaxConnectionLimit);
        });
    }

    [Test]
    public async Task Verify_can_delete_limits1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .Object<VirtualHostLimits>()
            .Delete("HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.AreEqual(0, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_can_delete_limits2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DeleteVirtualHostLimits("HareDu");
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.AreEqual(0, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_limits1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .Object<VirtualHostLimits>()
            .Delete(string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_limits2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DeleteVirtualHostLimits(string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }
}