# Create Global Parameter

The Broker API allows you to create a simple global parameter on the RabbitMQ broker. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<GlobalParameter>(x => x.UsingCredentials("guest", "guest"))
    .Create("param", x =>
    {
        x.Value("value");
    });
```
...or

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<GlobalParameter>(x => x.UsingCredentials("guest", "guest"))
    .Create("param", x =>
    {
        x.Value(arg =>
        {
            arg.Set("arg1", "value");
            arg.Set("arg2", 5);
        });
    });
```
The other way to do this is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .CreateGlobalParameter(x => x.UsingCredentials("guest", "guest"), "param", x =>
    {
        x.Value("value");
    });
```
...or

```c#
var result = await _services.GetService<IBrokerFactory>()
    .CreateGlobalParameter(x => x.UsingCredentials("guest", "guest"), "param", x =>
    {
        x.Value(arg =>
        {
            arg.Set("arg1", "value");
            arg.Set("arg2", 5);
        });
    });
```

*Please note that subsequent calls to any of the above methods within the Create method will result in overriding the argument.*

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

