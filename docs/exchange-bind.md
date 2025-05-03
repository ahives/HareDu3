# Bind Exchanges

The Broker API allows you to bind to an exchange on the RabbitMQ broker. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
    .BindToExchange();
```
<br>

The other way to get exchange information is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .BindToExchange(x => x.UsingCredentials("guest", "guest"));
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

