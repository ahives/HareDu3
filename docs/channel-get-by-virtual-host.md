# Get Channels By Virtual Host

The Broker API allows you to get all channels on the RabbitMQ broker filtered by virtual host. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Channel>(x => x.UsingCredentials("guest", "guest"))
    .GetByVirtualHost("test-vhost");
```
<br>

The other way to get channel information is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .GetChannelsByVirtualHost(x => x.UsingCredentials("guest", "guest"), "test-vhost");
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

