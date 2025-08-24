namespace HareDu.Snapshotting.IntegrationTests.Observers;

using System;
using System.IO;
using Core.Extensions;
using Core.Serialization;
using Model;
using Serialization;

public class BrokerQueuesJsonExporter :
    IObserver<SnapshotContext<BrokerQueuesSnapshot>>
{
    readonly IHareDuDeserializer _deserializer;

    public BrokerQueuesJsonExporter()
    {
        _deserializer = new BrokerDeserializer();
    }

    public void OnCompleted() => throw new NotImplementedException();

    public void OnError(Exception error) => throw new NotImplementedException();

    public void OnNext(SnapshotContext<BrokerQueuesSnapshot> value)
    {
        string path = $"{Directory.GetCurrentDirectory()}/snapshots";
            
        if (!File.Exists(path))
        {
            var directory = Directory.CreateDirectory(path);
                
            if (directory.Exists)
                File.WriteAllText($"{path}/snapshot_{value.Identifier}.json", _deserializer.ToJsonString(value));
        }
    }
}