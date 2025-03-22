# Create Scoped Parameter

The Broker API allows you to create a scoped parameter on the RabbitMQ broker. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<ScopedParameter>()
    .Create<long>("parameter", 89, "component", "vhost");
```
<br>

The other way to create a scoped parameter is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .CreateScopeParameter<long>("parameter", 89, "component", "vhost");
```

<br>

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

