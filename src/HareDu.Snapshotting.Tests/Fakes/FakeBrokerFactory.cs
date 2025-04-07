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
            Broker obj = new FakeBrokerImpl();

            return (T) obj;
        }

        if (typeof(T) == typeof(Node))
        {
            Node obj = new FakeNodeObject();

            return (T) obj;
        }

        if (typeof(T) == typeof(Connection))
        {
            Connection obj = new FakeConnectionImpl();

            return (T) obj;
        }

        if (typeof(T) == typeof(Channel))
        {
            Channel obj = new FakeChannelImpl();

            return (T) obj;
        }

        if (typeof(T) == typeof(Queue))
        {
            Queue obj = new FakeQueueImpl();

            return (T) obj;
        }

        return default;
    }

    public bool IsRegistered(string key) => throw new System.NotImplementedException();
        
    public IReadOnlyDictionary<string, object> GetObjects() => throw new System.NotImplementedException();

    public void CancelPendingRequest() => throw new System.NotImplementedException();

    public bool TryRegisterAll() => throw new System.NotImplementedException();
}