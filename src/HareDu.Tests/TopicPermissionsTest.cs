namespace HareDu.Tests
{
    using Core.Extensions;
    using Core.Serialization;
    using Microsoft.Extensions.DependencyInjection;
    using Model;
    using NUnit.Framework;
    using Registration;
    using Shouldly;

    [TestFixture]
    public class TopicPermissionsTest :
        HareDuTesting
    {
        [Test]
        public void Verify_can_get_all_topic_permissions()
        {
            var services = GetContainerBuilder("TestData/TopicPermissionsInfo.json").BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .GetAll()
                .GetResult();
            
            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public void Verify_can_create_user_permissions()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Create(x =>
                {
                    x.User("guest");
                    x.VirtualHost("HareDu");
                    x.Configure(c =>
                    {
                        c.OnExchange("E4");
                        c.UsingReadPattern(".*");
                        c.UsingWritePattern(".*");
                    });
                })
                .GetResult();

            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public void Verify_cannot_create_topic_permissions_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Create(x =>
                {
                    x.User(string.Empty);
                    x.VirtualHost("HareDu");
                    x.Configure(c =>
                    {
                        c.OnExchange("E4");
                        c.UsingReadPattern(".*");
                        c.UsingWritePattern(".*");
                    });
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();

            TopicPermissionsDefinition definition = result.DebugInfo.Request.ToObject<TopicPermissionsDefinition>(Deserializer.Options);
            
            definition.Exchange.ShouldBe("E4");
            definition.Read.ShouldBe(".*");
            definition.Write.ShouldBe(".*");
        }

        [Test]
        public void Verify_cannot_create_topic_permissions_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Create(x =>
                {
                    x.VirtualHost("HareDu");
                    x.Configure(c =>
                    {
                        c.OnExchange("E4");
                        c.UsingReadPattern(".*");
                        c.UsingWritePattern(".*");
                    });
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();

            TopicPermissionsDefinition definition = result.DebugInfo.Request.ToObject<TopicPermissionsDefinition>(Deserializer.Options);
            
            definition.Exchange.ShouldBe("E4");
            definition.Read.ShouldBe(".*");
            definition.Write.ShouldBe(".*");
        }

        [Test]
        public void Verify_cannot_create_topic_permissions_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Create(x =>
                {
                    x.User("guest");
                    x.VirtualHost(string.Empty);
                    x.Configure(c =>
                    {
                        c.OnExchange("E4");
                        c.UsingReadPattern(".*");
                        c.UsingWritePattern(".*");
                    });
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();

            TopicPermissionsDefinition definition = result.DebugInfo.Request.ToObject<TopicPermissionsDefinition>(Deserializer.Options);
            
            definition.Exchange.ShouldBe("E4");
            definition.Read.ShouldBe(".*");
            definition.Write.ShouldBe(".*");
        }

        [Test]
        public void Verify_cannot_create_topic_permissions_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Create(x =>
                {
                    x.User("guest");
                    x.Configure(c =>
                    {
                        c.OnExchange("E4");
                        c.UsingReadPattern(".*");
                        c.UsingWritePattern(".*");
                    });
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();

            TopicPermissionsDefinition definition = result.DebugInfo.Request.ToObject<TopicPermissionsDefinition>(Deserializer.Options);
            
            definition.Exchange.ShouldBe("E4");
            definition.Read.ShouldBe(".*");
            definition.Write.ShouldBe(".*");
        }

        [Test]
        public void Verify_cannot_create_topic_permissions_5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Create(x =>
                {
                    x.User("guest");
                    x.VirtualHost("HareDu");
                    x.Configure(c =>
                    {
                        c.OnExchange(string.Empty);
                        c.UsingReadPattern(".*");
                        c.UsingWritePattern(".*");
                    });
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();

            TopicPermissionsDefinition definition = result.DebugInfo.Request.ToObject<TopicPermissionsDefinition>(Deserializer.Options);
            
            definition.Exchange.ShouldBeNullOrEmpty();
            definition.Read.ShouldBe(".*");
            definition.Write.ShouldBe(".*");
        }

        [Test]
        public void Verify_cannot_create_topic_permissions_6()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Create(x =>
                {
                    x.User("guest");
                    x.VirtualHost("HareDu");
                    x.Configure(c =>
                    {
                        c.UsingReadPattern(".*");
                        c.UsingWritePattern(".*");
                    });
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();

            TopicPermissionsDefinition definition = result.DebugInfo.Request.ToObject<TopicPermissionsDefinition>(Deserializer.Options);
            
            definition.Exchange.ShouldBeNullOrEmpty();
            definition.Read.ShouldBe(".*");
            definition.Write.ShouldBe(".*");
        }

        [Test]
        public void Verify_cannot_create_topic_permissions_7()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Create(x =>
                {
                    x.User(string.Empty);
                    x.VirtualHost(string.Empty);
                    x.Configure(c =>
                    {
                        c.OnExchange("E4");
                        c.UsingReadPattern(".*");
                        c.UsingWritePattern(".*");
                    });
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
            result.DebugInfo.ShouldNotBeNull();

            TopicPermissionsDefinition definition = result.DebugInfo.Request.ToObject<TopicPermissionsDefinition>(Deserializer.Options);
            
            definition.Exchange.ShouldBe("E4");
            definition.Read.ShouldBe(".*");
            definition.Write.ShouldBe(".*");
        }

        [Test]
        public void Verify_cannot_create_topic_permissions_8()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Create(x =>
                {
                    x.Configure(c =>
                    {
                        c.UsingReadPattern(string.Empty);
                        c.UsingWritePattern(".*");
                    });
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(4);
            result.DebugInfo.ShouldNotBeNull();

            TopicPermissionsDefinition definition = result.DebugInfo.Request.ToObject<TopicPermissionsDefinition>(Deserializer.Options);
            
            definition.Exchange.ShouldBeNullOrEmpty();
            definition.Read.ShouldBeNullOrEmpty();
            definition.Write.ShouldBe(".*");
        }

        [Test]
        public void Verify_cannot_create_topic_permissions_9()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Create(x =>
                {
                    x.Configure(c =>
                    {
                        c.UsingWritePattern(".*");
                    });
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(4);
            result.DebugInfo.ShouldNotBeNull();

            TopicPermissionsDefinition definition = result.DebugInfo.Request.ToObject<TopicPermissionsDefinition>(Deserializer.Options);
            
            definition.Exchange.ShouldBeNullOrEmpty();
            definition.Read.ShouldBeNullOrEmpty();
            definition.Write.ShouldBe(".*");
        }

        [Test]
        public void Verify_cannot_create_topic_permissions_10()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Create(x =>
                {
                    x.Configure(c =>
                    {
                        c.OnExchange("E4");
                        c.UsingWritePattern(".*");
                    });
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(3);
            result.DebugInfo.ShouldNotBeNull();

            TopicPermissionsDefinition definition = result.DebugInfo.Request.ToObject<TopicPermissionsDefinition>(Deserializer.Options);
            
            definition.Exchange.ShouldBe("E4");
            definition.Read.ShouldBeNullOrEmpty();
            definition.Write.ShouldBe(".*");
        }

        [Test]
        public void Verify_cannot_create_topic_permissions_11()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Create(x =>
                {
                    x.Configure(c =>
                    {
                        c.OnExchange("E4");
                        c.UsingReadPattern(".*");
                    });
                })
                .GetResult();

            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(3);
            result.DebugInfo.ShouldNotBeNull();

            TopicPermissionsDefinition definition = result.DebugInfo.Request.ToObject<TopicPermissionsDefinition>(Deserializer.Options);
            
            definition.Exchange.ShouldBe("E4");
            definition.Read.ShouldBe(".*");
            definition.Write.ShouldBeNullOrEmpty();
        }

        [Test]
        public void Verify_can_delete_user_permissions()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Delete(x =>
                {
                    x.User("guest");
                    x.VirtualHost("HareDu7");
                })
                .GetResult();
            
            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public void Verify_cannot_delete_user_permissions_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Delete(x =>
                {
                    x.User(string.Empty);
                    x.VirtualHost("HareDu7");
                })
                .GetResult();
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public void Verify_cannot_delete_user_permissions_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Delete(x =>
                {
                    x.VirtualHost("HareDu7");
                })
                .GetResult();
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public void Verify_cannot_delete_user_permissions_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Delete(x =>
                {
                    x.User("guest");
                    x.VirtualHost(string.Empty);
                })
                .GetResult();
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public void Verify_cannot_delete_user_permissions_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Delete(x =>
                {
                    x.User("guest");
                })
                .GetResult();
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public void Verify_cannot_delete_user_permissions_5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Delete(x =>
                {
                })
                .GetResult();
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }
    }
}