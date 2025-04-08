namespace HareDu.Tests;

using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

[TestFixture]
public class BindingTests :
    HareDuTesting
{
    [Test]
    public async Task Verify_able_to_get_all_bindings1()
    {
        var result = await GetContainerBuilder("TestData/BindingInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
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
        var result = await GetContainerBuilder("TestData/BindingInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetAllBindings();

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasData);
            Assert.AreEqual(12, result.Data.Count);
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.Data);
        });
    }
}