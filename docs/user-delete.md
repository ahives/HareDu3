# Delete User

The Broker API allows you to delete a user(s) from the RabbitMQ broker. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<User>(x => x.UsingCredentials("guest", "guest"))
    .Delete("username");
```

The other way to delete a user is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _container.Resolve<IBrokerFactory>()
    .DeleteUser(x => x.UsingCredentials("guest", "guest"), "username");
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

