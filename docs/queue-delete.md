# Delete Queue

The Broker API allows you to delete a queue from the RabbitMQ broker. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Queue>(x => x.UsingCredentials("guest", "guest"))
    .Delete("queue", "vhost");
```

Since deleting a queue will also purge the queue of all messages as well, HareDu provides a conditional way to perform said action. You can delete a queue when there are no consumers and/or when the queue is empty. You need only call the ```WhenHasNoConsumers``` and/or the ```WhenEmpty``` method...

A complete example would look something like this...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Queue>(x => x.UsingCredentials("guest", "guest"))
    .Delete("queue", "vhost", x =>
    {
        x.WhenHasNoConsumers();
        x.WhenEmpty();
    });
```

The other way to do this is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .DeleteQueue(x => x.UsingCredentials("guest", "guest"), "queue", "vhost");
```

...or

```c#
var result = await _services.GetService<IBrokerFactory>()
    .DeleteQueue(x => x.UsingCredentials("guest", "guest"), "queue", "vhost", x =>
    {
        x.WhenHasNoConsumers();
        x.WhenEmpty();
    });
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

