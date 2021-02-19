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
    public class UserTests :
        HareDuTesting
    {
        [Test]
        public async Task Verify_can_get_all_users1()
        {
            var services = GetContainerBuilder("TestData/UserInfo1.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<User>()
                .GetAll();

            result.HasFaulted.ShouldBeFalse();
            result.HasData.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Count.ShouldBe(2);
            result.Data[0].ShouldNotBeNull();
            result.Data[0].Tags.ShouldBe("administrator");
            result.Data[0].Username.ShouldBe("testuser1");
            result.Data[0].PasswordHash.ShouldBe("EeJtW+FJi3yTLMxKFAfXEiNDJB97tHbplPlYM7v4T0pNqMlx");
            result.Data[0].HashingAlgorithm.ShouldBe("rabbit_password_hashing_sha256");
        }
        
        [Test]
        public async Task Verify_can_get_all_users2()
        {
            var services = GetContainerBuilder("TestData/UserInfo1.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .GetAllUsers();

            result.HasFaulted.ShouldBeFalse();
            result.HasData.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Count.ShouldBe(2);
            result.Data[0].ShouldNotBeNull();
            result.Data[0].Tags.ShouldBe("administrator");
            result.Data[0].Username.ShouldBe("testuser1");
            result.Data[0].PasswordHash.ShouldBe("EeJtW+FJi3yTLMxKFAfXEiNDJB97tHbplPlYM7v4T0pNqMlx");
            result.Data[0].HashingAlgorithm.ShouldBe("rabbit_password_hashing_sha256");
        }
        
        [Test]
        public async Task Verify_can_get_all_users_without_permissions1()
        {
            var services = GetContainerBuilder("TestData/UserInfo2.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<User>()
                .GetAllWithoutPermissions();

            result.HasFaulted.ShouldBeFalse();
            result.HasData.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Count.ShouldBe(1);
            result.Data[0].ShouldNotBeNull();
            result.Data[0].Tags.ShouldBe("administrator");
            result.Data[0].Username.ShouldBe("testuser2");
            result.Data[0].PasswordHash.ShouldBe("OasGMUAvOCqt8tFnTAZfvxiVsPAaSCMGHFThOvDXjc/exlxB");
            result.Data[0].HashingAlgorithm.ShouldBe("rabbit_password_hashing_sha256");
        }
        
        [Test]
        public async Task Verify_can_get_all_users_without_permissions2()
        {
            var services = GetContainerBuilder("TestData/UserInfo2.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .GetAllUsersWithoutPermissions();

            result.HasFaulted.ShouldBeFalse();
            result.HasData.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Count.ShouldBe(1);
            result.Data[0].ShouldNotBeNull();
            result.Data[0].Tags.ShouldBe("administrator");
            result.Data[0].Username.ShouldBe("testuser2");
            result.Data[0].PasswordHash.ShouldBe("OasGMUAvOCqt8tFnTAZfvxiVsPAaSCMGHFThOvDXjc/exlxB");
            result.Data[0].HashingAlgorithm.ShouldBe("rabbit_password_hashing_sha256");
        }
        
        [Test]
        public async Task Verify_can_create1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<User>()
                .Create("testuser3", "testuserpwd3", "gkgfjjhfjh".ComputePasswordHash(), x =>
                {
                    x.WithTags(t =>
                    {
                        t.Administrator();
                    });
                });
            
            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();

            UserDefinition definition = result.DebugInfo.Request.ToObject<UserDefinition>(Deserializer.Options);
            
            definition.Password.ShouldBe("testuserpwd3");
            definition.Tags.ShouldBe("administrator");
            definition.PasswordHash.ShouldBeNullOrEmpty();
        }
        
        [Test]
        public async Task Verify_can_create2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .CreateUser("testuser3", "testuserpwd3", "gkgfjjhfjh".ComputePasswordHash(), x =>
                {
                    x.WithTags(t =>
                    {
                        t.Administrator();
                    });
                });
            
            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();

            UserDefinition definition = result.DebugInfo.Request.ToObject<UserDefinition>(Deserializer.Options);
            
            definition.Password.ShouldBe("testuserpwd3");
            definition.Tags.ShouldBe("administrator");
            definition.PasswordHash.ShouldBeNullOrEmpty();
        }
        
        [Test]
        public async Task Verify_can_create_2()
        {
            string passwordHash = "gkgfjjhfjh".ComputePasswordHash();
            
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<User>()
                .Create("testuser3", passwordHash, configurator:x =>
                {
                    x.WithTags(t =>
                    {
                        t.Administrator();
                    });
                });
            
            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();

            UserDefinition definition = result.DebugInfo.Request.ToObject<UserDefinition>(Deserializer.Options);
            
            definition.Tags.ShouldBe("administrator");
            definition.Password.ShouldBeNullOrEmpty();
            definition.PasswordHash.ShouldBe(passwordHash);
        }
        
        [Test]
        public async Task Verify_can_create_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<User>()
                .Create("testuser3", "testuserpwd3", configurator:x =>
                {
                    x.WithTags(t =>
                    {
                        t.Administrator();
                    });
                });
            
            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();

            UserDefinition definition = result.DebugInfo.Request.ToObject<UserDefinition>(Deserializer.Options);
            
            definition.Tags.ShouldBe("administrator");
            definition.Password.ShouldBe("testuserpwd3");
            definition.PasswordHash.ShouldBeNullOrEmpty();
        }
        
        [Test]
        public async Task Verify_can_create_4()
        {
            string passwordHash = "gkgfjjhfjh".ComputePasswordHash();
            
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<User>()
                .Create("testuser3", string.Empty, passwordHash, x =>
                {
                    x.WithTags(t =>
                    {
                        t.Administrator();
                    });
                });
            
            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();

            UserDefinition definition = result.DebugInfo.Request.ToObject<UserDefinition>(Deserializer.Options);
            
            definition.Tags.ShouldBe("administrator");
            definition.Password.ShouldBeNullOrEmpty();
            definition.PasswordHash.ShouldBe(passwordHash);
        }
        
        [Test]
        public async Task Verify_cannot_create_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<User>()
                .Create(string.Empty, "testuserpwd3", "gkgfjjhfjh".ComputePasswordHash(),x =>
                {
                    x.WithTags(t =>
                    {
                        t.Administrator();
                    });
                });
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();

            UserDefinition definition = result.DebugInfo.Request.ToObject<UserDefinition>(Deserializer.Options);
            
            definition.Password.ShouldBe("testuserpwd3");
            definition.Tags.ShouldBe("administrator");
            definition.PasswordHash.ShouldBeNullOrEmpty();
        }
        
        [Test]
        public async Task Verify_cannot_create_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<User>()
                .Create("testuser3", string.Empty, string.Empty, x =>
                {
                    x.WithTags(t =>
                    {
                        t.Administrator();
                    });
                });
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();

            UserDefinition definition = result.DebugInfo.Request.ToObject<UserDefinition>(Deserializer.Options);
            
            definition.Tags.ShouldBe("administrator");
            definition.Password.ShouldBeNullOrEmpty();
            definition.PasswordHash.ShouldBeNullOrEmpty();
        }
        
        [Test]
        public async Task Verify_cannot_create_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<User>()
                .Create("testuser3", string.Empty, string.Empty,x =>
                {
                    x.WithTags(t =>
                    {
                        t.Administrator();
                    });
                });
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();

            UserDefinition definition = result.DebugInfo.Request.ToObject<UserDefinition>(Deserializer.Options);
            
            definition.Tags.ShouldBe("administrator");
            definition.Password.ShouldBeNullOrEmpty();
            definition.PasswordHash.ShouldBeNullOrEmpty();
        }
        
        [Test]
        public async Task Verify_cannot_create_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<User>()
                .Create(string.Empty, string.Empty, string.Empty, x =>
                {
                    x.WithTags(t =>
                    {
                        t.Administrator();
                    });
                });
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(2);
            result.DebugInfo.ShouldNotBeNull();

            UserDefinition definition = result.DebugInfo.Request.ToObject<UserDefinition>(Deserializer.Options);
            
            definition.Tags.ShouldBe("administrator");
            definition.Password.ShouldBeNullOrEmpty();
            definition.PasswordHash.ShouldBeNullOrEmpty();
        }
        
        [Test]
        public async Task Verify_cannot_create_5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<User>()
                .Create(string.Empty, string.Empty, configurator:x => x.WithTags(t => { t.Administrator(); }));
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(2);
            result.DebugInfo.ShouldNotBeNull();

            UserDefinition definition = result.DebugInfo.Request.ToObject<UserDefinition>(Deserializer.Options);
            
            definition.Tags.ShouldBe("administrator");
            definition.Password.ShouldBeNullOrEmpty();
            definition.PasswordHash.ShouldBeNullOrEmpty();
        }

        [Test]
        public async Task Verify_can_delete1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<User>()
                .Delete("fake_user");
            
            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public async Task Verify_can_delete2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .DeleteUser("fake_user");
            
            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public async Task Verify_cannot_delete_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<User>()
                .Delete(string.Empty);
            
            result.HasFaulted.ShouldBeTrue();
            result.DebugInfo.Errors.Count.ShouldBe(1);
        }
    }
}