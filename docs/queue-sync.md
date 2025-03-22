# Sync Queue

The Broker API allows you to sync queues. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Queue>()
    .Sync("queue", "vhost", QueueSyncAction.Sync);
```

The other way to get consumer information is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .SyncQueue("queue", "vhost");
```

```c#
var result = await _services.GetService<IBrokerFactory>()
    .CancelQueueSync("queue", "vhost");
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

