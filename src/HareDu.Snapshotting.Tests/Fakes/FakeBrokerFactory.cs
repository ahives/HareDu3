namespace HareDu.Snapshotting.Tests.Fakes;

using System;
using Core.Security;
using Core.Testing;

public class FakeBrokerFactory :
    IBrokerFactory,
    HareDuTestingFake
{
    readonly Broker _broker;
    readonly Node _node;
    readonly Connection _connection;
    readonly Channel _channel;
    readonly Queue _queue;

    public FakeBrokerFactory()
    {
        _broker = new FakeBrokerImpl();
        _node = new FakeNodeImpl();
        _connection = new FakeConnectionImpl();
        _channel = new FakeChannelImpl();
        _queue = new FakeQueueImpl();
    }

    public T API<T>(Action<HareDuCredentialProvider> credentials) where T : BrokerAPI
    {
        if (typeof(T) == typeof(Broker))
            return (T) _broker;

        if (typeof(T) == typeof(Node))
            return (T) _node;

        if (typeof(T) == typeof(Connection))
            return (T) _connection;

        if (typeof(T) == typeof(Channel))
            return (T) _channel;

        if (typeof(T) == typeof(Queue))
            return (T) _queue;

        return default;
    }
}