# Get Bindings

The Broker API allows you to get all bindings on the RabbitMQ broker. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Binding>(x => x.UsingCredentials("guest", "guest"))
    .GetAll();
```
<br>

The other way to create bindings is to call the extension methods off of ```IBrokerObjectFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .GetAllBindings(x => x.UsingCredentials("guest", "guest"));
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

