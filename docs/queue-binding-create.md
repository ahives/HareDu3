# Bind Queue

The Broker API allows you to binding an exchange to a queue if the node mirror sync is critical on the RabbitMQ broker. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Queue>(x => x.UsingCredentials("guest", "guest"))
    .BindToQueue("vhost", "exchange", x =>
    {
        x.Destination("queue");
        x.BindingKey("binding_key");
        x.OptionalArguments(o =>
        {
            o.Add("x-arg", "test");
        })
    }, cancellationToken);
```

The other way to do thia is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .BindToQueue("vhost", "exchange", x =>
    {
        x.Destination("queue");
        x.BindingKey("binding_key");
        x.OptionalArguments(o =>
        {
            o.Add("x-arg", "test");
        })
    }, cancellationToken);
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

