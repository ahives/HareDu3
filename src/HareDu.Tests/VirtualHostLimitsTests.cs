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
    public class VirtualHostLimitsTests :
        HareDuTesting
    {
        [Test]
        public async Task Verify_can_get_all_limits()
        {
            var services = GetContainerBuilder("TestData/VirtualHostLimitsInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHostLimits>()
                .GetAll()
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeFalse();
            result.HasData.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Count.ShouldBe(3);
            result.Data[0].VirtualHostName.ShouldBe("HareDu1");
            result.Data[0].Limits.Count.ShouldBe(2);
            result.Data[0].Limits["max-connections"].ShouldBe<ulong>(10);
            result.Data[0].Limits["max-queues"].ShouldBe<ulong>(10);
        }

        [Test]
        public async Task Verify_can_define_limits()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHostLimits>()
                .Define(x =>
                {
                    x.VirtualHost("HareDu5");
                    x.Configure(c =>
                    {
                        c.SetMaxQueueLimit(100);
                        c.SetMaxConnectionLimit(1000);
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();

            VirtualHostLimitsDefinition definition = result.DebugInfo.Request.ToObject<VirtualHostLimitsDefinition>(Deserializer.Options);
            
            definition.MaxConnectionLimit.ShouldBe<ulong>(1000);
            definition.MaxQueueLimit.ShouldBe<ulong>(100);
        }

        [Test]
        public async Task Verify_cannot_define_limits_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHostLimits>()
                .Define(x =>
                {
                    x.VirtualHost(string.Empty);
                    x.Configure(c =>
                    {
                        c.SetMaxQueueLimit(100);
                        c.SetMaxConnectionLimit(1000);
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();

            VirtualHostLimitsDefinition definition = result.DebugInfo.Request.ToObject<VirtualHostLimitsDefinition>(Deserializer.Options);
            
            definition.MaxConnectionLimit.ShouldBe<ulong>(1000);
            definition.MaxQueueLimit.ShouldBe<ulong>(100);
        }

        [Test]
        public async Task Verify_cannot_define_limits_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHostLimits>()
                .Define(x =>
                {
                    x.VirtualHost("FakeVirtualHost");
                    x.Configure(c =>
                    {
                        c.SetMaxConnectionLimit(1000);
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();

            VirtualHostLimitsDefinition definition = result.DebugInfo.Request.ToObject<VirtualHostLimitsDefinition>(Deserializer.Options);
            
            definition.MaxConnectionLimit.ShouldBe<ulong>(1000);
        }

        [Test]
        public async Task Verify_cannot_define_limits_3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHostLimits>()
                .Define(x =>
                {
                    x.VirtualHost("FakeVirtualHost");
                    x.Configure(c =>
                    {
                        c.SetMaxQueueLimit(100);
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
            result.DebugInfo.ShouldNotBeNull();

            VirtualHostLimitsDefinition definition = result.DebugInfo.Request.ToObject<VirtualHostLimitsDefinition>(Deserializer.Options);
            
            definition.MaxQueueLimit.ShouldBe<ulong>(100);
        }

        [Test]
        public async Task Verify_cannot_define_limits_4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHostLimits>()
                .Define(x =>
                {
                    x.VirtualHost("FakeVirtualHost");
                    x.Configure(c =>
                    {
                    });
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_cannot_define_limits_5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHostLimits>()
                .Define(x =>
                {
                    x.VirtualHost("FakeVirtualHost");
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_cannot_define_limits_6()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHostLimits>()
                .Define(x =>
                {
                })
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(3);
        }

        [Test]
        public async Task Verify_can_delete_limits()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHostLimits>()
                .Delete(x => x.For("HareDu3"))
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeFalse();
        }

        [Test]
        public async Task Verify_can_delete_limits_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHostLimits>()
                .Delete(x => x.For(string.Empty))
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }

        [Test]
        public async Task Verify_can_delete_limits_2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHostLimits>()
                .Delete(x => {})
                .ConfigureAwait(false);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }
    }
}