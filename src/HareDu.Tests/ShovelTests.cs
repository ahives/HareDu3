namespace HareDu.Tests
{
    using System.Threading.Tasks;
    using Core.Extensions;
    using Core.Serialization;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using Model;
    using NUnit.Framework;

    [TestFixture]
    public class ShovelTests :
        HareDuTesting
    {
        [Test]
        public async Task Verify_able_to_get_all_dynamic_shovels1()
        {
            var services = GetContainerBuilder("TestData/ShovelInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Shovel>()
                .GetAll();
            
            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.HasFaulted);
                Assert.IsTrue(result.HasData);
                Assert.IsNotNull(result.Data);
                Assert.AreEqual(1, result.Data.Count);
                Assert.IsNotNull(result.Data[0]);
                Assert.AreEqual("test-shovel", result.Data[0].Name);
                Assert.AreEqual("TestHareDu", result.Data[0].VirtualHost);
                Assert.AreEqual("rabbit@localhost", result.Data[0].Node);
                Assert.AreEqual("dynamic", result.Data[0].Type);
                Assert.AreEqual("starting", result.Data[0].State);
            });
        }

        [Test]
        public async Task Verify_able_to_get_all_dynamic_shovels2()
        {
            var services = GetContainerBuilder("TestData/ShovelInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .GetAllShovels();
            
            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.HasFaulted);
                Assert.IsTrue(result.HasData);
                Assert.IsNotNull(result.Data);
                Assert.AreEqual(1, result.Data.Count);
                Assert.IsNotNull(result.Data[0]);
                Assert.AreEqual("test-shovel", result.Data[0].Name);
                Assert.AreEqual("TestHareDu", result.Data[0].VirtualHost);
                Assert.AreEqual("rabbit@localhost", result.Data[0].Node);
                Assert.AreEqual("dynamic", result.Data[0].Type);
                Assert.AreEqual("starting", result.Data[0].State);
            });
        }

        [Test]
        public async Task Verify_can_create_dynamic_shovel1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Shovel>()
                .Create("test-shovel1", "TestHareDu", x =>
                {
                    x.Source("queue1", "amqp://user1@localhost", c =>
                    {
                        c.DeleteAfter(DeleteShovelAfterMode.QueueLength);
                    });
                    x.Destination("queue2", "amqp://user1@localhost");
                });
            
            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.HasFaulted);
                Assert.IsNotNull(result.DebugInfo);

                ShovelRequest request = result.DebugInfo.Request.ToObject<ShovelRequest>(Deserializer.Options);
                
                Assert.IsNotNull(request.Value);
                Assert.AreEqual(1000, request.Value.SourcePrefetchCount);
                Assert.AreEqual(ShovelProtocolType.Amqp091.Convert(), request.Value.SourceProtocol);
                Assert.AreEqual("queue1", request.Value.SourceQueue);
                Assert.AreEqual("amqp://user1@localhost", request.Value.SourceUri);
                Assert.AreEqual(DeleteShovelAfterMode.QueueLength.Convert(), request.Value.SourceDeleteAfter.ToString());
                Assert.AreEqual("queue2", request.Value.DestinationQueue);
                Assert.AreEqual("amqp://user1@localhost", request.Value.DestinationUri);
            });
        }

        [Test]
        public async Task Verify_can_create_dynamic_shovel2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .CreateShovel("test-shovel1", "TestHareDu", x =>
                {
                    x.Source("queue1", "amqp://user1@localhost", c =>
                    {
                        c.DeleteAfter(DeleteShovelAfterMode.QueueLength);
                    });
                    x.Destination("queue2", "amqp://user1@localhost");
                });
            
            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.HasFaulted);
                Assert.IsNotNull(result.DebugInfo);

                ShovelRequest request = result.DebugInfo.Request.ToObject<ShovelRequest>(Deserializer.Options);
                
                Assert.IsNotNull(request.Value);
                Assert.AreEqual(1000, request.Value.SourcePrefetchCount);
                Assert.AreEqual(ShovelProtocolType.Amqp091.Convert(), request.Value.SourceProtocol);
                Assert.AreEqual("queue1", request.Value.SourceQueue);
                Assert.AreEqual("amqp://user1@localhost", request.Value.SourceUri);
                Assert.AreEqual(DeleteShovelAfterMode.QueueLength.Convert(), request.Value.SourceDeleteAfter.ToString());
                Assert.AreEqual("queue2", request.Value.DestinationQueue);
                Assert.AreEqual("amqp://user1@localhost", request.Value.DestinationUri);
            });
        }

        [Test]
        public async Task Verify_can_delete_shovel1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Shovel>()
                .Delete("test-shovel2","TestHareDu");
            
            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.HasFaulted);
                Assert.IsNotNull(result.DebugInfo);
            });
        }

        [Test]
        public async Task Verify_can_delete_shovel2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .DeleteShovel("test-shovel2","TestHareDu");
            
            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.HasFaulted);
                Assert.IsNotNull(result.DebugInfo);
            });
        }
    }
}