namespace HareDu.Tests;

using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Configuration;
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
        var result = await GetContainerBuilder("TestData/UserInfo1.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
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
        var result = await GetContainerBuilder("TestData/UserInfo1.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetAllUsers(x => x.UsingCredentials("guest", "guest"));

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
        var result = await GetContainerBuilder("TestData/UserInfo2.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
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
        var result = await GetContainerBuilder("TestData/UserInfo2.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetAllUsersWithoutPermissions(x => x.UsingCredentials("guest", "guest"));

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
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .Create("testuser3", "testuserpwd3", "gkgfjjhfjh".ComputePasswordHash(), x =>
            {
                x.WithTags(t =>
                {
                    t.AddTag(UserAccessTag.Administrator);
                });
                x.WithTags(t =>
                {
                    t.AddTag(UserAccessTag.Management);
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
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .Create("testuser3", "testuserpwd3", "gkgfjjhfjh".ComputePasswordHash(), x =>
            {
                x.WithTags(t =>
                {
                    t.AddTag(UserAccessTag.Administrator);
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
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .CreateUser(x => x.UsingCredentials("guest", "guest"), "testuser3", "testuserpwd3", "gkgfjjhfjh".ComputePasswordHash(), x =>
            {
                x.WithTags(t =>
                {
                    t.AddTag(UserAccessTag.Administrator);
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
            
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .Create("testuser3", string.Empty, passwordHash, configurator:x =>
            {
                x.WithTags(t =>
                {
                    t.AddTag(UserAccessTag.Administrator);
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
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .Create("testuser3", "testuserpwd3", configurator:x =>
            {
                x.WithTags(t =>
                {
                    t.AddTag(UserAccessTag.Administrator);
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
            
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .Create("testuser3", string.Empty, passwordHash, x =>
            {
                x.WithTags(t =>
                {
                    t.AddTag(UserAccessTag.Administrator);
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
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .Create(string.Empty, "testuserpwd3", "gkgfjjhfjh".ComputePasswordHash(),x =>
            {
                x.WithTags(t =>
                {
                    t.AddTag(UserAccessTag.Administrator);
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
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .Create("testuser3", string.Empty, string.Empty, x =>
            {
                x.WithTags(t =>
                {
                    t.AddTag(UserAccessTag.Administrator);
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
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .Create("testuser3", string.Empty, string.Empty,x =>
            {
                x.WithTags(t =>
                {
                    t.AddTag(UserAccessTag.Administrator);
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
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .Create(string.Empty, string.Empty, string.Empty, x =>
            {
                x.WithTags(t =>
                {
                    t.AddTag(UserAccessTag.Administrator);
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
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .Create(string.Empty, string.Empty, configurator: x => x.WithTags(t =>
            {
                t.AddTag(UserAccessTag.Administrator);
            }));

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
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .Delete("fake_user");
            
        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_can_delete2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteUser(x => x.UsingCredentials("guest", "guest"), "fake_user");
            
        Assert.IsFalse(result.HasFaulted);
    }

    [Test]
    public async Task Verify_can_delete3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
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
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteUsers(x => x.UsingCredentials("guest", "guest"), new List<string>{"fake_user1", "fake_user2", "fake_user3"});

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
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
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
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteUser(x => x.UsingCredentials("guest", "guest"), string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
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
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteUsers(x => x.UsingCredentials("guest", "guest"), new List<string>());
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
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
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteUsers(x => x.UsingCredentials("guest", "guest"), new List<string>{"  ", string.Empty, null});
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete7()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
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
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteUsers(x => x.UsingCredentials("guest", "guest"), new List<string>{"  ", "fake_user1", null});
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }
    
    [Test]
    public async Task Verify_can_get_all_user_permissions1()
    {
        var result = await GetContainerBuilder("TestData/UserPermissionsInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .GetAllPermissions();
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(8, result.Data.Count);
            Assert.IsNotNull(result.Data[0]);
            Assert.AreEqual("guest", result.Data[0].User);
            Assert.AreEqual("/", result.Data[0].VirtualHost);
            Assert.AreEqual(".*", result.Data[0].Configure);
            Assert.AreEqual(".*", result.Data[0].Write);
            Assert.AreEqual(".*", result.Data[0].Read);
        });
    }

    [Test]
    public async Task Verify_can_get_all_user_permissions2()
    {
        var result = await GetContainerBuilder("TestData/UserPermissionsInfo.json")
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .GetAllUserPermissions(x => x.UsingCredentials("guest", "guest"));
            
        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.IsTrue(result.HasData);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(8, result.Data.Count);
            Assert.IsNotNull(result.Data[0]);
            Assert.AreEqual("guest", result.Data[0].User);
            Assert.AreEqual("/", result.Data[0].VirtualHost);
            Assert.AreEqual(".*", result.Data[0].Configure);
            Assert.AreEqual(".*", result.Data[0].Write);
            Assert.AreEqual(".*", result.Data[0].Read);
        });
    }

    [Test]
    public async Task Verify_can_delete_user_permissions1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .DeletePermissions("haredu_user", "HareDu5");

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.AreEqual(0, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_can_delete_user_permissions2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteUserPermissions(x => x.UsingCredentials("guest", "guest"), "haredu_user", "HareDu5");

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.AreEqual(0, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_user_permissions1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .DeletePermissions(string.Empty, "HareDu5");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_user_permissions2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteUserPermissions(x => x.UsingCredentials("guest", "guest"), string.Empty, "HareDu5");
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_user_permissions3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .DeletePermissions("haredu_user", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_user_permissions4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteUserPermissions(x => x.UsingCredentials("guest", "guest"), "haredu_user", string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_user_permissions5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .DeletePermissions(string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_cannot_delete_user_permissions6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .DeleteUserPermissions(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
        });
    }

    [Test]
    public async Task Verify_can_create_user_permissions1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .ApplyPermissions("haredu_user", "HareDu5", x =>
            {
                x.UsingConfigurePattern(".*");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.AreEqual(0, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            UserPermissionsRequest request = result.DebugInfo.Request.ToObject<UserPermissionsRequest>();
            
            Assert.AreEqual(".*", request.Configure);
            Assert.AreEqual(".*", request.Write);
            Assert.AreEqual(".*", request.Read);
        });
    }

    [Test]
    public async Task Verify_can_create_user_permissions2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .ApplyUserPermissions(x => x.UsingCredentials("guest", "guest"), "haredu_user", "HareDu5", x =>
            {
                x.UsingConfigurePattern(".*");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.IsFalse(result.HasFaulted);
            Assert.AreEqual(0, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            UserPermissionsRequest request = result.DebugInfo.Request.ToObject<UserPermissionsRequest>();
            
            Assert.AreEqual(".*", request.Configure);
            Assert.AreEqual(".*", request.Write);
            Assert.AreEqual(".*", request.Read);
        });
    }

    [Test]
    public async Task Verify_cannot_create_user_permissions1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .ApplyPermissions(string.Empty, "HareDu5", x =>
            {
                x.UsingConfigurePattern(".*");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            UserPermissionsRequest request = result.DebugInfo.Request.ToObject<UserPermissionsRequest>();
            
            Assert.AreEqual(".*", request.Configure);
            Assert.AreEqual(".*", request.Write);
            Assert.AreEqual(".*", request.Read);
        });
    }

    [Test]
    public async Task Verify_cannot_create_user_permissions2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .ApplyUserPermissions(x => x.UsingCredentials("guest", "guest"), string.Empty, "HareDu5", x =>
            {
                x.UsingConfigurePattern(".*");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            UserPermissionsRequest request = result.DebugInfo.Request.ToObject<UserPermissionsRequest>();
            
            Assert.AreEqual(".*", request.Configure);
            Assert.AreEqual(".*", request.Write);
            Assert.AreEqual(".*", request.Read);
        });
    }

    [Test]
    public async Task Verify_cannot_create_user_permissions3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .ApplyPermissions("haredu_user", string.Empty, x =>
            {
                x.UsingConfigurePattern(".*");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            UserPermissionsRequest request = result.DebugInfo.Request.ToObject<UserPermissionsRequest>();
            
            Assert.AreEqual(".*", request.Configure);
            Assert.AreEqual(".*", request.Write);
            Assert.AreEqual(".*", request.Read);
        });
    }

    [Test]
    public async Task Verify_cannot_create_user_permissions4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .ApplyUserPermissions(x => x.UsingCredentials("guest", "guest"), "haredu_user", string.Empty, x =>
            {
                x.UsingConfigurePattern(".*");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            UserPermissionsRequest request = result.DebugInfo.Request.ToObject<UserPermissionsRequest>();
            
            Assert.AreEqual(".*", request.Configure);
            Assert.AreEqual(".*", request.Write);
            Assert.AreEqual(".*", request.Read);
        });
    }

    [Test]
    public async Task Verify_cannot_create_user_permissions5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .ApplyPermissions(string.Empty, string.Empty, x =>
            {
                x.UsingConfigurePattern(".*");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            UserPermissionsRequest request = result.DebugInfo.Request.ToObject<UserPermissionsRequest>();
            
            Assert.AreEqual(".*", request.Configure);
            Assert.AreEqual(".*", request.Write);
            Assert.AreEqual(".*", request.Read);
        });
    }

    [Test]
    public async Task Verify_cannot_create_user_permissions6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IBrokerFactory>()
            .ApplyUserPermissions(x => x.UsingCredentials("guest", "guest"), string.Empty, string.Empty, x =>
            {
                x.UsingConfigurePattern(".*");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result.HasFaulted);
            Assert.AreEqual(2, result.DebugInfo.Errors.Count);
            Assert.IsNotNull(result.DebugInfo);

            UserPermissionsRequest request = result.DebugInfo.Request.ToObject<UserPermissionsRequest>();
            
            Assert.AreEqual(".*", request.Configure);
            Assert.AreEqual(".*", request.Write);
            Assert.AreEqual(".*", request.Read);
        });
    }
}