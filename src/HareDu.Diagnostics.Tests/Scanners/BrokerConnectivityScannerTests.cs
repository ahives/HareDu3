namespace HareDu.Diagnostics.Tests.Scanners;

using System.Collections.Generic;
using System.Linq;
using Core.Configuration;
using Core.Extensions;
using Diagnostics.Probes;
using Diagnostics.Scanners;
using KnowledgeBase;
using Model;
using NUnit.Framework;
using Snapshotting.Model;

[TestFixture]
public class BrokerConnectivityScannerTests
{
    IReadOnlyList<DiagnosticProbe> _probes;

    [OneTimeSetUp]
    public void Init()
    {
        var knowledgeBaseProvider = new KnowledgeBaseProvider();
        HareDuConfig config = new()
        {
            Diagnostics = new()
            {
                Probes = new()
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
            }
        };
            
        _probes = new List<DiagnosticProbe>
        {
            new HighConnectionCreationRateProbe(config.Diagnostics, knowledgeBaseProvider),
            new HighConnectionClosureRateProbe(config.Diagnostics, knowledgeBaseProvider),
            new UnlimitedPrefetchCountProbe(knowledgeBaseProvider),
            new ChannelThrottlingProbe(knowledgeBaseProvider),
            new ChannelLimitReachedProbe(knowledgeBaseProvider),
            new BlockedConnectionProbe(knowledgeBaseProvider)
        };
    }

    [Test]
    public void Verify_analyzers_fired()
    {
        BrokerConnectivitySnapshot snapshot = new ()
        {
            ConnectionsCreated = new () {Total = 100000, Rate = 102},
            ConnectionsClosed = new () {Total = 174000, Rate = 100},
            Connections = new List<ConnectionSnapshot>
            {
                new ()
                {
                    Identifier = "Connection1",
                    OpenChannelsLimit = 2,
                    Channels = new List<ChannelSnapshot>
                    {
                        new ()
                        {
                            PrefetchCount = 4,
                            UncommittedAcknowledgements = 2,
                            UncommittedMessages = 5,
                            UnconfirmedMessages = 8,
                            UnacknowledgedMessages = 5,
                            Consumers = 1,
                            Identifier = "Channel1"
                        }
                    },
                    State = BrokerConnectionState.Blocked
                }
            }
        };
            
        var result = new BrokerConnectivityScanner(_probes)
            .Scan(snapshot);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count, Is.EqualTo(6));
            Assert.That(result.Count(x => x.Id == typeof(HighConnectionCreationRateProbe).GetIdentifier()), Is.EqualTo(1));
            Assert.That(result.Count(x => x.Id == typeof(HighConnectionClosureRateProbe).GetIdentifier()), Is.EqualTo(1));
            Assert.That(result.Count(x => x.Id == typeof(UnlimitedPrefetchCountProbe).GetIdentifier()), Is.EqualTo(1));
            Assert.That(result.Count(x => x.Id == typeof(ChannelThrottlingProbe).GetIdentifier()), Is.EqualTo(1));
            Assert.That(result.Count(x => x.Id == typeof(ChannelLimitReachedProbe).GetIdentifier()), Is.EqualTo(1));
            Assert.That(result.Count(x => x.Id == typeof(BlockedConnectionProbe).GetIdentifier()), Is.EqualTo(1));
        });
    }

    [Test]
    public void Verify_empty_result_returned_when_snapshot_null()
    {
        BrokerConnectivitySnapshot snapshot = null;
            
        var result = new BrokerConnectivityScanner(_probes)
            .Scan(snapshot);

        Assert.That(result, Is.Empty);
    }
}