namespace HareDu.Tests;

using System;
using System.Threading.Tasks;
using Core;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;

[TestFixture]
public class ConnectionTests :
    HareDuTesting
{
    [Test]
    public async Task Verify_can_get_all_connections1()
    {
        var result = await GetContainerBuilder("TestData/ConnectionInfo.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Connection>(x => x.UsingCredentials("guest", "guest"))
            .GetAll();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(2));
            Assert.That(result.Data[0].Host, Is.EqualTo("127.0.0.0"));
            Assert.That(result.Data[0].Name, Is.EqualTo("127.0.0.0:79863 -> 127.0.0.0:5672"));
            Assert.That(result.Data[0].Node, Is.EqualTo("rabbit@localhost"));
            Assert.That(result.Data[0].State, Is.EqualTo(BrokerConnectionState.Running));
            Assert.That(result.Data[0].Type, Is.EqualTo(ConnectionType.Network));
            Assert.That(result.Data[0].AuthenticationMechanism, Is.EqualTo("PLAIN"));
            Assert.That(result.Data[0].ConnectedAt, Is.EqualTo(1572749839566));
            Assert.That(result.Data[0].MaxFrameSizeInBytes, Is.EqualTo(131072));
            Assert.That(result.Data[0].OpenChannelsLimit, Is.EqualTo(2047));
            Assert.That(result.Data[0].PeerHost, Is.EqualTo("127.0.0.0"));
            Assert.That(result.Data[0].PeerPort, Is.EqualTo(58675));
            Assert.That(result.Data[0].PeerCertificateIssuer, Is.Null);
            Assert.That(result.Data[0].PeerCertificateSubject, Is.Null);
            Assert.That(result.Data[0].TimePeriodPeerCertificateValid, Is.Null);
            Assert.That(result.Data[0].Protocol, Is.EqualTo("AMQP 0-9-1"));
            Assert.That(result.Data[0].Channels, Is.EqualTo(0));
            Assert.That(result.Data[0].PacketsReceived, Is.EqualTo(4));
            Assert.That(result.Data[0].PacketBytesReceived, Is.EqualTo(748));
            Assert.That(result.Data[0].PacketBytesReceivedDetails, Is.Not.Null);
            Assert.That(result.Data[0].PacketBytesReceivedDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].PacketsSent, Is.EqualTo(3));
            Assert.That(result.Data[0].PacketBytesSent, Is.EqualTo(542));
            Assert.That(result.Data[0].PacketBytesSentDetails, Is.Not.Null);
            Assert.That(result.Data[0].PacketBytesSentDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].TotalReductions, Is.EqualTo(5956));
            Assert.That(result.Data[0].ReductionDetails, Is.Not.Null);
            Assert.That(result.Data[0].ReductionDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].SendPending, Is.EqualTo(0));
            Assert.That(result.Data[0].IsSsl, Is.False);
            Assert.That(result.Data[0].User, Is.EqualTo("guest"));
            Assert.That(result.Data[0].UserWhoPerformedAction, Is.EqualTo("guest"));
            Assert.That(result.Data[0].ConnectionTimeout, Is.EqualTo(60));
            Assert.That(result.Data[0].ConnectionClientProperties, Is.Not.Null);
            Assert.That(result.Data[0].ConnectionClientProperties?.ClientApi, Is.EqualTo("MassTransit"));
            Assert.That(result.Data[0].ConnectionClientProperties?.ConnectionName, Is.EqualTo("undefined"));
            Assert.That(result.Data[0].ConnectionClientProperties?.Assembly, Is.EqualTo("HareDu.IntegrationTesting.Publisher"));
            Assert.That(result.Data[0].ConnectionClientProperties?.AssemblyVersion, Is.EqualTo("2.0.0.0"));
            Assert.That(result.Data[0].ConnectionClientProperties?.Copyright, Is.EqualTo("Copyright (c) 2007-2016 Pivotal Software, Inc."));
            Assert.That(result.Data[0].ConnectionClientProperties?.Host, Is.EqualTo("HareDu"));
            Assert.That(result.Data[0].ConnectionClientProperties?.Information, Is.EqualTo("Licensed under the MPL.  See http://www.rabbitmq.com/"));
            Assert.That(result.Data[0].ConnectionClientProperties?.Platform, Is.EqualTo(".NET"));
            Assert.That(result.Data[0].ConnectionClientProperties?.ProcessId, Is.EqualTo("74446"));
            Assert.That(result.Data[0].ConnectionClientProperties?.ProcessName, Is.EqualTo("dotnet"));
            Assert.That(result.Data[0].ConnectionClientProperties?.Product, Is.EqualTo("RabbitMQ"));
            Assert.That(result.Data[0].ConnectionClientProperties?.Version, Is.EqualTo("5.1.0"));
            Assert.That(result.Data[0].ConnectionClientProperties?.Connected, Is.EqualTo(DateTimeOffset.Parse("Sun, 03 Nov 2019 02:57:19 GMT")));
            Assert.That(result.Data[0].ConnectionClientProperties?.Capabilities, Is.Not.Null);
            Assert.That(result.Data[0].ConnectionClientProperties?.Capabilities?.ExchangeBindingEnabled, Is.True);
            Assert.That(result.Data[0].ConnectionClientProperties?.Capabilities?.PublisherConfirmsEnabled, Is.True);
            Assert.That(result.Data[0].ConnectionClientProperties?.Capabilities?.AuthenticationFailureNotificationEnabled, Is.True);
            Assert.That(result.Data[0].ConnectionClientProperties?.Capabilities?.ConnectionBlockedNotificationsEnabled, Is.True);
            Assert.That(result.Data[0].ConnectionClientProperties?.Capabilities?.ConsumerCancellationNotificationsEnabled, Is.True);
            Assert.That(result.Data[0].ConnectionClientProperties?.Capabilities?.NegativeAcknowledgmentNotificationsEnabled, Is.True);
        });
    }

    [Test]
    public async Task Verify_can_get_all_connections2()
    {
        var result = await GetContainerBuilder("TestData/ConnectionInfo.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .GetAllConnections(x => x.UsingCredentials("guest", "guest"));
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(2));
            Assert.That(result.Data[0].Host, Is.EqualTo("127.0.0.0"));
            Assert.That(result.Data[0].Name, Is.EqualTo("127.0.0.0:79863 -> 127.0.0.0:5672"));
            Assert.That(result.Data[0].Node, Is.EqualTo("rabbit@localhost"));
            Assert.That(result.Data[0].State, Is.EqualTo(BrokerConnectionState.Running));
            Assert.That(result.Data[0].Type, Is.EqualTo(ConnectionType.Network));
            Assert.That(result.Data[0].AuthenticationMechanism, Is.EqualTo("PLAIN"));
            Assert.That(result.Data[0].ConnectedAt, Is.EqualTo(1572749839566));
            Assert.That(result.Data[0].MaxFrameSizeInBytes, Is.EqualTo(131072));
            Assert.That(result.Data[0].OpenChannelsLimit, Is.EqualTo(2047));
            Assert.That(result.Data[0].PeerHost, Is.EqualTo("127.0.0.0"));
            Assert.That(result.Data[0].PeerPort, Is.EqualTo(58675));
            Assert.That(result.Data[0].PeerCertificateIssuer, Is.Null);
            Assert.That(result.Data[0].PeerCertificateSubject, Is.Null);
            Assert.That(result.Data[0].TimePeriodPeerCertificateValid, Is.Null);
            Assert.That(result.Data[0].Protocol, Is.EqualTo("AMQP 0-9-1"));
            Assert.That(result.Data[0].Channels, Is.EqualTo(0));
            Assert.That(result.Data[0].PacketsReceived, Is.EqualTo(4));
            Assert.That(result.Data[0].PacketBytesReceived, Is.EqualTo(748));
            Assert.That(result.Data[0].PacketBytesReceivedDetails, Is.Not.Null);
            Assert.That(result.Data[0].PacketBytesReceivedDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].PacketsSent, Is.EqualTo(3));
            Assert.That(result.Data[0].PacketBytesSent, Is.EqualTo(542));
            Assert.That(result.Data[0].PacketBytesSentDetails, Is.Not.Null);
            Assert.That(result.Data[0].PacketBytesSentDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].TotalReductions, Is.EqualTo(5956));
            Assert.That(result.Data[0].ReductionDetails, Is.Not.Null);
            Assert.That(result.Data[0].ReductionDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].SendPending, Is.EqualTo(0));
            Assert.That(result.Data[0].IsSsl, Is.False);
            Assert.That(result.Data[0].User, Is.EqualTo("guest"));
            Assert.That(result.Data[0].UserWhoPerformedAction, Is.EqualTo("guest"));
            Assert.That(result.Data[0].ConnectionTimeout, Is.EqualTo(60));
            Assert.That(result.Data[0].ConnectionClientProperties, Is.Not.Null);
            Assert.That(result.Data[0].ConnectionClientProperties?.ClientApi, Is.EqualTo("MassTransit"));
            Assert.That(result.Data[0].ConnectionClientProperties?.ConnectionName, Is.EqualTo("undefined"));
            Assert.That(result.Data[0].ConnectionClientProperties?.Assembly, Is.EqualTo("HareDu.IntegrationTesting.Publisher"));
            Assert.That(result.Data[0].ConnectionClientProperties?.AssemblyVersion, Is.EqualTo("2.0.0.0"));
            Assert.That(result.Data[0].ConnectionClientProperties?.Copyright, Is.EqualTo("Copyright (c) 2007-2016 Pivotal Software, Inc."));
            Assert.That(result.Data[0].ConnectionClientProperties?.Host, Is.EqualTo("HareDu"));
            Assert.That(result.Data[0].ConnectionClientProperties?.Information, Is.EqualTo("Licensed under the MPL.  See http://www.rabbitmq.com/"));
            Assert.That(result.Data[0].ConnectionClientProperties?.Platform, Is.EqualTo(".NET"));
            Assert.That(result.Data[0].ConnectionClientProperties?.ProcessId, Is.EqualTo("74446"));
            Assert.That(result.Data[0].ConnectionClientProperties?.ProcessName, Is.EqualTo("dotnet"));
            Assert.That(result.Data[0].ConnectionClientProperties?.Product, Is.EqualTo("RabbitMQ"));
            Assert.That(result.Data[0].ConnectionClientProperties?.Version, Is.EqualTo("5.1.0"));
            Assert.That(result.Data[0].ConnectionClientProperties?.Connected, Is.EqualTo(DateTimeOffset.Parse("Sun, 03 Nov 2019 02:57:19 GMT")));
            Assert.That(result.Data[0].ConnectionClientProperties?.Capabilities, Is.Not.Null);
            Assert.That(result.Data[0].ConnectionClientProperties?.Capabilities?.ExchangeBindingEnabled, Is.True);
            Assert.That(result.Data[0].ConnectionClientProperties?.Capabilities?.PublisherConfirmsEnabled, Is.True);
            Assert.That(result.Data[0].ConnectionClientProperties?.Capabilities?.AuthenticationFailureNotificationEnabled, Is.True);
            Assert.That(result.Data[0].ConnectionClientProperties?.Capabilities?.ConnectionBlockedNotificationsEnabled, Is.True);
            Assert.That(result.Data[0].ConnectionClientProperties?.Capabilities?.ConsumerCancellationNotificationsEnabled, Is.True);
            Assert.That(result.Data[0].ConnectionClientProperties?.Capabilities?.NegativeAcknowledgmentNotificationsEnabled, Is.True);
        });
    }

    [Test]
    public async Task Verify_can_get_all_connections_by_name()
    {
        var result = await GetContainerBuilder("TestData/ConnectionInfo.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Connection>(x => x.UsingCredentials("guest", "guest"))
            .GetByName("127.0.0.0:79863 -> 127.0.0.0:5672");
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.True);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data.Count, Is.EqualTo(2));
            Assert.That(result.Data[0].Host, Is.EqualTo("127.0.0.0"));
            Assert.That(result.Data[0].Name, Is.EqualTo("127.0.0.0:79863 -> 127.0.0.0:5672"));
            Assert.That(result.Data[0].Node, Is.EqualTo("rabbit@localhost"));
            Assert.That(result.Data[0].State, Is.EqualTo(BrokerConnectionState.Running));
            Assert.That(result.Data[0].Type, Is.EqualTo(ConnectionType.Network));
            Assert.That(result.Data[0].AuthenticationMechanism, Is.EqualTo("PLAIN"));
            Assert.That(result.Data[0].ConnectedAt, Is.EqualTo(1572749839566));
            Assert.That(result.Data[0].MaxFrameSizeInBytes, Is.EqualTo(131072));
            Assert.That(result.Data[0].OpenChannelsLimit, Is.EqualTo(2047));
            Assert.That(result.Data[0].PeerHost, Is.EqualTo("127.0.0.0"));
            Assert.That(result.Data[0].PeerPort, Is.EqualTo(58675));
            Assert.That(result.Data[0].PeerCertificateIssuer, Is.Null);
            Assert.That(result.Data[0].PeerCertificateSubject, Is.Null);
            Assert.That(result.Data[0].TimePeriodPeerCertificateValid, Is.Null);
            Assert.That(result.Data[0].Protocol, Is.EqualTo("AMQP 0-9-1"));
            Assert.That(result.Data[0].Channels, Is.EqualTo(0));
            Assert.That(result.Data[0].PacketsReceived, Is.EqualTo(4));
            Assert.That(result.Data[0].PacketBytesReceived, Is.EqualTo(748));
            Assert.That(result.Data[0].PacketBytesReceivedDetails, Is.Not.Null);
            Assert.That(result.Data[0].PacketBytesReceivedDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].PacketsSent, Is.EqualTo(3));
            Assert.That(result.Data[0].PacketBytesSent, Is.EqualTo(542));
            Assert.That(result.Data[0].PacketBytesSentDetails, Is.Not.Null);
            Assert.That(result.Data[0].PacketBytesSentDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].TotalReductions, Is.EqualTo(5956));
            Assert.That(result.Data[0].ReductionDetails, Is.Not.Null);
            Assert.That(result.Data[0].ReductionDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].SendPending, Is.EqualTo(0));
            Assert.That(result.Data[0].IsSsl, Is.False);
            Assert.That(result.Data[0].User, Is.EqualTo("guest"));
            Assert.That(result.Data[0].UserWhoPerformedAction, Is.EqualTo("guest"));
            Assert.That(result.Data[0].ConnectionTimeout, Is.EqualTo(60));
            Assert.That(result.Data[0].ConnectionClientProperties, Is.Not.Null);
            Assert.That(result.Data[0].ConnectionClientProperties?.ClientApi, Is.EqualTo("MassTransit"));
            Assert.That(result.Data[0].ConnectionClientProperties?.ConnectionName, Is.EqualTo("undefined"));
            Assert.That(result.Data[0].ConnectionClientProperties?.Assembly, Is.EqualTo("HareDu.IntegrationTesting.Publisher"));
            Assert.That(result.Data[0].ConnectionClientProperties?.AssemblyVersion, Is.EqualTo("2.0.0.0"));
            Assert.That(result.Data[0].ConnectionClientProperties?.Copyright, Is.EqualTo("Copyright (c) 2007-2016 Pivotal Software, Inc."));
            Assert.That(result.Data[0].ConnectionClientProperties?.Host, Is.EqualTo("HareDu"));
            Assert.That(result.Data[0].ConnectionClientProperties?.Information, Is.EqualTo("Licensed under the MPL.  See http://www.rabbitmq.com/"));
            Assert.That(result.Data[0].ConnectionClientProperties?.Platform, Is.EqualTo(".NET"));
            Assert.That(result.Data[0].ConnectionClientProperties?.ProcessId, Is.EqualTo("74446"));
            Assert.That(result.Data[0].ConnectionClientProperties?.ProcessName, Is.EqualTo("dotnet"));
            Assert.That(result.Data[0].ConnectionClientProperties?.Product, Is.EqualTo("RabbitMQ"));
            Assert.That(result.Data[0].ConnectionClientProperties?.Version, Is.EqualTo("5.1.0"));
            Assert.That(result.Data[0].ConnectionClientProperties?.Connected, Is.EqualTo(DateTimeOffset.Parse("Sun, 03 Nov 2019 02:57:19 GMT")));
            Assert.That(result.Data[0].ConnectionClientProperties?.Capabilities, Is.Not.Null);
            Assert.That(result.Data[0].ConnectionClientProperties?.Capabilities?.ExchangeBindingEnabled, Is.True);
            Assert.That(result.Data[0].ConnectionClientProperties?.Capabilities?.PublisherConfirmsEnabled, Is.True);
            Assert.That(result.Data[0].ConnectionClientProperties?.Capabilities?.AuthenticationFailureNotificationEnabled, Is.True);
            Assert.That(result.Data[0].ConnectionClientProperties?.Capabilities?.ConnectionBlockedNotificationsEnabled, Is.True);
            Assert.That(result.Data[0].ConnectionClientProperties?.Capabilities?.ConsumerCancellationNotificationsEnabled, Is.True);
            Assert.That(result.Data[0].ConnectionClientProperties?.Capabilities?.NegativeAcknowledgmentNotificationsEnabled, Is.True);
        });
    }
}