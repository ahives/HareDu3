# Get Server Details

The Broker API allows you to get details on a RabbitMQ server. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<ServerInfo>(x => x.UsingCredentials("guest", "guest"))
    .Get();
```

The other way to do this is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .GetServerInformation(x => x.UsingCredentials("guest", "guest"));
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu2/blob/master/docs/configuration.md) .

