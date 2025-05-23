# Delete Exchange

The Broker API allows you to delete an exchange from the RabbitMQ broker. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
    .Delete("exchange", "vhost");
```

Since deleting an exchange will cause messages to not be routed to queues, HareDu provides a means to conditional perform said action. You can delete an exchange when its not in use. You need only call the ```WhenUnused``` method like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
    .Delete("exchange", "vhost", x => x.WhenUnused());
```

The other way to do this is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .DeleteExchange(x => x.UsingCredentials("guest", "guest"), "exchange", "vhost");
```

...or

```c#
var result = await _services.GetService<IBrokerFactory>()
    .DeleteExchange(x => x.UsingCredentials("guest", "guest"), "exchange", "vhost", x => x.WhenUnused());
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

