# Define Virtual Host Limits

The Broker API allows you to create a virtual host on the RabbitMQ broker. To do so is pretty simple with HareDu 4. You can do it yourself or the DI way.

**Microsoft DI**

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<VirtualHostLimits>()
    .Define("vhost", x =>
    {
        x.SetMaxQueueLimit(100);
        x.SetMaxConnectionLimit(1000);
    });
```
<br>

The other way to define virtual host limits is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .DefineVirtualHostLimits("vhost", x =>
    {
        x.SetMaxQueueLimit(100);
        x.SetMaxConnectionLimit(1000);
    });
```

*Please note that subsequent calls to any of the above methods will result in overriding the argument.*

<br>

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

