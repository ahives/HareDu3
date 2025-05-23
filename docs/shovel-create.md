# Create Dynamic Shovel

The Broker API allows you to create a dynamic shovel on the RabbitMQ broker. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Shovel>()
    .Create("shovel", "uri", "vhost", x =>
    {
        x.Source("source_queue", c =>
        {
            ...
        });
        x.Destination("destination_queue", c =>
        {
            ...
        });
    });
```
<br>

The other way to create a dynamic shovel is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .CreateShovel("shovel", "uri", "vhost", x =>
    {
        x.Source("source_queue", c =>
        {
            ...
        });
        x.Destination("destination_queue", c =>
        {
            ...
        });
    });
```

Putting it altogether looks something like this...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .CreateShovel("test-shovel", "test-vhost", "amqp://user1@localhost", x =>
    {
        x.Source("queue1", c =>
        {
            c.DeleteAfter(DeleteShovelMode.QueueLength);
        });
        x.Destination("queue2");
    });
```

*Please note that subsequent calls to any of the above methods will result in overriding the argument.*

<br>

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

