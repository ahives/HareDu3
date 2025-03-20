# Start Up Virtual Host

The Broker API allows you to start up a virtual host on the RabbitMQ node. To do so is pretty simple with HareDu 4. You can do it yourself or the IoC way.

**Microsoft DI**

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<VirtualHost>()
    .Startup("vhost", "node");
```
<br>

The other way to startup a virtual host is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .StartupVirtualHost("vhost", "node");
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

