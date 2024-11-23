namespace HareDu.Snapshotting.Tests.Fakes;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Testing;
using HareDu.Model;

public class FakeNodeObject :
    Node,
    HareDuTestingFake
{
    public async Task<Results<NodeInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        var node1 = new NodeInfo
        {
            Partitions = GetPartitions().ToList(),
            MemoryAlarm = true,
            MemoryLimit = 723746434,
            OperatingSystemProcessId = "OS123",
            TotalFileDescriptors = 8923747343434,
            TotalSocketsAvailable = 8186263662,
            FreeDiskLimit = 8928739432,
            FreeDiskAlarm = true,
            TotalProcesses = 7234364,
            AvailableCoresDetected = 8,
            IsRunning = true,
            MemoryUsed = 7826871,
            FileDescriptorUsed = 9203797,
            SocketsUsed = 8298347,
            ProcessesUsed = 9199849,
            FreeDiskSpace = 7265368234,
            TotalOpenFileHandleAttempts = 356446478,
            TotalIOWrites = 36478608776,
            TotalIOReads = 892793874982,
            TotalMessageStoreReads = 9097887,
            TotalMessageStoreWrites = 776788733,
            TotalIOBytesRead = 78738764,
            TotalIOBytesWritten = 728364283
        };
        var node2 = new NodeInfo
        {
            Partitions = GetPartitions().ToList(),
            MemoryAlarm = true,
            MemoryLimit = 723746434,
            OperatingSystemProcessId = "OS123",
            TotalFileDescriptors = 8923747343434,
            TotalSocketsAvailable = 8186263662,
            FreeDiskLimit = 8928739432,
            FreeDiskAlarm = true,
            TotalProcesses = 7234364,
            AvailableCoresDetected = 8,
            IsRunning = true,
            MemoryUsed = 7826871,
            FileDescriptorUsed = 9203797,
            SocketsUsed = 8298347,
            ProcessesUsed = 9199849,
            FreeDiskSpace = 7265368234,
            TotalOpenFileHandleAttempts = 356446478,
            TotalIOWrites = 36478608776,
            TotalIOReads = 892793874982,
            TotalMessageStoreReads = 9097887,
            TotalMessageStoreWrites = 776788733,
            TotalIOBytesRead = 78738764,
            TotalIOBytesWritten = 728364283
        };

        return new SuccessfulResults<NodeInfo>{Data = new List<NodeInfo> {node1, node2}, DebugInfo = null};
    }

    public async Task<Result<NodeHealthInfo>> GetHealth(string node = null, CancellationToken cancellationToken = default) => throw new System.NotImplementedException();

    public async Task<Result<NodeMemoryUsageInfo>> GetMemoryUsage(string node, CancellationToken cancellationToken = default) => throw new System.NotImplementedException();

    IEnumerable<string> GetPartitions()
    {
        yield return "partition1";
        yield return "partition2";
        yield return "partition3";
        yield return "partition4";
    }
}