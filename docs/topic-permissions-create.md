# Create Topic Permissions

The Broker API allows you to create topic permissions on the RabbitMQ broker. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<TopicPermissions>()
    .Create("username", "exchange", "vhost", x =>
    {
        x.UsingReadPattern(".*");
        x.UsingWritePattern(".*");
    });
```
<br>

The other way to create topic permissions is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .CreateTopicPermission("username", "exchange", "vhost", x =>
    {
        x.UsingReadPattern(".*");
        x.UsingWritePattern(".*");
    });
```

<br>

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

