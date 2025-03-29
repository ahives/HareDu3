# Delete Virtual Host Limit

The Broker API allows you to delete virtual host limits on a RabbitMQ broker. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<VirtualHost>()
    .DeleteLimits("vhost", VirtualHostLimit.MaximumConnections);
```
<br>

The other way to delete virtual host limits is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .DeleteVirtualHostLimit("vhost", VirtualHostLimit.MaximumConnections);
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

