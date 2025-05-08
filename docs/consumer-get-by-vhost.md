# Get All Consumers By Virtual Host

The Broker API allows you to get all consumers connected to the RabbitMQ broker filtered by virtual host. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Consumer>(x => x.UsingCredentials("guest", "guest"))
    .GetByVirtualHost();
```

The other way to define virtual host limits is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .GetConsumersByVirtualHost(x => x.UsingCredentials("guest", "guest"));
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

