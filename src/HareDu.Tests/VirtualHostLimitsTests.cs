namespace HareDu.Tests;

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
public class VirtualHostLimitsTests :
    HareDuTesting
{
    readonly IHareDuDeserializer _deserializer;

    public VirtualHostLimitsTests()
    {
        _deserializer = new BrokerDeserializer();
    }

    [OneTimeSetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Verify_can_get_all_limits1()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostLimitsInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .GetAllLimits();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(3));
            Assert.That(result.Data[0].VirtualHost, Is.EqualTo("HareDu1"));;
            Assert.That(result.Data[0].Limits.Count, Is.EqualTo(2));;
            Assert.That(result.Data[0].Limits["max-connections"], Is.EqualTo(10));;
            Assert.That(result.Data[0].Limits["max-queues"], Is.EqualTo(10));
        });
    }

    [Test]
    public async Task Verify_can_get_all_limits2()
    {
        var result = await GetContainerBuilder("TestData/VirtualHostLimitsInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetAllVirtualHostLimits(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(3));
            Assert.That(result.Data[0].VirtualHost, Is.EqualTo("HareDu1"));;
            Assert.That(result.Data[0].Limits.Count, Is.EqualTo(2));;
            Assert.That(result.Data[0].Limits["max-connections"], Is.EqualTo(10));;
            Assert.That(result.Data[0].Limits["max-queues"], Is.EqualTo(10));
        });
    }

    [Test]
    public async Task Verify_can_define_limits1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .DefineLimit("HareDu5", x =>
            {
                x.SetMaxQueueLimit(1);
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(0));
            Assert.That(result.DebugInfo, Is.Not.Null);
        });
    }

    [Test]
    public async Task Verify_can_define_limits2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DefineVirtualHostLimit(x => x.UsingCredentials("guest", "guest"), "HareDu5", x =>
            {
                x.SetMaxQueueLimit(1);
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(0));
            Assert.That(result.DebugInfo, Is.Not.Null);
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .DefineLimit("HareDu5", x =>
            {
                x.SetMaxQueueLimit(0);
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
            Assert.That(result.DebugInfo, Is.Not.Null);

            VirtualHostLimitsRequest request = _deserializer.ToObject<VirtualHostLimitsRequest>(result.DebugInfo.Request);

            Assert.That(request.Value, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DefineVirtualHostLimit(x => x.UsingCredentials("guest", "guest"), "HareDu5", x =>
            {
                x.SetMaxQueueLimit(0);
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
            Assert.That(result.DebugInfo, Is.Not.Null);

            VirtualHostLimitsRequest request = _deserializer.ToObject<VirtualHostLimitsRequest>(result.DebugInfo.Request);

            Assert.That(request.Value, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .DefineLimit("HareDu5", x =>
            {
                x.SetMaxConnectionLimit(0);
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
            Assert.That(result.DebugInfo, Is.Not.Null);

            VirtualHostLimitsRequest request = _deserializer.ToObject<VirtualHostLimitsRequest>(result.DebugInfo.Request);

            Assert.That(request.Value, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DefineVirtualHostLimit(x => x.UsingCredentials("guest", "guest"), "HareDu5", x =>
            {
                x.SetMaxConnectionLimit(0);
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
            Assert.That(result.DebugInfo, Is.Not.Null);

            VirtualHostLimitsRequest request = _deserializer.ToObject<VirtualHostLimitsRequest>(result.DebugInfo.Request);

            Assert.That(request.Value, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .DefineLimit(string.Empty, x =>
            {
                x.SetMaxQueueLimit(100);
                x.SetMaxConnectionLimit(1000);
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
            Assert.That(result.DebugInfo, Is.Not.Null);

            VirtualHostLimitsRequest request = _deserializer.ToObject<VirtualHostLimitsRequest>(result.DebugInfo.Request);

            Assert.That(request.Value, Is.EqualTo(1000));
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DefineVirtualHostLimit(x => x.UsingCredentials("guest", "guest"), string.Empty, x =>
            {
                x.SetMaxQueueLimit(100);
                x.SetMaxConnectionLimit(1000);
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
            Assert.That(result.DebugInfo, Is.Not.Null);

            VirtualHostLimitsRequest request = _deserializer.ToObject<VirtualHostLimitsRequest>(result.DebugInfo.Request);

            Assert.That(request.Value, Is.EqualTo(1000));
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits7()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .DefineLimit(string.Empty, x =>
            {
                x.SetMaxQueueLimit(0);
                x.SetMaxConnectionLimit(1000);
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
            Assert.That(result.DebugInfo, Is.Not.Null);

            VirtualHostLimitsRequest request = _deserializer.ToObject<VirtualHostLimitsRequest>(result.DebugInfo.Request);

            Assert.That(request.Value, Is.EqualTo(1000));
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits8()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DefineVirtualHostLimit(x => x.UsingCredentials("guest", "guest"), string.Empty, x =>
            {
                x.SetMaxQueueLimit(0);
                x.SetMaxConnectionLimit(1000);
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
            Assert.That(result.DebugInfo, Is.Not.Null);

            VirtualHostLimitsRequest request = _deserializer.ToObject<VirtualHostLimitsRequest>(result.DebugInfo.Request);

            Assert.That(request.Value, Is.EqualTo(1000));
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits9()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .DefineLimit(string.Empty, x =>
            {
                x.SetMaxQueueLimit(100);
                x.SetMaxConnectionLimit(0);
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
            Assert.That(result.DebugInfo, Is.Not.Null);

            VirtualHostLimitsRequest request = _deserializer.ToObject<VirtualHostLimitsRequest>(result.DebugInfo.Request);

            Assert.That(request.Value, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits10()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DefineVirtualHostLimit(x => x.UsingCredentials("guest", "guest"), string.Empty, x =>
            {
                x.SetMaxQueueLimit(100);
                x.SetMaxConnectionLimit(0);
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
            Assert.That(result.DebugInfo, Is.Not.Null);

            VirtualHostLimitsRequest request = _deserializer.ToObject<VirtualHostLimitsRequest>(result.DebugInfo.Request);

            Assert.That(request.Value, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits11()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .DefineLimit(string.Empty, x =>
            {
                x.SetMaxQueueLimit(0);
                x.SetMaxConnectionLimit(0);
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(3));
            Assert.That(result.DebugInfo, Is.Not.Null);

            VirtualHostLimitsRequest request = _deserializer.ToObject<VirtualHostLimitsRequest>(result.DebugInfo.Request);

            Assert.That(request.Value, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task Verify_cannot_define_limits12()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DefineVirtualHostLimit(x => x.UsingCredentials("guest", "guest"), string.Empty, x =>
            {
                x.SetMaxQueueLimit(0);
                x.SetMaxConnectionLimit(0);
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(3));
            Assert.That(result.DebugInfo, Is.Not.Null);

            VirtualHostLimitsRequest request = _deserializer.ToObject<VirtualHostLimitsRequest>(result.DebugInfo.Request);

            Assert.That(request.Value, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task Verify_can_delete_limits1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .DeleteLimit("HareDu", VirtualHostLimit.MaxConnections);

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task Verify_can_delete_limits2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteVirtualHostLimit(x => x.UsingCredentials("guest", "guest"), "HareDu", VirtualHostLimit.MaxQueues);

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_limits1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .DeleteLimit(string.Empty, VirtualHostLimit.MaxQueues);

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_limits2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteVirtualHostLimit(x => x.UsingCredentials("guest", "guest"), string.Empty, VirtualHostLimit.MaxConnections);

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }
}