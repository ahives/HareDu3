# Create Exchange

The Broker API allows you to create an exchange on the RabbitMQ broker. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
    .Create("exchange", "vhost");
```
<br>

RabbitMQ supports the concept of [durability](https://www.rabbitmq.com/tutorials/amqp-concepts.html#exchanges), which means that if the broker restarts the exchange will survive. To configure an exchange to be durable during creation, add the ```IsDurable``` method like so...

```c#
.Create("fake_exchange", "HareDu", x =>
{
    x.IsDurable();
    x.IsForInternalUse();
    x.HasRoutingType(ExchangeRoutingType.Fanout);
    x.HasArguments(arg =>
    {
        arg.Add("arg", "value");
    });
})
```
<br>

There are situations where you wish to create exchanges that are bound to other exchanges and are not exposed to applications (i.e. publishers). In such a scenario, you can mark the exchange as internal like so...
```c#
.Create("fake_exchange", "HareDu", x =>
{
    x.IsForInternalUse();
})
```
<br>

HareDu 4 supports adding adhoc arguments to exchanges during creation. The addition of the below code to ```Create``` will set the above RabbitMQ arguments.

```c#
c.HasArguments(arg =>
{
    arg.Set("your_arg", "your_value");
});
```
<br>

A complete example would look something like this...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
    .Create("exchange", "vhost", x =>
    {
        x.IsDurable();
        x.IsForInternalUse();
        x.HasRoutingType(ExchangeRoutingType.Fanout);
        x.HasArguments(arg =>
        {
            arg.Add("arg", "value");
        });
    });
```

<br>

The other way to create an exchange is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .CreateExchange(x => x.UsingCredentials("guest", "guest"), "exchange", "vhost", x =>
    {
        x.IsDurable();
        x.IsForInternalUse();
        x.HasRoutingType(ExchangeRoutingType.Fanout);
        x.HasArguments(arg =>
        {
            arg.Add("arg", "value");
        });
    });
```

*Please note that subsequent calls to any of the above methods will result in overriding the argument.*

<br>

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

