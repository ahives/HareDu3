# Create Global Parameters

The Broker API allows you to create a simple global parameter on the RabbitMQ broker. To do so is pretty simple with HareDu 4. You can do it yourself or the DI way.

**Microsoft DI**

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<GlobalParameter>()
    .Create("param", x =>
    {
        x.Value("value");
    });
```
...or

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<GlobalParameter>()
    .Create("param", x =>
    {
        x.Value(arg =>
        {
            arg.Set("arg1", "value");
            arg.Set("arg2", 5);
        });
    });
```

<br>

The other way to create a global parameter is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .CreateGlobalParameter("param", x =>
    {
        x.Value("value");
    });
```
...or

```c#
var result = await _services.GetService<IBrokerFactory>()
    .CreateGlobalParameter("param", x =>
    {
        x.Value(arg =>
        {
            arg.Set("arg1", "value");
            arg.Set("arg2", 5);
        });
    });
```

<br>

*Please note that subsequent calls to any of the above methods within the Create method will result in overriding the argument.*

<br>

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

