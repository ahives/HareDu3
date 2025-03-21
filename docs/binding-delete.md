# Delete Binding

The Broker API allows you to delete a binding from a RabbitMQ broker object (e.g., exchanges and/or queues). To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Binding>()
    .Delete("source_exchange", "destination_queue", "properties_key", "vhost", BindingType.Exchange);
```
<br>

If the destination binding is a queue then you simply need to set the ```BindingType``` accordingly and specify the queue's name via the ```Destination``` method like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Binding>()
    .Delete("source_exchange", "destination_queue", "properties_key", "vhost", BindingType.Queue);
```
<br>

The other way to delete a binding is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .DeleteExchangeBinding("source_exchange", "destination_exchange", "properties_key", "vhost");
```
or...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .DeleteQueueBinding("source_exchange", "destination_queue", "properties_key", "vhost");
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

