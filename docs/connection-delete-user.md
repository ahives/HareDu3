# Delete Connection

The Broker API allows you to delete a connection to the RabbitMQ broker. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<Connection>(x => x.UsingCredentials("guest", "guest"))
    .DeleteByUser("test_user");
```

The other way to create a user is to call the extension methods off of ```IBrokerObjectFactory``` like so...

```c#
var result = await _container.Resolve<IBrokerFactory>()
    .DeleteConnectionByUser(x => x.UsingCredentials("guest", "guest"), "test_user");
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

