namespace HareDu.Tests;

using System;
using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;

[TestFixture]
public class ExchangeTests :
    HareDuTesting
{
    [Test]
    public async Task Should_be_able_to_get_all_exchanges1()
    {
        var result = await GetContainerBuilder("TestData/ExchangeInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .GetAll();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(9));
            Assert.That(result.Data[1].Durable, Is.True);
            Assert.That(result.Data[1].Internal, Is.False);
            Assert.That(result.Data[1].AutoDelete, Is.False);
            Assert.That(result.Data[1].Name, Is.EqualTo("E2"));
            Assert.That(result.Data[1].RoutingType, Is.EqualTo(RoutingType.Direct));
            Assert.That(result.Data[1].VirtualHost, Is.EqualTo("HareDu"));
            Assert.That(result.Data[1].Arguments, Is.Not.Null);
            Assert.That(result.Data[1].Arguments.Count, Is.EqualTo(1));
            Assert.That(result.Data[1].Arguments["alternate-exchange"].ToString(), Is.EqualTo("exchange"));
        });
    }

    [Test]
    public async Task Should_be_able_to_get_all_exchanges2()
    {
        var result = await GetContainerBuilder("TestData/ExchangeInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetAllExchanges(x => x.UsingCredentials("guest", "guest"));
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(9));
            Assert.That(result.Data[1].Durable, Is.True);
            Assert.That(result.Data[1].Internal, Is.False);
            Assert.That(result.Data[1].AutoDelete, Is.False);
            Assert.That(result.Data[1].Name, Is.EqualTo("E2"));
            Assert.That(result.Data[1].RoutingType, Is.EqualTo(RoutingType.Direct));
            Assert.That(result.Data[1].VirtualHost, Is.EqualTo("HareDu"));
            Assert.That(result.Data[1].Arguments, Is.Not.Null);
            Assert.That(result.Data[1].Arguments.Count, Is.EqualTo(1));
            Assert.That(result.Data[1].Arguments["alternate-exchange"].ToString(), Is.EqualTo("exchange"));
        });
    }

    [Test]
    public async Task Verify_can_create_exchange1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Create("fake_exchange", "HareDu", x =>
            {
                x.IsDurable();
                x.IsForInternalUse();
                x.WithRoutingType(RoutingType.Direct);
                x.HasArguments(arg =>
                {
                    arg.Add("fake_arg", "8238b");
                });
            });
            
        Console.WriteLine(result.ToJsonString());
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            ExchangeRequest request = result.DebugInfo.Request.ToObject<ExchangeRequest>();

            Assert.That(request.Durable, Is.True);
            Assert.That(request.Internal, Is.True);
            Assert.That(request.Arguments.Count, Is.EqualTo(1));
            Assert.That(request.AutoDelete, Is.False);
            Assert.That(result.DebugInfo.Url, Is.EqualTo("api/exchanges/HareDu/fake_exchange"));
            Assert.That(request.RoutingType, Is.EqualTo(RoutingType.Direct));
            Assert.That(request.Arguments["fake_arg"].ToString(), Is.EqualTo("8238b"));
        });
    }

    [Test]
    public async Task Verify_can_create_exchange2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateExchange(x => x.UsingCredentials("guest", "guest"), "fake_exchange", "HareDu", x =>
            {
                x.IsDurable();
                x.IsForInternalUse();
                x.WithRoutingType(RoutingType.Fanout);
                x.HasArguments(arg =>
                {
                    arg.Add("fake_arg", "8238b");
                });
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            ExchangeRequest request = result.DebugInfo.Request.ToObject<ExchangeRequest>();

            Assert.That(request.Durable, Is.True);
            Assert.That(request.Internal, Is.True);
            Assert.That(request.Arguments.Count, Is.EqualTo(1));
            Assert.That(request.AutoDelete, Is.False);
            Assert.That(result.DebugInfo.Url, Is.EqualTo("api/exchanges/HareDu/fake_exchange"));
            Assert.That(request.RoutingType, Is.EqualTo(RoutingType.Fanout));
            Assert.That(request.Arguments["fake_arg"].ToString(), Is.EqualTo("8238b"));
        });
    }

    [Test]
    public async Task Verify_can_create_exchange3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateExchange(x => x.UsingCredentials("guest", "guest"), "fake_exchange", "HareDu", x =>
            {
                x.IsDurable();
                x.IsForInternalUse();
                x.WithRoutingType(RoutingType.Fanout);
                x.HasArguments(null);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            ExchangeRequest request = result.DebugInfo.Request.ToObject<ExchangeRequest>();

            Assert.That(request.Durable, Is.True);
            Assert.That(request.Internal, Is.True);
            Assert.That(request.Arguments, Is.Null);
            Assert.That(request.AutoDelete, Is.False);
            Assert.That(result.DebugInfo.Url, Is.EqualTo("api/exchanges/HareDu/fake_exchange"));
            Assert.That(request.RoutingType, Is.EqualTo(RoutingType.Fanout));
        });
    }

    [Test]
    public async Task Verify_cannot_create_exchange_1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Create(string.Empty, "HareDu", x =>
            {
                x.IsDurable();
                x.IsForInternalUse();
                x.WithRoutingType(RoutingType.Fanout);
                x.HasArguments(arg =>
                {
                    arg.Add("fake_arg", "8238b");
                });
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
            Assert.That(result.DebugInfo, Is.Not.Null);
        });
    }

    [Test]
    public async Task Verify_cannot_create_exchange_2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Create(string.Empty, "HareDu", x =>
            {
                x.IsDurable();
                x.IsForInternalUse();
                x.WithRoutingType(RoutingType.Fanout);
                x.HasArguments(arg =>
                {
                    arg.Add("fake_arg", "8238b");
                });
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
            Assert.That(result.DebugInfo, Is.Not.Null);
        });
    }

    [Test]
    public async Task Verify_cannot_create_exchange_3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Create("fake_exchange", string.Empty, x =>
            {
                x.IsDurable();
                x.IsForInternalUse();
                x.WithRoutingType(RoutingType.Fanout);
                x.HasArguments(arg =>
                {
                    arg.Add("fake_arg", "8238b");
                });
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
            Assert.That(result.DebugInfo, Is.Not.Null);
        });
    }

    [Test]
    public async Task Verify_can_delete_exchange1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Delete("E3", "HareDu", x =>
            {
                x.WhenUnused();
            });
            
        Assert.That(result.HasFaulted, Is.False);
    }

    [Test]
    public async Task Verify_can_delete_exchange2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteExchange(x => x.UsingCredentials("guest", "guest"), "E3", "HareDu", x =>
            {
                x.WhenUnused();
            });
            
        Assert.That(result.HasFaulted, Is.False);
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Delete(string.Empty, "HareDu", x =>
            {
                x.WhenUnused();
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Delete("E3", string.Empty, x =>
            {
                x.WhenUnused();
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Delete("E3", string.Empty, x =>
            {
                x.WhenUnused();
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Delete(string.Empty, string.Empty, x =>
            {
                x.WhenUnused();
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_can_delete_queue_binding1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Unbind("HareDu", x =>
            {
                x.Source("E1");
                x.Destination("Q1");
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task Verify_can_delete_queue_binding3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Unbind("HareDu", x =>
            {
                x.Source("E1");
                x.Destination("Q1");
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Unbind("HareDu", x =>
            {
                x.Source("E2");
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .UnbindFromExchange(x => x.UsingCredentials("guest", "guest"), "HareDu", x =>
            {
                x.Source("E2");
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Unbind("HareDu", x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .UnbindFromExchange(x => x.UsingCredentials("guest", "guest"), "HareDu", x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Unbind(string.Empty, x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(3));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_queue_binding6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .UnbindFromExchange(x => x.UsingCredentials("guest", "guest"), string.Empty, x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(3));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Unbind("HareDu", x =>
            {
                x.Source("E2");
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .UnbindFromExchange(x => x.UsingCredentials("guest", "guest"), "HareDu", x =>
            {
                x.Source("E2");
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Unbind("HareDu", x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .UnbindFromExchange(x => x.UsingCredentials("guest", "guest"), "HareDu", x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Unbind(string.Empty, x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(3));
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_binding6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .UnbindFromExchange(x => x.UsingCredentials("guest", "guest"), string.Empty, x =>
            {
                x.Source(string.Empty);
                x.Destination(string.Empty);
            });
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(3));
        });
    }
}