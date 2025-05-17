namespace HareDu.Tests;

using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

[TestFixture]
public class ServerTests :
    HareDuTesting
{
    [Test]
    public async Task Verify_can_get_all_definitions1()
    {
        var result = await GetContainerBuilder("TestData/ServerDefinitionInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<Server>(x => x.UsingCredentials("guest", "guest"))
            .Get();
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Bindings.Count, Is.EqualTo(8));
            Assert.That(result.Data.Exchanges.Count, Is.EqualTo(11));
            Assert.That(result.Data.Queues.Count, Is.EqualTo(5));
            Assert.That(result.Data.Parameters.Count, Is.EqualTo(3));
            Assert.That(result.Data.Permissions.Count, Is.EqualTo(8));
            Assert.That(result.Data.Policies.Count, Is.EqualTo(2));
            Assert.That(result.Data.Users.Count, Is.EqualTo(2));
            Assert.That(result.Data.VirtualHosts.Count, Is.EqualTo(9));
            Assert.That(result.Data.GlobalParameters.Count, Is.EqualTo(5));
            Assert.That(result.Data.TopicPermissions.Count, Is.EqualTo(3));
            Assert.That(result.Data.RabbitMqVersion, Is.EqualTo("3.7.15"));
        });
    }

    [Test]
    public async Task Verify_can_get_all_definitions2()
    {
        var result = await GetContainerBuilder("TestData/ServerDefinitionInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetServerInformation(x => x.UsingCredentials("guest", "guest"));
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Bindings.Count, Is.EqualTo(8));
            Assert.That(result.Data.Exchanges.Count, Is.EqualTo(11));
            Assert.That(result.Data.Queues.Count, Is.EqualTo(5));
            Assert.That(result.Data.Parameters.Count, Is.EqualTo(3));
            Assert.That(result.Data.Permissions.Count, Is.EqualTo(8));
            Assert.That(result.Data.Policies.Count, Is.EqualTo(2));
            Assert.That(result.Data.Users.Count, Is.EqualTo(2));
            Assert.That(result.Data.VirtualHosts.Count, Is.EqualTo(9));
            Assert.That(result.Data.GlobalParameters.Count, Is.EqualTo(5));
            Assert.That(result.Data.TopicPermissions.Count, Is.EqualTo(3));
            Assert.That(result.Data.RabbitMqVersion, Is.EqualTo("3.7.15"));
        });
    }
}