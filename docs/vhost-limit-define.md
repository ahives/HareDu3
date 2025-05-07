# Define Virtual Host Limit

The Broker API allows you to create a virtual host limit on the RabbitMQ broker. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
    .DefineVirtualHostLimit("vhost", x =>
    {
        x.SetMaxConnectionLimit(1000);
    });
```

The other way to do this is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .DefineVirtualHostLimit(x => x.UsingCredentials("guest", "guest"), "vhost", x =>
    {
        x.SetMaxConnectionLimit(1000);
    });
```

*Please note that subsequent calls to any of the above methods will result in overriding the argument.*

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

