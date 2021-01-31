# Creating Global Parameters

The Broker API allows you to create a simple global parameter on the RabbitMQ broker. To do so is pretty simple with HareDu 2. You can do it yourself or the IoC way.

**Do It Yourself**

```csharp
var result = await new BrokerObjectFactory(config)
                .Object<GlobalParameter>()
                .Create(x =>
                {
                    x.Parameter("your_param");
                    x.Value("your_value");
                });
```
<br>

**Autofac**

```csharp
var result = await _container.Resolve<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Create(x =>
                {
                    x.Parameter("your_param");
                    x.Value("your_value");
                });
```
<br>

**.NET Core DI**

```csharp
var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Create(x =>
                {
                    x.Parameter("your_param");
                    x.Value("your_value");
                });
```
<br>

If you need to create a complex parameter then you would use the ```Value``` overload like this...

```csharp
var result = await _container.Resolve<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Create(x =>
                {
                    x.Parameter("your_param");
                    x.Value(y =>
                    {
                        y.Set("your_arg", "your_value");
                    });
                });
```

<br>

*Please note that subsequent calls to any of the above methods will result in overriding the argument.*

<br>

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu2/blob/master/docs/configuration.md) .

