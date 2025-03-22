# Delete User

The Broker API allows you to delete a user(s) from the RabbitMQ broker. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<User>()
    .Delete("username");
```
<br>

There is an overloaded version of the above method that will allow you to bulk delete users that looks like this...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<User>()
    .Delete(new List<string>{"username1", "username2", "username3"});
```

<br>

The other way to delete a user is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _container.Resolve<IBrokerFactory>()
    .DeleteUser("username");
```

...and for deleting in bulk, you can use the following extension method like so...

```c#
var result = await _container.Resolve<IBrokerFactory>()
    .DeleteUsers(new List<string>{"username1", "username2", "username3"});
```

<br>

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

