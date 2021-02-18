namespace HareDu.Tests
{
    using System.Threading.Tasks;
    using Core.Extensions;
    using Core.Serialization;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using Model;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class TopicPermissionsTest :
        HareDuTesting
    {
        [Test]
        public async Task Verify_can_get_all_topic_permissions1()
        {
            var services = GetContainerBuilder("TestData/TopicPermissionsInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .GetAll();
            
            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public async Task Verify_can_get_all_topic_permissions2()
        {
            var services = GetContainerBuilder("TestData/TopicPermissionsInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .GetAllTopicPermissions();
            
            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public async Task Verify_can_create_user_permissions1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Create("guest", "HareDu", x =>
                {
                    x.OnExchange("E4");
                    x.UsingReadPattern(".*");
                    x.UsingWritePattern(".*");
                });

            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public async Task Verify_can_create_user_permissions2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .CreateTopicPermission("guest", "HareDu", x =>
                {
                    x.OnExchange("E4");
                    x.UsingReadPattern(".*");
                    x.UsingWritePattern(".*");
                });

            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public async Task Verify_cannot_create_topic_permissions_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Create(string.Empty, "HareDu", x =>
                {
                    x.OnExchange("E4");
                    x.UsingReadPattern(".*");
                    x.UsingWritePattern(".*");
                });

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();

            TopicPermissionsDefinition definition = result.DebugInfo.Request.ToObject<TopicPermissionsDefinition>(Deserializer.Options);
            
            definition.Exchange.ShouldBe("E4");
            definition.Read.ShouldBe(".*");
            definition.Write.ShouldBe(".*");
        }

        [Test]
        public async Task Verify_cannot_create_topic_permissions_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Create(string.Empty, "HareDu", x =>
                {
                    x.OnExchange("E4");
                    x.UsingReadPattern(".*");
                    x.UsingWritePattern(".*");
                });

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();

            TopicPermissionsDefinition definition = result.DebugInfo.Request.ToObject<TopicPermissionsDefinition>(Deserializer.Options);
            
            definition.Exchange.ShouldBe("E4");
            definition.Read.ShouldBe(".*");
            definition.Write.ShouldBe(".*");
        }

        [Test]
        public async Task Verify_cannot_create_topic_permissions_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Create("guest", string.Empty, x =>
                {
                    x.OnExchange("E4");
                    x.UsingReadPattern(".*");
                    x.UsingWritePattern(".*");
                });

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();

            TopicPermissionsDefinition definition = result.DebugInfo.Request.ToObject<TopicPermissionsDefinition>(Deserializer.Options);
            
            definition.Exchange.ShouldBe("E4");
            definition.Read.ShouldBe(".*");
            definition.Write.ShouldBe(".*");
        }

        [Test]
        public async Task Verify_cannot_create_topic_permissions_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Create("guest", string.Empty, x =>
                {
                    x.OnExchange("E4");
                    x.UsingReadPattern(".*");
                    x.UsingWritePattern(".*");
                });

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();

            TopicPermissionsDefinition definition = result.DebugInfo.Request.ToObject<TopicPermissionsDefinition>(Deserializer.Options);
            
            definition.Exchange.ShouldBe("E4");
            definition.Read.ShouldBe(".*");
            definition.Write.ShouldBe(".*");
        }

        [Test]
        public async Task Verify_cannot_create_topic_permissions_5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Create("guest", "HareDu", x =>
                {
                    x.OnExchange(string.Empty);
                    x.UsingReadPattern(".*");
                    x.UsingWritePattern(".*");
                });

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();

            TopicPermissionsDefinition definition = result.DebugInfo.Request.ToObject<TopicPermissionsDefinition>(Deserializer.Options);
            
            definition.Exchange.ShouldBeNullOrEmpty();
            definition.Read.ShouldBe(".*");
            definition.Write.ShouldBe(".*");
        }

        [Test]
        public async Task Verify_cannot_create_topic_permissions_6()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Create("guest", "HareDu", x =>
                {
                    x.UsingReadPattern(".*");
                    x.UsingWritePattern(".*");
                });

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();

            TopicPermissionsDefinition definition = result.DebugInfo.Request.ToObject<TopicPermissionsDefinition>(Deserializer.Options);
            
            definition.Exchange.ShouldBeNullOrEmpty();
            definition.Read.ShouldBe(".*");
            definition.Write.ShouldBe(".*");
        }

        [Test]
        public async Task Verify_cannot_create_topic_permissions_7()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Create(string.Empty, string.Empty, x =>
                {
                    x.OnExchange("E4");
                    x.UsingReadPattern(".*");
                    x.UsingWritePattern(".*");
                });

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
            result.DebugInfo.ShouldNotBeNull();

            TopicPermissionsDefinition definition = result.DebugInfo.Request.ToObject<TopicPermissionsDefinition>(Deserializer.Options);
            
            definition.Exchange.ShouldBe("E4");
            definition.Read.ShouldBe(".*");
            definition.Write.ShouldBe(".*");
        }

        [Test]
        public async Task Verify_cannot_create_topic_permissions_8()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Create(string.Empty, string.Empty, x =>
                {
                    x.UsingReadPattern(string.Empty);
                    x.UsingWritePattern(".*");
                });

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(4);
            result.DebugInfo.ShouldNotBeNull();

            TopicPermissionsDefinition definition = result.DebugInfo.Request.ToObject<TopicPermissionsDefinition>(Deserializer.Options);
            
            definition.Exchange.ShouldBeNullOrEmpty();
            definition.Read.ShouldBeNullOrEmpty();
            definition.Write.ShouldBe(".*");
        }

        [Test]
        public async Task Verify_can_delete_user_permissions1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Delete("guest", "HareDu7");
            
            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public async Task Verify_can_delete_user_permissions2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .DeleteTopicPermission("guest", "HareDu7");
            
            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public async Task Verify_cannot_delete_user_permissions_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Delete(string.Empty, "HareDu7");
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_user_permissions_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Delete("guest", string.Empty);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_user_permissions_5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Delete(string.Empty, string.Empty);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }
    }
}