# Get Virtual Host Topic Permissions

The Broker API allows you to get all topic permissions associated with a particular virtual host on the RabbitMQ broker. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
    .GetTopicPermissions("vhost");
```

The other way to do this is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .GetVirtualHostTopicPermissions(x => x.UsingCredentials("guest", "guest"), "vhost");
```

*Please note that subsequent calls to any of the above methods will result in overriding the argument.*

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

