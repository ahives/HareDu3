# Rebalance Broker System Queues

The Broker API allows you to rebalance all queues across all virtual hosts on the RabbitMQ cluster. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<BrokerSystem>()
    .RebalanceAllQueues()
```
<br>

The other way to rebalance queues is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .RebalanceAllQueues();
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

