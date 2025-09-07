namespace HareDu.Tests;

using System.Threading.Tasks;
using Core;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;

[TestFixture]
public class NodeTests :
    HareDuTesting
{
    [Test]
    public async Task Should_be_able_to_get_all_nodes1()
    {
        var result = await GetContainerBuilder("TestData/NodeInfo.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Node>(x => x.UsingCredentials("guest", "guest"))
            .GetAll();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasData, Is.True);
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data[0], Is.Not.Null);
            Assert.That(result.Data[0].Applications.Count, Is.EqualTo(32));
            Assert.That(result.Data[0].Contexts.Count, Is.EqualTo(1));
            Assert.That(result.Data[0].Contexts[0].Description, Is.EqualTo("RabbitMQ Management"));;
            Assert.That(result.Data[0].Contexts[0].Path, Is.EqualTo("/"));
            Assert.That(result.Data[0].Contexts[0].Port, Is.EqualTo("15672"));;
            Assert.That(result.Data[0].Name, Is.EqualTo("rabbit@localhost"));;
            Assert.That(result.Data[0].Partitions.Count, Is.EqualTo(2));
            Assert.That(result.Data[0].Partitions[0], Is.EqualTo("node1"));;
            Assert.That(result.Data[0].Uptime, Is.EqualTo(737892679));
            Assert.That(result.Data[0].AuthenticationMechanisms, Is.Not.Null);
            Assert.That(result.Data[0].AuthenticationMechanisms.Count, Is.EqualTo(3));
            Assert.That(result.Data[0].AuthenticationMechanisms[0].Name, Is.EqualTo("RABBIT-CR-DEMO"));
            Assert.That(result.Data[0].AuthenticationMechanisms[0].Description, Is.EqualTo("RabbitMQ Demo challenge-response authentication mechanism"));;
            Assert.That(result.Data[0].AuthenticationMechanisms[0].IsEnabled, Is.False);
            Assert.That(result.Data[0].LogFiles.Count, Is.EqualTo(2));
            Assert.That(result.Data[0].LogFiles[0], Is.EqualTo("/usr/local/var/log/rabbitmq/rabbit@localhost.log"));
            Assert.That(result.Data[0].ContextSwitches, Is.EqualTo(19886066));
            Assert.That(result.Data[0].ContextSwitchDetails, Is.Not.Null);
            Assert.That(result.Data[0].ContextSwitchDetails.Value, Is.EqualTo(32.4M));
            Assert.That(result.Data[0].DatabaseDirectory, Is.EqualTo("/usr/local/var/lib/rabbitmq/mnesia/rabbit@localhost"));
            Assert.That(result.Data[0].EnabledPlugins.Count, Is.EqualTo(5));
            Assert.That(result.Data[0].EnabledPlugins[0], Is.EqualTo("rabbitmq_management"));
            Assert.That(result.Data[0].ExchangeTypes.Count, Is.EqualTo(4));
            Assert.That(result.Data[0].ExchangeTypes[0].Name, Is.EqualTo("headers"));
            Assert.That(result.Data[0].ExchangeTypes[0].Description, Is.EqualTo("AMQP headers exchange, as per the AMQP specification"));;
            Assert.That(result.Data[0].ExchangeTypes[0].IsEnabled, Is.True);
            Assert.That(result.Data[0].GcDetails, Is.Not.Null);
            Assert.That(result.Data[0].GcDetails.Value, Is.EqualTo(6.0M));
            Assert.That(result.Data[0].NumberOfGarbageCollected, Is.EqualTo(4815693));
            Assert.That(result.Data[0].IsRunning, Is.True);
            Assert.That(result.Data[0].MemoryAlarm, Is.False);
            Assert.That(result.Data[0].MemoryLimit, Is.EqualTo(429496729));
            Assert.That(result.Data[0].MemoryUsed, Is.EqualTo(11784192));
            Assert.That(result.Data[0].MemoryUsageDetails, Is.Not.Null);
            Assert.That(result.Data[0].MemoryUsageDetails.Value, Is.EqualTo(20480.0M));
            Assert.That(result.Data[0].SocketsUsed, Is.EqualTo(1));
            Assert.That(result.Data[0].SocketsUsedDetails, Is.Not.Null);
            Assert.That(result.Data[0].SocketsUsedDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].TotalSocketsAvailable, Is.EqualTo(138));
            Assert.That(result.Data[0].ProcessesUsed, Is.EqualTo(598));
            Assert.That(result.Data[0].ProcessUsageDetails, Is.Not.Null);
            Assert.That(result.Data[0].ProcessUsageDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].TotalProcesses, Is.EqualTo(1048576));
            Assert.That(result.Data[0].OperatingSystemProcessId, Is.EqualTo("1864"));
            Assert.That(result.Data[0].RatesMode, Is.EqualTo(RatesMode.Basic));
            Assert.That(result.Data[0].AvailableCoresDetected, Is.EqualTo(4));
            Assert.That(result.Data[0].FreeDiskAlarm, Is.False);
            Assert.That(result.Data[0].FreeDiskLimit, Is.EqualTo(50000000));
            Assert.That(result.Data[0].FreeDiskSpace, Is.EqualTo(0));
            Assert.That(result.Data[0].FreeDiskSpaceDetails, Is.Not.Null);
            Assert.That(result.Data[0].FreeDiskSpaceDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].FileDescriptorUsed, Is.EqualTo(0));
            Assert.That(result.Data[0].FileDescriptorUsedDetails, Is.Not.Null);
            Assert.That(result.Data[0].FileDescriptorUsedDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].TotalFileDescriptors, Is.EqualTo(256));
            Assert.That(result.Data[0].NetworkTickTime, Is.EqualTo(60));
            Assert.That(result.Data[0].TotalIOReads, Is.EqualTo(11));
            Assert.That(result.Data[0].IOReadDetails, Is.Not.Null);
            Assert.That(result.Data[0].IOReadDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].TotalIOWrites, Is.EqualTo(932));
            Assert.That(result.Data[0].IOWriteDetails, Is.Not.Null);
            Assert.That(result.Data[0].IOWriteDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].TotalMessageStoreReads, Is.EqualTo(0));
            Assert.That(result.Data[0].MessageStoreReadDetails, Is.Not.Null);
            Assert.That(result.Data[0].MessageStoreReadDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].TotalMessageStoreWrites, Is.EqualTo(0));
            Assert.That(result.Data[0].MessageStoreReadDetails, Is.Not.Null);
            Assert.That(result.Data[0].MessageStoreReadDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].TotalMnesiaDiskTransactions, Is.EqualTo(33));
            Assert.That(result.Data[0].MnesiaDiskTransactionCountDetails, Is.Not.Null);
            Assert.That(result.Data[0].TotalMnesiaRamTransactions, Is.EqualTo(1278));
            Assert.That(result.Data[0].MnesiaDiskTransactionCountDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].MnesiaRAMTransactionCountDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].MnesiaRAMTransactionCountDetails, Is.Not.Null);
            Assert.That(result.Data[0].TotalQueueIndexReads, Is.EqualTo(5));
            Assert.That(result.Data[0].QueueIndexReadDetails, Is.Not.Null);
            Assert.That(result.Data[0].QueueIndexReadDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].TotalQueueIndexWrites, Is.EqualTo(43));
            Assert.That(result.Data[0].QueueIndexWriteDetails, Is.Not.Null);
            Assert.That(result.Data[0].QueueIndexWriteDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].TotalIOBytesRead, Is.EqualTo(44904556));
            Assert.That(result.Data[0].IOBytesReadDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].IOBytesWrittenDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].FileHandleOpenAttemptDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].FileHandleOpenAttemptAvgTimeDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].CreatedConnectionDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].ClosedConnectionDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].CreatedChannelDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].ClosedChannelDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].DeclaredQueueDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].CreatedQueueDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].DeletedQueueDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].AvgIOSeekTimeDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].IOSeeksDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].AvgIOSyncTimeDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].IOSyncsDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].IOBytesReadDetails, Is.Not.Null);
            Assert.That(result.Data[0].IOBytesWrittenDetails, Is.Not.Null);
            Assert.That(result.Data[0].FileHandleOpenAttemptDetails, Is.Not.Null);
            Assert.That(result.Data[0].FileHandleOpenAttemptAvgTimeDetails, Is.Not.Null);
            Assert.That(result.Data[0].CreatedConnectionDetails, Is.Not.Null);
            Assert.That(result.Data[0].ClosedConnectionDetails, Is.Not.Null);
            Assert.That(result.Data[0].CreatedChannelDetails, Is.Not.Null);
            Assert.That(result.Data[0].ClosedChannelDetails, Is.Not.Null);
            Assert.That(result.Data[0].DeletedQueueDetails, Is.Not.Null);
            Assert.That(result.Data[0].DeclaredQueueDetails, Is.Not.Null);
            Assert.That(result.Data[0].CreatedQueueDetails, Is.Not.Null);
            Assert.That(result.Data[0].IOSyncsDetails, Is.Not.Null);
            Assert.That(result.Data[0].AvgIOSyncTimeDetails, Is.Not.Null);
            Assert.That(result.Data[0].IOSeeksDetails, Is.Not.Null);
            Assert.That(result.Data[0].AvgIOSeekTimeDetails, Is.Not.Null);
            Assert.That(result.Data[0].IOSyncCount, Is.EqualTo(932));
            Assert.That(result.Data[0].TotalIOBytesWritten, Is.EqualTo(574531094));
            Assert.That(result.Data[0].TotalOpenFileHandleAttempts, Is.EqualTo(901142));
            Assert.That(result.Data[0].TotalConnectionsCreated, Is.EqualTo(15));
            Assert.That(result.Data[0].TotalConnectionsClosed, Is.EqualTo(14));
            Assert.That(result.Data[0].TotalChannelsCreated, Is.EqualTo(75));
            Assert.That(result.Data[0].TotalChannelsClosed, Is.EqualTo(73));
            Assert.That(result.Data[0].TotalQueuesDeclared, Is.EqualTo(11));
            Assert.That(result.Data[0].TotalQueuesCreated, Is.EqualTo(9));
            Assert.That(result.Data[0].TotalQueuesDeleted, Is.EqualTo(7));
            Assert.That(result.Data[0].IOSeekCount, Is.EqualTo(80));
            Assert.That(result.Data[0].BytesReclaimedByGarbageCollector, Is.EqualTo(114844443912));
            Assert.That(result.Data[0].ReclaimedBytesFromGCDetails.Value, Is.EqualTo(147054.4M));
            Assert.That(result.Data[0].Type, Is.EqualTo("disc"));
        });
    }

    [Test]
    public async Task Should_be_able_to_get_all_nodes2()
    {
        var result = await GetContainerBuilder("TestData/NodeInfo.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .GetAllNodes(x => x.UsingCredentials("guest", "guest"));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasData, Is.True);
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data[0], Is.Not.Null);
            Assert.That(result.Data[0].Applications.Count, Is.EqualTo(32));
            Assert.That(result.Data[0].Contexts.Count, Is.EqualTo(1));
            Assert.That(result.Data[0].Contexts[0].Description, Is.EqualTo("RabbitMQ Management"));;
            Assert.That(result.Data[0].Contexts[0].Path, Is.EqualTo("/"));
            Assert.That(result.Data[0].Contexts[0].Port, Is.EqualTo("15672"));;
            Assert.That(result.Data[0].Name, Is.EqualTo("rabbit@localhost"));;
            Assert.That(result.Data[0].Partitions.Count, Is.EqualTo(2));
            Assert.That(result.Data[0].Partitions[0], Is.EqualTo("node1"));;
            Assert.That(result.Data[0].Uptime, Is.EqualTo(737892679));
            Assert.That(result.Data[0].AuthenticationMechanisms, Is.Not.Null);
            Assert.That(result.Data[0].AuthenticationMechanisms.Count, Is.EqualTo(3));
            Assert.That(result.Data[0].AuthenticationMechanisms[0].Name, Is.EqualTo("RABBIT-CR-DEMO"));
            Assert.That(result.Data[0].AuthenticationMechanisms[0].Description, Is.EqualTo("RabbitMQ Demo challenge-response authentication mechanism"));;
            Assert.That(result.Data[0].AuthenticationMechanisms[0].IsEnabled, Is.False);
            Assert.That(result.Data[0].LogFiles.Count, Is.EqualTo(2));
            Assert.That(result.Data[0].LogFiles[0], Is.EqualTo("/usr/local/var/log/rabbitmq/rabbit@localhost.log"));
            Assert.That(result.Data[0].ContextSwitches, Is.EqualTo(19886066));
            Assert.That(result.Data[0].ContextSwitchDetails, Is.Not.Null);
            Assert.That(result.Data[0].ContextSwitchDetails.Value, Is.EqualTo(32.4M));
            Assert.That(result.Data[0].DatabaseDirectory, Is.EqualTo("/usr/local/var/lib/rabbitmq/mnesia/rabbit@localhost"));
            Assert.That(result.Data[0].EnabledPlugins.Count, Is.EqualTo(5));
            Assert.That(result.Data[0].EnabledPlugins[0], Is.EqualTo("rabbitmq_management"));
            Assert.That(result.Data[0].ExchangeTypes.Count, Is.EqualTo(4));
            Assert.That(result.Data[0].ExchangeTypes[0].Name, Is.EqualTo("headers"));
            Assert.That(result.Data[0].ExchangeTypes[0].Description, Is.EqualTo("AMQP headers exchange, as per the AMQP specification"));;
            Assert.That(result.Data[0].ExchangeTypes[0].IsEnabled, Is.True);
            Assert.That(result.Data[0].GcDetails, Is.Not.Null);
            Assert.That(result.Data[0].GcDetails.Value, Is.EqualTo(6.0M));
            Assert.That(result.Data[0].NumberOfGarbageCollected, Is.EqualTo(4815693));
            Assert.That(result.Data[0].IsRunning, Is.True);
            Assert.That(result.Data[0].MemoryAlarm, Is.False);
            Assert.That(result.Data[0].MemoryLimit, Is.EqualTo(429496729));
            Assert.That(result.Data[0].MemoryUsed, Is.EqualTo(11784192));
            Assert.That(result.Data[0].MemoryUsageDetails, Is.Not.Null);
            Assert.That(result.Data[0].MemoryUsageDetails.Value, Is.EqualTo(20480.0M));
            Assert.That(result.Data[0].SocketsUsed, Is.EqualTo(1));
            Assert.That(result.Data[0].SocketsUsedDetails, Is.Not.Null);
            Assert.That(result.Data[0].SocketsUsedDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].TotalSocketsAvailable, Is.EqualTo(138));
            Assert.That(result.Data[0].ProcessesUsed, Is.EqualTo(598));
            Assert.That(result.Data[0].ProcessUsageDetails, Is.Not.Null);
            Assert.That(result.Data[0].ProcessUsageDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].TotalProcesses, Is.EqualTo(1048576));
            Assert.That(result.Data[0].OperatingSystemProcessId, Is.EqualTo("1864"));
            Assert.That(result.Data[0].RatesMode, Is.EqualTo(RatesMode.Basic));
            Assert.That(result.Data[0].AvailableCoresDetected, Is.EqualTo(4));
            Assert.That(result.Data[0].FreeDiskAlarm, Is.False);
            Assert.That(result.Data[0].FreeDiskLimit, Is.EqualTo(50000000));
            Assert.That(result.Data[0].FreeDiskSpace, Is.EqualTo(0));
            Assert.That(result.Data[0].FreeDiskSpaceDetails, Is.Not.Null);
            Assert.That(result.Data[0].FreeDiskSpaceDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].FileDescriptorUsed, Is.EqualTo(0));
            Assert.That(result.Data[0].FileDescriptorUsedDetails, Is.Not.Null);
            Assert.That(result.Data[0].FileDescriptorUsedDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].TotalFileDescriptors, Is.EqualTo(256));
            Assert.That(result.Data[0].NetworkTickTime, Is.EqualTo(60));
            Assert.That(result.Data[0].TotalIOReads, Is.EqualTo(11));
            Assert.That(result.Data[0].IOReadDetails, Is.Not.Null);
            Assert.That(result.Data[0].IOReadDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].TotalIOWrites, Is.EqualTo(932));
            Assert.That(result.Data[0].IOWriteDetails, Is.Not.Null);
            Assert.That(result.Data[0].IOWriteDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].TotalMessageStoreReads, Is.EqualTo(0));
            Assert.That(result.Data[0].MessageStoreReadDetails, Is.Not.Null);
            Assert.That(result.Data[0].MessageStoreReadDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].TotalMessageStoreWrites, Is.EqualTo(0));
            Assert.That(result.Data[0].MessageStoreReadDetails, Is.Not.Null);
            Assert.That(result.Data[0].MessageStoreReadDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].TotalMnesiaDiskTransactions, Is.EqualTo(33));
            Assert.That(result.Data[0].MnesiaDiskTransactionCountDetails, Is.Not.Null);
            Assert.That(result.Data[0].TotalMnesiaRamTransactions, Is.EqualTo(1278));
            Assert.That(result.Data[0].MnesiaDiskTransactionCountDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].MnesiaRAMTransactionCountDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].MnesiaRAMTransactionCountDetails, Is.Not.Null);
            Assert.That(result.Data[0].TotalQueueIndexReads, Is.EqualTo(5));
            Assert.That(result.Data[0].QueueIndexReadDetails, Is.Not.Null);
            Assert.That(result.Data[0].QueueIndexReadDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].TotalQueueIndexWrites, Is.EqualTo(43));
            Assert.That(result.Data[0].QueueIndexWriteDetails, Is.Not.Null);
            Assert.That(result.Data[0].QueueIndexWriteDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].TotalIOBytesRead, Is.EqualTo(44904556));
            Assert.That(result.Data[0].IOBytesReadDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].IOBytesWrittenDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].FileHandleOpenAttemptDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].FileHandleOpenAttemptAvgTimeDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].CreatedConnectionDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].ClosedConnectionDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].CreatedChannelDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].ClosedChannelDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].DeclaredQueueDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].CreatedQueueDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].DeletedQueueDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].AvgIOSeekTimeDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].IOSeeksDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].AvgIOSyncTimeDetails.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].IOSyncsDetails?.Value, Is.EqualTo(0.0M));
            Assert.That(result.Data[0].IOBytesReadDetails, Is.Not.Null);
            Assert.That(result.Data[0].IOBytesWrittenDetails, Is.Not.Null);
            Assert.That(result.Data[0].FileHandleOpenAttemptDetails, Is.Not.Null);
            Assert.That(result.Data[0].FileHandleOpenAttemptAvgTimeDetails, Is.Not.Null);
            Assert.That(result.Data[0].CreatedConnectionDetails, Is.Not.Null);
            Assert.That(result.Data[0].ClosedConnectionDetails, Is.Not.Null);
            Assert.That(result.Data[0].CreatedChannelDetails, Is.Not.Null);
            Assert.That(result.Data[0].ClosedChannelDetails, Is.Not.Null);
            Assert.That(result.Data[0].DeletedQueueDetails, Is.Not.Null);
            Assert.That(result.Data[0].DeclaredQueueDetails, Is.Not.Null);
            Assert.That(result.Data[0].CreatedQueueDetails, Is.Not.Null);
            Assert.That(result.Data[0].IOSyncsDetails, Is.Not.Null);
            Assert.That(result.Data[0].AvgIOSyncTimeDetails, Is.Not.Null);
            Assert.That(result.Data[0].IOSeeksDetails, Is.Not.Null);
            Assert.That(result.Data[0].AvgIOSeekTimeDetails, Is.Not.Null);
            Assert.That(result.Data[0].IOSyncCount, Is.EqualTo(932));
            Assert.That(result.Data[0].TotalIOBytesWritten, Is.EqualTo(574531094));
            Assert.That(result.Data[0].TotalOpenFileHandleAttempts, Is.EqualTo(901142));
            Assert.That(result.Data[0].TotalConnectionsCreated, Is.EqualTo(15));
            Assert.That(result.Data[0].TotalConnectionsClosed, Is.EqualTo(14));
            Assert.That(result.Data[0].TotalChannelsCreated, Is.EqualTo(75));
            Assert.That(result.Data[0].TotalChannelsClosed, Is.EqualTo(73));
            Assert.That(result.Data[0].TotalQueuesDeclared, Is.EqualTo(11));
            Assert.That(result.Data[0].TotalQueuesCreated, Is.EqualTo(9));
            Assert.That(result.Data[0].TotalQueuesDeleted, Is.EqualTo(7));
            Assert.That(result.Data[0].IOSeekCount, Is.EqualTo(80));
            Assert.That(result.Data[0].BytesReclaimedByGarbageCollector, Is.EqualTo(114844443912));
            Assert.That(result.Data[0].ReclaimedBytesFromGCDetails.Value, Is.EqualTo(147054.4M));
            Assert.That(result.Data[0].Type, Is.EqualTo("disc"));
        });
    }
        
    [Test]
    public async Task Verify_will_return_node_memory_usage1()
    {
        var result = await GetContainerBuilder("TestData/MemoryUsageInfo.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Node>(x => x.UsingCredentials("guest", "guest"))
            .GetMemoryUsage("haredu@localhost");

        Assert.Multiple(() =>
        {
            Assert.That(result.HasData, Is.True);
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.Data.Memory, Is.Not.Null);
            Assert.That(result.Data.Memory.ConnectionReaders, Is.EqualTo(0));
            Assert.That(result.Data.Memory.ConnectionWriters, Is.EqualTo(0));
            Assert.That(result.Data.Memory.ConnectionChannels, Is.EqualTo(0));
            Assert.That(result.Data.Memory.QueueSlaveProcesses, Is.EqualTo(0));
            Assert.That(result.Data.Memory.ReservedUnallocated, Is.EqualTo(0));
            Assert.That(result.Data.Memory.ConnectionOther, Is.EqualTo(132692));
            Assert.That(result.Data.Memory.QueueProcesses, Is.EqualTo(210444));
            Assert.That(result.Data.Memory.QuorumQueueProcesses, Is.EqualTo(76132));
            Assert.That(result.Data.Memory.Plugins, Is.EqualTo(4035284));
            Assert.That(result.Data.Memory.OtherProcesses, Is.EqualTo(23706508));
            Assert.That(result.Data.Memory.Metrics, Is.EqualTo(235692));
            Assert.That(result.Data.Memory.ManagementDatabase, Is.EqualTo(1053904));
            Assert.That(result.Data.Memory.Mnesia, Is.EqualTo(190488));
            Assert.That(result.Data.Memory.QuorumInMemoryStorage, Is.EqualTo(45464));
            Assert.That(result.Data.Memory.OtherInMemoryStorage, Is.EqualTo(3508368));
            Assert.That(result.Data.Memory.Binary, Is.EqualTo(771152));
            Assert.That(result.Data.Memory.MessageIndex, Is.EqualTo(338800));
            Assert.That(result.Data.Memory.ByteCode, Is.EqualTo(27056269));
            Assert.That(result.Data.Memory.Atom, Is.EqualTo(1566897));
            Assert.That(result.Data.Memory.OtherSystem, Is.EqualTo(16660090));
            Assert.That(result.Data.Memory.AllocatedUnused, Is.EqualTo(17765544));
            Assert.That(result.Data.Memory.Strategy, Is.EqualTo("rss"));
            Assert.That(result.Data.Memory.Total.Erlang, Is.EqualTo(79588184));
            Assert.That(result.Data.Memory.Total.Strategy, Is.EqualTo(32055296));
            Assert.That(result.Data.Memory.Total.Allocated, Is.EqualTo(97353728));
        });
    }
        
    [Test]
    public async Task Verify_will_return_node_memory_usage2()
    {
        var result = await GetContainerBuilder("TestData/MemoryUsageInfo.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .GetNodeMemoryUsage(x => x.UsingCredentials("guest", "guest"), "haredu@localhost");

        Assert.Multiple(() =>
        {
            Assert.That(result.HasData, Is.True);
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.Data.Memory, Is.Not.Null);
            Assert.That(result.Data.Memory.ConnectionReaders, Is.EqualTo(0));
            Assert.That(result.Data.Memory.ConnectionWriters, Is.EqualTo(0));
            Assert.That(result.Data.Memory.ConnectionChannels, Is.EqualTo(0));
            Assert.That(result.Data.Memory.QueueSlaveProcesses, Is.EqualTo(0));
            Assert.That(result.Data.Memory.ReservedUnallocated, Is.EqualTo(0));
            Assert.That(result.Data.Memory.ConnectionOther, Is.EqualTo(132692));
            Assert.That(result.Data.Memory.QueueProcesses, Is.EqualTo(210444));
            Assert.That(result.Data.Memory.QuorumQueueProcesses, Is.EqualTo(76132));
            Assert.That(result.Data.Memory.Plugins, Is.EqualTo(4035284));
            Assert.That(result.Data.Memory.OtherProcesses, Is.EqualTo(23706508));
            Assert.That(result.Data.Memory.Metrics, Is.EqualTo(235692));
            Assert.That(result.Data.Memory.ManagementDatabase, Is.EqualTo(1053904));
            Assert.That(result.Data.Memory.Mnesia, Is.EqualTo(190488));
            Assert.That(result.Data.Memory.QuorumInMemoryStorage, Is.EqualTo(45464));
            Assert.That(result.Data.Memory.OtherInMemoryStorage, Is.EqualTo(3508368));
            Assert.That(result.Data.Memory.Binary, Is.EqualTo(771152));
            Assert.That(result.Data.Memory.MessageIndex, Is.EqualTo(338800));
            Assert.That(result.Data.Memory.ByteCode, Is.EqualTo(27056269));
            Assert.That(result.Data.Memory.Atom, Is.EqualTo(1566897));
            Assert.That(result.Data.Memory.OtherSystem, Is.EqualTo(16660090));
            Assert.That(result.Data.Memory.AllocatedUnused, Is.EqualTo(17765544));
            Assert.That(result.Data.Memory.Strategy, Is.EqualTo("rss"));
            Assert.That(result.Data.Memory.Total.Erlang, Is.EqualTo(79588184));
            Assert.That(result.Data.Memory.Total.Strategy, Is.EqualTo(32055296));
            Assert.That(result.Data.Memory.Total.Allocated, Is.EqualTo(97353728));
        });
    }

    [Test]
    public async Task Verify_will_not_return_node_memory_usage_when_node_missing1()
    {
        var result = await GetContainerBuilder("TestData/MemoryUsageInfo.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Node>(x => x.UsingCredentials("guest", "guest"))
            .GetMemoryUsage(string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.False);
        });
    }

    [Test]
    public async Task Verify_will_not_return_node_memory_usage_when_node_missing2()
    {
        var result = await GetContainerBuilder("TestData/MemoryUsageInfo.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .GetNodeMemoryUsage(x => x.UsingCredentials("guest", "guest"), string.Empty);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.False);
        });
    }

    [Test]
    public async Task Verify_will_not_return_node_memory_usage_when_node_missing3()
    {
        var result = await GetContainerBuilder("TestData/MemoryUsageInfo.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Node>(x => x.UsingCredentials("guest", "guest"))
            .GetMemoryUsage(null);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.False);
        });
    }

    [Test]
    public async Task Verify_will_not_return_node_memory_usage_when_node_missing4()
    {
        var result = await GetContainerBuilder("TestData/MemoryUsageInfo.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .GetNodeMemoryUsage(x => x.UsingCredentials("guest", "guest"), null);
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.False);
        });
    }

    [Test]
    public async Task Verify_will_not_return_node_memory_usage_when_node_missing5()
    {
        var result = await GetContainerBuilder("TestData/MemoryUsageInfo.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .API<Node>(x => x.UsingCredentials("guest", "guest"))
            .GetMemoryUsage("   ");
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.False);
        });
    }

    [Test]
    public async Task Verify_will_not_return_node_memory_usage_when_node_missing6()
    {
        var result = await GetContainerBuilder("TestData/MemoryUsageInfo.json")
            .BuildServiceProvider()
            .GetService<IHareDuFactory>()
            .GetNodeMemoryUsage(x => x.UsingCredentials("guest", "guest"), "   ");
            
        Assert.Multiple(() =>
        {
            Assert.That(result.HasFaulted, Is.False);
            Assert.That(result.HasData, Is.False);
        });
    }
}