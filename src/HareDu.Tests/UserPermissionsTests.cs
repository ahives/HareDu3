namespace HareDu.Tests
{
    using System.Threading.Tasks;
    using Core.Extensions;
    using Core.Serialization;
    using Microsoft.Extensions.DependencyInjection;
    using Model;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class UserPermissionsTests :
        HareDuTesting
    {
        [Test]
        public async Task Should_be_able_to_get_all_user_permissions()
        {
            var services = GetContainerBuilder("TestData/UserPermissionsInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<UserPermissions>()
                .GetAll()
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeFalse();
            result.HasData.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Count.ShouldBe(8);
            result.Data[0].ShouldNotBeNull();
            result.Data[0].User.ShouldBe("guest");
            result.Data[0].VirtualHost.ShouldBe("/");
            result.Data[0].Configure.ShouldBe(".*");
            result.Data[0].Write.ShouldBe(".*");
            result.Data[0].Read.ShouldBe(".*");
        }

        [Test]
        public async Task Verify_can_delete_user_permissions()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<UserPermissions>()
                .Delete("haredu_user", "HareDu5");

            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public async Task Verify_cannot_delete_user_permissions_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<UserPermissions>()
                .Delete(string.Empty, "HareDu5");

            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_user_permissions_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<UserPermissions>()
                .Delete("haredu_user", string.Empty);

            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_cannot_delete_user_permissions_6()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<UserPermissions>()
                .Delete(string.Empty, string.Empty);

            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_can_create_user_permissions()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<UserPermissions>()
                .Create("haredu_user", "HareDu5", x =>
                {
                    x.UsingConfigurePattern(".*");
                    x.UsingReadPattern(".*");
                    x.UsingWritePattern(".*");
                });

            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();

            UserPermissionsDefinition definition = result.DebugInfo.Request.ToObject<UserPermissionsDefinition>(Deserializer.Options);
            
            definition.Configure.ShouldBe(".*");
            definition.Write.ShouldBe(".*");
            definition.Read.ShouldBe(".*");
        }

        [Test]
        public async Task Verify_cannot_create_user_permissions_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<UserPermissions>()
                .Create(string.Empty, "HareDu5", x =>
                {
                    x.UsingConfigurePattern(".*");
                    x.UsingReadPattern(".*");
                    x.UsingWritePattern(".*");
                });

            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();

            UserPermissionsDefinition definition = result.DebugInfo.Request.ToObject<UserPermissionsDefinition>(Deserializer.Options);
            
            definition.Configure.ShouldBe(".*");
            definition.Write.ShouldBe(".*");
            definition.Read.ShouldBe(".*");
        }

        [Test]
        public async Task Verify_cannot_create_user_permissions_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<UserPermissions>()
                .Create(string.Empty, "HareDu5", x =>
                {
                    x.UsingConfigurePattern(".*");
                    x.UsingReadPattern(".*");
                    x.UsingWritePattern(".*");
                });

            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();

            UserPermissionsDefinition definition = result.DebugInfo.Request.ToObject<UserPermissionsDefinition>(Deserializer.Options);
            
            definition.Configure.ShouldBe(".*");
            definition.Write.ShouldBe(".*");
            definition.Read.ShouldBe(".*");
        }

        [Test]
        public async Task Verify_cannot_create_user_permissions_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<UserPermissions>()
                .Create("haredu_user", string.Empty, x =>
                {
                    x.UsingConfigurePattern(".*");
                    x.UsingReadPattern(".*");
                    x.UsingWritePattern(".*");
                });

            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();

            UserPermissionsDefinition definition = result.DebugInfo.Request.ToObject<UserPermissionsDefinition>(Deserializer.Options);
            
            definition.Configure.ShouldBe(".*");
            definition.Write.ShouldBe(".*");
            definition.Read.ShouldBe(".*");
        }

        [Test]
        public async Task Verify_cannot_create_user_permissions_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<UserPermissions>()
                .Create("haredu_user", string.Empty, x =>
                {
                    x.UsingConfigurePattern(".*");
                    x.UsingReadPattern(".*");
                    x.UsingWritePattern(".*");
                });

            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();

            UserPermissionsDefinition definition = result.DebugInfo.Request.ToObject<UserPermissionsDefinition>(Deserializer.Options);
            
            definition.Configure.ShouldBe(".*");
            definition.Write.ShouldBe(".*");
            definition.Read.ShouldBe(".*");
        }

        [Test]
        public async Task Verify_cannot_create_user_permissions_6()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<UserPermissions>()
                .Create(string.Empty, string.Empty, x =>
                {
                    x.UsingConfigurePattern(".*");
                    x.UsingReadPattern(".*");
                    x.UsingWritePattern(".*");
                });

            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(2);
            result.DebugInfo.ShouldNotBeNull();

            UserPermissionsDefinition definition = result.DebugInfo.Request.ToObject<UserPermissionsDefinition>(Deserializer.Options);
            
            definition.Configure.ShouldBe(".*");
            definition.Write.ShouldBe(".*");
            definition.Read.ShouldBe(".*");
        }
    }
}