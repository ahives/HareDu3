# Delete Users

The Broker API allows you to delete several users from the RabbitMQ broker. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<User>(x => x.UsingCredentials("guest", "guest"))
    .BulkDelete(["username1", "username2", "username3"]);
```

The other way to create a user is to call the extension methods off of ```IBrokerObjectFactory``` like so...

```c#
var result = await _container.Resolve<IBrokerFactory>()
    .DeleteUsers(x => x.UsingCredentials("guest", "guest"), "username");
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

