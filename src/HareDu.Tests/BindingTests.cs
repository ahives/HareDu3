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
            .API<Binding>(x => x.UsingCredentials("guest", "guest"))
            .GetAll();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasData);
            Assert.That(result.Data.Count == 12);
            Assert.That(result.HasFaulted);
            Assert.That(result.Data != null);
        });
    }

    [Test]
    public async Task Verify_able_to_get_all_bindings2()
    {
        var result = await GetContainerBuilder("TestData/BindingInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetAllBindings(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasData);
            Assert.That(result.Data.Count == 12);
            Assert.That(!result.HasFaulted);
            Assert.That(result.Data != null);
        });
    }
}