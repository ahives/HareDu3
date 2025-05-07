# Rebalance Broker Queues

The Broker API allows you to rebalance all queues across all virtual hosts on the RabbitMQ cluster. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Broker>(x => x.UsingCredentials("guest", "guest"))
    .RebalanceQueues()
```
<br>

The other way to do this is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .RebalanceQueues(x => x.UsingCredentials("guest", "guest"));
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

