namespace HareDu.Snapshotting.Tests.Fakes;

using System.Collections.Generic;
using Core.Configuration;
using Core.Testing;

public class FakeBrokerFactory :
    IBrokerFactory,
    HareDuTestingFake
{
    public HareDuConfig Config { get; }

    public T API<T>()
        where T : BrokerAPI
    {
        if (typeof(T) == typeof(Broker))
        {
            Broker obj = new BrokerObject();

            return (T) obj;
        }

        if (typeof(T) == typeof(Node))
        {
            Node obj = new FakeNodeObject();

            return (T) obj;
        }

        if (typeof(T) == typeof(Connection))
        {
            Connection obj = new FakeConnectionObject();

            return (T) obj;
        }

        if (typeof(T) == typeof(Channel))
        {
            Channel obj = new FakeChannelObject();

            return (T) obj;
        }

        if (typeof(T) == typeof(Queue))
        {
            Queue obj = new FakeQueueObject();

            return (T) obj;
        }

        return default;
    }

    public T Object<T>() where T : BrokerAPI => throw new System.NotImplementedException();

    public bool IsRegistered(string key) => throw new System.NotImplementedException();
        
    public IReadOnlyDictionary<string, object> GetObjects() => throw new System.NotImplementedException();

    public void CancelPendingRequest() => throw new System.NotImplementedException();

    public bool TryRegisterAll() => throw new System.NotImplementedException();
}