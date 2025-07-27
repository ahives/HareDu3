# Get All Queues

The Broker API allows you to get all queues on the RabbitMQ broker. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Queue>(x => x.UsingCredentials("guest", "guest"))
    .GetAll();
```

The other way to do this is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .GetAllQueues(x => x.UsingCredentials("guest", "guest"));
```

If you want to know how to use the above methods with pagination please go [here](https://github.com/ahives/HareDu3/blob/master/docs/pagination.md).

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

