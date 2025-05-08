# Broker is Alive Check

The Broker API allows you to check if the broker is alive on the RabbitMQ cluster. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Broker>(x => x.UsingCredentials("guest", "guest"))
    .IsBrokerAlive()
```

The other way to do thia is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .IsBrokerAlive(x => x.UsingCredentials("guest", "guest"));
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

