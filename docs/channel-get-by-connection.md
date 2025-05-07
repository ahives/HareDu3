# Get Channels By Connection

The Broker API allows you to get all channels on the RabbitMQ broker filtered by connection. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Channel>(x => x.UsingCredentials("guest", "guest"))
    .GetByConnection("test-connection");
```

The other way to do this is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.Get[connection-delete.md](connection-delete.md)Service<IBrokerFactory>()
    .GetChannelsByConnection(x => x.UsingCredentials("guest", "guest"), "test-connection");
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

