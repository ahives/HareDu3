# Delete Scoped Parameter

The Broker API allows you to delete a scoped parameter from the RabbitMQ broker. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
    .Delete("parameter", "component", "vhost");
```

The other way to do this is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .DeleteScopedParameter("parameter", "component", "vhost");
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

