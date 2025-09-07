namespace HareDu.Tests;

using System.Threading.Tasks;
using Core;
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
            .GetService<IHareDuFactory>()
            .API<Binding>(x => x.UsingCredentials("guest", "guest"))
            .GetAll();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data.Count, Is.EqualTo(12));
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.Data, Is.Not.Null);
        });
    }

    [Test]
    public async Task Verify_able_to_get_all_bindings2()
    {
        var result = await GetContainerBuilder("TestData/BindingInfo.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .GetAllBindings(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data.Count, Is.EqualTo(12));
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.Data, Is.Not.Null);
        });
    }
}