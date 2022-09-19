namespace HareDu.Tests;

using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

[TestFixture]
public class UserLimitsTest :
    HareDuTesting
{
    [Test]
    public async Task Verify_can_get_max_connections()
    {
        var services = GetContainerBuilder("TestData/UserLimitsMaxConnections.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerObjectFactory>()
            .Object<UserLimits>()
            .GetMaxConnections("guest");

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(55, result.Data.Value);
        });
    }

    [Test]
    public async Task Verify_can_get_max_channels()
    {
        var services = GetContainerBuilder("TestData/UserLimitsMaxChannels.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerObjectFactory>()
            .Object<UserLimits>()
            .GetMaxConnections("guest");

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(50, result.Data.Value);
        });
    }
}