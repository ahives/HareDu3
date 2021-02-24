namespace HareDu.Diagnostics.Tests.Scanners
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.Configuration;
    using Core.Extensions;
    using Diagnostics.Probes;
    using Diagnostics.Scanners;
    using KnowledgeBase;
    using NUnit.Framework;
    using Snapshotting.Model;

    [TestFixture]
    public class ClusterScannerTests
    {
        IReadOnlyList<DiagnosticProbe> _probes;

        [OneTimeSetUp]
        public void Init()
        {
            var knowledgeBaseProvider = new KnowledgeBaseProvider();
            HareDuConfig config = new () {Diagnostics = new ()
            {
                Probes = new ()
                {
                    HighConnectionClosureRateThreshold = 100,
                    HighConnectionCreationRateThreshold = 100,
                    QueueHighFlowThreshold = 100,
                    QueueLowFlowThreshold = 20,
                    MessageRedeliveryThresholdCoefficient = 0.50M,
                    SocketUsageThresholdCoefficient = 0.60M,
                    RuntimeProcessUsageThresholdCoefficient = 0.65M,
                    FileDescriptorUsageThresholdCoefficient = 0.65M,
                    ConsumerUtilizationThreshold = 0.50M
                }
            }};
            
            _probes = new List<DiagnosticProbe>
            {
                new RuntimeProcessLimitProbe(config.Diagnostics, knowledgeBaseProvider),
                new SocketDescriptorThrottlingProbe(config.Diagnostics, knowledgeBaseProvider),
                new NetworkPartitionProbe(knowledgeBaseProvider),
                new MemoryAlarmProbe(knowledgeBaseProvider),
                new DiskAlarmProbe(knowledgeBaseProvider),
                new AvailableCpuCoresProbe(knowledgeBaseProvider),
                new FileDescriptorThrottlingProbe(config.Diagnostics, knowledgeBaseProvider),
            };
        }

        [Test]
        public void Verify_analyzers_fired()
        {
            ClusterSnapshot snapshot = new ()
            {
                Nodes = new List<NodeSnapshot>
                {
                    new ()
                    {
                        NetworkPartitions = new List<string>
                        {
                            "node1@rabbitmq",
                            "node2@rabbitmq",
                            "node3@rabbitmq"
                        },
                        Runtime = new() {Processes = new() {Limit = 38, Used = 36, UsageRate = 5.3M}},
                        Disk = new() {AlarmInEffect = true, Capacity = new() {Available = 8, Rate = 5.5M}},
                        Memory = new() {Used = 273, Limit = 270, AlarmInEffect = true},
                        OS = new()
                        {
                            FileDescriptors = new() {Available = 100, Used = 90, UsageRate = 5.5M},
                            SocketDescriptors = new() {Available = 100, Used = 90, UsageRate = 5.5M}
                        },
                        AvailableCoresDetected = 1
                    }
                }
            };
            
            var result = new ClusterScanner(_probes)
                .Scan(snapshot);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(7, result.Count);
                Assert.AreEqual(1, result.Count(x => x.Id == typeof(RuntimeProcessLimitProbe).GetIdentifier()));
                Assert.AreEqual(1, result.Count(x => x.Id == typeof(SocketDescriptorThrottlingProbe).GetIdentifier()));
                Assert.AreEqual(1, result.Count(x => x.Id == typeof(NetworkPartitionProbe).GetIdentifier()));
                Assert.AreEqual(1, result.Count(x => x.Id == typeof(MemoryAlarmProbe).GetIdentifier()));
                Assert.AreEqual(1, result.Count(x => x.Id == typeof(DiskAlarmProbe).GetIdentifier()));
                Assert.AreEqual(1, result.Count(x => x.Id == typeof(AvailableCpuCoresProbe).GetIdentifier()));
                Assert.AreEqual(1, result.Count(x => x.Id == typeof(FileDescriptorThrottlingProbe).GetIdentifier()));
            });
        }

        [Test]
        public void Verify_empty_result_returned_when_snapshot_null()
        {
            ClusterSnapshot snapshot = null;
            
            var result = new ClusterScanner(_probes)
                .Scan(snapshot);

            Assert.IsEmpty(result);
        }
    }
}