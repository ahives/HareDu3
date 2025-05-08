# Apply Virtual Host User Permissions

The Broker API allows you to apply user permissions to a virtual host on the RabbitMQ broker. To do so is pretty simple with HareDu 4.

```c#
var result = await _services.GetService<IBrokerFactory>()
    .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
    .ApplyPermissions("username", "vhost", x =>
    {
        x.UsingConfigurePattern(".*");
        x.UsingReadPattern(".*");
        x.UsingWritePattern(".*");
    });
```

The other way to delete a user is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await _services.GetService<IBrokerFactory>()
    .ApplyUserPermissions(x => x.UsingCredentials("guest", "guest"), "username", "vhost", x =>
    {
        x.UsingConfigurePattern(".*");
        x.UsingReadPattern(".*");
        x.UsingWritePattern(".*");
    });
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

