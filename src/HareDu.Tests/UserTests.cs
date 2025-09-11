namespace HareDu.Tests;

using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;
using Serialization;

[TestFixture]
public class UserTests :
    HareDuTesting
{
    [Test]
    public async Task Verify_can_get_all_users1()
    {
        var result = await GetContainerBuilder("TestData/UserInfo1.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .GetAll();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(2));
            Assert.That(result.Data[0], Is.Not.Null);
            Assert.That(result.Data[0].Tags, Is.EqualTo("administrator"));
            Assert.That(result.Data[0].Username, Is.EqualTo("testuser1"));
            Assert.That(result.Data[0].PasswordHash, Is.EqualTo("EeJtW+FJi3yTLMxKFAfXEiNDJB97tHbplPlYM7v4T0pNqMlx"));
            Assert.That(result.Data[0].HashingAlgorithm, Is.EqualTo("rabbit_password_hashing_sha256"));
        });
    }
        
    [Test]
    public async Task Verify_can_get_all_users2()
    {
        var result = await GetContainerBuilder("TestData/UserInfo1.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .GetAllUsers(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(2));
            Assert.That(result.Data[0], Is.Not.Null);
            Assert.That(result.Data[0].Tags, Is.EqualTo("administrator"));
            Assert.That(result.Data[0].Username, Is.EqualTo("testuser1"));
            Assert.That(result.Data[0].PasswordHash, Is.EqualTo("EeJtW+FJi3yTLMxKFAfXEiNDJB97tHbplPlYM7v4T0pNqMlx"));
            Assert.That(result.Data[0].HashingAlgorithm, Is.EqualTo("rabbit_password_hashing_sha256"));
        });
    }
        
    [Test]
    public async Task Verify_can_get_all_users_without_permissions1()
    {
        var result = await GetContainerBuilder("TestData/UserInfo2.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .GetAllWithoutPermissions();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(1));
            Assert.That(result.Data[0], Is.Not.Null);
            Assert.That(result.Data[0].Tags, Is.EqualTo("administrator"));
            Assert.That(result.Data[0].Username, Is.EqualTo("testuser2"));
            Assert.That(result.Data[0].PasswordHash, Is.EqualTo("OasGMUAvOCqt8tFnTAZfvxiVsPAaSCMGHFThOvDXjc/exlxB"));
            Assert.That(result.Data[0].HashingAlgorithm, Is.EqualTo("rabbit_password_hashing_sha256"));
        });
    }
        
    [Test]
    public async Task Verify_can_get_all_users_without_permissions2()
    {
        var result = await GetContainerBuilder("TestData/UserInfo2.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .GetAllUsersWithoutPermissions(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(1));
            Assert.That(result.Data[0], Is.Not.Null);
            Assert.That(result.Data[0].Tags, Is.EqualTo("administrator"));
            Assert.That(result.Data[0].Username, Is.EqualTo("testuser2"));
            Assert.That(result.Data[0].PasswordHash, Is.EqualTo("OasGMUAvOCqt8tFnTAZfvxiVsPAaSCMGHFThOvDXjc/exlxB"));
            Assert.That(result.Data[0].HashingAlgorithm, Is.EqualTo("rabbit_password_hashing_sha256"));
        });
    }
        
    [Test]
    public async Task Verify_can_create_user_with_multiple_tags()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            UserRequest request = BrokerDeserializer.Instance.ToObject<UserRequest>(result.DebugInfo.Request);

            Assert.That(request.Tags, Is.EqualTo("administrator,management"));
            Assert.That(request.Password, Is.EqualTo("testuserpwd3"));
            Assert.That(request.PasswordHash, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_can_create_user_with_tags()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            UserRequest request = BrokerDeserializer.Instance.ToObject<UserRequest>(result.DebugInfo.Request);

            Assert.That(request.Tags, Is.EqualTo("administrator"));
            Assert.That(request.Password, Is.EqualTo("testuserpwd3"));
            Assert.That(request.PasswordHash, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_can_create_user_with_tags_via_extension_method()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .CreateUser(x => x.UsingCredentials("guest", "guest"), "testuser3", "testuserpwd3", "gkgfjjhfjh".ComputePasswordHash(), x =>
            {
                x.WithTags(t =>
                {
                    t.AddTag(UserAccessTag.Administrator);
                });
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            UserRequest request = BrokerDeserializer.Instance.ToObject<UserRequest>(result.DebugInfo.Request);

            Assert.That(request.Tags, Is.EqualTo("administrator"));
            Assert.That(request.Password, Is.EqualTo("testuserpwd3"));
            Assert.That(request.PasswordHash, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_can_create_2()
    {
        string passwordHash = "gkgfjjhfjh".ComputePasswordHash();
            
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            UserRequest request = BrokerDeserializer.Instance.ToObject<UserRequest>(result.DebugInfo.Request);

            Assert.That(request.Tags, Is.EqualTo("administrator"));
            Assert.That(request.Password, Is.Empty.Or.Null);
            Assert.That(request.PasswordHash, Is.EqualTo(passwordHash));
        });
    }
        
    [Test]
    public async Task Verify_can_create_3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            UserRequest request = BrokerDeserializer.Instance.ToObject<UserRequest>(result.DebugInfo.Request);

            Assert.That(request.Tags, Is.EqualTo("administrator"));
            Assert.That(request.Password, Is.EqualTo("testuserpwd3"));
            Assert.That(request.PasswordHash, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_can_create_4()
    {
        string passwordHash = "gkgfjjhfjh".ComputePasswordHash();

        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);

            UserRequest request = BrokerDeserializer.Instance.ToObject<UserRequest>(result.DebugInfo.Request);

            Assert.That(request.Tags, Is.EqualTo("administrator"));
            Assert.That(request.Password, Is.Empty.Or.Null);
            Assert.That(request.PasswordHash, Is.EqualTo(passwordHash));
        });
    }
        
    [Test]
    public async Task Verify_cannot_create_1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));

            UserRequest request = BrokerDeserializer.Instance.ToObject<UserRequest>(result.DebugInfo.Request);

            Assert.That(request.Tags, Is.EqualTo("administrator"));
            Assert.That(request.Password, Is.EqualTo("testuserpwd3"));
            Assert.That(request.PasswordHash, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create_2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));

            UserRequest request = BrokerDeserializer.Instance.ToObject<UserRequest>(result.DebugInfo.Request);

            Assert.That(request.Tags, Is.EqualTo("administrator"));
            Assert.That(request.Password, Is.Empty.Or.Null);
            Assert.That(request.PasswordHash, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create_3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));

            UserRequest request = BrokerDeserializer.Instance.ToObject<UserRequest>(result.DebugInfo.Request);

            Assert.That(request.Tags, Is.EqualTo("administrator"));
            Assert.That(request.Password, Is.Empty.Or.Null);
            Assert.That(request.PasswordHash, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create_4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
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
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            UserRequest request = BrokerDeserializer.Instance.ToObject<UserRequest>(result.DebugInfo.Request);

            Assert.That(request.Tags, Is.EqualTo("administrator"));
            Assert.That(request.Password, Is.Empty.Or.Null);
            Assert.That(request.PasswordHash, Is.Empty.Or.Null);
        });
    }
        
    [Test]
    public async Task Verify_cannot_create_5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .Create(string.Empty, string.Empty, configurator: x => x.WithTags(t =>
            {
                t.AddTag(UserAccessTag.Administrator);
            }));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo, Is.Not.Null);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));

            UserRequest request = BrokerDeserializer.Instance.ToObject<UserRequest>(result.DebugInfo.Request);

            Assert.That(request.Tags, Is.EqualTo("administrator"));
            Assert.That(request.Password, Is.Empty.Or.Null);
            Assert.That(request.PasswordHash, Is.Empty.Or.Null);
        });
    }

    [Test]
    public async Task Verify_can_delete1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .Delete("fake_user");

        Assert.That(result.HasFaulted, Is.False);
    }

    [Test]
    public async Task Verify_can_delete2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .DeleteUser(x => x.UsingCredentials("guest", "guest"), "fake_user");

        Assert.That(result.HasFaulted, Is.False);
    }

    [Test]
    public async Task Verify_can_delete3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .BulkDelete(new List<string>{"fake_user1", "fake_user2", "fake_user3"});

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);

            BulkUserDeleteRequest request = BrokerDeserializer.Instance.ToObject<BulkUserDeleteRequest>(result.DebugInfo.Request);

            Assert.That(request.Users, Is.Not.Null);
            Assert.That(request.Users[0], Is.EqualTo("fake_user1"));;
            Assert.That(request.Users[1], Is.EqualTo("fake_user2"));;
            Assert.That(request.Users[2], Is.EqualTo("fake_user3"));
        });
    }

    [Test]
    public async Task Verify_can_delete4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .DeleteUsers(x => x.UsingCredentials("guest", "guest"), new List<string>{"fake_user1", "fake_user2", "fake_user3"});

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);

            BulkUserDeleteRequest request = BrokerDeserializer.Instance.ToObject<BulkUserDeleteRequest>(result.DebugInfo.Request);

            Assert.That(request.Users, Is.Not.Null);
            Assert.That(request.Users[0], Is.EqualTo("fake_user1"));;
            Assert.That(request.Users[1], Is.EqualTo("fake_user2"));;
            Assert.That(request.Users[2], Is.EqualTo("fake_user3"));
        });
    }

    [Test]
    public async Task Verify_cannot_delete1()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .Delete(string.Empty);

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete2()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .DeleteUser(x => x.UsingCredentials("guest", "guest"), string.Empty);

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete3()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .BulkDelete(new List<string>());

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete4()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .DeleteUsers(x => x.UsingCredentials("guest", "guest"), new List<string>());

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete5()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .BulkDelete(new List<string>{"  ", string.Empty, null});

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete6()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .DeleteUsers(x => x.UsingCredentials("guest", "guest"), new List<string>{"  ", string.Empty, null});

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Verify_cannot_delete7()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .BulkDelete(new List<string>{"  ", "fake_user1", null});

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task Verify_cannot_delete8()
    {
        var result = await GetContainerBuilder()
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .DeleteUsers(x => x.UsingCredentials("guest", "guest"), new List<string>{"  ", "fake_user1", null});

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.DebugInfo.Errors.Count, Is.EqualTo(2));
        });
    }
    
    [Test]
    public async Task Verify_can_get_all_user_permissions1()
    {
        var result = await GetContainerBuilder("TestData/UserPermissionsInfo.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .GetAllPermissions();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(8));
            Assert.That(result.Data[0], Is.Not.Null);
            Assert.That(result.Data[0].User, Is.EqualTo("guest"));
            Assert.That(result.Data[0].VirtualHost, Is.EqualTo("/"));
            Assert.That(result.Data[0].Configure, Is.EqualTo(".*"));
            Assert.That(result.Data[0].Write, Is.EqualTo(".*"));
            Assert.That(result.Data[0].Read, Is.EqualTo(".*"));
        });
    }

    [Test]
    public async Task Verify_can_get_all_user_permissions2()
    {
        var result = await GetContainerBuilder("TestData/UserPermissionsInfo.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .GetAllUserPermissions(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(8));
            Assert.That(result.Data[0], Is.Not.Null);
            Assert.That(result.Data[0].User, Is.EqualTo("guest"));
            Assert.That(result.Data[0].VirtualHost, Is.EqualTo("/"));
            Assert.That(result.Data[0].Configure, Is.EqualTo(".*"));
            Assert.That(result.Data[0].Write, Is.EqualTo(".*"));
            Assert.That(result.Data[0].Read, Is.EqualTo(".*"));
        });
    }
}