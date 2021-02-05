namespace HareDu.Tests
{
    using Core.Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class ServerTests :
        HareDuTesting
    {
        [Test]
        public void Verify_can_get_all_definitions()
        {
            var services = GetContainerBuilder("TestData/ServerDefinitionInfo.json").BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<Server>()
                .Get()
                .GetResult();
            
            result.HasFaulted.ShouldBeFalse();
            result.HasData.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Bindings.Count.ShouldBe(8);
            result.Data.Exchanges.Count.ShouldBe(11);
            result.Data.Queues.Count.ShouldBe(5);
            result.Data.Parameters.Count.ShouldBe(3);
            result.Data.Permissions.Count.ShouldBe(8);
            result.Data.Policies.Count.ShouldBe(2);
            result.Data.Users.Count.ShouldBe(2);
            result.Data.VirtualHosts.Count.ShouldBe(9);
            result.Data.GlobalParameters.Count.ShouldBe(5);
            result.Data.TopicPermissions.Count.ShouldBe(3);
            result.Data.RabbitMqVersion.ShouldBe("3.7.15");
        }
    }
}