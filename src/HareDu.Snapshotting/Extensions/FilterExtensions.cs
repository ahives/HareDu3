namespace HareDu.Snapshotting.Extensions;

using System.Collections.Generic;
using System.Linq;
using HareDu.Model;
using Model;

public static class FilterExtensions
{
    public static IReadOnlyList<ChannelSnapshot> FilterByConnection(this IReadOnlyList<ChannelInfo> channels, string connection)
    {
        if (channels == null || !channels.Any())
            return new List<ChannelSnapshot>();

        return channels
            .Where(x => x.ConnectionDetails?.Name == connection)
            .Select(x => new ChannelSnapshot
            {
                Identifier = x.Name,
                ConnectionIdentifier = connection,
                Consumers = x.TotalConsumers,
                Node = x.Node,
                PrefetchCount = x.PrefetchCount,
                UncommittedAcknowledgements = x.UncommittedAcknowledgements,
                UncommittedMessages = x.UncommittedMessages,
                UnconfirmedMessages = x.UnconfirmedMessages,
                UnacknowledgedMessages = x.UnacknowledgedMessages,
                QueueOperations = new ()
                {
                    Incoming = new ()
                    {
                        Total = x.OperationStats?.TotalMessagesPublished ?? 0,
                        Rate = x.OperationStats?.MessagesPublishedDetails?.Value ?? 0.0M
                    },
                    Gets = new()
                    {
                        Total = x.OperationStats?.TotalMessageGets ?? 0,
                        Rate = x.OperationStats?.MessageGetDetails?.Value ?? 0.0M
                    },
                    GetsWithoutAck = new ()
                    {
                        Total = x.OperationStats?.TotalMessageGetsWithoutAck ?? 0,
                        Rate = x.OperationStats?.MessageGetsWithoutAckDetails?.Value ?? 0.0M
                    },
                    Delivered = new ()
                    {
                        Total = x.OperationStats?.TotalMessagesDelivered ?? 0,
                        Rate = x.OperationStats?.MessageDeliveryDetails?.Value ?? 0.0M
                    },
                    DeliveredWithoutAck = new ()
                    {
                        Total = x.OperationStats?.TotalMessageDeliveredWithoutAck ?? 0,
                        Rate = x.OperationStats?.MessagesDeliveredWithoutAckDetails?.Value ?? 0.0M
                    },
                    DeliveredGets = new ()
                    {
                        Total = x.OperationStats?.TotalMessageDeliveryGets ?? 0,
                        Rate = x.OperationStats?.MessageDeliveryGetDetails?.Value ?? 0.0M
                    },
                    Redelivered = new ()
                    {
                        Total = x.OperationStats?.TotalMessagesRedelivered ?? 0,
                        Rate = x.OperationStats?.MessagesRedeliveredDetails?.Value ?? 0.0M
                    },
                    Acknowledged = new ()
                    {
                        Total = x.OperationStats?.TotalMessagesAcknowledged ?? 0,
                        Rate = x.OperationStats?.MessagesAcknowledgedDetails?.Value ?? 0.0M
                    },
                    NotRouted = new ()
                    {
                        Total = x.OperationStats?.TotalMessagesNotRouted ?? 0,
                        Rate = x.OperationStats?.MessagesNotRoutedDetails?.Value ?? 0.0M
                    }
                }
            })
            .ToList();
    }

    public static IEnumerable<ConnectionInfo> FilterByNode(this IReadOnlyList<ConnectionInfo> connections, string node)
    {
        if (connections == null || !connections.Any())
            return Enumerable.Empty<ConnectionInfo>();

        return connections.Where(x => x.Node == node);
    }
}