# Create Binding

The Broker API allows you to create a binding to an exchange or queue on the RabbitMQ broker. To do so is pretty simple with HareDu 4.

The folllowing code has been deprecated in version 4 of the API.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Binding>(x => x.UsingCredentials("guest", "guest"))
    .Create("source_exchange", "destination_exchange", BindingType.Exchange, "vhost", "binding_key");
```
<br>

The above examples show how you would bind a source exchange to a destination exchange. However, RabbitMQ allows you to bind an exchange to a queue. In this scenario, you would set the exchange as the source and the destination as the queue like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Binding>(x => x.UsingCredentials("guest", "guest"))
    .Create("source_exchange", "destination_queue", BindingType.Queue, "vhost", "binding_key");
```

<br>

If you want to add adhoc arguments to the binding then you just need to call the ```Add``` method like so...

```c#
x =>
{
    x.Add("arg", "value");
};
```
<br>

A complete example would look something like this...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Binding>(x => x.UsingCredentials("guest", "guest"))
    .Create("source_exchange", "destination_queue", BindingType.Queue, "vhost", "*.", x =>
    {
        x.Add("arg", "value");
    });
```

<br>

The other way to create a binding is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .CreateExchangeBinding(x => x.UsingCredentials("guest", "guest"), "source_exchange", "destination_exchange", "vhost");
```
or...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .CreateExchangeBindingToQueue(x => x.UsingCredentials("guest", "guest"), "source_exchange", "destination_queue", "vhost");
```

*Please note that subsequent calls to any of the above methods will result in overriding the argument.*

<br>

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

