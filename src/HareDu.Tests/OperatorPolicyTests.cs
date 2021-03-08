namespace HareDu.Tests
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;

    [TestFixture]
    public class OperatorPolicyTests :
        HareDuTesting
    {
        [Test]
        public async Task Should_be_able_to_get_all_policies1()
        {
            var services = GetContainerBuilder("TestData/OperatorPolicyInfo.json").BuildServiceProvider();
            var result = await services.GetService<IBrokerObjectFactory>()
                .Object<OperatorPolicy>()
                .GetAll();

            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.HasData);
                Assert.IsFalse(result.HasFaulted);
                Assert.IsNotNull(result.Data);
                Assert.AreEqual(1, result.Data.Count);
                Assert.AreEqual("test", result.Data[0].Name);
                Assert.AreEqual("TestHareDu", result.Data[0].VirtualHost);
                Assert.AreEqual(".*", result.Data[0].Pattern);
                Assert.AreEqual("queues", result.Data[0].AppliedTo);
                Assert.IsNotNull(result.Data[0].Definition);
                Assert.AreEqual(100, result.Data[0].Definition["max-length"]);
                Assert.AreEqual(0, result.Data[0].Priority);
            });
        }
    }
}