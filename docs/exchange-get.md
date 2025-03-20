# Get Exchanges

The Broker API allows you to get all exchanges on the RabbitMQ broker. To do so is pretty simple with HareDu 4. You can do it yourself or the DI way.

**Microsoft DI**

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Exchange>()
    .GetAll();
```
<br>

The other way to get exchange information is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .GetAllExchanges();
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

