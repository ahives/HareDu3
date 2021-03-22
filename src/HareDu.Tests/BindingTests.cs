namespace HareDu.Tests
{
    using System.Threading.Tasks;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using Model;
    using NUnit.Framework;
    using Serialization.Converters;

    [TestFixture]
    public class BindingTests :
        HareDuTesting
    {
        [Test]
        public async Task Verify_able_to_get_all_bindings1()
        {
            var services = GetContainerBuilder("TestData/BindingInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .GetAll();

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasData);
                Assert.AreEqual(12, result.Data.Count);
                Assert.IsFalse(result.HasFaulted);
                Assert.IsNotNull(result.Data);
            });
        }

        [Test]
        public async Task Verify_able_to_get_all_bindings2()
        {
            var services = GetContainerBuilder("TestData/BindingInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .GetAllBindings();

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasData);
                Assert.AreEqual(12, result.Data.Count);
                Assert.IsFalse(result.HasFaulted);
                Assert.IsNotNull(result.Data);
            });
        }

        [Test]
        public async Task Verify_can_create_exchange_binding_without_arguments1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create("E2", "Q1", BindingType.Exchange, "HareDu");

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.HasFaulted);
                Assert.IsNotNull(result.DebugInfo);
                Assert.IsNotNull(result.DebugInfo.Request);

                BindingRequest request = result.DebugInfo.Request.ToObject<BindingRequest>(Deserializer.Options);
                
                Assert.That(request.BindingKey, Is.Empty.Or.Null);
                Assert.IsNull(request.Arguments);
            });
        }

        [Test]
        public async Task Verify_can_create_exchange_binding_without_arguments2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .CreateExchangeBinding("E2", "Q1", "HareDu");

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.HasFaulted);
                Assert.IsNotNull(result.DebugInfo);
                Assert.IsNotNull(result.DebugInfo.Request);

                BindingRequest request = result.DebugInfo.Request.ToObject<BindingRequest>(Deserializer.Options);
                
                Assert.That(request.BindingKey, Is.Empty.Or.Null);
                Assert.IsNull(request.Arguments);
            });
        }

        [Test]
        public async Task Verify_can_create_queue_binding_without_arguments1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create("E2", "Q1", BindingType.Queue, "HareDu");

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.HasFaulted);
                Assert.IsNotNull(result.DebugInfo);
                Assert.IsNotNull(result.DebugInfo.Request);

                BindingRequest request = result.DebugInfo.Request.ToObject<BindingRequest>(Deserializer.Options);
                
                Assert.That(request.BindingKey, Is.Empty.Or.Null);
                Assert.IsNull(request.Arguments);
            });
        }

        [Test]
        public async Task Verify_can_create_queue_binding_without_arguments2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .CreateExchangeBindingToQueue("E2", "Q1", "HareDu");

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.HasFaulted);
                Assert.IsNotNull(result.DebugInfo);
                Assert.IsNotNull(result.DebugInfo.Request);

                BindingRequest request = result.DebugInfo.Request.ToObject<BindingRequest>(Deserializer.Options);
                
                Assert.That(request.BindingKey, Is.Empty.Or.Null);
                Assert.IsNull(request.Arguments);
            });
        }

        [Test]
        public async Task Verify_can_create_exchange_binding_with_arguments1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create("E2", "Q1", BindingType.Exchange, "HareDu", "*.", x =>
                {
                    x.Add("arg1", "value1");
                });

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.HasFaulted);
                Assert.IsNotNull(result.DebugInfo);
                Assert.IsNotNull(result.DebugInfo.Request);
                
                BindingRequest request = result.DebugInfo.Request.ToObject<BindingRequest>(Deserializer.Options);

                Assert.AreEqual("*.", request.BindingKey);
                Assert.AreEqual("value1", request.Arguments["arg1"].ToString());
            });
        }

        [Test]
        public async Task Verify_can_create_exchange_binding_with_arguments2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .CreateExchangeBinding("E2", "Q1", "HareDu", "*.", x =>
                {
                    x.Add("arg1", "value1");
                });

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.HasFaulted);
                Assert.IsNotNull(result.DebugInfo);
                Assert.IsNotNull(result.DebugInfo.Request);
                
                BindingRequest request = result.DebugInfo.Request.ToObject<BindingRequest>(Deserializer.Options);

                Assert.AreEqual("*.", request.BindingKey);
                Assert.AreEqual("value1", request.Arguments["arg1"].ToString());
            });
        }

        [Test]
        public async Task Verify_can_create_queue_binding_with_arguments1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create("E2", "Q1", BindingType.Queue, "HareDu", "*.", x =>
                {
                    x.Add("arg1", "value1");
                });

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.HasFaulted);
                Assert.IsNotNull(result.DebugInfo);
                Assert.IsNotNull(result.DebugInfo.Request);
                
                BindingRequest request = result.DebugInfo.Request.ToObject<BindingRequest>(Deserializer.Options);

                Assert.AreEqual("*.", request.BindingKey);
                Assert.AreEqual("value1", request.Arguments["arg1"].ToString());
            });
        }

        [Test]
        public async Task Verify_can_create_queue_binding_with_arguments2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .CreateExchangeBindingToQueue("E2", "Q1", "HareDu", "*.", x =>
                {
                    x.Add("arg1", "value1");
                });

            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.HasFaulted);
                Assert.IsNotNull(result.DebugInfo);
                Assert.IsNotNull(result.DebugInfo.Request);
                
                BindingRequest request = result.DebugInfo.Request.ToObject<BindingRequest>(Deserializer.Options);

                Assert.AreEqual("*.", request.BindingKey);
                Assert.AreEqual("value1", request.Arguments["arg1"].ToString());
            });
        }

        [Test]
        public async Task Verify_cannot_create_exchange_binding_without_arguments1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create(string.Empty, "Q1", BindingType.Exchange, "HareDu");

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasFaulted);
                Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_cannot_create_exchange_binding_without_arguments2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create("E1", string.Empty, BindingType.Exchange, "HareDu");

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasFaulted);
                Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_cannot_create_exchange_binding_without_arguments3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .CreateExchangeBinding(string.Empty, "Q1", "HareDu");

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasFaulted);
                Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_cannot_create_exchange_binding_without_arguments4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .CreateExchangeBinding("E1", string.Empty, "HareDu");

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasFaulted);
                Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_cannot_create_queue_binding_without_arguments1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .CreateExchangeBindingToQueue(string.Empty, "Q1", "HareDu");

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasFaulted);
                Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_cannot_create_queue_binding_without_arguments2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .CreateExchangeBindingToQueue("E1", string.Empty, "HareDu");

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasFaulted);
                Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_cannot_create_queue_binding_without_arguments3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .CreateExchangeBindingToQueue(string.Empty, string.Empty, "HareDu");

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasFaulted);
                Assert.AreEqual(2, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_cannot_create_queue_binding_without_arguments4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .CreateExchangeBindingToQueue("E1", "Q1", string.Empty);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasFaulted);
                Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_can_delete_queue_binding1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete("E1", "Q1", string.Empty, "HareDu", BindingType.Queue);
            
            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.HasFaulted);
                Assert.AreEqual(0, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_can_delete_queue_binding2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .CreateExchangeBindingToQueue("E1", "Q1", "HareDu");
            
            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.HasFaulted);
                Assert.AreEqual(0, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_can_delete_queue_binding3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete("E1", "Q1", string.Empty, "HareDu", BindingType.Exchange);
            
            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.HasFaulted);
                Assert.AreEqual(0, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_can_delete_queue_binding4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .CreateExchangeBinding("E1", "Q1", "HareDu");
            
            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.HasFaulted);
                Assert.AreEqual(0, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_cannot_delete_queue_binding1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete("E2", string.Empty, string.Empty, "HareDu", BindingType.Queue);
            
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasFaulted);
                Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_cannot_delete_queue_binding2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .DeleteQueueBinding("E2", string.Empty, string.Empty, "HareDu");
            
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasFaulted);
                Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_cannot_delete_queue_binding3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete(string.Empty, string.Empty, string.Empty, "HareDu", BindingType.Queue);
            
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasFaulted);
                Assert.AreEqual(2, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_cannot_delete_queue_binding4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .DeleteQueueBinding(string.Empty, string.Empty, string.Empty, "HareDu");
            
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasFaulted);
                Assert.AreEqual(2, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_cannot_delete_queue_binding5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete(string.Empty, string.Empty, string.Empty, string.Empty, BindingType.Queue);
            
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasFaulted);
                Assert.AreEqual(3, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_cannot_delete_queue_binding6()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .DeleteQueueBinding(string.Empty, string.Empty, string.Empty, string.Empty);
            
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasFaulted);
                Assert.AreEqual(3, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_cannot_delete_exchange_binding1()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete("E2", string.Empty, string.Empty, "HareDu", BindingType.Exchange);
            
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasFaulted);
                Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_cannot_delete_exchange_binding2()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .DeleteExchangeBinding("E2", string.Empty, string.Empty, "HareDu");
            
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasFaulted);
                Assert.AreEqual(1, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_cannot_delete_exchange_binding3()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete(string.Empty, string.Empty, string.Empty, "HareDu", BindingType.Exchange);
            
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasFaulted);
                Assert.AreEqual(2, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_cannot_delete_exchange_binding4()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .DeleteExchangeBinding(string.Empty, string.Empty, string.Empty, "HareDu");
            
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasFaulted);
                Assert.AreEqual(2, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_cannot_delete_exchange_binding5()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Delete(string.Empty, string.Empty, string.Empty, string.Empty, BindingType.Exchange);
            
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasFaulted);
                Assert.AreEqual(3, result.DebugInfo.Errors.Count);
            });
        }

        [Test]
        public async Task Verify_cannot_delete_exchange_binding6()
        {
            var services = GetContainerBuilder().BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .DeleteExchangeBinding(string.Empty, string.Empty, string.Empty, string.Empty);
            
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasFaulted);
                Assert.AreEqual(3, result.DebugInfo.Errors.Count);
            });
        }
    }
}