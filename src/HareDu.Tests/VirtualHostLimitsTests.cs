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
    public class VirtualHostLimitsTests :
        HareDuTesting
    {
        [Test]
        public async Task Verify_can_get_all_limits1()
        {
            var services = GetContainerBuilder("TestData/VirtualHostLimitsInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHostLimits>()
                .GetAll();
            
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
        public async Task Verify_can_get_all_limits2()
        {
            var services = GetContainerBuilder("TestData/VirtualHostLimitsInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .GetAllVirtualHostLimits();
            
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
        public async Task Verify_can_define_limits1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHostLimits>()
                .Define("HareDu5", x =>
                {
                    x.SetMaxQueueLimit(100);
                    x.SetMaxConnectionLimit(1000);
                });
            
            result.HasFaulted.ShouldBeFalse();
            result.DebugInfo.ShouldNotBeNull();

            VirtualHostLimitsDefinition definition = result.DebugInfo.Request.ToObject<VirtualHostLimitsDefinition>(Deserializer.Options);
            
            definition.MaxConnectionLimit.ShouldBe<ulong>(1000);
            definition.MaxQueueLimit.ShouldBe<ulong>(100);
        }

        [Test]
        public async Task Verify_can_define_limits2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .DefineVirtualHostLimits("HareDu5", x =>
                {
                    x.SetMaxQueueLimit(100);
                    x.SetMaxConnectionLimit(1000);
                });
            
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
                .Define(string.Empty, x =>
                {
                    x.SetMaxQueueLimit(100);
                    x.SetMaxConnectionLimit(1000);
                });
            
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
                .Define("FakeVirtualHost", x =>
                {
                    x.SetMaxConnectionLimit(1000);
                });
            
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
                .Define("FakeVirtualHost", x =>
                {
                    x.SetMaxQueueLimit(100);
                });
            
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
                .Define("FakeVirtualHost");
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(2);
        }

        [Test]
        public async Task Verify_can_delete_limits_1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHostLimits>()
                .Delete(string.Empty);
            
            result.HasFaulted.ShouldBeTrue();
            result.Errors.Count.ShouldBe(1);
        }
    }
}