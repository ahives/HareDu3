# Getting Server Health Details

The Broker API allows you to health details on a RabbitMQ server. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Server>()
    .GetHealth(x =>
    {
        x.Node("your_name");
        x.VirtualHost("your_vhost");
    });
```
<br>

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu2/blob/master/docs/configuration.md) .

