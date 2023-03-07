namespace HareDu.Tests;

using System.Collections.Generic;
using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;

[TestFixture]
public class UserTests :
    HareDuTesting
{
    [Test]
    public async Task Verify_can_get_all_users1()
    {
        var services = GetContainerBuilder("TestData/UserInfo1.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<User>()
            .GetAll();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(2, result.Data.Count);
            Assert.IsNotNull(result.Data[0]);
            Assert.AreEqual("administrator", result.Data[0].Tags);
            Assert.AreEqual("testuser1", result.Data[0].Username);
            Assert.AreEqual("EeJtW+FJi3yTLMxKFAfXEiNDJB97tHbplPlYM7v4T0pNqMlx", result.Data[0].PasswordHash);
            Assert.AreEqual("rabbit_password_hashing_sha256", result.Data[0].HashingAlgorithm);
        });
    }
        
    [Test]
    public async Task Verify_can_get_all_users2()
    {
        var services = GetContainerBuilder("TestData/UserInfo1.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .GetAllUsers();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(2, result.Data.Count);
            Assert.IsNotNull(result.Data[0]);
            Assert.AreEqual("administrator", result.Data[0].Tags);
            Assert.AreEqual("testuser1", result.Data[0].Username);
            Assert.AreEqual("EeJtW+FJi3yTLMxKFAfXEiNDJB97tHbplPlYM7v4T0pNqMlx", result.Data[0].PasswordHash);
            Assert.AreEqual("rabbit_password_hashing_sha256", result.Data[0].HashingAlgorithm);
        });
    }
        
    [Test]
    public async Task Verify_can_get_all_users_without_permissions1()
    {
        var services = GetContainerBuilder("TestData/UserInfo2.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<User>()
            .GetAllWithoutPermissions();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(1, result.Data.Count);
            Assert.IsNotNull(result.Data[0]);
            Assert.AreEqual("administrator", result.Data[0].Tags);
            Assert.AreEqual("testuser2", result.Data[0].Username);
            Assert.AreEqual("OasGMUAvOCqt8tFnTAZfvxiVsPAaSCMGHFThOvDXjc/exlxB", result.Data[0].PasswordHash);
            Assert.AreEqual("rabbit_password_hashing_sha256", result.Data[0].HashingAlgorithm);
        });
    }
        
    [Test]
    public async Task Verify_can_get_all_users_without_permissions2()
    {
        var services = GetContainerBuilder("TestData/UserInfo2.json").BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .GetAllUsersWithoutPermissions();

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(1, result.Data.Count);
            Assert.IsNotNull(result.Data[0]);
            Assert.AreEqual("administrator", result.Data[0].Tags);
            Assert.AreEqual("testuser2", result.Data[0].Username);
            Assert.AreEqual("OasGMUAvOCqt8tFnTAZfvxiVsPAaSCMGHFThOvDXjc/exlxB", result.Data[0].PasswordHash);
            Assert.AreEqual("rabbit_password_hashing_sha256", result.Data[0].HashingAlgorithm);
        });
    }
        
    [Test]
    public async Task Verify_can_create_user_with_multiple_tags()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<User>()
            .Create("testuser3", "testuserpwd3", "gkgfjjhfjh".ComputePasswordHash(), x =>
            {
                x.WithTags(t =>
                {
                    t.Administrator();
                });
                x.WithTags(t =>
                {
                    t.Management();
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);

            UserRequest request = result.DebugInfo.Request.ToObject<UserRequest>();
            
            Assert.AreEqual("testuserpwd3", request.Password);
            Assert.AreEqual("administrator,management", request.Tags);
            Assert.That(request.PasswordHash, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_can_create_user_with_tags()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<User>()
            .Create("testuser3", "testuserpwd3", "gkgfjjhfjh".ComputePasswordHash(), x =>
            {
                x.WithTags(t =>
                {
                    t.Administrator();
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);

            UserRequest request = result.DebugInfo.Request.ToObject<UserRequest>();
            
            Assert.AreEqual("testuserpwd3", request.Password);
            Assert.AreEqual("administrator", request.Tags);
            Assert.That(request.PasswordHash, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_can_create_user_with_tags_via_extension_method()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .CreateUser("testuser3", "testuserpwd3", "gkgfjjhfjh".ComputePasswordHash(), x =>
            {
                x.WithTags(t =>
                {
                    t.Administrator();
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);

            UserRequest request = result.DebugInfo.Request.ToObject<UserRequest>();
            
            Assert.AreEqual("testuserpwd3", request.Password);
            Assert.AreEqual("administrator", request.Tags);
            Assert.That(request.PasswordHash, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_can_create_2()
    {
        string passwordHash = "gkgfjjhfjh".ComputePasswordHash();
            
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<User>()
            .Create("testuser3", string.Empty, passwordHash, configurator:x =>
            {
                x.WithTags(t =>
                {
                    t.Administrator();
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);

            UserRequest request = result.DebugInfo.Request.ToObject<UserRequest>();
            
            Assert.That(request.Password, Is.Empty.Or.Null);
            Assert.AreEqual("administrator", request.Tags);
            Assert.AreEqual(passwordHash, request.PasswordHash);
        });
    }
        
    [Test]
    public async Task Verify_can_create_3()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<User>()
            .Create("testuser3", "testuserpwd3", configurator:x =>
            {
                x.WithTags(t =>
                {
                    t.Administrator();
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);

            UserRequest request = result.DebugInfo.Request.ToObject<UserRequest>();
            
            Assert.AreEqual("administrator", request.Tags);
            Assert.AreEqual("testuserpwd3", request.Password);
            Assert.That(request.PasswordHash, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_can_create_4()
    {
        string passwordHash = "gkgfjjhfjh".ComputePasswordHash();
            
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<User>()
            .Create("testuser3", string.Empty, passwordHash, x =>
            {
                x.WithTags(t =>
                {
                    t.Administrator();
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);

            UserRequest request = result.DebugInfo.Request.ToObject<UserRequest>();
            
            Assert.AreEqual("administrator", request.Tags);
            Assert.That(request.Password, Is.Empty.Or.Null);
            Assert.AreEqual(passwordHash, request.PasswordHash);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create_1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<User>()
            .Create(string.Empty, "testuserpwd3", "gkgfjjhfjh".ComputePasswordHash(),x =>
            {
                x.WithTags(t =>
                {
                    t.Administrator();
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);

            UserRequest request = result.DebugInfo.Request.ToObject<UserRequest>();
            
            Assert.AreEqual("administrator", request.Tags);
            Assert.AreEqual("testuserpwd3", request.Password);
            Assert.That(request.PasswordHash, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create_2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<User>()
            .Create("testuser3", string.Empty, string.Empty, x =>
            {
                x.WithTags(t =>
                {
                    t.Administrator();
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);

            UserRequest request = result.DebugInfo.Request.ToObject<UserRequest>();
            
            Assert.AreEqual("administrator", request.Tags);
            Assert.That(request.Password, Is.Empty.Or.Null);
            Assert.That(request.PasswordHash, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create_3()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<User>()
            .Create("testuser3", string.Empty, string.Empty,x =>
            {
                x.WithTags(t =>
                {
                    t.Administrator();
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);

            UserRequest request = result.DebugInfo.Request.ToObject<UserRequest>();
            
            Assert.AreEqual("administrator", request.Tags);
            Assert.That(request.Password, Is.Empty.Or.Null);
            Assert.That(request.PasswordHash, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create_4()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<User>()
            .Create(string.Empty, string.Empty, string.Empty, x =>
            {
                x.WithTags(t =>
                {
                    t.Administrator();
                });
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            UserRequest request = result.DebugInfo.Request.ToObject<UserRequest>();
            
            Assert.AreEqual("administrator", request.Tags);
            Assert.That(request.Password, Is.Empty.Or.Null);
            Assert.That(request.PasswordHash, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create_5()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<User>()
            .Create(string.Empty, string.Empty, configurator:x => x.WithTags(t => { t.Administrator(); }));

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.IsNotNull(result.DebugInfo);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);

            UserRequest request = result.DebugInfo.Request.ToObject<UserRequest>();
            
            Assert.AreEqual("administrator", request.Tags);
            Assert.That(request.Password, Is.Empty.Or.Null);
            Assert.That(request.PasswordHash, Is.Empty.Or.Null);
        });
    }

    [Test]
    public async Task Verify_can_delete1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<User>()
            .Delete("fake_user");
            
        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_can_delete2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DeleteUser("fake_user");
            
        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_can_delete3()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<User>()
            .BulkDelete(new List<string>{"fake_user1", "fake_user2", "fake_user3"});
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);

            BulkUserDeleteRequest request = result.DebugInfo.Request.ToObject<BulkUserDeleteRequest>();
                
            Assert.IsNotNull(request.Users);
            Assert.AreEqual("fake_user1", request.Users[0]);
            Assert.AreEqual("fake_user2", request.Users[1]);
            Assert.AreEqual("fake_user3", request.Users[2]);
        });
    }

    [Test]
    public async Task Verify_can_delete4()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DeleteUsers(new List<string>{"fake_user1", "fake_user2", "fake_user3"});
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);

            BulkUserDeleteRequest request = result.DebugInfo.Request.ToObject<BulkUserDeleteRequest>();
                
            Assert.IsNotNull(request.Users);
            Assert.AreEqual("fake_user1", request.Users[0]);
            Assert.AreEqual("fake_user2", request.Users[1]);
            Assert.AreEqual("fake_user3", request.Users[2]);
        });
    }

    [Test]
    public async Task Verify_cannot_delete1()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<User>()
            .Delete(string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete2()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DeleteUser(string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete3()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<User>()
            .BulkDelete(new List<string>());
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete4()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DeleteUsers(new List<string>());
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete5()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<User>()
            .BulkDelete(new List<string>{"  ", string.Empty, null});
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete6()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DeleteUsers(new List<string>{"  ", string.Empty, null});
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete7()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .API<User>()
            .BulkDelete(new List<string>{"  ", "fake_user1", null});
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete8()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
        var result = await services.GetService<IBrokerApiFactory>()
            .DeleteUsers(new List<string>{"  ", "fake_user1", null});
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }
}