# Pagination

The Broker API allows you to filter the below endpoints to constrain the returned results from the broker using pagination parameters.
- Connection
- Channel
- Queue
- Exchange

The Broker API allows you to get all established connections to the RabbitMQ broker. To do so is pretty simple with HareDu 4. You can do it yourself or the DI way.

Below is the ```PaginationConfigurator``` interface that is responsible for pagination.

```c#
x =>
    {
        x.Name("test");
        x.Page(1);
        x.PageSize(100);
        x.UseRegex(false);
    }
```

Example outuput URL: http://localhost:15672/api/queues/my-vhost?page=1&page_size=100&name=test&use_regex=false&pagination=true

The other way to get connection information is to call the extension methods off of ```IBrokerFactory``` like so...

```c#
var result = await services.GetService<IBrokerFactory>()
    .API<Queue>(x => x.UsingCredentials("guest", "guest"))
    .GetAll(x =>
    {
        x.Name("test");
        x.Page(1);
        x.PageSize(100);
        x.UseRegex(false);
    });
```

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu3/blob/master/docs/configuration.md).

