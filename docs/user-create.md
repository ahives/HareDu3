# Create User

The Broker API allows you to create a user on the RabbitMQ broker. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<User>()
    .Create("user", "password", "password_hash");
```
<br>

By default, the tags on a user is set to "None" but the API allows for setting this property by way of the ```NewUserConfigurator``` configurator using the ```WithTags``` method like so...

```c#
x.WithTags(t =>
{
    t.Administrator();
});
```
<br>

A complete example would look something like this...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<User>()
    .Create("user", "password", "password_hash", x =>
    {
        x.WithTags(t =>
        {
            t.Administrator();
        });
    });
```
<br>

The other way to create a user is to call the extension methods off of ```IBrokerObjectFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .CreateUser("user", "password", "password_hash");
```

...or

```c#
var result = await _services.GetService<IBrokerFactory>()
    .CreateUser("user", "password", "password_hash", x =>
    {
        x.WithTags(t =>
        {
            t.Administrator();
        });
    });
```

<br>

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

