# Get Connections

The Broker API allows you to get all established connections to the RabbitMQ broker. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Connection>()
    .GetByVirtualHost("test_vhost");
```
<br>

The other way to get connection information is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .GetConnectionsByVirtualHost("test_vhost");
```

If you want to know how to use the above methods with pagination please go [here](https://github.com/ahives/HareDu3/blob/master/docs/pagination.md).
<br>

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

