# Get All Connections By Name

The Broker API allows you to get all established connections to the RabbitMQ broker filtered by name. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Connection>(x => x.UsingCredentials("guest", "guest"))
    .GetByName("test_connection");
```
<br>

The other way to get connection information is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .GetConnectionsByName(x => x.UsingCredentials("guest", "guest"), "test_connection");
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

