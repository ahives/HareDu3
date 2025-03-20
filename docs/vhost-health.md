# Get Virtual Host Health Details

The Broker API allows you to get virtual host health details on the RabbitMQ broker. To do so is pretty simple with HareDu 4. You can do it yourself or the DI way.

**Microsoft DI**

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<VirtualHost>()
    .GetHealth("vhost");
```
<br>

The other way to get virtual host health details is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .GetVirtualHostHealth("vhost");
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

