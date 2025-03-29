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
    ServiceProvider _services;
    
    [OneTimeSetUp]
    public void Setup()
    {
        _services = GetContainerBuilder().BuildServiceProvider();
    }

    [Test]
    public async Task Verify_can_get_all_limits1()
    {
        var services = GetContainerBuilder("TestData/VirtualHostLimitsInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .GetAllLimits();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(3, result.Data.Count);
            Assert.AreEqual("HareDu1", result.Data[0].VirtualHost);
            Assert.AreEqual(2, result.Data[0].Limits.Count);
            Assert.AreEqual(10, result.Data[0].Limits["max-connections"]);
            Assert.AreEqual(10, result.Data[0].Limits["max-queues"]);
        });
    }

    [Test]
    public async Task Verify_can_get_all_limits2()
    {
        var services = GetContainerBuilder("TestData/VirtualHostLimitsInfo.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerFactory>()
            .GetAllVirtualHostLimits();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(3, result.Data.Count);
            Assert.AreEqual("HareDu1", result.Data[0].VirtualHost);
            Assert.AreEqual(2, result.Data[0].Limits.Count);
            Assert.AreEqual(10, result.Data[0].Limits["max-connections"]);
            Assert.AreEqual(10, result.Data[0].Limits["max-queues"]);
        });
    }

    [Test]
    public async Task Verify_can_define_limits1()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .DefineLimit("HareDu5", x =>
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
        var result = await _services.GetService<IBrokerFactory>()
            .DefineVirtualHostLimit("HareDu5", x =>
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
        var result = await _services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .DefineLimit("HareDu5", x =>
            {
                x.SetMaxQueueLimit(0);
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            VirtualHostLimitsRequest request = result.DebugInfo.Request.ToObject<VirtualHostLimitsRequest>();
                
            Assert.AreEqual(0, request.Value);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits2()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .DefineVirtualHostLimit("HareDu5", x =>
            {
                x.SetMaxQueueLimit(0);
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            VirtualHostLimitsRequest request = result.DebugInfo.Request.ToObject<VirtualHostLimitsRequest>();

            Assert.AreEqual(0, request.Value);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits3()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .DefineLimit("HareDu5", x =>
            {
                x.SetMaxConnectionLimit(0);
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            VirtualHostLimitsRequest request = result.DebugInfo.Request.ToObject<VirtualHostLimitsRequest>();

            Assert.AreEqual(0, request.Value);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits4()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .DefineVirtualHostLimit("HareDu5", x =>
            {
                x.SetMaxConnectionLimit(0);
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            VirtualHostLimitsRequest request = result.DebugInfo.Request.ToObject<VirtualHostLimitsRequest>();

            Assert.AreEqual(0, request.Value);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits5()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .DefineLimit(string.Empty, x =>
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

            Assert.AreEqual(100, request.Value);
            Assert.AreEqual(1000, request.Value);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits6()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .DefineVirtualHostLimit(string.Empty, x =>
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
                
            Assert.AreEqual(100, request.Value);
            Assert.AreEqual(1000, request.Value);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits7()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .DefineLimit(string.Empty, x =>
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
                
            Assert.AreEqual(0, request.Value);
            Assert.AreEqual(1000, request.Value);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits8()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .DefineVirtualHostLimit(string.Empty, x =>
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
                
            Assert.AreEqual(0, request.Value);
            Assert.AreEqual(1000, request.Value);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits9()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .DefineLimit(string.Empty, x =>
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
                
            Assert.AreEqual(100, request.Value);
            Assert.AreEqual(0, request.Value);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits10()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .DefineVirtualHostLimit(string.Empty, x =>
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
                
            Assert.AreEqual(100, request.Value);
            Assert.AreEqual(0, request.Value);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits11()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .DefineLimit(string.Empty, x =>
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
                
            Assert.AreEqual(0, request.Value);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits12()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .DefineVirtualHostLimit(string.Empty, x =>
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
                
            Assert.AreEqual(0, request.Value);
        });
    }

    [Test]
    public async Task Verify_can_delete_limits1()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .DeleteLimit("HareDu", VirtualHostLimit.MaximumConnections);

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.AreEqual(0, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_can_delete_limits2()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .DeleteVirtualHostLimit("HareDu", VirtualHostLimit.MaximumQueues);

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.AreEqual(0, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_limits1()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .API<VirtualHost>()
            .DeleteLimit(string.Empty, VirtualHostLimit.MaximumQueues);

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_limits2()
    {
        var result = await _services.GetService<IBrokerFactory>()
            .DeleteVirtualHostLimit(string.Empty, VirtualHostLimit.MaximumConnections);

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }
}