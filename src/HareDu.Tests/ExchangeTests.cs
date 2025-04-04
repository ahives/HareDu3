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
            .API<Exchange>()
            .GetAll();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(9, result.Data.Count);
            Assert.IsTrue(result.Data[1].Durable);
            Assert.IsFalse(result.Data[1].Internal);
            Assert.IsFalse(result.Data[1].AutoDelete);
            Assert.AreEqual("E2", result.Data[1].Name);
            Assert.AreEqual(ExchangeRoutingType.Direct, result.Data[1].RoutingType);
            Assert.AreEqual("HareDu", result.Data[1].VirtualHost);
            Assert.IsNotNull(result.Data[1].Arguments);
            Assert.AreEqual(1, result.Data[1].Arguments.Count);
            Assert.AreEqual(result.Data[1].Arguments["alternate-exchange"].ToString(), "exchange");
        });
    }

    [Test]
    public async Task Should_be_able_to_get_all_exchanges2()
    {
        var result = await GetContainerBuilder("TestData/ExchangeInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetAllExchanges();
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(9, result.Data.Count);
            Assert.IsTrue(result.Data[1].Durable);
            Assert.IsFalse(result.Data[1].Internal);
            Assert.IsFalse(result.Data[1].AutoDelete);
            Assert.AreEqual("E2", result.Data[1].Name);
            Assert.AreEqual(ExchangeRoutingType.Direct, result.Data[1].RoutingType);
            Assert.AreEqual("HareDu", result.Data[1].VirtualHost);
            Assert.IsNotNull(result.Data[1].Arguments);
            Assert.AreEqual(1, result.Data[1].Arguments.Count);
            Assert.AreEqual(result.Data[1].Arguments["alternate-exchange"].ToString(), "exchange");
        });
    }

    [Test]
    public async Task Verify_can_create_exchange1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>()
            .Create("fake_exchange", "HareDu", x =>
            {
                x.IsDurable();
                x.IsForInternalUse();
                x.HasRoutingType(ExchangeRoutingType.Direct);
                x.HasArguments(arg =>
                {
                    arg.Add("fake_arg", "8238b");
                });
            });
            
        Console.WriteLine(result.ToJsonString());
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
                
            ExchangeRequest request = result.DebugInfo.Request.ToObject<ExchangeRequest>();

            Assert.IsTrue(request.Durable);
            Assert.IsTrue(request.Internal);
            Assert.AreEqual(1, request.Arguments.Count);
            Assert.IsFalse(request.AutoDelete);
            Assert.AreEqual("api/exchanges/HareDu/fake_exchange", result.DebugInfo.URL);
            Assert.AreEqual(ExchangeRoutingType.Direct, request.RoutingType);
            Assert.AreEqual("8238b", request.Arguments["fake_arg"].ToString());
        });
    }

    [Test]
    public async Task Verify_can_create_exchange2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateExchange("fake_exchange", "HareDu", x =>
            {
                x.IsDurable();
                x.IsForInternalUse();
                x.HasRoutingType(ExchangeRoutingType.Fanout);
                x.HasArguments(arg =>
                {
                    arg.Add("fake_arg", "8238b");
                });
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
                
            ExchangeRequest request = result.DebugInfo.Request.ToObject<ExchangeRequest>();

            Assert.IsTrue(request.Durable);
            Assert.IsTrue(request.Internal);
            Assert.AreEqual(1, request.Arguments.Count);
            Assert.IsFalse(request.AutoDelete);
            Assert.AreEqual("api/exchanges/HareDu/fake_exchange", result.DebugInfo.URL);
            Assert.AreEqual(ExchangeRoutingType.Fanout, request.RoutingType);
            Assert.AreEqual("8238b", request.Arguments["fake_arg"].ToString());
        });
    }

    [Test]
    public async Task Verify_can_create_exchange3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateExchange("fake_exchange", "HareDu", x =>
            {
                x.IsDurable();
                x.IsForInternalUse();
                x.HasRoutingType(ExchangeRoutingType.Fanout);
                x.HasArguments(null);
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
                
            ExchangeRequest request = result.DebugInfo.Request.ToObject<ExchangeRequest>();

            Assert.IsTrue(request.Durable);
            Assert.IsTrue(request.Internal);
            Assert.AreEqual(1, request.Arguments.Count);
            Assert.IsFalse(request.AutoDelete);
            Assert.AreEqual("api/exchanges/HareDu/fake_exchange", result.DebugInfo.URL);
            Assert.AreEqual(ExchangeRoutingType.Fanout, request.RoutingType);
            Assert.AreEqual("8238b", request.Arguments["fake_arg"].ToString());
        });
    }

    [Test]
    public async Task Verify_cannot_create_exchange_1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>()
            .Create(string.Empty, "HareDu", x =>
            {
                x.IsDurable();
                x.IsForInternalUse();
                x.HasRoutingType(ExchangeRoutingType.Fanout);
                x.HasArguments(arg =>
                {
                    arg.Add("fake_arg", "8238b");
                });
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual("api/exchanges/HareDu/", result.DebugInfo.URL);
        });
    }

    [Test]
    public async Task Verify_cannot_create_exchange_2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>()
            .Create(string.Empty, "HareDu", x =>
            {
                x.IsDurable();
                x.IsForInternalUse();
                x.HasRoutingType(ExchangeRoutingType.Fanout);
                x.HasArguments(arg =>
                {
                    arg.Add("fake_arg", "8238b");
                });
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual("api/exchanges/HareDu/", result.DebugInfo.URL);
        });
    }

    [Test]
    public async Task Verify_cannot_create_exchange_3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>()
            .Create("fake_exchange", string.Empty, x =>
            {
                x.IsDurable();
                x.IsForInternalUse();
                x.HasRoutingType(ExchangeRoutingType.Fanout);
                x.HasArguments(arg =>
                {
                    arg.Add("fake_arg", "8238b");
                });
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual("api/exchanges//fake_exchange", result.DebugInfo.URL);
        });
    }

    [Test]
    public async Task Verify_can_delete_exchange1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>()
            .Delete("E3", "HareDu", x =>
            {
                x.WhenUnused();
            });
            
        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_can_delete_exchange2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteExchange("E3", "HareDu", x =>
            {
                x.WhenUnused();
            });
            
        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>()
            .Delete(string.Empty, "HareDu", x =>
            {
                x.WhenUnused();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>()
            .Delete("E3", string.Empty, x =>
            {
                x.WhenUnused();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>()
            .Delete("E3", string.Empty, x =>
            {
                x.WhenUnused();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_exchange_4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Exchange>()
            .Delete(string.Empty, string.Empty, x =>
            {
                x.WhenUnused();
            });
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }
}